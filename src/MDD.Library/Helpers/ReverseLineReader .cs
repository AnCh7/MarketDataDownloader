#region Usings

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

using MDD.Library.Configuration;

#endregion

namespace MDD.Library.Helpers
{
	public sealed class ReverseLineReader : IEnumerable<string>
	{
		private const int DefaultBufferSize = 4096;
		private readonly int _bufferSize;
		private readonly Func<long, byte, bool> _characterStartDetector;
		private readonly Encoding _encoding;
		private readonly Func<Stream> _streamSource;

		public ReverseLineReader(string filename, Encoding encoding)
			: this(() => File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), encoding)
		{}

		private ReverseLineReader(Func<Stream> streamSource, Encoding encoding, int bufferSize = DefaultBufferSize)
		{
			_streamSource = streamSource;
			_encoding = encoding;
			_bufferSize = bufferSize;

			if (encoding.IsSingleByte)
			{
				_characterStartDetector = (pos, data) => true;
			}
			else if (encoding is UnicodeEncoding)
			{
				_characterStartDetector = (pos, data) => (pos & 1) == 0;
			}
			else if (encoding is UTF8Encoding)
			{
				_characterStartDetector = (pos, data) => (data & 128) == 0 || (data & 64) != 0;
			}
			else
			{
				throw new ArgumentException("Only single byte, UTF-8 and Unicode encodings are permitted");
			}
		}

		public IEnumerator<string> GetEnumerator()
		{
			var stream = _streamSource();

			if (!stream.CanSeek)
			{
				stream.Dispose();
				throw new NotSupportedException("Unable to seek within stream");
			}
			if (!stream.CanRead)
			{
				stream.Dispose();
				throw new NotSupportedException("Unable to read within stream");
			}

			return GetEnumeratorImpl(stream);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private IEnumerator<string> GetEnumeratorImpl(Stream stream)
		{
			try
			{
				var position = stream.Length;

				if (_encoding is UnicodeEncoding && (position & 1) != 0)
				{
					throw new InvalidDataException("UTF-16 encoding provided, but stream has odd length.");
				}

				var buffer = new byte[_bufferSize + 2];
				var charBuffer = new char[_encoding.GetMaxCharCount(buffer.Length)];
				var leftOverData = 0;
				String previousEnd = null;

				var firstYield = true;
				var swallowCarriageReturn = false;

				while (position > 0)
				{
					var bytesToRead = Math.Min(position > int.MaxValue ? _bufferSize : (int)position, _bufferSize);

					position -= bytesToRead;
					stream.Position = position;
					ReadExactly(stream, buffer, bytesToRead);

					if (leftOverData > 0 && bytesToRead != _bufferSize)
					{
						Array.Copy(buffer, _bufferSize, buffer, bytesToRead, leftOverData);
					}

					bytesToRead += leftOverData;

					var firstCharPosition = 0;
					while (!_characterStartDetector(position + firstCharPosition, buffer[firstCharPosition]))
					{
						firstCharPosition++;

						if (firstCharPosition == 3 || firstCharPosition == bytesToRead)
						{
							throw new InvalidDataException("Invalid UTF-8 data");
						}
					}

					leftOverData = firstCharPosition;

					var charsRead = _encoding.GetChars(buffer, firstCharPosition, bytesToRead - firstCharPosition, charBuffer, 0);
					var endExclusive = charsRead;

					for (var i = charsRead - 1; i >= 0; i--)
					{
						var lookingAt = charBuffer[i];

						if (swallowCarriageReturn)
						{
							swallowCarriageReturn = false;

							if (lookingAt == Cfg.CR)
							{
								endExclusive--;
								continue;
							}
						}

						if (lookingAt != Cfg.LF && lookingAt != Cfg.CR)
						{
							continue;
						}

						if (lookingAt == Cfg.LF)
						{
							swallowCarriageReturn = true;
						}

						var start = i + 1;
						var bufferContents = new string(charBuffer, start, endExclusive - start);
						endExclusive = i;
						var stringToYield = previousEnd == null ? bufferContents : bufferContents + previousEnd;

						if (!firstYield || stringToYield.Length != 0)
						{
							yield return stringToYield;
						}

						firstYield = false;
						previousEnd = null;
					}

					previousEnd = endExclusive == 0 ? null : (new string(charBuffer, 0, endExclusive) + previousEnd);

					if (leftOverData != 0)
					{
						Buffer.BlockCopy(buffer, 0, buffer, _bufferSize, leftOverData);
					}
				}

				if (leftOverData != 0)
				{
					throw new InvalidDataException("Invalid UTF-8 data at start of stream");
				}
				if (firstYield && string.IsNullOrEmpty(previousEnd))
				{
					yield break;
				}

				yield return previousEnd ?? "";
			}
			finally
			{
				stream.Dispose();
			}
		}

		private void ReadExactly(Stream input, byte[] buffer, int bytesToRead)
		{
			var index = 0;
			while (index < bytesToRead)
			{
				var read = input.Read(buffer, index, bytesToRead - index);

				if (read == 0)
				{
					throw new EndOfStreamException(String.Format("End of stream reached with {0} byte{1} left to read.",
																 bytesToRead - index, bytesToRead - index == 1 ? "s" : ""));
				}

				index += read;
			}
		}
	}
}

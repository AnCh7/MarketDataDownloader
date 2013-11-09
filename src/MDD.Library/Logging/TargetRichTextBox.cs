#region Usings

using System.Drawing;

using NLog;
using NLog.Config;
using NLog.Targets;

#endregion

namespace MDD.Library.Logging
{
	public sealed class TargetRichTextBox
	{
		public LoggingConfiguration Initialize()
		{
			var config = new LoggingConfiguration();

			var target = new RichTextBoxTarget();
			target.AutoScroll = true;
			target.Name = "RichTextBoxTarget";
			target.Layout = "${date:format=HH\\:MM\\:ss} ${message}";
			target.ControlName = "rtbLog";
			target.FormName = "MainForm";
			target.UseDefaultRowColoringRules = false;

			target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Warn", "Green", "Control"));
			target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Info", "Black", "Control"));
			target.RowColoringRules.Add(new RichTextBoxRowColoringRule("level == LogLevel.Error", "DarkRed", "Control",
																	   FontStyle.Bold));

			config.AddTarget("RichTextBoxTarget", target);

			var rule = new LoggingRule("*", LogLevel.Debug, target);
			config.LoggingRules.Add(rule);

			return config;
		}
	}
}

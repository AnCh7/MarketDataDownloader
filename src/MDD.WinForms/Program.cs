#region Usings

using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

#endregion

namespace MDD.WinForms
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			var culture = CultureInfo.CreateSpecificCulture("en-US");
			Thread.CurrentThread.CurrentCulture = culture;
			Thread.CurrentThread.CurrentUICulture = culture;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}

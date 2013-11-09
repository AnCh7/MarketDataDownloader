#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using MDD.DataFeed.Fidelity.Models;
using MDD.DataFeed.IQFeed.Models;
using MDD.Library.Abstraction;
using MDD.Library.Configuration;
using MDD.Library.Helpers;
using MDD.Library.Logging;
using MDD.Library.Models;
using MDD.WinForms.Abstraction;
using MDD.WinForms.Presenters;
using MDD.WinForms.Resolver;

using Microsoft.Practices.Unity;

#endregion

namespace MDD.WinForms
{
	public partial class MainForm : Form, IMainFormView
	{
		private MainFormPresenter _presenter;

		public MainForm()
		{
			InitializeComponent();
		}

		public IMyLogger Logger { get; private set; }

		public void UpdateWithConnection(bool isConnected, string connectionMessage)
		{
			if (!isConnected)
			{
				statusStrip1.Items[0].Visible = false;
				statusStrip1.Items[1].Visible = true;
				Logger.Error(connectionMessage);
			}
			else
			{
				statusStrip1.Items[0].Visible = true;
				statusStrip1.Items[1].Visible = false;
				Logger.Warn(connectionMessage);
			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			Logger = DependencyFactory.Container.Resolve<IMyLogger>();
			_presenter = new MainFormPresenter(this);
			SetDefaultValues();
		}

		private void CbTimeframeSelectedIndexChanged(object sender, EventArgs e)
		{
			switch (cbTimeframe.SelectedItem.ToString())
			{
				case "Tick":
					cbTimeframeIntraday.Enabled = false;
					rbDays.Enabled = true;
					rbInterval.Enabled = true;
					rbMainSession.Enabled = true;
					rbCustomInt.Enabled = true;
					rbAlldata.Enabled = true;
					chbRealtime.Enabled = true;
					if (rbCustomInt.Checked)
					{
						dtpCustomIntBegin.Enabled = true;
						dtpCustomIntEnd.Enabled = true;
					}
					break;

				case "Intraday":
					cbTimeframeIntraday.Enabled = true;
					rbDays.Enabled = true;
					rbInterval.Enabled = true;
					rbMainSession.Enabled = true;
					rbCustomInt.Enabled = true;
					rbAlldata.Enabled = true;
					chbRealtime.Enabled = true;
					if (rbCustomInt.Checked)
					{
						dtpCustomIntBegin.Enabled = true;
						dtpCustomIntEnd.Enabled = true;
					}
					break;

				default:
					cbTimeframeIntraday.Enabled = false;
					rbMainSession.Enabled = false;
					rbCustomInt.Enabled = false;
					rbAlldata.Enabled = false;
					dtpCustomIntBegin.Enabled = false;
					dtpCustomIntEnd.Enabled = false;
					chbRealtime.Enabled = false;
					chbRealtime.Checked = false;
					dtpStartDateTime.Enabled = false;
					dtpEndDateTime.Enabled = false;
					break;
			}

			
			
		}

		private void cbSource_SelectedIndexChanged(object sender, EventArgs e)
		{
			ClearLog();

			var comboBox = (ComboBox)sender;
			switch (comboBox.SelectedItem.ToString())
			{
				case "Fidelity":
					_presenter.InitDataFeedManager("Fidelity");

					rbCustomInt.Checked = false;
					rbCustomInt.Enabled = false;
					dtpCustomIntBegin.Enabled = false;
					dtpCustomIntEnd.Enabled = false;
					rbAlldata.Checked = true;

					cbTimeframeIntraday.Items.Clear();
					cbTimeframeIntraday.Items.AddRange(new object[] {"1", "3", "5", "10", "15", "30"});
					cbTimeframeIntraday.SelectedIndex = 0;

					cbTimeframe.Items.Clear();
					cbTimeframe.Items.AddRange(new object[] {"Intraday", "Daily", "Weekly", "Monthly"});
					cbTimeframe.SelectedItem = "Intraday";
					break;

				case "IQFeed":
					_presenter.InitDataFeedManager("IQFeed");

					rbCustomInt.Enabled = true;
					dtpCustomIntBegin.Enabled = true;
					dtpCustomIntEnd.Enabled = true;

					cbTimeframeIntraday.Items.Clear();
					cbTimeframeIntraday.Items.AddRange(new object[] {"1", "3", "5", "10", "15", "30", "60", "120", "180", "240"});
					cbTimeframeIntraday.SelectedIndex = 0;

					cbTimeframe.Items.Clear();
					cbTimeframe.Items.AddRange(new object[] {"Tick", "Intraday", "Daily", "Weekly", "Monthly"});
					cbTimeframe.SelectedItem = "Intraday";
					break;
			}

			_presenter.Start();
		}

		private void rbCustomInt_CheckedChanged(object sender, EventArgs e)
		{
			dtpCustomIntBegin.Enabled = true;
			dtpCustomIntEnd.Enabled = true;
		}

		private void rbMainSession_CheckedChanged(object sender, EventArgs e)
		{
			dtpCustomIntBegin.Enabled = false;
			dtpCustomIntEnd.Enabled = false;
		}

		private void rbAlldata_CheckedChanged(object sender, EventArgs e)
		{
			dtpCustomIntBegin.Enabled = false;
			dtpCustomIntEnd.Enabled = false;
		}

		private void BtnChooseStoreFolderClick(object sender, EventArgs e)
		{
			var dialog = new FolderBrowserDialog();
			dialog.ShowDialog();
			tbFolder.Text = dialog.SelectedPath;
		}

		private void RbDaysCheckedChanged(object sender, EventArgs e)
		{
			dtpStartDateTime.Enabled = false;
			dtpEndDateTime.Enabled = false;
			tbAmountOfDays.Enabled = true;
		}

		private void RbIntervalCheckedChanged(object sender, EventArgs e)
		{
			dtpStartDateTime.Enabled = true;
			dtpEndDateTime.Enabled = true;
			tbAmountOfDays.Enabled = false;
		}

		private void btnReconnect_Click(object sender, EventArgs e)
		{
			_presenter.Start();
		}

		private void BtnStopClick(object sender, EventArgs e)
		{
			_presenter.Stop();
			UnlockControls();
		}

		private async void BtnStartClick(object sender, EventArgs e)
		{
			if (!_presenter.CheckSavePath(tbFolder.Text))
			{
				return;
			}

			if (!_presenter.IsConnected)
			{
				Logger.Error("Not connected to the Datafeed. Use Reconnect button.");
				return;
			}

			ClearLog();
			LockControls();

			try
			{
				var programParameters = new Parameters(tbFolder.Text, cbDTFormatDate.Text, cbDTFormatDelimiter.Text,
													   cbDTFormatTime.Text, cbDataDelimiter.Text, cbSource.SelectedItem.ToString());

				RequestBase request;
				switch (programParameters.DataFeedName)
				{
					case "Fidelity":
						request = new RequestFidelity(cbTimeframe.SelectedItem.ToString(), rbDays.Checked, rbInterval.Checked,
													  tbAmountOfDays.Text, dtpStartDateTime.Value, dtpEndDateTime.Value, rbMainSession.Checked,
													  rbAlldata.Checked, cbTimeframeIntraday.SelectedItem.ToString(), GetTickersList());
						break;

					case "IQFeed":
						request = new RequestIQFeed(cbTimeframe.SelectedItem.ToString(), rbDays.Checked, rbInterval.Checked,
													tbAmountOfDays.Text, dtpStartDateTime.Value, dtpEndDateTime.Value, rbMainSession.Checked, rbAlldata.Checked,
													cbTimeframeIntraday.SelectedItem.ToString(), rbCustomInt.Checked, dtpCustomIntBegin.Value,
													dtpCustomIntEnd.Value, GetTickersList());
						break;

					default:
						Logger.Error("Error creating request");
						return;
				}

				await _presenter.DownloadMarketData(request, programParameters, chbRealtime.Checked);
			}
			catch (Exception ex)
			{
				Logger.Error(string.Format("[BtnStartClick] {0}", ex.Message));
			}

			if (!chbRealtime.Checked)
			{
				UnlockControls();
			}
		}

		private List<string> GetTickersList()
		{
			return
				rtbSymbols.Text.Split(Cfg.SymbolsSeparator, StringSplitOptions.RemoveEmptyEntries).Select(s => s.ToUpper()).ToList();
		}

		private void ClearLog()
		{
			rtbLog.Clear();
		}

		private void LockControls()
		{
			cbSource.Enabled = false;
			btnStart.Enabled = false;
			btnReconnect.Enabled = false;
			chbRealtime.Enabled = false;
		}

		private void UnlockControls()
		{
			cbSource.Enabled = true;
			btnStart.Enabled = true;
			btnReconnect.Enabled = true;
			chbRealtime.Enabled = true;
		}

		private void SetDefaultValues()
		{
			rtbSymbols.Text = Cfg.Symbols();
			tbFolder.Text = Cfg.FolderForSaving();

			rbInterval.Enabled = true;
			rbDays.Enabled = true;
			tbAmountOfDays.Text = Cfg.AmountOfDays();
			rbDays.Checked = true;

			cbTimeframeIntraday.Enabled = false;
			cbTimeframeIntraday.Items.Clear();
			cbTimeframeIntraday.Items.AddRange(new object[] {"1", "5", "10", "15", "30", "60", "120", "180", "240"});
			cbTimeframeIntraday.SelectedIndex = 0;

			cbTimeframe.Enabled = true;
			cbTimeframe.Items.Clear();
			cbTimeframe.Items.AddRange(new object[] {"Tick", "Intraday", "Daily", "Weekly", "Monthly"});
			cbTimeframe.SelectedItem = "Intraday";

			dtpStartDateTime.Enabled = false;
			dtpEndDateTime.Enabled = false;

			rbMainSession.Checked = false;
			rbCustomInt.Checked = false;
			rbAlldata.Checked = true;

			dtpCustomIntBegin.Enabled = false;
			dtpCustomIntEnd.Enabled = false;

			dtpStartDateTime.Value = DateTimeHelper.ConvertLocalToEST(DateTime.Now.AddDays(-365 * 5));
			dtpEndDateTime.Value = DateTimeHelper.ConvertLocalToEST(DateTime.Now);

			cbDTFormatDate.SelectedIndex = 0;
			cbDTFormatTime.SelectedIndex = 0;
			cbDTFormatDelimiter.SelectedIndex = 0;

			cbDataDelimiter.SelectedIndex = 2;

			cbSource.SelectedIndex = 0;

			chbRealtime.Enabled = true;
			chbRealtime.Checked = false;

			chbRealtime.Enabled = true;
		}
	}
}

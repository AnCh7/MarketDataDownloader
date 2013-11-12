namespace MDD.WinForms
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnStop = new System.Windows.Forms.Button();
			this.dtpEndDateTime = new System.Windows.Forms.DateTimePicker();
			this.dtpStartDateTime = new System.Windows.Forms.DateTimePicker();
			this.cbSource = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.chbRealtime = new System.Windows.Forms.CheckBox();
			this.rbInterval = new System.Windows.Forms.RadioButton();
			this.cbTimeframe = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.rbDays = new System.Windows.Forms.RadioButton();
			this.label11 = new System.Windows.Forms.Label();
			this.tbFolder = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cbTimeframeIntraday = new System.Windows.Forms.ComboBox();
			this.label12 = new System.Windows.Forms.Label();
			this.tbAmountOfDays = new System.Windows.Forms.TextBox();
			this.btnChooseStoreFolder = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.btnStart = new System.Windows.Forms.Button();
			this.tbFeeds = new System.Windows.Forms.TabControl();
			this.tabIQFeed = new System.Windows.Forms.TabPage();
			this.btnReconnect = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.rtbSymbols = new System.Windows.Forms.RichTextBox();
			this.tabSetup = new System.Windows.Forms.TabPage();
			this.cbDataDelimiter = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.rbAlldata = new System.Windows.Forms.RadioButton();
			this.rbCustomInt = new System.Windows.Forms.RadioButton();
			this.rbMainSession = new System.Windows.Forms.RadioButton();
			this.dtpCustomIntEnd = new System.Windows.Forms.DateTimePicker();
			this.dtpCustomIntBegin = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.cbDTFormatDelimiter = new System.Windows.Forms.ComboBox();
			this.cbDTFormatTime = new System.Windows.Forms.ComboBox();
			this.cbDTFormatDate = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.rtbLog = new System.Windows.Forms.RichTextBox();
			this.groupBox2.SuspendLayout();
			this.tbFeeds.SuspendLayout();
			this.tabIQFeed.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tabSetup.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(260, 246);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(95, 22);
			this.btnStop.TabIndex = 19;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.BtnStopClick);
			// 
			// dtpEndDateTime
			// 
			this.dtpEndDateTime.CustomFormat = "yyyy-MM-dd H:mm:ss";
			this.dtpEndDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpEndDateTime.Location = new System.Drawing.Point(97, 143);
			this.dtpEndDateTime.Name = "dtpEndDateTime";
			this.dtpEndDateTime.ShowUpDown = true;
			this.dtpEndDateTime.Size = new System.Drawing.Size(197, 20);
			this.dtpEndDateTime.TabIndex = 42;
			// 
			// dtpStartDateTime
			// 
			this.dtpStartDateTime.CustomFormat = "yyyy-MM-dd H:mm:ss";
			this.dtpStartDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpStartDateTime.Location = new System.Drawing.Point(97, 117);
			this.dtpStartDateTime.Name = "dtpStartDateTime";
			this.dtpStartDateTime.ShowUpDown = true;
			this.dtpStartDateTime.Size = new System.Drawing.Size(197, 20);
			this.dtpStartDateTime.TabIndex = 41;
			// 
			// cbSource
			// 
			this.cbSource.FormattingEnabled = true;
			this.cbSource.Items.AddRange(new object[] {
            "IQFeed",
            "Fidelity"});
			this.cbSource.Location = new System.Drawing.Point(97, 14);
			this.cbSource.Name = "cbSource";
			this.cbSource.Size = new System.Drawing.Size(197, 21);
			this.cbSource.TabIndex = 40;
			this.cbSource.SelectedIndexChanged += new System.EventHandler(this.cbSource_SelectedIndexChanged);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(9, 17);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(75, 13);
			this.label9.TabIndex = 21;
			this.label9.Text = "Select source:";
			// 
			// chbRealtime
			// 
			this.chbRealtime.AutoSize = true;
			this.chbRealtime.Enabled = false;
			this.chbRealtime.Location = new System.Drawing.Point(6, 206);
			this.chbRealtime.Name = "chbRealtime";
			this.chbRealtime.Size = new System.Drawing.Size(114, 17);
			this.chbRealtime.TabIndex = 39;
			this.chbRealtime.Text = "Real-time updating";
			this.chbRealtime.UseVisualStyleBackColor = true;
			// 
			// rbInterval
			// 
			this.rbInterval.AutoSize = true;
			this.rbInterval.Location = new System.Drawing.Point(97, 95);
			this.rbInterval.Name = "rbInterval";
			this.rbInterval.Size = new System.Drawing.Size(60, 17);
			this.rbInterval.TabIndex = 34;
			this.rbInterval.TabStop = true;
			this.rbInterval.Text = "Interval";
			this.rbInterval.UseVisualStyleBackColor = true;
			this.rbInterval.CheckedChanged += new System.EventHandler(this.RbIntervalCheckedChanged);
			// 
			// cbTimeframe
			// 
			this.cbTimeframe.FormattingEnabled = true;
			this.cbTimeframe.Location = new System.Drawing.Point(97, 169);
			this.cbTimeframe.Name = "cbTimeframe";
			this.cbTimeframe.Size = new System.Drawing.Size(116, 21);
			this.cbTimeframe.TabIndex = 29;
			this.cbTimeframe.SelectedIndexChanged += new System.EventHandler(this.CbTimeframeSelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(9, 172);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 13);
			this.label6.TabIndex = 21;
			this.label6.Text = "Timeframe";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.dtpEndDateTime);
			this.groupBox2.Controls.Add(this.dtpStartDateTime);
			this.groupBox2.Controls.Add(this.cbSource);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.chbRealtime);
			this.groupBox2.Controls.Add(this.rbInterval);
			this.groupBox2.Controls.Add(this.cbTimeframe);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.rbDays);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.tbFolder);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.cbTimeframeIntraday);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.tbAmountOfDays);
			this.groupBox2.Controls.Add(this.btnChooseStoreFolder);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new System.Drawing.Point(159, 6);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(300, 234);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Settings";
			// 
			// rbDays
			// 
			this.rbDays.AutoSize = true;
			this.rbDays.Location = new System.Drawing.Point(97, 72);
			this.rbDays.Name = "rbDays";
			this.rbDays.Size = new System.Drawing.Size(100, 17);
			this.rbDays.TabIndex = 33;
			this.rbDays.TabStop = true;
			this.rbDays.Text = "Amount of Days";
			this.rbDays.UseVisualStyleBackColor = true;
			this.rbDays.CheckedChanged += new System.EventHandler(this.RbDaysCheckedChanged);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(9, 123);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(60, 13);
			this.label11.TabIndex = 28;
			this.label11.Text = "Begin Date";
			// 
			// tbFolder
			// 
			this.tbFolder.Location = new System.Drawing.Point(171, 45);
			this.tbFolder.Name = "tbFolder";
			this.tbFolder.Size = new System.Drawing.Size(123, 20);
			this.tbFolder.TabIndex = 20;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 72);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(71, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "History Depth";
			// 
			// cbTimeframeIntraday
			// 
			this.cbTimeframeIntraday.FormattingEnabled = true;
			this.cbTimeframeIntraday.Location = new System.Drawing.Point(219, 169);
			this.cbTimeframeIntraday.Name = "cbTimeframeIntraday";
			this.cbTimeframeIntraday.Size = new System.Drawing.Size(75, 21);
			this.cbTimeframeIntraday.TabIndex = 15;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(9, 149);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(52, 13);
			this.label12.TabIndex = 29;
			this.label12.Text = "End Date";
			// 
			// tbAmountOfDays
			// 
			this.tbAmountOfDays.Location = new System.Drawing.Point(203, 71);
			this.tbAmountOfDays.Name = "tbAmountOfDays";
			this.tbAmountOfDays.Size = new System.Drawing.Size(91, 20);
			this.tbAmountOfDays.TabIndex = 31;
			// 
			// btnChooseStoreFolder
			// 
			this.btnChooseStoreFolder.Location = new System.Drawing.Point(97, 43);
			this.btnChooseStoreFolder.Name = "btnChooseStoreFolder";
			this.btnChooseStoreFolder.Size = new System.Drawing.Size(68, 23);
			this.btnChooseStoreFolder.TabIndex = 2;
			this.btnChooseStoreFolder.Text = "Choose...";
			this.btnChooseStoreFolder.UseVisualStyleBackColor = true;
			this.btnChooseStoreFolder.Click += new System.EventHandler(this.BtnChooseStoreFolderClick);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(73, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Save to folder";
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(159, 246);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(95, 22);
			this.btnStart.TabIndex = 14;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.BtnStartClick);
			// 
			// tbFeeds
			// 
			this.tbFeeds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbFeeds.Controls.Add(this.tabIQFeed);
			this.tbFeeds.Controls.Add(this.tabSetup);
			this.tbFeeds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbFeeds.Location = new System.Drawing.Point(12, 6);
			this.tbFeeds.Name = "tbFeeds";
			this.tbFeeds.SelectedIndex = 0;
			this.tbFeeds.Size = new System.Drawing.Size(479, 305);
			this.tbFeeds.TabIndex = 26;
			// 
			// tabIQFeed
			// 
			this.tabIQFeed.Controls.Add(this.btnReconnect);
			this.tabIQFeed.Controls.Add(this.groupBox3);
			this.tabIQFeed.Controls.Add(this.groupBox2);
			this.tabIQFeed.Controls.Add(this.btnStart);
			this.tabIQFeed.Controls.Add(this.btnStop);
			this.tabIQFeed.Location = new System.Drawing.Point(4, 22);
			this.tabIQFeed.Name = "tabIQFeed";
			this.tabIQFeed.Padding = new System.Windows.Forms.Padding(3);
			this.tabIQFeed.Size = new System.Drawing.Size(471, 279);
			this.tabIQFeed.TabIndex = 0;
			this.tabIQFeed.Text = "IQFeed";
			this.tabIQFeed.UseVisualStyleBackColor = true;
			// 
			// btnReconnect
			// 
			this.btnReconnect.Location = new System.Drawing.Point(364, 246);
			this.btnReconnect.Name = "btnReconnect";
			this.btnReconnect.Size = new System.Drawing.Size(95, 22);
			this.btnReconnect.TabIndex = 20;
			this.btnReconnect.Text = "Reconnect";
			this.btnReconnect.UseVisualStyleBackColor = true;
			this.btnReconnect.Click += new System.EventHandler(this.btnReconnect_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.rtbSymbols);
			this.groupBox3.Location = new System.Drawing.Point(6, 6);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(147, 262);
			this.groupBox3.TabIndex = 17;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Symbols";
			// 
			// rtbSymbols
			// 
			this.rtbSymbols.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbSymbols.Location = new System.Drawing.Point(3, 16);
			this.rtbSymbols.Name = "rtbSymbols";
			this.rtbSymbols.Size = new System.Drawing.Size(141, 243);
			this.rtbSymbols.TabIndex = 0;
			this.rtbSymbols.Text = "";
			// 
			// tabSetup
			// 
			this.tabSetup.Controls.Add(this.cbDataDelimiter);
			this.tabSetup.Controls.Add(this.label7);
			this.tabSetup.Controls.Add(this.rbAlldata);
			this.tabSetup.Controls.Add(this.rbCustomInt);
			this.tabSetup.Controls.Add(this.rbMainSession);
			this.tabSetup.Controls.Add(this.dtpCustomIntEnd);
			this.tabSetup.Controls.Add(this.dtpCustomIntBegin);
			this.tabSetup.Controls.Add(this.label5);
			this.tabSetup.Controls.Add(this.cbDTFormatDelimiter);
			this.tabSetup.Controls.Add(this.cbDTFormatTime);
			this.tabSetup.Controls.Add(this.cbDTFormatDate);
			this.tabSetup.Controls.Add(this.label3);
			this.tabSetup.Location = new System.Drawing.Point(4, 22);
			this.tabSetup.Name = "tabSetup";
			this.tabSetup.Padding = new System.Windows.Forms.Padding(3);
			this.tabSetup.Size = new System.Drawing.Size(471, 279);
			this.tabSetup.TabIndex = 1;
			this.tabSetup.Text = "Parameters";
			this.tabSetup.UseVisualStyleBackColor = true;
			// 
			// cbDataDelimiter
			// 
			this.cbDataDelimiter.FormattingEnabled = true;
			this.cbDataDelimiter.Items.AddRange(new object[] {
            "Space",
            "Tab",
            ",",
            ".",
            ":",
            ";",
            "/",
            "\\",
            "-"});
			this.cbDataDelimiter.Location = new System.Drawing.Point(95, 36);
			this.cbDataDelimiter.Name = "cbDataDelimiter";
			this.cbDataDelimiter.Size = new System.Drawing.Size(86, 21);
			this.cbDataDelimiter.TabIndex = 52;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 39);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73, 13);
			this.label7.TabIndex = 51;
			this.label7.Text = "Data Delimiter";
			// 
			// rbAlldata
			// 
			this.rbAlldata.AutoSize = true;
			this.rbAlldata.Location = new System.Drawing.Point(95, 109);
			this.rbAlldata.Name = "rbAlldata";
			this.rbAlldata.Size = new System.Drawing.Size(60, 17);
			this.rbAlldata.TabIndex = 50;
			this.rbAlldata.TabStop = true;
			this.rbAlldata.Text = "All data";
			this.rbAlldata.UseVisualStyleBackColor = true;
			this.rbAlldata.CheckedChanged += new System.EventHandler(this.rbAlldata_CheckedChanged);
			// 
			// rbCustomInt
			// 
			this.rbCustomInt.AutoSize = true;
			this.rbCustomInt.Location = new System.Drawing.Point(95, 86);
			this.rbCustomInt.Name = "rbCustomInt";
			this.rbCustomInt.Size = new System.Drawing.Size(97, 17);
			this.rbCustomInt.TabIndex = 49;
			this.rbCustomInt.TabStop = true;
			this.rbCustomInt.Text = "Custom interval";
			this.rbCustomInt.UseVisualStyleBackColor = true;
			this.rbCustomInt.CheckedChanged += new System.EventHandler(this.rbCustomInt_CheckedChanged);
			// 
			// rbMainSession
			// 
			this.rbMainSession.AutoSize = true;
			this.rbMainSession.Location = new System.Drawing.Point(95, 63);
			this.rbMainSession.Name = "rbMainSession";
			this.rbMainSession.Size = new System.Drawing.Size(255, 17);
			this.rbMainSession.TabIndex = 48;
			this.rbMainSession.TabStop = true;
			this.rbMainSession.Text = "Main stock market session (09:30:00  - 16:00:00)";
			this.rbMainSession.UseVisualStyleBackColor = true;
			this.rbMainSession.CheckedChanged += new System.EventHandler(this.rbMainSession_CheckedChanged);
			// 
			// dtpCustomIntEnd
			// 
			this.dtpCustomIntEnd.CustomFormat = "HH:mm:ss";
			this.dtpCustomIntEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dtpCustomIntEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpCustomIntEnd.Location = new System.Drawing.Point(280, 86);
			this.dtpCustomIntEnd.Name = "dtpCustomIntEnd";
			this.dtpCustomIntEnd.ShowUpDown = true;
			this.dtpCustomIntEnd.Size = new System.Drawing.Size(76, 20);
			this.dtpCustomIntEnd.TabIndex = 43;
			// 
			// dtpCustomIntBegin
			// 
			this.dtpCustomIntBegin.CustomFormat = "HH:mm:ss";
			this.dtpCustomIntBegin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dtpCustomIntBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpCustomIntBegin.Location = new System.Drawing.Point(198, 86);
			this.dtpCustomIntBegin.Name = "dtpCustomIntBegin";
			this.dtpCustomIntBegin.ShowUpDown = true;
			this.dtpCustomIntBegin.Size = new System.Drawing.Size(76, 20);
			this.dtpCustomIntBegin.TabIndex = 42;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 63);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(75, 13);
			this.label5.TabIndex = 39;
			this.label5.Text = "Intraday Filters";
			// 
			// cbDTFormatDelimiter
			// 
			this.cbDTFormatDelimiter.FormattingEnabled = true;
			this.cbDTFormatDelimiter.Items.AddRange(new object[] {
            "Space",
            "Tab",
            ",",
            ".",
            ":",
            ";",
            "/",
            "\\",
            "-"});
			this.cbDTFormatDelimiter.Location = new System.Drawing.Point(187, 9);
			this.cbDTFormatDelimiter.Name = "cbDTFormatDelimiter";
			this.cbDTFormatDelimiter.Size = new System.Drawing.Size(77, 21);
			this.cbDTFormatDelimiter.TabIndex = 3;
			// 
			// cbDTFormatTime
			// 
			this.cbDTFormatTime.FormattingEnabled = true;
			this.cbDTFormatTime.Items.AddRange(new object[] {
            "H:mm:ss",
            "H.mm",
            "H.mm.ss",
            "H:mm",
            "Hmm",
            "Hmmss",
            "hh.mm tt",
            "hh.mm.ss tt",
            "hh:mm tt",
            "hh:mm:ss tt",
            "hhmm tt",
            "hhmmss tt"});
			this.cbDTFormatTime.Location = new System.Drawing.Point(270, 9);
			this.cbDTFormatTime.Name = "cbDTFormatTime";
			this.cbDTFormatTime.Size = new System.Drawing.Size(86, 21);
			this.cbDTFormatTime.TabIndex = 2;
			// 
			// cbDTFormatDate
			// 
			this.cbDTFormatDate.FormattingEnabled = true;
			this.cbDTFormatDate.Items.AddRange(new object[] {
            "yyyy-MM-dd",
            "d/M/yyyy",
            "ddMMyyyy",
            "d-MMM-yyyy",
            "d-M-yyyy",
            "d.M.yy",
            "dd.MM.yyyy",
            "M/d/yyyy",
            "M-d-yyyy",
            "MMddyy",
            "MMddyyyy",
            "yyddMM",
            "yyMMdd",
            "yyyy/MM/dd",
            "yyyyddMM",
            "yyyyMMdd"});
			this.cbDTFormatDate.Location = new System.Drawing.Point(95, 9);
			this.cbDTFormatDate.Name = "cbDTFormatDate";
			this.cbDTFormatDate.Size = new System.Drawing.Size(86, 21);
			this.cbDTFormatDate.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 12);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "DateTime Format";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
			this.statusStrip1.Location = new System.Drawing.Point(0, 561);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(503, 22);
			this.statusStrip1.TabIndex = 24;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Green;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(77, 17);
			this.toolStripStatusLabel1.Text = "CONNECTED";
			this.toolStripStatusLabel1.Visible = false;
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.Red;
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(94, 17);
			this.toolStripStatusLabel2.Text = "DISCONNECTED";
			this.toolStripStatusLabel2.Visible = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 314);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 13);
			this.label1.TabIndex = 25;
			this.label1.Text = "Log messages";
			// 
			// rtbLog
			// 
			this.rtbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rtbLog.Location = new System.Drawing.Point(12, 330);
			this.rtbLog.Name = "rtbLog";
			this.rtbLog.ReadOnly = true;
			this.rtbLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.rtbLog.Size = new System.Drawing.Size(479, 228);
			this.rtbLog.TabIndex = 23;
			this.rtbLog.Text = "";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(503, 583);
			this.Controls.Add(this.tbFeeds);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.rtbLog);
			this.Name = "MainForm";
			this.Text = "MarketDataDownloader 3.0.0.1";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.tbFeeds.ResumeLayout(false);
			this.tabIQFeed.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.tabSetup.ResumeLayout(false);
			this.tabSetup.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.DateTimePicker dtpEndDateTime;
		private System.Windows.Forms.DateTimePicker dtpStartDateTime;
		private System.Windows.Forms.ComboBox cbSource;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox chbRealtime;
		private System.Windows.Forms.RadioButton rbInterval;
		private System.Windows.Forms.ComboBox cbTimeframe;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton rbDays;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox tbFolder;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cbTimeframeIntraday;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox tbAmountOfDays;
		private System.Windows.Forms.Button btnChooseStoreFolder;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.TabControl tbFeeds;
		private System.Windows.Forms.TabPage tabIQFeed;
		private System.Windows.Forms.Button btnReconnect;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RichTextBox rtbSymbols;
		private System.Windows.Forms.TabPage tabSetup;
		private System.Windows.Forms.ComboBox cbDataDelimiter;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.RadioButton rbAlldata;
		private System.Windows.Forms.RadioButton rbCustomInt;
		private System.Windows.Forms.RadioButton rbMainSession;
		private System.Windows.Forms.DateTimePicker dtpCustomIntEnd;
		private System.Windows.Forms.DateTimePicker dtpCustomIntBegin;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cbDTFormatDelimiter;
		private System.Windows.Forms.ComboBox cbDTFormatTime;
		private System.Windows.Forms.ComboBox cbDTFormatDate;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RichTextBox rtbLog;
	}
}
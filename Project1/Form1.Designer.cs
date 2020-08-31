namespace ScopeSnapSharp
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tblLayoutButtons = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGrab = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tblLayoutAdvanced = new System.Windows.Forms.TableLayoutPanel();
            this.txtSCPIcmd = new System.Windows.Forms.TextBox();
            this.cmdReadSCPI = new System.Windows.Forms.Button();
            this.cmdWriteSCPI = new System.Windows.Forms.Button();
            this.txtSCPIresponse = new System.Windows.Forms.TextBox();
            this.txtUpdateSpeed = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolstripLabelMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLblPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusUpdateSpeed = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayscaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showButtonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showRightPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tblLayoutAdvanced.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(1050, 618);
            this.splitContainer1.SplitterDistance = 758;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tblLayoutButtons);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer2.Size = new System.Drawing.Size(758, 618);
            this.splitContainer2.SplitterDistance = 49;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 1;
            // 
            // tblLayoutButtons
            // 
            this.tblLayoutButtons.ColumnCount = 1;
            this.tblLayoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutButtons.Location = new System.Drawing.Point(0, 0);
            this.tblLayoutButtons.Margin = new System.Windows.Forms.Padding(2);
            this.tblLayoutButtons.Name = "tblLayoutButtons";
            this.tblLayoutButtons.RowCount = 1;
            this.tblLayoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblLayoutButtons.Size = new System.Drawing.Size(49, 618);
            this.tblLayoutButtons.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(706, 618);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseHover);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.btnSearch, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnConnect, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.listBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnGrab, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.checkBox1, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.tblLayoutAdvanced, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtUpdateSpeed, 2, 7);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(288, 618);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Location = new System.Drawing.Point(3, 183);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(138, 24);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // textBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.textBox1, 3);
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 157);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(282, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.DoubleClick += new System.EventHandler(this.TextBox1_DoubleClick);
            // 
            // btnConnect
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.btnConnect, 2);
            this.btnConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConnect.Location = new System.Drawing.Point(147, 183);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(138, 24);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // listBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.listBox1, 3);
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(282, 148);
            this.listBox1.Sorted = true;
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.btnSave, 3);
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(3, 531);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(282, 54);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnGrab
            // 
            this.btnGrab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGrab.Location = new System.Drawing.Point(3, 591);
            this.btnGrab.Name = "btnGrab";
            this.btnGrab.Size = new System.Drawing.Size(138, 24);
            this.btnGrab.TabIndex = 8;
            this.btnGrab.Text = "Grab";
            this.btnGrab.UseVisualStyleBackColor = true;
            this.btnGrab.Click += new System.EventHandler(this.BtnGrab_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(147, 591);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(66, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Live Update";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // tblLayoutAdvanced
            // 
            this.tblLayoutAdvanced.AutoSize = true;
            this.tblLayoutAdvanced.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tblLayoutAdvanced.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tblLayoutAdvanced, 3);
            this.tblLayoutAdvanced.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLayoutAdvanced.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLayoutAdvanced.Controls.Add(this.txtSCPIcmd, 0, 0);
            this.tblLayoutAdvanced.Controls.Add(this.cmdReadSCPI, 1, 1);
            this.tblLayoutAdvanced.Controls.Add(this.cmdWriteSCPI, 0, 1);
            this.tblLayoutAdvanced.Controls.Add(this.txtSCPIresponse, 1, 2);
            this.tblLayoutAdvanced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutAdvanced.Location = new System.Drawing.Point(2, 232);
            this.tblLayoutAdvanced.Margin = new System.Windows.Forms.Padding(2);
            this.tblLayoutAdvanced.Name = "tblLayoutAdvanced";
            this.tblLayoutAdvanced.RowCount = 3;
            this.tblLayoutAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tblLayoutAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblLayoutAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblLayoutAdvanced.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblLayoutAdvanced.Size = new System.Drawing.Size(284, 143);
            this.tblLayoutAdvanced.TabIndex = 10;
            // 
            // txtSCPIcmd
            // 
            this.tblLayoutAdvanced.SetColumnSpan(this.txtSCPIcmd, 2);
            this.txtSCPIcmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSCPIcmd.Location = new System.Drawing.Point(4, 4);
            this.txtSCPIcmd.Margin = new System.Windows.Forms.Padding(2);
            this.txtSCPIcmd.Multiline = true;
            this.txtSCPIcmd.Name = "txtSCPIcmd";
            this.txtSCPIcmd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSCPIcmd.Size = new System.Drawing.Size(276, 45);
            this.txtSCPIcmd.TabIndex = 14;
            // 
            // cmdReadSCPI
            // 
            this.cmdReadSCPI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdReadSCPI.Location = new System.Drawing.Point(145, 55);
            this.cmdReadSCPI.Margin = new System.Windows.Forms.Padding(2);
            this.cmdReadSCPI.Name = "cmdReadSCPI";
            this.cmdReadSCPI.Size = new System.Drawing.Size(135, 20);
            this.cmdReadSCPI.TabIndex = 16;
            this.cmdReadSCPI.Text = "Query";
            this.cmdReadSCPI.UseVisualStyleBackColor = true;
            this.cmdReadSCPI.Click += new System.EventHandler(this.CmdQuerySCPI_Click);
            // 
            // cmdWriteSCPI
            // 
            this.cmdWriteSCPI.Location = new System.Drawing.Point(4, 55);
            this.cmdWriteSCPI.Margin = new System.Windows.Forms.Padding(2);
            this.cmdWriteSCPI.Name = "cmdWriteSCPI";
            this.cmdWriteSCPI.Size = new System.Drawing.Size(135, 20);
            this.cmdWriteSCPI.TabIndex = 15;
            this.cmdWriteSCPI.Text = "Write";
            this.cmdWriteSCPI.UseVisualStyleBackColor = true;
            this.cmdWriteSCPI.Click += new System.EventHandler(this.CmdSCPISend_Click);
            // 
            // txtSCPIresponse
            // 
            this.txtSCPIresponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblLayoutAdvanced.SetColumnSpan(this.txtSCPIresponse, 2);
            this.txtSCPIresponse.Location = new System.Drawing.Point(9, 87);
            this.txtSCPIresponse.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.txtSCPIresponse.Multiline = true;
            this.txtSCPIresponse.Name = "txtSCPIresponse";
            this.txtSCPIresponse.ReadOnly = true;
            this.txtSCPIresponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSCPIresponse.Size = new System.Drawing.Size(266, 48);
            this.txtSCPIresponse.TabIndex = 12;
            this.txtSCPIresponse.Leave += new System.EventHandler(this.TxtSCPIresponse_Leave);
            // 
            // txtUpdateSpeed
            // 
            this.txtUpdateSpeed.Location = new System.Drawing.Point(219, 591);
            this.txtUpdateSpeed.Name = "txtUpdateSpeed";
            this.txtUpdateSpeed.Size = new System.Drawing.Size(66, 20);
            this.txtUpdateSpeed.TabIndex = 11;
            this.txtUpdateSpeed.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtUpdateSpeed_KeyDown);
            this.txtUpdateSpeed.Leave += new System.EventHandler(this.TxtUpdateSpeed_Leave);
            this.txtUpdateSpeed.Validating += new System.ComponentModel.CancelEventHandler(this.TxtUpdateSpeed_Validating);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolstripLabelMessage,
            this.toolStripSpring,
            this.toolStripLblPosition,
            this.toolStripStatusUpdateSpeed});
            this.statusStrip1.Location = new System.Drawing.Point(0, 648);
            this.statusStrip1.MaximumSize = new System.Drawing.Size(0, 22);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1050, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // toolstripLabelMessage
            // 
            this.toolstripLabelMessage.Name = "toolstripLabelMessage";
            this.toolstripLabelMessage.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolstripLabelMessage.Size = new System.Drawing.Size(56, 17);
            this.toolstripLabelMessage.Text = "Message:";
            // 
            // toolStripSpring
            // 
            this.toolStripSpring.Name = "toolStripSpring";
            this.toolStripSpring.Size = new System.Drawing.Size(659, 17);
            this.toolStripSpring.Spring = true;
            // 
            // toolStripLblPosition
            // 
            this.toolStripLblPosition.Name = "toolStripLblPosition";
            this.toolStripLblPosition.Size = new System.Drawing.Size(129, 17);
            this.toolStripLblPosition.Text = "Position: Not Initialized";
            // 
            // toolStripStatusUpdateSpeed
            // 
            this.toolStripStatusUpdateSpeed.Name = "toolStripStatusUpdateSpeed";
            this.toolStripStatusUpdateSpeed.Size = new System.Drawing.Size(89, 17);
            this.toolStripStatusUpdateSpeed.Text = "UpdateSpeed: 0";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.pictureOptionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1050, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newConnectionToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newConnectionToolStripMenuItem
            // 
            this.newConnectionToolStripMenuItem.Name = "newConnectionToolStripMenuItem";
            this.newConnectionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newConnectionToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.newConnectionToolStripMenuItem.Text = "&New Connection";
            this.newConnectionToolStripMenuItem.Click += new System.EventHandler(this.NewConnectionToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem1});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // copyToolStripMenuItem1
            // 
            this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            this.copyToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+C";
            this.copyToolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.copyToolStripMenuItem1.Text = "&Copy";
            this.copyToolStripMenuItem1.Click += new System.EventHandler(this.CopyToolStripMenuItem1_Click);
            // 
            // pictureOptionsToolStripMenuItem
            // 
            this.pictureOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.invertToolStripMenuItem,
            this.grayscaleToolStripMenuItem,
            this.liveToolStripMenuItem,
            this.showButtonsToolStripMenuItem,
            this.showRightPanelToolStripMenuItem,
            this.advancedPanelToolStripMenuItem});
            this.pictureOptionsToolStripMenuItem.Name = "pictureOptionsToolStripMenuItem";
            this.pictureOptionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.pictureOptionsToolStripMenuItem.Text = "Options";
            this.pictureOptionsToolStripMenuItem.Click += new System.EventHandler(this.PictureOptionsToolStripMenuItem_Click);
            // 
            // invertToolStripMenuItem
            // 
            this.invertToolStripMenuItem.Checked = true;
            this.invertToolStripMenuItem.CheckOnClick = true;
            this.invertToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.invertToolStripMenuItem.Name = "invertToolStripMenuItem";
            this.invertToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.invertToolStripMenuItem.Text = "Invert";
            this.invertToolStripMenuItem.Click += new System.EventHandler(this.InvertToolStripMenuItem_Click);
            // 
            // grayscaleToolStripMenuItem
            // 
            this.grayscaleToolStripMenuItem.CheckOnClick = true;
            this.grayscaleToolStripMenuItem.Name = "grayscaleToolStripMenuItem";
            this.grayscaleToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.grayscaleToolStripMenuItem.Text = "Grayscale";
            this.grayscaleToolStripMenuItem.Click += new System.EventHandler(this.GrayscaleToolStripMenuItem_Click);
            // 
            // liveToolStripMenuItem
            // 
            this.liveToolStripMenuItem.Checked = true;
            this.liveToolStripMenuItem.CheckOnClick = true;
            this.liveToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.liveToolStripMenuItem.Name = "liveToolStripMenuItem";
            this.liveToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.liveToolStripMenuItem.Text = "Live";
            this.liveToolStripMenuItem.Click += new System.EventHandler(this.LiveToolStripMenuItem_Click);
            // 
            // showButtonsToolStripMenuItem
            // 
            this.showButtonsToolStripMenuItem.CheckOnClick = true;
            this.showButtonsToolStripMenuItem.Name = "showButtonsToolStripMenuItem";
            this.showButtonsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.showButtonsToolStripMenuItem.Text = "Show Buttons";
            this.showButtonsToolStripMenuItem.Click += new System.EventHandler(this.ShowButtonsToolStripMenuItem_Click);
            // 
            // showRightPanelToolStripMenuItem
            // 
            this.showRightPanelToolStripMenuItem.Checked = true;
            this.showRightPanelToolStripMenuItem.CheckOnClick = true;
            this.showRightPanelToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showRightPanelToolStripMenuItem.Name = "showRightPanelToolStripMenuItem";
            this.showRightPanelToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.showRightPanelToolStripMenuItem.Text = "Show Right Panel";
            this.showRightPanelToolStripMenuItem.Click += new System.EventHandler(this.ShowRightPanelToolStripMenuItem_Click);
            // 
            // advancedPanelToolStripMenuItem
            // 
            this.advancedPanelToolStripMenuItem.CheckOnClick = true;
            this.advancedPanelToolStripMenuItem.Name = "advancedPanelToolStripMenuItem";
            this.advancedPanelToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.advancedPanelToolStripMenuItem.Text = "Advanced Panel";
            this.advancedPanelToolStripMenuItem.Click += new System.EventHandler(this.AdvancedPanelToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usageToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // usageToolStripMenuItem
            // 
            this.usageToolStripMenuItem.Name = "usageToolStripMenuItem";
            this.usageToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.usageToolStripMenuItem.Text = "Usage";
            this.usageToolStripMenuItem.Click += new System.EventHandler(this.UsageToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 670);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Scope Snap Sharp";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tblLayoutAdvanced.ResumeLayout(false);
            this.tblLayoutAdvanced.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnGrab;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pictureOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayscaleToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TableLayoutPanel tblLayoutButtons;
        private System.Windows.Forms.ToolStripMenuItem showButtonsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem liveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showRightPanelToolStripMenuItem;
        private System.Windows.Forms.Button cmdWriteSCPI;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLblPosition;
        private System.Windows.Forms.Button cmdReadSCPI;
        private System.Windows.Forms.TextBox txtSCPIresponse;
        private System.Windows.Forms.TableLayoutPanel tblLayoutAdvanced;
        private System.Windows.Forms.ToolStripMenuItem advancedPanelToolStripMenuItem;
        private System.Windows.Forms.TextBox txtSCPIcmd;
        private System.Windows.Forms.ToolStripStatusLabel toolstripLabelMessage;
        private System.Windows.Forms.ToolStripStatusLabel toolStripSpring;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusUpdateSpeed;
        private System.Windows.Forms.TextBox txtUpdateSpeed;
    }
}
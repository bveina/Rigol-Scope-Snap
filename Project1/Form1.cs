﻿using NationalInstruments.Visa;
using ScopeSnapSharp.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using System.Text.RegularExpressions;
using Ivi.Visa;

namespace ScopeSnapSharp
{
    public partial class Form1 : Form
    {
        private MessageBasedSession mbSession;
        private Timer t;
        private byte[] lastPacket;
        private Bitmap model;
        private bool Connected;
        private int FailCount;
        private bool _live;
        private Func<byte[]> imgGrabFunction;
        Size LastPicSize = new Size(0, 0);


        // databinding for available SCPI devices in a listbox
        public BindingList<string> resourceList { get; set; }
        
        private int _ScreenUpdate_mS;
        public int ScreenUpdateRate
        {
            get
            {
                return _ScreenUpdate_mS;
            }
            set
            {
                if (value < 500) return;
                _ScreenUpdate_mS = value;
            }
        }
        private string[] invertCommands;
        private string[] turnGrayscaleCommands;
        private string getDataCommand;



        public bool Live { get
            {
                return _live;
            }
            set
            {
                _live = value;
                if (liveToolStripMenuItem.Checked != _live) liveToolStripMenuItem.Checked = _live;
                if (checkBox1.Checked != _live) checkBox1.Checked = _live;
                t.Enabled = _live;
                

            }
        }

        private Dictionary<string, string> QuickList = new Dictionary<string, string>() {
            {"Menu Off","moff"},
            {"Measure" ,"MEASure"},
            { "Acquire","ACQuire" },
            { "Storage","STORage" },
            { "Cursor" ,"CURSor"},
            { "Display","DISPlay" },
            { "Utility","UTILity" },
            { "LA","LA" },
            { "REF","REF" },
            { "MATH","MATH" },
            { "DECODE" ,"DECode"},
            { "TRIGGER","TMENu" },
            { "G1" ,"GENerator1"},
            { "G2" ,"GENerator2"},
            { "SEARCH" ,"SEARch"},
            { "ZOOM" ,"ZOOM"},
            { "QUICK","QUICk" },
            { "AUTO","AUTO" },
            {  "DEFAULT","DEFault" },
            { "CLEAR" ,"CLEar"},
            { "BACK","BACK"}
        };

        public bool getInvertStatus()
        {
            if (invertCommands[0] == "") return false;
            if (invertCommands[2] == "") return false;
            lock (mbSession)
            {
                
                mbSession.RawIO.Write(invertCommands[0]);
                string s = mbSession.FormattedIO.ReadLine();
                if (s.Trim() == invertCommands[2])
                    return true;
                else
                    return false;
            }
        }

        public void setInvert(bool inverted)
        {
            if (invertCommands[1] == "") return ;
            if (invertCommands[2] == "") return ;
            if (invertCommands[3] == "") return ;
            lock (mbSession)
            {
                if (inverted)
                    mbSession.RawIO.Write(string.Format(invertCommands[1], invertCommands[2]));
                else
                    mbSession.RawIO.Write(string.Format(invertCommands[1], invertCommands[3]));
            }
        }

        public bool getGrayscaleStatus()
        {
            if (turnGrayscaleCommands[0] == "") return false;
            if (turnGrayscaleCommands[2] == "") return false;
            lock (mbSession)
            {

                mbSession.RawIO.Write(turnGrayscaleCommands[0]);
                string s = mbSession.FormattedIO.ReadLine();
                if (s.Trim() == turnGrayscaleCommands[2])
                    return true;
                else
                    return false;
            }
        }
        public void setGrayscale(bool gray)
        {
            if (turnGrayscaleCommands[1] == "") return;
            if (turnGrayscaleCommands[2] == "") return;
            if (turnGrayscaleCommands[3] == "") return;

            lock (mbSession)
            {
                if (gray)
                {
                    mbSession.RawIO.Write(string.Format(turnGrayscaleCommands[1], turnGrayscaleCommands[2]));
                }
                else
                {
                    mbSession.RawIO.Write(string.Format(turnGrayscaleCommands[1], turnGrayscaleCommands[3]));
                }
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }

        BackgroundWorker ImageGrabber;

        public Form1()
        {
            InitializeComponent();
            validateUpdateTime("2000", out _ScreenUpdate_mS);
            t = new Timer();
            t.Interval = ScreenUpdateRate;
            t.Tick += T_Tick;
            SetupControlState(false);
            FailCount = 0;

            //setup image grabber thread
            ImageGrabber = new BackgroundWorker();
            ImageGrabber.DoWork += ImageGrabber_DoWork;
            ImageGrabber.RunWorkerCompleted += ImageGrabber_RunWorkerCompleted;
            ImageGrabber.ProgressChanged += ImageGrabber_ProgressChanged;
            ImageGrabber.WorkerReportsProgress = true;

            // setup data bindind for the Instrument Listbox
            this.resourceList = new BindingList<string>();
            this.listBox1.DataSource = resourceList;

            Binding updateBnd = new Binding("Text", this, "ScreenUpdateRate", true);
            updateBnd.BindingComplete += Form1_BindingComplete;
            updateBnd.Parse += Form1_Parse;
            txtUpdateSpeed.DataBindings.Add(updateBnd);
            

            
            //this.updateLabel = new Binding("ScreenUpdateRate",ScreenUpdateRate,)
            Application.EnableVisualStyles();
        }

        private void Form1_Parse(object sender, ConvertEventArgs e)
        {
            int tmp;
            if (int.TryParse((string)e.Value,out tmp))
            {
                e.Value = tmp;
            }
            
            
        }

        private void Form1_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            System.Console.WriteLine(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                tblLayoutButtons.SuspendLayout();
                tblLayoutButtons.ColumnCount = 1;
                tblLayoutButtons.RowCount = QuickList.Count;
                tblLayoutButtons.RowStyles.Clear();
                int i = 0;
                foreach (KeyValuePair<string, string> pair in QuickList)
                {
                    Button b = new Button();
                    b.Text = pair.Key;
                    b.Click += cmdSendButtonPress_click;
                    b.Dock = DockStyle.Fill;
                    b.Tag = pair.Value;
                    tblLayoutButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / QuickList.Count));
                    tblLayoutButtons.Controls.Add(b, 0, i);
                    i++;
                }

                tblLayoutButtons.ResumeLayout();
                tblLayoutButtons.Width = 80;
                splitContainer2.Panel1Collapsed = true;
                tblLayoutAdvanced.Visible = advancedPanelToolStripMenuItem.Checked;

                if (Settings.Default.LastConnectionSuccessful)
                {
                    this.Live = true;
                    this.beginSearchConnections();
                }
               // txtScreenUpdateRate.
               //this.DataBindings.Add("ScreenUpdateTime",txtScreenUpdateRate,"text",)
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //start background worker to get image (if not already in progress
        private void updateScreen(PictureBox pb)
        {
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            if (this.imgGrabFunction ==null)
            {
                toolStripProgressBar1.Style = ProgressBarStyle.Continuous;
                return;
            }
            if (ImageGrabber.IsBusy) return;
            ImageGrabber.RunWorkerAsync(pb);
        }

        #region Image Grabber Background Task
        private void ImageGrabber_DoWork(object sender, DoWorkEventArgs e)
        {
            


            //query what the scope is doing for image settings and setup controls to reflect that
            lock (mbSession)
            {
                if (mbSession == null || mbSession.IsDisposed) return;

                try
                {
                    //Use invoke to make this happen. or use a data binding.
                    //invertToolStripMenuItem.Checked = getInvertStatus();
                    //grayscaleToolStripMenuItem.Checked = getGrayscaleStatus();
                }
                catch (Ivi.Visa.VisaException ex)
                {
                    (sender as BackgroundWorker).ReportProgress(00, ex.Message);
                    //e.Result = null;
                    //return;
                }
            }
            lock(mbSession)
            { 
                // yes this is just trying to get a single image and having 3 catch blocks just for this
                byte[] tmpPacket = null;
                try
                {

                    //tmpPacket = getImage();
                    //tmpPacket = getJFIFImage();
                    tmpPacket = this.imgGrabFunction();
                }
                catch (ArgumentException ex)
                {
                    (sender as BackgroundWorker).ReportProgress(00, ex.Message);
                    e.Result = null;
                    return;
                }
                catch (Ivi.Visa.IOTimeoutException)
                {
                    (sender as BackgroundWorker).ReportProgress(100, "Scope Stopped Responding, is it on and plugged in?");
                    e.Result = null;
                    return;
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, exp.Message);
                    e.Result = null;
                    return;

                }

                if (tmpPacket == null)
                {
                    e.Result = null;
                    return; // something broke. fail silently and add to the fail count
                }
                lastPacket = tmpPacket;
            }

            // we finally have a bitmap, convert it to a bitmap.
            MemoryStream ms = new MemoryStream();
            ms.Write(lastPacket, 0, Convert.ToInt32(lastPacket.Length));
            // removing this fixes the resize bug that tries to display a new image while resizing.
            // this will stay until the next code clean up at which time if no memory or display issues have occured,
            // this can be removed from the code base.
            /*
            if (model != null)   
            {
                model.Dispose();
            }
            */
            model = new Bitmap(ms, false);
            e.Result = e.Argument; // the result is the picture box we want to populate...
        }


        private void ImageGrabber_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
            setMessage(e.UserState as string);
        }

        private void ImageGrabber_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripProgressBar1.Style = ProgressBarStyle.Continuous;
            toolStripProgressBar1.Value = 0;

            if (e.Result == null)
            {
                FailCount++;
                if (FailCount > 5)
                {
                    SetupControlState(false);
                    lock (mbSession)
                    {
                        mbSession.Dispose();
                    }
                    setMessage("Disconnected!");
                    MessageBox.Show("There was a problem while grabbbing an image, please reconnect to the Scope");
                }
                return;

            }
            PictureBox pb = (e.Result as PictureBox);
            if (pb.Image != null)
            {
                pb.Image.Dispose();
            }
            pb.Image = model;
            FailCount = 0;
            setMessage("Connected to " + textBox1.Text);
            
        }
        #endregion


        // starts a background worker
        private void beginSearchConnections()
        {
            if (listBox1.Items.Contains(textBox1.Text))
            {
                textBox1.Text = "";
            }
            //listBox1.Items.Clear();

            textBox1.ReadOnly = true;
            Cursor.Current = Cursors.WaitCursor;
            setMessage("Finding Instruments");

            BackgroundWorker find = new BackgroundWorker();
            find.WorkerReportsProgress = true;
            find.DoWork += Find_DoWork;
            find.RunWorkerCompleted += Find_RunWorkerCompleted;
            find.ProgressChanged += Find_ProgressChanged;
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            find.RunWorkerAsync();
        }


        #region find Instruments Background worker

        private void Find_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
            setMessage(e.UserState as string);
        }


        private void Find_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Style = ProgressBarStyle.Continuous;
            resourceList.Clear();
            resourceList.RaiseListChangedEvents = true;
            if (e.Result == null)
            {
                setMessage("could not find any instruments, check connections and try again.");
                return;
            }
            foreach (string s in (e.Result as List<string>))
            {
                resourceList.Add(s);

            }


            if (resourceList.Count == 1)
            {
                listBox1.SelectedIndex = 0;
                textBox1.Text = (string)resourceList[0];
                setMessage("One Instrument Found.");
                connect((string)resourceList[0]);
                
                updateScreen(pictureBox1);
            }
            else if (resourceList.Count != 0)
            {
                listBox1.SelectedIndex = 0;
                textBox1.Text = (string)resourceList[0];
                setMessage("Multiple Instruments Found, Please Select one and click connect.");
                //listBox1.SelectedIndex = 0;
            }
            else
            {
                setMessage("No Instruments Found.");
            }
        }

        private void Find_DoWork(object sender, DoWorkEventArgs e)
        {

            (sender as BackgroundWorker).ReportProgress(0, "Searchign - getting session.");
            using (var rmSession = new ResourceManager())
            {

                //var resources = rmSession.Find("(ASRL|GPIB|TCPIP|USB)?*");
                try
                {
                    (sender as BackgroundWorker).ReportProgress(50, "Searching - finding Items.");
                    var resources = rmSession.Find(Resources.SCPISearchString);

                    e.Result = resources.ToList();
                    /*foreach (string s in resources)
                    {
                        listBox1.Items.Add(s);
                    }
                    */
                }
                catch (Exception ex)
                {
                    (sender as BackgroundWorker).ReportProgress(0, "Error - Could Not Find Any Instruments.");
                    Cursor.Current = Cursors.Default;
                    return;

                }
            }
        }

        #endregion


        private void T_Tick(object sender, EventArgs e)
        {
            
            t.Enabled = false;
            if (mbSession == null) return;
            if (this.Live)
            {
                lock (mbSession)
                {
                    try
                    {
                        if (mbSession == null || mbSession.IsDisposed) return;
                        //starts up a background thread
                        updateScreen(pictureBox1);
                        //FailCount = 0;
                    }
                    catch (Exception ex)
                    {
                        FailCount++;
                        if (FailCount > 5)
                        {
                            MessageBox.Show(ex.Message);
                            SetupControlState(false);
                            mbSession.Dispose();
                            setMessage("Disconnected");
                        }
                    }
                }
            }
            t.Interval = this.ScreenUpdateRate;
            t.Enabled = true;
            

        }



        #region control event handler methods
        // this method is called by all side panel buttons on click. 
        // button tag must contain the string that should be sent
        // sends a simulated keypress to the scope
        private void cmdSendButtonPress_click(object sender, EventArgs e)
        {
            lock (mbSession)
            {
                mbSession.RawIO.Write(String.Format(":SYST:KEY:PRESS {0}", (string)(sender as Button).Tag));
            }
        }


        // launch a background worker to find and populate instruments
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //SearchConnections();
            beginSearchConnections();
        }


        //todo, replace this with a data binding.
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = (string)(sender as ListBox).SelectedItem;
        }


        // users can double click on the instrument text box to specify custom connection strings
        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb.ReadOnly = false;
        }

        // destroy the existing connection to the instrument and update the controls to show we are disconnected.
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            lock (mbSession)
            {
                SetupControlState(false);
                mbSession.Dispose();
                setMessage("Disconnected");
            }
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            connect(textBox1.Text);
        }

        private void btnGrab_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                updateScreen(pictureBox1);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "error while geting image");
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!Connected)
            {
                setMessage("You are not connected to any scope! (did you search AND connect?)");
                return;
            }
            MouseEventArgs ee = e as MouseEventArgs;

            if (ee.Button == MouseButtons.Right)
            {
                updateScreen(pictureBox1);
                return;
            }
            int myX, myY;
            convertCoordinates(ee.X, ee.Y, out myX, out myY);
            if (myX < 0 || myY < 0) return;
            string txt2Send = string.Format(":SYSTem:TOUCh {0}, {1}", myX, myY);
            lock (mbSession)
            {

                try
                {
                    mbSession.RawIO.Write(txt2Send);
                }

                catch (Ivi.Visa.IOTimeoutException)
                {
                    MessageBox.Show("Scope Lost Contact", "Error Sending Touch Request");
                    SetupControlState(false);
                    mbSession.Dispose();
                    setMessage("Disconnected");
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, "Error Sending Touch Request");
                    SetupControlState(false);
                    mbSession.Dispose();
                    setMessage("Disconnected");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.Live = (sender as CheckBox).Checked;
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveScreenShot();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetImage(model);
        }

        private void newConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            beginSearchConnections();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AppString = "Scope Snap Sharp";
            string versionString = Application.ProductVersion;
            string credits = "logo: \"Oscilloscope\" by Filter Forge is licensed under CC BY 2.0";
            MessageBox.Show(String.Format("{0}\r\n(c) Ben Viall\r\nVersion: {1}\r\n\r\n", AppString, versionString, credits), "About");

        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setInvert(invertToolStripMenuItem.Checked);
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setGrayscale(grayscaleToolStripMenuItem.Checked);
        }

        private void showButtonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer2.SplitterDistance = 100;
            splitContainer2.Panel1Collapsed = !showButtonsToolStripMenuItem.Checked;
        }

        private void pictureOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveScreenShot();
        }

        private void liveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool selectedState = (sender as ToolStripMenuItem).Checked;
            this.Live = selectedState;
        }

        private void showRightPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !(sender as ToolStripMenuItem).Checked;
        }
        private void pictureBox1_MouseHover(object sender, MouseEventArgs e)
        {
            int myX, myY;
            try
            {
                convertCoordinates(e.X, e.Y, out myX, out myY);
                toolStripLblPosition.Text = String.Format("Position:({0}, {1})", myX, myY);
            }
            catch (Exception ex)
            {
                //ignore
            }

        }

        private void advancedPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tblLayoutAdvanced.Visible = advancedPanelToolStripMenuItem.Checked;
            if (advancedPanelToolStripMenuItem.Checked == true)
            {
                splitContainer1.Panel2Collapsed = false;
                showRightPanelToolStripMenuItem.Checked = true;
            }
        }

        private void cmdQuerySCPI_Click(object sender, EventArgs e)
        {
            lock (mbSession)
            {
                if (mbSession == null || mbSession.IsDisposed) return;
                try
                {
                    txtSCPIresponse.Text = "";
                    string[] arr = txtSCPIcmd.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var line in arr)
                    {
                        mbSession.RawIO.Write(line);
                        txtSCPIresponse.Text +=mbSession.RawIO.ReadString() + "\r\n";
                    }
                }
                catch (Ivi.Visa.IOTimeoutException ex)
                {
                    txtSCPIresponse.Text = "Query Timed Out";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void cmdSCPISend_Click(object sender, EventArgs e)
        {
            lock (mbSession)
            {
                if (mbSession == null || mbSession.IsDisposed) return;
                
                string[] arr = txtSCPIcmd.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in arr)
                {
                    mbSession.RawIO.Write(line);
                    mbSession.RawIO.Write("*WAI");

                }
            }

        }

        private void usageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://github.com/bveina/Rigol-Scope-Snap");
        }
        #endregion


        // sets a message string on the control bar.
        private void setMessage(string s)
        {
            toolstripLabelMessage.Text = String.Format("Message: {0}", s);
        }

        

        // enables or disables controls to reflect if the app is connected to an instrument.
        private void SetupControlState(bool state)
        {
            Connected = state;
            textBox1.ReadOnly = true;
            if (state)
            {
                btnConnect.Text = "Disconnect";
                btnConnect.Click -= btnConnect_Click;
                btnConnect.Click -= btnDisconnect_Click;
                btnConnect.Click += btnDisconnect_Click;
            }
            else
            {
                btnConnect.Text = "Connect";
                btnConnect.Click -= btnConnect_Click;
                btnConnect.Click -= btnDisconnect_Click;
                btnConnect.Click += btnConnect_Click;
            }

            btnSearch.Enabled = !state;
            listBox1.Enabled = !state;
            textBox1.Enabled = !state;

            cmdReadSCPI.Enabled = state;
            cmdWriteSCPI.Enabled = state;
            txtSCPIcmd.Enabled = state;

            // if there is a picture available leave save and copy available, even if there is no connection.
            if (pictureBox1.Image == null) 
            {
                btnSave.Enabled = state;
                saveToolStripMenuItem.Enabled = state;
                copyToolStripMenuItem1.Enabled = state;
            }
            else
            {
                btnSave.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem1.Enabled = true;
            }

            checkBox1.Enabled = state;

            btnGrab.Enabled = state;
            newConnectionToolStripMenuItem.Enabled = !state;
        }


        // runs on the main thread, dont call this from a background worker
        // connects to an instrument.
        private void connect(string res)
        {
            if (res == "")
            {
                setMessage("No Resouce Selected - Choose search, then select an item from the list");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            setMessage("Connecting to " + res);

            using (var rmSession = new ResourceManager())
            {
                ResourceOpenStatus myStatus;
                try
                {
                    mbSession = (MessageBasedSession)rmSession.Open(res, AccessModes.ExclusiveLock, 10, out myStatus);
                    if (myStatus != ResourceOpenStatus.Success)
                    {
                        SetupControlState(false);
                        setMessage("there was an error while trying to open that device.");
                        return;
                    }
                    SetupControlState(true);
                    // at this point we are connected, lets get our initial image
                    // TODO: does this really belong in this connect method?
                    // probably not.
                    // update Strings
                    mbSession.RawIO.Write("*IDN?");
                    if (!UpdateQueryStrings(mbSession.RawIO.ReadString()))
                    {
                        // cant figure out how to use this scope, so leave everything disabled and be done.
                        setMessage("Limited Connected to " + res);
                        return;
                    }

                    if (invertToolStripMenuItem.Checked && invertCommands[1] != "")
                    {
                        //mbSession.RawIO.Write(":SAVE:IMAGE:INVERT ON");
                        mbSession.RawIO.Write(string.Format(invertCommands[1], invertCommands[2]));
                    }
                    setMessage("Connected to " + res);
                    if (Settings.Default.LastConnectionSuccessful && this.Live)
                    {
                        t.Enabled = true; //force timer on incase it timed out while connecting.
                    }
                    updateScreen(pictureBox1);
                    Settings.Default.LastConnectionSuccessful = true;

                }
                catch (InvalidCastException)
                {
                    setMessage("Could not connect to " + res);

                }
                catch (Exception exp)
                {

                    MessageBox.Show(exp.Message, "Connect Error");
                    //TODO: log this error
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }

            }
        }

        private bool UpdateQueryStrings(string idnResponse)
        {
            //RIGOL TECHNOLOGIES, DS1104Z, DS1ZA192309090,00.04.04.SP4
            string[] idnList = idnResponse.Split(',');
            //if (idnList[0].ToUpper().Contains("RIGOL"))
            //{
                string model = idnList[1].Trim();
                Regex find1 = new System.Text.RegularExpressions.Regex("DS1[0-9][0-9][0-9][ZB]");
                Regex find2 = new System.Text.RegularExpressions.Regex("MSO5[0-9][0-9][0-9]");
                Regex find3 = new System.Text.RegularExpressions.Regex("TBS2[0-9][0-9][0-9]");

            if (find1.IsMatch(model))
            {
                this.turnGrayscaleCommands = new string[] { ":STORage:IMAGE:COLOR?", ":STORage:IMAGE:COLOR {0}", "OFF", "ON" };
                this.invertCommands = new string[] { ":STORage:IMAGE:INVERT?", ":STORage:IMAGE:INVERT {0}", "ON", "Off" };
                this.getDataCommand = "DISP:DATA?";
                this.imgGrabFunction = getImage;
                return true;
            }
            else if (find2.IsMatch(model))
            {
                this.turnGrayscaleCommands = new string[] { ":SAVE:IMAGE:COLOR?", ":SAVE:IMAGE:COLOR {0}", "GRAY", "COLor" };
                this.invertCommands = new string[] { ":SAVE:IMAGE:INVERT?", ":SAVE:IMAGE:INVERT {0}", "1", "0" };
                this.getDataCommand = "DISP:DATA?";
                this.imgGrabFunction = getImage;
                return true;
            }
            else if (find3.IsMatch(model))
            {
                this.turnGrayscaleCommands = new string[] { "", "", "", "" };
                this.invertCommands = new string[] { "", "", "", "" };
                this.getDataCommand = "HARDCOPY START";
                this.imgGrabFunction = getJFIFImage;
                return true;
            }
            else
            {
                MessageBox.Show("This Device is not currently supported but you can use SCPI commands.");
                this.turnGrayscaleCommands = new string[] { "", "", "", "" };
                this.invertCommands = new string[] { "", "", "", "" };
                this.getDataCommand = "";
                this.Live = false;
                return false;
            }


        }

        //SCPI helper function to deal with escape chars
        private string ReplaceCommonEscapeSequences(string s)
        {
            return s.Replace("\\n", "\n").Replace("\\r", "\r");
        }
        //SCPI helper function to deal with escape chars
        private string InsertCommonEscapeSequences(string s)
        {
            return s.Replace("\n", "\\n").Replace("\r", "\\r");
        }



        // excpects that it already has a lock on mbSession.
        // can be run from a background thread.
        private byte[] getJFIFImage()
        {
            //string getDataCmd = "DISP:DATA?"; // mso5000
            //string getDataCmd = "DISP:DATA? ON,ON,BMP"; //DS1054Z
            string getDataCmd = this.getDataCommand;


            mbSession.RawIO.Write(ReplaceCommonEscapeSequences("*CLS"));
            //mbSession.TimeoutMilliseconds = 10 * 1000;
            string textToWrite = ReplaceCommonEscapeSequences(getDataCmd);
            mbSession.RawIO.Write(textToWrite);
            List<byte> results = new List<byte>();
            byte[] packet = new byte[8192];
            ReadStatus rS;
            do
            {
                packet = mbSession.RawIO.Read(8192, out rS);
                results.AddRange(packet);
            } while (rS != ReadStatus.EndReceived);
            return results.ToArray();
        }

        // retrieve an image from the instrument. can be run from a background thread.
        // excpects that it already has a lock on mbSession.
        private byte[] getImage()
        {
            //string getDataCmd = "DISP:DATA?"; // mso5000
            //string getDataCmd = "DISP:DATA? ON,ON,BMP"; //DS1054Z
            string getDataCmd = this.getDataCommand;


            mbSession.RawIO.Write(ReplaceCommonEscapeSequences("*CLS"));
            //mbSession.TimeoutMilliseconds = 10 * 1000;
            string textToWrite = ReplaceCommonEscapeSequences(getDataCmd);
            mbSession.RawIO.Write(textToWrite);
            byte[] tmcSize = mbSession.RawIO.Read(2);
            if (tmcSize[0] != '#')
            {
                throw new ArgumentException("Error while grabbing screen: Bad tmcSize");
            }
            int tmcHeaderSize = tmcSize[1] - '0';
            byte[] packetSizeString = mbSession.RawIO.Read(tmcHeaderSize);
            
            int packetSize;
            if (!int.TryParse(System.Text.Encoding.ASCII.GetString(packetSizeString), out packetSize))
            {
                throw new ArgumentException("Error while parsing packet size: {0}", System.Text.Encoding.ASCII.GetString(packetSizeString));
            }
            //int packetSize = int.Parse(System.Text.Encoding.ASCII.GetString(packetSizeString));
            byte[] packet = new byte[packetSize];

            long blocksize = (long)Math.Pow(2, 17);
            long totalRead = 0;
            long currentRead = 0;
            Ivi.Visa.ReadStatus stat;
            while (totalRead < packetSize)
            {
                if (blocksize < (packetSize - totalRead))
                    mbSession.RawIO.Read(packet, totalRead, blocksize, out currentRead, out stat);
                else
                    mbSession.RawIO.Read(packet, totalRead, packetSize - totalRead, out currentRead, out stat);
                totalRead += currentRead;
            }
            return packet;
        }


        // 2d XY scaling of coordinates.
        private void convertCoordinates(int ClickX, int ClickY, out int imageX, out int imageY)
        {
            imageX = -1;
            imageY = -1;
            if (pictureBox1.Image == null) return;

            Size boxSize = pictureBox1.Size;
            Size picSize;
            try
            {
                if (LastPicSize.Height !=0)
                {
                    picSize = LastPicSize;
                }
                else
                {
                    picSize = pictureBox1.Image.Size;
                    LastPicSize = picSize;
                }
            }
            catch ( ArgumentException ex)
            {
                return;
            }


            double boxRatio = (double)boxSize.Height / boxSize.Width;
            double picRatio = (double)picSize.Height / picSize.Width;



            double realWidth, realHeight;
            // if H/W of box > H/W of the Pic there are vertical black bars
            if (boxRatio <= picRatio)
            {
                //calulate a width offest to picture x==0
                realWidth = boxSize.Height / picRatio;
                realHeight = boxSize.Height;
            }
            else
            {
                // if H/W of box < H/W of the Pic there aer Horizontal bars
                realWidth = boxSize.Width;
                realHeight = boxSize.Width * picRatio;
            }

            // check for out of bounds
            double deadWidth = (boxSize.Width - realWidth) / 2.0;
            double deadHeight = (boxSize.Height - realHeight) / 2.0;
            if ((ClickX < deadWidth) || (ClickX > boxSize.Width - deadWidth) ||
                (ClickY < deadHeight) || (ClickY > boxSize.Height - deadHeight))
            {
                //readTextBox.Text = "out of bounds click";
                return;
            }
            else
            {
                //readTextBox.Text = "in bounds";
            }
            ClickX -= (int)deadWidth;
            ClickY -= (int)deadHeight;

            imageX = (int)((double)ClickX / realWidth * picSize.Width);
            imageY = (int)((double)ClickY / realHeight * picSize.Height);
        }

        //save the current image to a file
        private void SaveScreenShot()
        {
            // save the memory stream that is showing before you use the save dialog box.
            // this way, if live mode is enabled you save the image that existed when you hit save.
            byte[] tmpPacket = new byte[lastPacket.Length];
            Array.Copy(lastPacket, tmpPacket,lastPacket.Length);
            
            // ask where to save
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Bitmap (*.bmp)|*.bmp|All Files (*.*)|*.*";
            DialogResult d = sf.ShowDialog();

            if (d != DialogResult.OK) return;
            
            // do the save.
            string filename = sf.FileName;
            MemoryStream ms = new MemoryStream();
            ms.Write(tmpPacket, 0, Convert.ToInt32(tmpPacket.Length));
            Bitmap tmp = new Bitmap(ms, false);
            tmp.Save(filename, ImageFormat.Bmp);
            
            // cleanup
            tmp.Dispose();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.C && e.Modifiers == Keys.Control)
            {
                if (isTextSelected()) return; 
                System.Windows.Forms.Clipboard.SetImage(model);
                e.Handled = true;
            }
        }

        private bool isTextSelected()
        {
            if (txtSCPIcmd.SelectionLength != 0) return true;
            if (txtSCPIresponse.SelectionLength != 0) return true;
            if (textBox1.SelectionLength != 0) return true;
            return false;
        }

        private void txtSCPIresponse_Leave(object sender, EventArgs e)
        {
            txtSCPIresponse.SelectionLength = 0;
        }

        

        private bool validateUpdateTime(string input,out int val)
        {
            int tmp;
            bool valid = int.TryParse(input, out tmp);
            val = tmp;
            this.ScreenUpdateRate = val;
            if (valid)
            {
                toolStripStatusUpdateSpeed.Text = string.Format("Update (mS): {0}", ScreenUpdateRate);
            }

            return valid;
        }

        private void txtUpdateSpeed_Validating(object sender, CancelEventArgs e)
        {
            System.Console.WriteLine("Validating");
            int tmp;
            e.Cancel = !validateUpdateTime(txtUpdateSpeed.Text, out tmp);
            
                
        }

        private void txtUpdateSpeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtUpdateSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkBox1.Focus();
                //this.ActiveControl = null;
            }
        }

        private void txtUpdateSpeed_Leave(object sender, EventArgs e)
        {
            System.Console.WriteLine("Leaving");
        }
    }
}

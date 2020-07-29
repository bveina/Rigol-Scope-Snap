using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.Visa;


namespace ScopeSnapSharp
{
    public partial class Form1 : Form
    {
        private MessageBasedSession mbSession;
        private Timer t;
        //private MemoryStream ms;
        private byte[] lastPacket;
        private Bitmap model;
        private bool Connected;
        private int FailCount;
        private bool _live;
        
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
        private string[] buttonList = {"MenuOff","Measure","Aquire","Storage","Cursor","Display",
            "Utility","LA","REF","MATH","DECODE","TRIGGER", "G1","G2","SEARCH","ZOOM","QUICK","AUTO",
            "DEFAULT","CLEAR","BACK"};
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }

        public Form1()
        {
            InitializeComponent();
            t = new Timer();
            t.Interval = 1000;
            t.Tick += T_Tick;
            SetupControlState(false);
            FailCount = 0;
            textBox2.Visible = false;
            button1.Visible = false;

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
                    b.Click += quickButton_click;
                    b.Dock = DockStyle.Fill;
                    b.Tag = pair.Value;
                    tblLayoutButtons.RowStyles.Add(new RowStyle(SizeType.Percent,100/QuickList.Count));
                    tblLayoutButtons.Controls.Add(b,0,i);
                    i++;
                }
                //Panel fake = new Panel();
                //fake.Dock = DockStyle.Fill;
                //tblLayoutButtons.Controls.Add(fake, 0, QuickList.Count);

                tblLayoutButtons.ResumeLayout();
                tblLayoutButtons.Width = 80;
                splitContainer2.Panel1Collapsed = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void quickButton_click(object sender, EventArgs e)
        {
            mbSession.RawIO.Write(String.Format(":SYST:KEY:PRESS {0}", (string)(sender as Button).Tag));
        }

        private void T_Tick(object sender, EventArgs e)
        {
            
            t.Enabled = false;
            if (this.Live)
            {
                try
                {
                    updateScreen(pictureBox1);
                    FailCount = 0;
                }
                catch ( Exception ex)
                {
                    FailCount++;
                    if (FailCount > 5)
                    {
                        MessageBox.Show(ex.Message);
                        SetupControlState(false);
                        mbSession.Dispose();
                    }
                }
            }
            t.Enabled = true;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchConnections();
        }

        private void SearchConnections()
        {
            
            // if there is a manual string provided, leave it in the textbox.
            // otherwise erase it.
            if (listBox1.Items.Contains( textBox1.Text))
            {
                textBox1.Text = "";
            }
            listBox1.Items.Clear();

            textBox1.ReadOnly = true;
            using (var rmSession = new ResourceManager())
            {
                //var resources = rmSession.Find("(ASRL|GPIB|TCPIP|USB)?*");
                try
                {
                    var resources = rmSession.Find("(GPIB|TCPIP|USB)?*");

                    foreach (string s in resources)
                    {
                        listBox1.Items.Add(s);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Could not find Any Instruments!");
                    return;
                    
                }
            }
            if (listBox1.Items.Count == 1)
            {
                listBox1.SelectedIndex = 0;
                connect((string)listBox1.SelectedItem);
            }
            if (listBox1.Items.Count != 0)
            {
                listBox1.SelectedIndex = 0;
            }
            else
            {
                // this will probably never happen since it should be caught by the above try block.
                // TODO: run code coverege and see if this can be refactored
                MessageBox.Show("could not find any lab equipment, check your connections?","Error Connecting");
            }
        }

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

            //checkBox1.Checked = false;// yes, anytime we connect or disconnect we turn off live mode.
            this.Live = false;
            btnGrab.Enabled = state;
            newConnectionToolStripMenuItem.Enabled = !state;



        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = (string) (sender as ListBox).SelectedItem;
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            TextBox tb = (sender as TextBox);
            tb.ReadOnly = false;
        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            SetupControlState(false);
            mbSession.Dispose();
        }

        private void connect(string res)
        {
            if (res == "")
            {
                MessageBox.Show("Choose search, then select an item from the list", "No Resouce Selected");
                return;
            }

            using (var rmSession = new ResourceManager())
            {
                try
                {
                    mbSession = (MessageBasedSession)rmSession.Open(res);
                    SetupControlState(true);
                    mbSession.RawIO.Write(":SAVE:IMAGE:INVERT ON");
                    updateScreen(pictureBox1);
                    
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Resouce selected must be a message-based session","Connect Error");
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, "Connect Error");
                    //TODO: log this error
                }
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

        private string ReplaceCommonEscapeSequences(string s)
        {
            return s.Replace("\\n", "\n").Replace("\\r", "\r");
        }

        private string InsertCommonEscapeSequences(string s)
        {
            return s.Replace("\n", "\\n").Replace("\r", "\\r");
        }

        private byte[] getImage()
        {
            //string getDataCmd = "DISP:DATA?";
            string getDataCmd = "DISP:DATA? ON,ON,PNG";
            //string getDataCmd = "DISP:PULL? PNG";

            try
            {
                mbSession.RawIO.Write(ReplaceCommonEscapeSequences("*CLS"));
                //mbSession.TimeoutMilliseconds = 10 * 1000;
                string textToWrite = ReplaceCommonEscapeSequences(getDataCmd);
                mbSession.RawIO.Write(textToWrite);
                byte[] tmcSize = mbSession.RawIO.Read(2);
                if (tmcSize[0] != '#')
                {
                    MessageBox.Show("error with tmcSize","Error on screen grab");
                    return null;
                }
                int tmcHeaderSize = tmcSize[1] - '0';
                byte[] packetSizeString = mbSession.RawIO.Read(tmcHeaderSize);
                int packetSize = int.Parse(System.Text.Encoding.ASCII.GetString(packetSizeString));
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
            catch (Ivi.Visa.IOTimeoutException)
            {
                MessageBox.Show("Scope Stopped Responding, is it on and plugged in?", "TMC packet Error");
                SetupControlState(false);
                mbSession.Dispose();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message,"TMC packet Error");
                SetupControlState(false);
                mbSession.Dispose();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return null;
        }

        private void updateScreen(PictureBox pb)
        {
            
            if (mbSession==null || mbSession.IsDisposed) return;

            // update save options
            mbSession.RawIO.Write(":SAVE:IMAGE:INVERT?");
            string s = mbSession.FormattedIO.ReadLine();
            if (s.Trim() == "1")
                invertToolStripMenuItem.Checked = true;

            mbSession.RawIO.Write(":SAVE:IMAGE:COLOR?");
            s = mbSession.FormattedIO.ReadLine();
            if (s.Trim() == "GRAY")
                grayscaleToolStripMenuItem.Checked = true;


            lastPacket = getImage();
            if (lastPacket == null) return; // something broke. fail silently.
            MemoryStream ms = new MemoryStream();
            ms.Write(lastPacket, 0, Convert.ToInt32(lastPacket.Length));
            if (model != null)
            {
                model.Dispose();
            }
            model = new Bitmap(ms, false);
            if (pb.Image != null)
            {
                pb.Image.Dispose();
            }
            pb.Image = model;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!Connected)
            {
                MessageBox.Show("You are not connected to any scope! (did you search AND connect?)", "Error Sending Touch Command");
                return;
            }
            MouseEventArgs ee = e as MouseEventArgs;

            if (ee.Button== MouseButtons.Right)
            {
                updateScreen(pictureBox1);
                return;
            }
            int myX, myY;
            convertCoordinates(ee.X, ee.Y, out myX, out myY);
            if (myX < 0 || myY < 0) return;
            string txt2Send = string.Format(":SYSTem:TOUCh {0}, {1}", myX, myY);
            try
            {
                mbSession.RawIO.Write(txt2Send);
            }
            catch (Ivi.Visa.IOTimeoutException)
            {
                MessageBox.Show("Scope Lost Contact","Error Sending Touch Request");
                SetupControlState(false);
                mbSession.Dispose();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error Sending Touch Request");
                SetupControlState(false);
                mbSession.Dispose();
            }
            
        }

        private void convertCoordinates(int ClickX, int ClickY, out int imageX, out int imageY)
        {
            imageX = -1;
            imageY = -1;
            if (pictureBox1.Image == null) return;

            Size boxSize = pictureBox1.Size;
            Size picSize = pictureBox1.Image.Size;

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.Live = (sender as CheckBox).Checked;
            //liveToolStripMenuItem.Checked = (sender as CheckBox).Checked;
            //t.Enabled = (sender as CheckBox).Checked;

            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveScreenShot();
        }

        private void SaveScreenShot()
        {
            // save the memory stream that is showing before you use the save dialog box.

            byte[] tmpPacket = new byte[lastPacket.Length];
            Array.Copy(lastPacket, tmpPacket,lastPacket.Length);
          
            
            // ask where to save
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Bitmap (*.bmp)|*.bmp|All Files (*.*)|*.*";
            DialogResult d = sf.ShowDialog();

            if (d != DialogResult.OK)
            {
                return;
            }
            string filename = sf.FileName;


            // do the save.
            MemoryStream ms = new MemoryStream();
            ms.Write(tmpPacket, 0, Convert.ToInt32(tmpPacket.Length));
            Bitmap tmp = new Bitmap(ms, false);
            tmp.Save(filename, ImageFormat.Bmp);
            
            // cleanup
            tmp.Dispose();
            //msTemp.Dispose();

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
            //SaveScreenShot();
            System.Windows.Forms.Clipboard.SetImage(model);
        }

        private void newConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchConnections();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                Version currentVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;

                string AppString = "Scope Snap Sharp";
                string versionString = currentVersion.ToString(4);
                string credits = "logo: \"Oscilloscope\" by Filter Forge is licensed under CC BY 2.0";
                MessageBox.Show(String.Format("{0}\r\n(c) Ben Viall\r\nVersion: {1}\r\n\r\n",AppString, versionString,credits),"About");
            }
            else
            {
                MessageBox.Show("Debug Version","About");

            }
            
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (invertToolStripMenuItem.Checked)
            {
                mbSession.RawIO.Write(":SAVE:IMAGE:INVERT ON");
            }
            else
            {
                mbSession.RawIO.Write(":SAVE:IMAGE:INVERT OFF");
            }
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grayscaleToolStripMenuItem.Checked)
            {
                mbSession.RawIO.Write(":SAVE:IMAGE:COLOR GRAY");
            }
            else
            {
                mbSession.RawIO.Write(":SAVE:IMAGE:COLOR COLOR");
            }
        }

        private void showButtonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer2.SplitterDistance = 100;
            splitContainer2.Panel1Collapsed = !showButtonsToolStripMenuItem.Checked;
        }

        private void pictureOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            mbSession.RawIO.Write(textBox2.Text);
        }

        

        private void pictureBox1_MouseHover(object sender, MouseEventArgs e)
        {
            int myX, myY;
            convertCoordinates(e.X, e.Y, out myX, out myY);
            toolStripStatusLabel1.Text = String.Format("Position:({0}, {1})", myX,myY);
        }
    }
}

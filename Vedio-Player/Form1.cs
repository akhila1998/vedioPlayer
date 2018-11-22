using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using QuartzTypeLib;

namespace vedioPlayer
{
    
    public partial class Form1 : Form
    {
             public int maxvalue;
        private const int WS_CHILD = 0x40000000;
        private const int WS_CLIPCHILDREN = 0x2000000;
        public Boolean check = true;
        functions FunObj = new functions();

        public Form1()
        {
            InitializeComponent();
            axWindowsMediaPlayer1.PositionChange += test;
        }
        private void test(object sender,_WMPOCXEvents_PositionChangeEvent e)

        {
           // if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
             //  axWindowsMediaPlayer1.Ctlcontrols.currentPosition = trackBar1.Value;
            //UpdateStatusBarTime();


        }
        private void handlepos(object sender,EventArgs e) {
        }
        private void Form1_Load(object sender, EventArgs e)
        {


        }
        public void play_Local(String filename)
        {
            FunObj.CleanUp();
            FunObj.m_objFilterGraph = new FilgraphManager();

            if (filename != null)
            {
                FunObj.m_objFilterGraph.RenderFile(filename);
            }
            FunObj.m_objBasicAudio = FunObj.m_objFilterGraph as IBasicAudio;
            try
            {
                /**
                 * For Video files
                 * */
                FunObj.m_objVideoWindow = FunObj.m_objFilterGraph as IVideoWindow;
                //FunObj.m_objVideoWindow = (int)axWindowsMediaPlayer1.Handle;
               // FunObj.m_objVideoWindow.WindowStyle = WS_CHILD | WS_CLIPCHILDREN;

            }
            catch (Exception)
            {
                FunObj.m_objVideoWindow = null;
            }

            FunObj.m_objMediaEvent = FunObj.m_objFilterGraph as IMediaEvent;
            FunObj.m_objMediaEventEx = FunObj.m_objFilterGraph as IMediaEventEx;
            FunObj.m_objMediaPosition = FunObj.m_objFilterGraph as IMediaPosition;
            FunObj.m_objMediaControl = FunObj.m_objFilterGraph as IMediaControl;
            FunObj.m_objMediaTypeInfo = FunObj.m_objFilterGraph as IMediaTypeInfo;
            FunObj.m_objMediaControl.Run();
            trackBar1.Maximum = (int)FunObj.m_objMediaPosition.Duration;
            maxvalue= (int)FunObj.m_objMediaPosition.Duration;
            MessageBox.Show(Convert.ToString(maxvalue));
            trackBar1.Minimum = 0;

            //trackBar1.TickFrequency = (int)FunObj.m_objMediaPosition.Duration;
           // trackBar1.LargeChange = 1;  //on clicking left side of scroll
            //trackBar1.SmallChange = 1; // scrolling through Keyboard

        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "(mp3,wav,mp4,mov,wmv,mpg)|*.mp3;*.wav;*.mp4;*.mov;*.wmv;*.mpgn|all files|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //      openFileDialog1.Filter = "(mp3,wav,mp4,mov,wmv,mpg)|*.mp3;*.wav;*.mp4;*.mov;*.wmv;*.mpgn|all files|*.*";
                //   if (openFileDialog1.ShowDialog() == DialogResult.OK)
                axWindowsMediaPlayer1.URL = openFileDialog1.FileName;
                UpdateStatusBarTime();

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            trackBar1.Value += 1;
            if (trackBar1.Value == (int)FunObj.m_objMediaPosition.Duration)
            {
                trackBar1.Value = trackBar1.Minimum;
                timer1.Stop();
                trackBar1.Value += 0;
            }
           // if (FunObj.m_objVideoWindow != null)
            //{
              //  trackBar1.Value = (int)FunObj.m_objMediaPosition.CurrentPosition;
            //}
            UpdateStatusBarTime();
        }
        public void UpdateStatusBarTime()
        {

            if (axWindowsMediaPlayer1.Ctlcontrols.currentPosition >0)
            {
                int s = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                int h = s / 3600;
                int m = (s - (h * 3600)) / 60;
                s = s - (h * 3600 + m * 60);

                string duration = String.Format("{0:D2}:{1:D2}:{2:D2}", h, m, s);

                s = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                h = s / 3600;
                m = (s - (h * 3600)) / 60;
                s = s - (h * 3600 + m * 60);

                string curr = String.Format("{0:D2}:{1:D2}:{2:D2}", h, m, s);
                statusBarPanel1.Text = curr;
                statusBarPanel2.Text = duration;


            }
        }
      

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        /*  private void button2_Click(object sender, EventArgs e)
          {
              MessageBox.Show("A basic Media Player version 1.0 ");
          }

          private void help_Click(object sender, EventArgs e)
          {
              MessageBox.Show("please help yourself\nor use www.google.com");
          } */

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Basic Video Player");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Use www.google.com");
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "(mp3,wav,mp4,mov,wmv,mpg)|*.mp3;*.wav;*.mp4;*.mov;*.wmv;*.mpgn|all files|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //      openFileDialog1.Filter = "(mp3,wav,mp4,mov,wmv,mpg)|*.mp3;*.wav;*.mp4;*.mov;*.wmv;*.mpgn|all files|*.*";
                //   if (openFileDialog1.ShowDialog() == DialogResult.OK)
               // play_Local(Convert.ToString(openFileDialog1.FileName));

                axWindowsMediaPlayer1.URL = openFileDialog1.FileName;
                timer1.Start();
            }
            statusBarPanel3.Text = "Playing";
            UpdateStatusBarTime();

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "(srt)|*.srtn|all files|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                axWindowsMediaPlayer1.URL = openFileDialog1.FileName;
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

         
        private void Btn_Play_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
            statusBarPanel3.Text = "Playing";       

        }

        private void Btn_Previous_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.previous();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            statusBarPanel3.Text = "Paused";


        }

        private void Btn_Next_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.next();

        }

        private void Btn_FastBackward_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.fastReverse();
            statusBarPanel3.Text = "Reversing";


        }

        private void Btn_Stop_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            statusBarPanel3.Text = "Stopped";


        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.fastForward();
            statusBarPanel3.Text = "Fastforwarding";


        }

      

       
       
      
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
            //FunObj.m_objMediaPosition.CurrentPosition = trackBar1.Value;
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = trackBar1.Value;
            UpdateStatusBarTime();
        }

        private void axWindowsMediaPlayer1_Enter_1(object sender, EventArgs e)
        {

        }

        private void Btn_Pause_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Pause_Click_1(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            statusBarPanel3.Text = "Paused";
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

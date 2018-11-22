using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using QuartzTypeLib;
namespace vedioPlayer

{
    class functions
    {
        
        private const int WM_APP = 0x8000;
        private const int WM_GRAPHNOTIFY = WM_APP + 1;
        private const int EC_COMPLETE = 0x01;
        private const int WS_CHILD = 0x40000000;
        private const int WS_CLIPCHILDREN = 0x2000000;


        public FilgraphManager m_objFilterGraph = null;
        public IBasicAudio m_objBasicAudio = null;
        public IVideoWindow m_objVideoWindow = null;
        public IMediaEvent m_objMediaEvent = null;
        public IMediaEventEx m_objMediaEventEx = null;
        public IMediaPosition m_objMediaPosition = null;
        public IMediaControl m_objMediaControl = null;
        public IMediaTypeInfo m_objMediaTypeInfo = null;
        public System.Windows.Forms.MenuItem menuItem5;
        



        String DefaultDir = Convert.ToString(System.Environment.SpecialFolder.MyMusic) + @"\";




        /**
         * Customized OpenDialogBox for opening files.
         * Files including Audio,video,playlist.
         * We have to check if file is playlist file then call C_ReadPlyaListFile()
         * It will return an array of file names ...
         **/
        public string[] C_OpenFileDialogBox(String Title, String InitialDirectory, String Filter, String AutoUpgradeEnabled, String MultiSelect, String FileExsist)
        {
            OpenFileDialog FileDialog = new OpenFileDialog();
            FileDialog.Title = Title;
            FileDialog.InitialDirectory = InitialDirectory;
            FileDialog.Filter = Filter;
            FileDialog.AutoUpgradeEnabled = Convert.ToBoolean(AutoUpgradeEnabled);
            FileDialog.Multiselect = Convert.ToBoolean(MultiSelect);
            FileDialog.CheckFileExists = Convert.ToBoolean(FileExsist);

            if (FileDialog.ShowDialog() == DialogResult.OK)
            {

                return FileDialog.SafeFileNames;

            }
            return null;
        }

        /**
           * Let's first create directory in My music folder
           * Name of directory is GRS-Playlist
           * If user does not have permission to create folder then all play list files will be saved to Mymusic folder only
           * */
        public void C_WritePlayListFile(string Name, List<string> list)
        {

            string FolderPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            try
            {
                if (Directory.Exists(DefaultDir + "GRS-Playlist") == false)
                {
                    Directory.CreateDirectory(FolderPath + @"\GRS-Playlist");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error in creating folder");
            }

            string GRSPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + @"\GRS-Playlist\" + Name;
            FileStream PlaylistFile = new FileStream(GRSPath, FileMode.Append, FileAccess.Write);
            BinaryWriter BWriter = new BinaryWriter(PlaylistFile);
            foreach (string value in list)
            {
                BWriter.Write(value); BWriter.Write("\n");
            }
        }





        /**
           * Code to pause Media file
           * 
           * 
           * */
        public void pause()
        {
            m_objMediaControl.Pause();
        }

        /**
           * Code to stop Media file
           * 
           * 
           * */
        public void stop()
        {
            m_objMediaControl.Stop();

        }



        /**
         * To clean up the FilterGraph and other Objects
         * */
        public void CleanUp()
        {
            if (m_objMediaControl != null)
                m_objMediaControl.Stop();

            if (m_objMediaEventEx != null)
                m_objMediaEventEx.SetNotifyWindow(0, 0, 0);

            if (m_objVideoWindow != null)
            {
                m_objVideoWindow.Visible = 0;
                m_objVideoWindow.Owner = 0;
            }

            if (m_objMediaControl != null) m_objMediaControl = null;
            if (m_objMediaPosition != null) m_objMediaPosition = null;
            if (m_objMediaEventEx != null) m_objMediaEventEx = null;
            if (m_objMediaEvent != null) m_objMediaEvent = null;
            if (m_objVideoWindow != null) m_objVideoWindow = null;
            if (m_objBasicAudio != null) m_objBasicAudio = null;
            if (m_objFilterGraph != null) m_objFilterGraph = null;
        }

    }
}

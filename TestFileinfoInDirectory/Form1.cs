using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace TestFileinfoInDirectory
{
    public partial class Form1 : Form
    {
        delegate void mydele(string path);
        delegate void mydele2(string path);
        event mydele testeventhandler; // listbox1
        event mydele2 testeventhandler2; // listbox2
        int milliseconds = 30; // Directory read시 프로세서 충돌 제거를 위한 delay
        int AutoScanTimer = 0;
        FileSystemWatcher watcherSourceDir = new FileSystemWatcher();
        FileSystemWatcher watcherDestinationDir = new FileSystemWatcher();

        FileSystemWatcher justWatcherSourceDir = new FileSystemWatcher();
        FileSystemWatcher justWatcherDestinationDir = new FileSystemWatcher();


        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        string sourceDirPath;
        string desDirPath;
        bool isFirst = true;
        bool isJustFirst = true;


        public Form1()
        {
            InitializeComponent();
            JustWatcherSourceDir();
            JustWatcherDestinationDir();
        }

        private void JustWatcherSourceDir()
        {
            //sourceDirPath = textBox1.Text;
            sourceDirPath = @"D:\sourceDir";
            //desDirPath = textBox2.Text;
            desDirPath = @"D:\destDir";

            if (sourceDirPath == "" || desDirPath == "")
            {
                MessageBox.Show("경로를 지정하세요.");
                return;
            }
            justWatcherSourceDir.IncludeSubdirectories = true;

            justWatcherSourceDir.Path = sourceDirPath;
            justWatcherSourceDir.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.LastAccess | NotifyFilters.Size;
            justWatcherSourceDir.Filter = "";
            justWatcherSourceDir.Created += new FileSystemEventHandler(justChangedSource);
            justWatcherSourceDir.Deleted += new FileSystemEventHandler(justChangedSource);
            justWatcherSourceDir.Changed += new FileSystemEventHandler(justChangedSource);
            justWatcherSourceDir.Renamed += new RenamedEventHandler(justRenamedSource);
            justWatcherSourceDir.EnableRaisingEvents = true;

            testeventhandler += new mydele(Form1_testeventhandler);
        }
        private void JustWatcherDestinationDir()
        {
            //sourceDirPath = textBox1.Text;
            sourceDirPath = @"D:\sourceDir";
            //desDirPath = textBox2.Text;
            desDirPath = @"D:\destDir";

            if (sourceDirPath == "" || desDirPath == "")
            {
                MessageBox.Show("경로를 지정하세요.");
                return;
            }
            justWatcherDestinationDir.IncludeSubdirectories = true;

            justWatcherDestinationDir.Path = desDirPath;
            justWatcherDestinationDir.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName /*| NotifyFilters.Attributes | NotifyFilters.CreationTime /*| NotifyFilters.LastAccess */ | NotifyFilters.Size;
            justWatcherDestinationDir.Filter = "";
            justWatcherDestinationDir.Created += new FileSystemEventHandler(justChangedDestination);
            justWatcherDestinationDir.Deleted += new FileSystemEventHandler(justChangedDestination);
            justWatcherDestinationDir.Changed += new FileSystemEventHandler(justChangedDestination);
            justWatcherDestinationDir.Renamed += new RenamedEventHandler(justRenamedDestination);
            justWatcherDestinationDir.EnableRaisingEvents = true;

            testeventhandler2 += new mydele2(Form1_testeventhandler2);
        }

        /// <summary>
        /// Source Directory 가 바뀌었을 때, 파일 복사x, 단지 로그 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void justChangedSource(object sender, FileSystemEventArgs e)
        {
            string msg = string.Format(e.FullPath + " " + e.ChangeType);
            MakeMessage(e.FullPath, msg, "sourceDir");
        }
        void justRenamedSource(object sender, RenamedEventArgs e)
        {
            string msg = string.Format("{0} renamed to {1}", e.OldFullPath, e.FullPath);
            MakeMessage(e.FullPath, msg, "sourceDir");
        }

        /// <summary>
        /// Destination Directoty 가 변경 되었을 때, 파일 복사x 단지 로그 출력
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void justChangedDestination(object sender, FileSystemEventArgs e)
        {
            string msg = string.Format(e.FullPath + " " + e.ChangeType);
            MakeMessage(e.FullPath, msg, "destinationDir");
        }
        void justRenamedDestination(object sender, RenamedEventArgs e)
        {
            string msg = string.Format("{0} renamed to {1}", e.OldFullPath, e.FullPath);
            MakeMessage(e.FullPath, msg, "destinationDir");
        }

        /// <summary>
        /// Destination Directory 감시 코드 이벤트 핸들러
        /// </summary>
        private void WatcherDestinationDir()
        {
            watcherDestinationDir.IncludeSubdirectories = true;
            
            watcherDestinationDir.Path = desDirPath;
            watcherDestinationDir.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName /*| NotifyFilters.Attributes | NotifyFilters.CreationTime /*| NotifyFilters.LastAccess */ | NotifyFilters.Size;
            watcherDestinationDir.Filter = "";
            watcherDestinationDir.Created += new FileSystemEventHandler(Changed2);
            watcherDestinationDir.Deleted += new FileSystemEventHandler(Changed2);
            watcherDestinationDir.Changed += new FileSystemEventHandler(Changed2);
            watcherDestinationDir.Renamed += new RenamedEventHandler(Renamed2);
            watcherDestinationDir.EnableRaisingEvents = true;

            //testeventhandler2 += new mydele2(Form1_testeventhandler2);
        }

        /// <summary>
        /// Source Directory 감시 코드 이벤트 핸들러
        /// </summary>
        private void WatcherSourceDir()
        {
            watcherSourceDir.IncludeSubdirectories = true;
            //watcher.Path = @"D:\sourceDir";

            watcherSourceDir.Path = sourceDirPath;
            watcherSourceDir.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.LastAccess | NotifyFilters.Size;
            watcherSourceDir.Filter = "";
            watcherSourceDir.Created += new FileSystemEventHandler(Changed);
            watcherSourceDir.Deleted += new FileSystemEventHandler(Changed);
            watcherSourceDir.Changed += new FileSystemEventHandler(Changed);
            watcherSourceDir.Renamed += new RenamedEventHandler(Renamed);
            watcherSourceDir.EnableRaisingEvents = true;

            //testeventhandler += new mydele(Form1_testeventhandler);
        }

        /// <summary>
        /// Source Directory 이벤트 핸들러
        /// </summary>
        void Changed(object sender, FileSystemEventArgs e)
        {
            //string msg = string.Format(e.FullPath + " " + e.ChangeType);
            //MakeMessage(e.FullPath, msg, "sourceDir");
            Thread.Sleep(milliseconds);
            DirectoryCopy(@sourceDirPath, @desDirPath, true);
        }
        void Renamed(object sender, RenamedEventArgs e)
        {
            //string msg = string.Format("{0} renamed to {1}", e.OldFullPath, e.FullPath);
            //MakeMessage(e.FullPath, msg, "sourceDir");
            Thread.Sleep(milliseconds);
            DirectoryCopy(@sourceDirPath, @desDirPath, true);
        }
        /// <summary>
        /// Destination Directory 이벤트 핸들러
        /// </summary>
        void Changed2(object sender, FileSystemEventArgs e)
        {
            //string msg = string.Format(e.FullPath + " " + e.ChangeType);
            //MakeMessage(e.FullPath, msg, "destinationDir");
        }
        void Renamed2(object sender, RenamedEventArgs e)
        {
            //string msg = string.Format("{0} renamed to {1}", e.OldFullPath, e.FullPath);
            //MakeMessage(e.FullPath, msg, "destinationDir");
        }

        /// <summary>
        /// Source Directory Log 출력
        /// </summary>
        void Form1_testeventhandler(string path)
        {
            SourceDirLogListBox.Items.Add(path);
        }
        /// <summary>
        /// Destination Directoy Log 출력
        /// </summary>
        void Form1_testeventhandler2(string path)
        {
            DestinationDirLogListBox.Items.Add(path);
        }
        
        /// <summary>
        /// Log Message 출력
        /// </summary>
        private void MakeMessage(string FullPath, string msg, string WhatDir)
        {
            string path = string.Format("{0}//{1}", Application.StartupPath, FullPath);
            string extension = Path.GetExtension(path);

            if (extension == ".tmp" || extension == ".TMP")
            {

            }
            else
            {
                switch (WhatDir)
                {
                    case "sourceDir":
                        SourceDirLogListBox.BeginInvoke(testeventhandler, new object[] { msg });
                        break;
                    case "destinationDir":
                        DestinationDirLogListBox.BeginInvoke(testeventhandler2, new object[] { msg });
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Directory Copy Code
        /// </summary>
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // 지정된 디렉토리의 서브 디렉토리를 가져옵니다.
            DirectoryInfo dirSource = new DirectoryInfo(sourceDirName);
            DirectoryInfo dirDestination = new DirectoryInfo(destDirName);

            if (!dirSource.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // 대상 디렉토리가 존재하지 않으면 만들어라.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            //  디렉토리에서 파일을 가져 와서 새 위치로 복사하십시오.
            try
            {
                FileInfo[] filesSource = dirSource.GetFiles();
                FileInfo[] filesDestination = dirDestination.GetFiles();

                Console.WriteLine(filesSource.Length);
                for (int i = 0; i < filesSource.Length; ++i)
                {
                    Console.WriteLine(filesSource[i].Name);
                }

                foreach (FileInfo fileSource in filesSource)
                {
                    if (fileSource.Name[0] == '~')   // 복제 파일 복사 안함
                    {

                    }
                    if (fileSource.Extension == ".tmp" || fileSource.Extension == ".TMP") // 임시 파일은 복사 안함
                    {

                    }
                    else
                    {
                        int i = 0;
                        foreach (FileInfo fileDest in filesDestination)
                        {
                            if( fileSource.Name == fileDest.Name )
                            {
                                if(fileSource.Length != fileDest.Length ) // 이름이 같고 싸이즈가 다른 폴더 찾으면 덮어쓰기
                                {
                                    string temppath = Path.Combine(destDirName, fileSource.Name);
                                    fileSource.CopyTo(temppath, true);
                                }
                                i++; // 같은 이름 있었으면 증가
                            }
                        }
                        if (i == 0) // 같은 이름이 없었으면 하나도 없었으면 파일 복사.
                        {
                            string temppath = Path.Combine(destDirName, fileSource.Name);
                            fileSource.CopyTo(temppath, true);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }

            // 서브 디렉토리를 복사하는 경우 서브 디렉토리를 복사하고 그 내용을 새 위치로 복사하십시오.
            try
            {
                DirectoryInfo[] dirs = dirSource.GetDirectories();
                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string temppath = Path.Combine(destDirName, subdir.Name);
                        DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        /// <summary>
        /// Soure Directoy Browsing 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceDirBrowsingBtn_Click(object sender, EventArgs e) // 공유 폴더 경로
        {
            FolderBrowserDialog folderBrowse = new FolderBrowserDialog();

            if (folderBrowse.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowse.SelectedPath;
            }
        }
        /// <summary>
        /// Destination Directory Browsing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DestinationDirBrowsingBtn_Click(object sender, EventArgs e) // 로컬 저장소 경로
        {
            FolderBrowserDialog folderBrowse = new FolderBrowserDialog();

            if (folderBrowse.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowse.SelectedPath;
            }
        }
        /// <summary>
        /// Copy Button 클릭시 이벤트 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyBtn_Click(object sender, EventArgs e) // 동기 시작 버튼
        {
            //sourceDirPath = textBox1.Text;
            sourceDirPath = @"D:\sourceDir";
            //desDirPath = textBox2.Text;
            desDirPath = @"D:\destDir";

            AutoScanBtn.Enabled = false;
            AutoStopBtn.Enabled = false;

            try
            {
                if (sourceDirPath == "" || desDirPath == "")
                {
                    MessageBox.Show("경로를 지정하세요.");
                    return;
                }
                RealTimeCopyButton.Enabled = false;
                RealTimeCopyButton.Text = "동기화 가동";

                if (isFirst)
                {
                    WatcherSourceDir(); // 감시 시작
                    WatcherDestinationDir();
                    isFirst = false;
                }
                else
                {
                    watcherSourceDir.EnableRaisingEvents = true;
                    watcherDestinationDir.EnableRaisingEvents = true;
                    DirectoryCopy(sourceDirPath, desDirPath, true);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("{0}" + ex);
            }

        }
        /// <summary>
        /// Stop Button 클릭시 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopButton(object sender, EventArgs e) // 중지 버튼
        {
            AutoScanBtn.Enabled = true;
            AutoStopBtn.Enabled = true;
            watcherSourceDir.EnableRaisingEvents = false;
            watcherDestinationDir.EnableRaisingEvents = false;

            RealTimeCopyButton.Enabled = true;
        }

        /// <summary>
        /// 로그 출력 화면 클리어
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceDirLogDeletebutton_Click(object sender, EventArgs e) // 로그 지울 때
        {
            SourceDirLogListBox.Items.Clear();
        }
        /// <summary>
        /// 로그 출력 화면 클리어
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DestinationDirLogDeletebutton_Click(object sender, EventArgs e) // 로그 지울 때
        {
            DestinationDirLogListBox.Items.Clear();
        }


        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            switch(this.domainUpDown1.Text)
            {
                case "3 sec":
                    AutoScanTimer = 1000 * 3;
                    break;
                case "10 sec":
                    AutoScanTimer = 1000 * 10;
                    break;
                case "30 sec":
                    AutoScanTimer = 1000 * 30;
                    break;
                case "1 min":
                    AutoScanTimer = 1000 * 60;
                    break;
                case "10 min":
                    AutoScanTimer = 1000 * 60 * 10;
                    break;
                case "30 min":
                    AutoScanTimer = 1000 * 60 * 30;
                    break;
                case "1 hour":
                    AutoScanTimer = 1000 * 60 * 60;
                    break;
                default:
                    break;
            }
        }

        private void Form1_Load2(object sender, EventArgs e)
        {
            DomainUpDown.DomainUpDownItemCollection collection = this.domainUpDown1.Items;
            collection.Add("3 sec");
            collection.Add("10 sec");
            collection.Add("30 sec");
            collection.Add("1 min");
            collection.Add("10 min");
            collection.Add("30 min");
            collection.Add("1 hour");

            this.domainUpDown1.Text = "3 sec";
        }

        private void AutoBtn_Click(object sender, EventArgs e)
        {
            RealTimeCopyButton.Enabled = false;
            CopyStopButton.Enabled = false;

            timer.Interval = AutoScanTimer;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            AutoScanBtn.Enabled = false;
            AutoScanBtn.Text = "동기화 가동";
            domainUpDown1.Enabled = false;
        }
        void timer_Tick(object sender, EventArgs e)
        {
            //sourceDirPath = textBox1.Text;
            sourceDirPath = @"D:\sourceDir";
            //desDirPath = textBox2.Text;
            desDirPath = @"D:\destDir";

            if (sourceDirPath == "" || desDirPath == "")
            {
                MessageBox.Show("경로를 지정하세요.");
                return;
            }
            if (isFirst)
            {
                JustWatcherSourceDir(); // 감시 시작
                JustWatcherDestinationDir();
                isFirst = false;
            }
            else
            {
                justWatcherSourceDir.EnableRaisingEvents = true;
                justWatcherDestinationDir.EnableRaisingEvents = true;
                DirectoryCopy(sourceDirPath, desDirPath, true);
            }
        }

        private void AutoStopBtn_Click(object sender, EventArgs e)
        {
            //justWatcherSourceDir.EnableRaisingEvents = false;
            //justWatcherDestinationDir.EnableRaisingEvents = false;

            RealTimeCopyButton.Enabled = true;
            CopyStopButton.Enabled = true;

            timer.Stop();
            AutoScanBtn.Enabled = true;
            domainUpDown1.Enabled = true;
            AutoScanBtn.Text = "Auto Scan";
        }

        private void justOneCopy(object sender, EventArgs e)
        {
            //sourceDirPath = textBox1.Text;
            sourceDirPath = @"D:\sourceDir";
            //desDirPath = textBox2.Text;
            desDirPath = @"D:\destDir";

            if (sourceDirPath == "" || desDirPath == "")
            {
                MessageBox.Show("경로를 지정하세요.");
                return;
            }
            if (isJustFirst)
            {
                //JustWatcherSourceDir(); // 감시 시작
                //JustWatcherDestinationDir();
                isJustFirst = false;
            }
            DirectoryCopy(sourceDirPath, desDirPath, true);
        }
    }
}



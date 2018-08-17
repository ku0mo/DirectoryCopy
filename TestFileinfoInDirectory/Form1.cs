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
        static int milliseconds = 30;
        FileSystemWatcher watcherSourceDir = new FileSystemWatcher();
        FileSystemWatcher watcherDestinationDir = new FileSystemWatcher();
        string sourceDirPath;
        string desDirPath;
        bool isFirst = true;


        public Form1()
        {
            InitializeComponent();
        }

        private void InitWatcherDestinationDir()
        {
            watcherDestinationDir.IncludeSubdirectories = true;
            
            watcherDestinationDir.Path = desDirPath;
            watcherDestinationDir.NotifyFilter = NotifyFilters.FileName /*| NotifyFilters.DirectoryName /*| NotifyFilters.Attributes | NotifyFilters.CreationTime /*| NotifyFilters.LastAccess */ | NotifyFilters.Size;
            watcherDestinationDir.Filter = "";
            watcherDestinationDir.Created += new FileSystemEventHandler(Changed2);
            watcherDestinationDir.Deleted += new FileSystemEventHandler(Changed2);
            watcherDestinationDir.Changed += new FileSystemEventHandler(Changed2);
            watcherDestinationDir.Renamed += new RenamedEventHandler(Renamed2);
            watcherDestinationDir.EnableRaisingEvents = true;

            testeventhandler2 += new mydele2(Form1_testeventhandler2);
        }
        private void InitWatcherSourceDir()
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

            testeventhandler += new mydele(Form1_testeventhandler);

            DirectoryCopy(@sourceDirPath, @desDirPath, true);
        }


        //////////////////////////////////////////////////////////////
        void Changed(object sender, FileSystemEventArgs e)
        {
            string msg = string.Format(e.FullPath + " " + e.ChangeType);
            MakeMessage(e.FullPath, msg, "sourceDir");
            Thread.Sleep(milliseconds);
            DirectoryCopy(@sourceDirPath, @desDirPath, true);
        }
        void Renamed(object sender, RenamedEventArgs e)
        {
            string msg = string.Format("{0} renamed to {1}", e.OldFullPath, e.FullPath);
            MakeMessage(e.FullPath, msg, "sourceDir");
            Thread.Sleep(milliseconds);
            DirectoryCopy(@sourceDirPath, @desDirPath, true);
        }
        //////////////////////////////////////////////////////////////

        void Changed2(object sender, FileSystemEventArgs e)
        {
            string msg = string.Format(e.FullPath + " " + e.ChangeType);
            MakeMessage(e.FullPath, msg, "destinationDir");
        }
        void Renamed2(object sender, RenamedEventArgs e)
        {
            string msg = string.Format("{0} renamed to {1}", e.OldFullPath, e.FullPath);
            MakeMessage(e.FullPath, msg, "destinationDir");
        }
        //////////////////////////////////////////////////////////////
        void Form1_testeventhandler(string path)
        {
            listBox1.Items.Add(path);
        }
        void Form1_testeventhandler2(string path)
        {
            listBox2.Items.Add(path);
        }

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
                        listBox1.BeginInvoke(testeventhandler, new object[] { msg });
                        break;
                    case "destinationDir":
                        listBox2.BeginInvoke(testeventhandler2, new object[] { msg });
                        break;
                    default:
                        break;
                }
            }
        }

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
                        //string temppath = Path.Combine(destDirName, fileSource.Name);
                        //fileSource.CopyTo(temppath, true);
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

        private void button1_Click(object sender, EventArgs e) // 공유 폴더 경로
        {
            FolderBrowserDialog folderBrowse = new FolderBrowserDialog();

            if (folderBrowse.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowse.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e) // 로컬 저장소 경로
        {
            FolderBrowserDialog folderBrowse = new FolderBrowserDialog();

            if (folderBrowse.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowse.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e) // 동기 시작 버튼
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
            button3.Enabled = false;
            button3.Text = "동기화 가동";

            if (isFirst)
            {
                InitWatcherSourceDir(); // 감시 시작
                InitWatcherDestinationDir();
                isFirst = false;
            }
            else
            {
                watcherSourceDir.EnableRaisingEvents = true;
                watcherDestinationDir.EnableRaisingEvents = true;
            }
        }

        private void button4_Click(object sender, EventArgs e) // 중지 버튼
        {
            watcherSourceDir.EnableRaisingEvents = false;
            watcherDestinationDir.EnableRaisingEvents = false;

            button3.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }
    }
}






//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using System.IO;
//using System.Threading;
//namespace TestFileinfoInDirectory
//{
//    public partial class Form1 : Form
//    {
//        delegate void mydele(string path);
//        delegate void mydele2(string path);
//        event mydele testeventhandler; // listbox1
//        event mydele2 testeventhandler2; // listbox2
//        static int milliseconds = 30;
//        FileSystemWatcher watcherSourceDir = new FileSystemWatcher();
//        FileSystemWatcher watcherDestinationDir = new FileSystemWatcher();
//        string sourceDirPath;
//        string desDirPath;

//        public Form1()
//        {
//            InitializeComponent();
//        }

//        private void InitWatcherDestinationDir()
//        {
//            watcherDestinationDir.IncludeSubdirectories = true;

//            watcherDestinationDir.Path = desDirPath;
//            watcherDestinationDir.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName /*| NotifyFilters.Attributes */| NotifyFilters.CreationTime /*| NotifyFilters.LastAccess */ | NotifyFilters.Size ;
//            watcherDestinationDir.Filter = "";
//            watcherDestinationDir.Created += new FileSystemEventHandler(Changed2);
//            watcherDestinationDir.Deleted += new FileSystemEventHandler(Changed2);
//            watcherDestinationDir.Changed += new FileSystemEventHandler(Changed2);
//            watcherDestinationDir.Renamed += new RenamedEventHandler(Renamed2);
//            watcherDestinationDir.EnableRaisingEvents = true;

//            testeventhandler2 += new mydele2(Form1_testeventhandler2);
//        }
//        private void InitWatcherSourceDir()
//        {
//            watcherSourceDir.IncludeSubdirectories = true;
//            //watcher.Path = @"D:\sourceDir";

//            watcherSourceDir.Path = sourceDirPath;
//            watcherSourceDir.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.LastAccess  | NotifyFilters.Size  ;
//            watcherSourceDir.Filter = "";
//            watcherSourceDir.Created += new FileSystemEventHandler(Changed);
//            watcherSourceDir.Deleted += new FileSystemEventHandler(Changed);
//            watcherSourceDir.Changed += new FileSystemEventHandler(Changed);
//            watcherSourceDir.Renamed += new RenamedEventHandler(Renamed);
//            watcherSourceDir.EnableRaisingEvents = true;

//            testeventhandler += new mydele(Form1_testeventhandler);

//            DirectoryCopy(@sourceDirPath, @desDirPath, true);
//        }


//        //////////////////////////////////////////////////////////////
//        void Changed(object sender, FileSystemEventArgs e)
//        {
//            string msg = string.Format(e.FullPath + " " + e.ChangeType);
//            MakeMessage(e.FullPath, msg, "sourceDir");
//            Thread.Sleep(milliseconds);
//            DirectoryCopy(@sourceDirPath, @desDirPath, true);
//        }
//        void Renamed(object sender, RenamedEventArgs e)
//        {
//            string msg = string.Format("{0} renamed to {1}", e.OldFullPath, e.FullPath);
//            MakeMessage(e.FullPath, msg, "sourceDir");
//            Thread.Sleep(milliseconds);
//            DirectoryCopy(@sourceDirPath, @desDirPath, true);
//        }
//        //////////////////////////////////////////////////////////////

//        void Changed2(object sender, FileSystemEventArgs e)
//        {
//            string msg = string.Format(e.FullPath + " " + e.ChangeType);
//            MakeMessage(e.FullPath, msg, "destinationDir");
//        }
//        void Renamed2(object sender, RenamedEventArgs e)
//        {
//            string msg = string.Format("{0} renamed to {1}", e.OldFullPath, e.FullPath);
//            MakeMessage(e.FullPath, msg, "destinationDir");
//        }
//        //////////////////////////////////////////////////////////////
//        void Form1_testeventhandler(string path)
//        {
//            listBox1.Items.Add(path);
//        }
//        void Form1_testeventhandler2(string path)
//        {
//            listBox2.Items.Add(path);
//        }

//        private void MakeMessage(string FullPath, string msg, string WhatDir)
//        {
//            string path = string.Format("{0}//{1}", Application.StartupPath, FullPath);
//            string extension = Path.GetExtension(path);

//            if (extension == ".tmp" || extension == ".TMP")
//            {

//            }
//            else
//            {
//                switch (WhatDir)
//                {
//                    case "sourceDir":
//                        listBox1.BeginInvoke(testeventhandler, new object[] { msg });
//                        break;
//                    case "destinationDir":
//                        listBox2.BeginInvoke(testeventhandler2, new object[] { msg });
//                        break;
//                    default:
//                        break;
//                }
//            }
//        }

//        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
//        {
//            // 지정된 디렉토리의 서브 디렉토리를 가져옵니다.
//            DirectoryInfo dirSource = new DirectoryInfo(sourceDirName);
//            //DirectoryInfo dirDestination = new DirectoryInfo(destDirName);

//            if (!dirSource.Exists)
//            {
//                throw new DirectoryNotFoundException(
//                    "Source directory does not exist or could not be found: "
//                    + sourceDirName);
//            }

//            // 대상 디렉토리가 존재하지 않으면 만들어라.
//            if (!Directory.Exists(destDirName))
//            {
//                Directory.CreateDirectory(destDirName);
//            }

//            //  디렉토리에서 파일을 가져 와서 새 위치로 복사하십시오.
//            try
//            {
//                FileInfo[] filesSource = dirSource.GetFiles();
//                //FileInfo[] filesDestination = dirDestination.GetFiles();

//                Console.WriteLine(filesSource.Length);
//                for(int i = 0; i<filesSource.Length; ++i)
//                {
//                    Console.WriteLine(filesSource[i].Name);
//                }

//                foreach (FileInfo fileSource in filesSource)
//                {
//                    if (fileSource.Name[0] == '~')   // 복제 파일 복사 안함
//                    {

//                    }
//                    if (fileSource.Extension == ".tmp" || fileSource.Extension == ".TMP") // 임시 파일은 복사 안함
//                    {

//                    }
//                    else
//                    {
//                        string temppath = Path.Combine(destDirName, fileSource.Name);
//                        fileSource.CopyTo(temppath, false);
//                    }
//                }
//            }
//            catch (IOException e)
//            {
//                MessageBox.Show(e.Message);
//            }
//            // 서브 디렉토리를 복사하는 경우 서브 디렉토리를 복사하고 그 내용을 새 위치로 복사하십시오.
//            try
//            {
//                DirectoryInfo[] dirs = dirSource.GetDirectories();
//                if (copySubDirs)
//                {
//                    foreach (DirectoryInfo subdir in dirs)
//                    {
//                        string temppath = Path.Combine(destDirName, subdir.Name);
//                        DirectoryCopy(subdir.FullName, temppath, copySubDirs);
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                MessageBox.Show(e.Message);
//            }

//        }

//        private void button1_Click(object sender, EventArgs e) // 공유 폴더 경로
//        {
//            FolderBrowserDialog folderBrowse = new FolderBrowserDialog();

//            if (folderBrowse.ShowDialog() == DialogResult.OK)
//            {
//                textBox1.Text = folderBrowse.SelectedPath;
//            }
//        }

//        private void button2_Click(object sender, EventArgs e) // 로컬 저장소 경로
//        {
//            FolderBrowserDialog folderBrowse = new FolderBrowserDialog();

//            if (folderBrowse.ShowDialog() == DialogResult.OK)
//            {
//                textBox2.Text = folderBrowse.SelectedPath;
//            }
//        }

//        private void button3_Click(object sender, EventArgs e) // 동기 시작 버튼
//        {
//            //sourceDirPath = textBox1.Text;
//            sourceDirPath = @"D:\sourceDir";
//            //desDirPath = textBox2.Text;
//            desDirPath = @"D:\destDir";
//            if (sourceDirPath == "" || desDirPath == "")
//            {
//                MessageBox.Show("경로를 지정하세요.");
//                return;
//            }
//            button3.Enabled = false;
//            button3.Text = "동기화 가동";

//            InitWatcherSourceDir(); // 감시 시작
//            InitWatcherDestinationDir();
//        }

//        private void button4_Click(object sender, EventArgs e) // 중지 버튼
//        {
//            watcherSourceDir.EnableRaisingEvents = false;
//            button3.Enabled = true;
//        }

//        private void button5_Click(object sender, EventArgs e)
//        {
//            listBox1.Items.Clear();
//        }

//        private void button6_Click(object sender, EventArgs e)
//        {
//            listBox2.Items.Clear();
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TestFileinfoInDirectory
{
    public partial class Form1 : Form
    {
        delegate void mydele(string path);
        event mydele testeventhandler;
        private Object lockThis = new object();

        string sourceDirPath;
        string desDirPath;

        public Form1()
        {
            InitializeComponent();
            //InitWatcher();
        }

        private void InitWatcher()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.IncludeSubdirectories = true;
            //watcher.Path = @"C:\Users\Public\sourceDir\";
            watcher.Path = @sourceDirPath;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.LastAccess | NotifyFilters.Size;
            watcher.Filter = "";
            watcher.Created += new FileSystemEventHandler(Created);
            watcher.Deleted += new FileSystemEventHandler(Deleted);
            watcher.Changed += new FileSystemEventHandler(Changed);
            watcher.Renamed += new RenamedEventHandler(Renamed);
            watcher.EnableRaisingEvents = true;

            testeventhandler += new mydele(Form1_testeventhandler);
            lock (lockThis)
            {
                DirectoryCopy(@sourceDirPath, @desDirPath, true);
            }
        }

        void Deleted(object sender, FileSystemEventArgs e)
        {
            MakeMessage(e.FullPath, "삭제");
            lock (lockThis)
            {
                DirectoryCopy(@sourceDirPath, @desDirPath, true);
            }
        }
        void Created(object sender, FileSystemEventArgs e)
        {
            MakeMessage(e.FullPath, "생성");
            lock (lockThis)
            {
                DirectoryCopy(@sourceDirPath, @desDirPath, true);
            }
        }
        void Changed(object sender, FileSystemEventArgs e)
        {
            MakeMessage(e.FullPath, "변경");
            lock (lockThis)
            {
                DirectoryCopy(@sourceDirPath, @desDirPath, true);
            }
        }
        void Renamed(object sender, RenamedEventArgs e)
        {
            MakeMessage(e.FullPath, "이름 변경");
            lock (lockThis)
            {
                DirectoryCopy(@sourceDirPath, @desDirPath, true);
            }
        }

        void Form1_testeventhandler(string path)
        {
            listBox1.Items.Add(path);
        }

        private void MakeMessage(string FullPath, string msg)
        {
            string path = string.Format("{0}\\{1}", Application.StartupPath, FullPath);
            string extension = Path.GetExtension(path);
            if (extension == string.Empty)
            {
                path = string.Format("{0} 폴더가 {1}되었습니다", path, msg);
            }
            else
            {
                path = string.Format("{0} 파일이 {1}되었습니다", path, msg);
            }
            listBox1.BeginInvoke(testeventhandler, new object[] { path });
        }
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // 지정된 디렉토리의 서브 디렉토리를 가져옵니다.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
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
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, true);
                    //File.Copy(sourceDirName, destDirName);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            // 서브 디렉토리를 복사하는 경우 서브 디렉토리를 복사하고 그 내용을 새 위치로 복사하십시오.
            DirectoryInfo[] dirs = dir.GetDirectories();
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) // 공유 폴더 
        {
            FolderBrowserDialog folderBrowse = new FolderBrowserDialog();

            if (folderBrowse.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowse.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e) // 로컬 저장소
        {
            FolderBrowserDialog folderBrowse = new FolderBrowserDialog();

            if (folderBrowse.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowse.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //sourceDirPath = textBox1.Text;
            sourceDirPath = @"D:\sourceDir";
            //desDirPath = textBox2.Text;
            desDirPath = @"D:\destDir";

            button3.Enabled = false;
            InitWatcher();
        }
    }
}

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
        event mydele testeventhandler;
        private Object lockThis = new object();
        static int milliseconds = 100;
        FileSystemWatcher watcher = new FileSystemWatcher();

        string sourceDirPath;
        string desDirPath;

        public Form1()
        {
            InitializeComponent();
            //InitWatcher();
        }

        private void InitWatcher()
        {
            watcher.IncludeSubdirectories = true;
            //watcher.Path = @"D:\sourceDir";

            watcher.Path = @sourceDirPath;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.LastAccess | NotifyFilters.Size;
            watcher.Filter = "";
            watcher.Created += new FileSystemEventHandler(Created);
            watcher.Deleted += new FileSystemEventHandler(Deleted);
            watcher.Changed += new FileSystemEventHandler(Changed);
            watcher.Renamed += new RenamedEventHandler(Renamed);
            watcher.EnableRaisingEvents = true;

            testeventhandler += new mydele(Form1_testeventhandler);

            DirectoryCopy(@sourceDirPath, @desDirPath, true);
        }

        void Deleted(object sender, FileSystemEventArgs e)
        {
            MakeMessage(e.FullPath, "삭제");
            //Thread.Sleep(milliseconds);
            //DirectoryCopy(@sourceDirPath, @desDirPath, true);
        }
        void Created(object sender, FileSystemEventArgs e)
        {
            MakeMessage(e.FullPath, "생성");
            Thread.Sleep(milliseconds);
            DirectoryCopy(@sourceDirPath, @desDirPath, true);
        }
        void Changed(object sender, FileSystemEventArgs e)
        {
            MakeMessage(e.FullPath, "변경");
            Thread.Sleep(milliseconds);
            DirectoryCopy(@sourceDirPath, @desDirPath, true);
        }
        void Renamed(object sender, RenamedEventArgs e)
        {
            MakeMessage(e.FullPath, "이름 변경");
            Thread.Sleep(milliseconds);
            DirectoryCopy(@sourceDirPath, @desDirPath, true);
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
            else if(extension == ".tmp" || extension == ".TMP")// 임시 폴더는 출력 안함
            {
                return;
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
            DirectoryInfo dirSource = new DirectoryInfo(sourceDirName);

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
                FileInfo[] files = dirSource.GetFiles();
                foreach (FileInfo file in files)
                {
                    if(file.Extension == ".tmp" || file.Extension == ".TMP")
                    {
                        return;
                    }
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, true);
                    //File.Copy(file.Name, destDirName, true);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }
            // 서브 디렉토리를 복사하는 경우 서브 디렉토리를 복사하고 그 내용을 새 위치로 복사하십시오.
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
            sourceDirPath = textBox1.Text;
            //sourceDirPath = @"D:\sourceDir";
            desDirPath = textBox2.Text;
            //desDirPath = @"D:\destDir";
            if (sourceDirPath == "" || desDirPath == "")
            {
                MessageBox.Show("경로를 지정하세요.");
                return;
            }
            button3.Enabled = false;
            button3.Text = "동기화 가동";

            InitWatcher(); // 감시 시작
        }

        private void button4_Click(object sender, EventArgs e) // 중지 버튼
        {
            watcher.EnableRaisingEvents = false;
            button3.Enabled = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestFileinfoInDirectory
{
    class PathManager
    {
        private string sourceDirPath;
        private string desDirPath;

        public string SourceDirPath
        {
            get { return sourceDirPath; }
            set { sourceDirPath = value; }
        }
        public string DesDirPath
        {
            get { return desDirPath; }
            set { desDirPath = value; }
        }

        public bool IsPath(TextBox sourcePath, TextBox desPath)
        {
            this.SourceDirPath = sourcePath.Text;
            this.DesDirPath = desPath.Text;

            if (this.SourceDirPath == "" || this.DesDirPath == "")
            {
                MessageBox.Show("경로를 지정하세요.");
                return false;
            }
            return true;
        }
    }
}

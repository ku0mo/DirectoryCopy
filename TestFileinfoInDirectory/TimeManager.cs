using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestFileinfoInDirectory
{
    class TimeManager
    {
        private int milliseconds; // Directory read시 프로세서 충돌 제거를 위한 delay
        private int autoScanTimer;

        public TimeManager(int _milliseconds = 30, int _autoScanTimer = 0)
        {
            this.milliseconds = _milliseconds;
            this.autoScanTimer = _autoScanTimer;
        }
        public int MillSeconds
        {
            get { return milliseconds; }
            set { milliseconds = value; }
        }
        public int AutoScanTimer
        {
            get { return autoScanTimer; }
            set { autoScanTimer = value; }
        }
    }
}

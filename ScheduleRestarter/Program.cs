using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SSV = SimScheduleViewer;

namespace ScheduleRestarter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (IsProcessOpen("SimScheduleViewer"))
            {
                SSV.code.common.AddToLogList("Restarter checked to see if SimScheduleViewer was running.  It is, so I am doing nothing.");
            }
            else
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = @"\\wf3nj\groups\training\sim\Alex\SimSchedule\SimScheduleViewer\SimScheduleViewer\bin\Debug\SimScheduleViewer.exe ";
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    process.Start();
                    Exception ex = new System.IO.IOException();
                    SSV.code.common.error_logger(ex, "For some reason, SimScheduleViewer wasn't running.  That's all that is known.  Attempting to restart.  This is being reported by Restarter.");
                    SSV.code.common.AddToLogList("Restarter found SimScheduleViewer was not running and is attempting to restart it.");
                }
                catch(Exception ey)
                {
                    SSV.code.common.DoNothing(ey);
                }
            }
        }
        public static bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }
            }

            return false;
        }
    
    }
}

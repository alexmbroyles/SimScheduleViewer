using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimScheduleViewer_Starter
{

    
    class Program
    {

        static void Main(string[] args)
        {
            if (IsProcessOpen("SimScheduleViewer.exe"))
            {
                for(int i = 0; i < 10000; i++)
                {
                    Console.WriteLine("TRUE!");
                }
                Console.ReadKey();
            }
            else
            {
                for (int i = 0; i < 10000; i++)
                {
                    Console.WriteLine("FALSE!");
                }
                Console.ReadKey();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SimScheduleViewer.code
{
    public static class globs
    {
        public static int RetryInterval;
        public static int RetryCount;
        public static int RetryMax = 10;
        public static string logDB = @"log.sqlite";
        public static string csvfile;
        public static int NumberOfTabsOnMainWindow = 5;
        public static string htmlLocation = @"\\wf3nj\groups\TRAINING\SIM\Alex\SimSchedule\HTML\sched.html";
        public static string htmlLocation_dev = @"sched.html";
        public static bool PerformARefresh;

        // misc
        public static bool DevMode = false;
        public static bool DoNotDBErrors = false;
        public static string errPath = @"errors.txt";
        public static int NoHTMLItems;
        public static GenericTrainingCycle CurrentTrainingCycleNow;
        public static GenericTrainingWeek CurrentTrainingWeekNow;

        // Timer Intervals
        public static TimeSpan TS_ReadMyLearningCSV = TimeSpan.FromHours(12);
        public static TimeSpan TS_UpdateCurrentClass = TimeSpan.FromMinutes(1);
        public static TimeSpan TS_RefreshCalendars = TimeSpan.FromMinutes(30);
        public static TimeSpan TS_CycleUI = TimeSpan.FromSeconds(10);
        public static TimeSpan TS_Logger = TimeSpan.FromSeconds(10);
        public static TimeSpan TS_WeeklyNewsletter = TimeSpan.FromDays(1);


        public static DateTime DTCSVFileWasCreated;
        public static DateTime DTCSVFileWasModified;
        public static DateTime DT_Previous;

        // lists
        public static List<ErrorInfo> ErrorList;
        public static List<LogEntry> LogList;
        public static List<MyLearningItem> MyLearningItemsList;
        public static List<GenericTrainingCycle> CurrentTrainingCycles;

        public static void InitGlobalLists()
        {
            ErrorList = new List<ErrorInfo>();
            LogList = new List<LogEntry>();
            MyLearningItemsList = new List<MyLearningItem>();
            CurrentTrainingCycles = new List<GenericTrainingCycle>();

        }

    }
}

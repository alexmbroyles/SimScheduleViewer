using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimScheduleViewer.code
{
    public class initialize
    {
        public static bool Startup()
        {
            bool ret = true;

            // temp:
            //code.globs.logDB = "log-" + DateTime.Now.ToString("yy-MM-dd-HH-mm-ss") + ".sqlite";

            code.globs.InitGlobalLists();
            code.globs.CurrentTrainingCycles = TrainingCycleMethods.SetTrainingCycles();
            code.globs.CurrentTrainingCycleNow = TrainingCycleMethods.GetCurrentTrainingCycle();
            code.globs.CurrentTrainingWeekNow = TrainingCycleMethods.GetCurrentTrainingWeek();
            if (!generic.DoesFileExist(globs.logDB))
            {
                bool ret1, ret2, ret3;
                ret1 = sqlitedbmethods.CreateDatase(globs.logDB);
                ret2 = sqlitedbmethods.CreateTable(globs.logDB, "log", "Time datetime, User text, Machine text, Event text, Version text");
                ret3 = sqlitedbmethods.CreateTable(globs.logDB, "errorlogger", "User text, DateTime datetime, DTS text, Machine text, IP text, Error text, Version text, AssemblyName text, Line text, Column text, Extra text");
                if (!ret1 || !ret2 || !ret3) return false;
            }
            try
            {
                globs.csvfile = generic.GetNewestFile();

                if (code.generic.WhenWasFileCreated(code.globs.csvfile) > DateTime.MinValue)
                {
                    globs.DTCSVFileWasCreated = code.generic.WhenWasFileCreated(code.globs.csvfile);
                    globs.DTCSVFileWasModified = code.generic.WhenWasFileModified(code.globs.csvfile);
                }

               

                //var temp = File.ReadAllLines(globs.csvfile).Skip(1).Select(v => MyLearningItem.FromCSV(v)).ToList(); // this errors out if a class description has a comma.
                var temp = code.common.ParseCSVFile(code.globs.csvfile);
                //temp = temp.Where(x => Math.Abs(x.StartTime.Ticks - DateTime.Now.Ticks) < TimeSpan.FromDays(7).Ticks).ToList(); /// This is not what I wanted but I can't figure out how to stop getting errors.
                globs.MyLearningItemsList = common.RemoveDupsFromMyLearning(temp).OrderBy(y => y.StartTime).ToList();
            }
            catch(Exception ex)
            {
                common.error_logger(ex, "error during startup.");
                return false;
            }
            

            return ret;
        }
        public static bool RefreshAll()
        {
            bool ret = true;
            try
            {
                code.globs.InitGlobalLists();
                globs.csvfile = generic.GetNewestFile();
                globs.DTCSVFileWasCreated = code.generic.WhenWasFileCreated(code.globs.csvfile);
                globs.DTCSVFileWasModified = code.generic.WhenWasFileModified(code.globs.csvfile);
                //var temp = File.ReadAllLines(globs.csvfile).Skip(1).Select(v => MyLearningItem.FromCSV(v)).ToList(); // this errors out if a class description has a comma.
                var temp = code.common.ParseCSVFile(code.globs.csvfile);
                globs.MyLearningItemsList = common.RemoveDupsFromMyLearning(temp).OrderBy(y => y.StartTime).ToList();
                code.globs.CurrentTrainingCycles = TrainingCycleMethods.SetTrainingCycles();
                code.globs.CurrentTrainingCycleNow = TrainingCycleMethods.GetCurrentTrainingCycle();
                code.globs.CurrentTrainingWeekNow = TrainingCycleMethods.GetCurrentTrainingWeek();
            }
            catch(Exception ex)
            {
                code.common.error_logger(ex, "Error during refresh method.");
                return false;
            }
            return ret;
        }


        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimScheduleViewer.code
{
    public class TrainingCycleMethods
    {


        public static GenericTrainingCycle GetCurrentTrainingCycle()
        {
            GenericTrainingCycle x = new GenericTrainingCycle();

            foreach(var y in code.globs.CurrentTrainingCycles)
            {
                if( DateTime.Now >= y.start && DateTime.Now <= y.end)
                {
                    return y;
                }
            }
            return x;

        }
        public static GenericTrainingWeek GetCurrentTrainingWeek()
        {
            var x = new GenericTrainingWeek();

            foreach (var y in code.globs.CurrentTrainingCycles)
            {
                if (DateTime.Now >= y.start && DateTime.Now <= y.end)
                {
                    x.WeekNumber = generic.GetCurrentWeekNumber(DateTime.Now);
                    x.TrainingCycle = y;
        
                }
            }


            return x;

        }
        public static List<GenericTrainingCycle> SetTrainingCycles()
        {
            List<GenericTrainingCycle> ret = new List<GenericTrainingCycle>();

            // 23.1
            var x = new GenericTrainingCycle();
            x.CycleNo = 1;
            x.CycleTitleLong = "Cycle 23.1";
            x.CycleTitleNumOnly = "1";
            x.CycleTitleShort = "23.1";
            x.start = DateTime.Parse("1/8/2023");
            x.end = DateTime.Parse("3/4/2023");
            ret.Add(x);

            // 23.2
            x = new GenericTrainingCycle();
            x.CycleNo = 2;
            x.CycleTitleLong = "Cycle 23.2";
            x.CycleTitleNumOnly = "2";
            x.CycleTitleShort = "23.2";
            x.start = DateTime.Parse("3/5/2023");
            x.end = DateTime.Parse("4/23/2023");
            ret.Add(x);

            // 23.3
            x = new GenericTrainingCycle();
            x.CycleNo = 3;
            x.CycleTitleLong = "Cycle 23.3";
            x.CycleTitleNumOnly = "3";
            x.CycleTitleShort = "23.3";
            x.start = DateTime.Parse("4/24/2023");
            x.end = DateTime.Parse("6/17/2023");
            ret.Add(x);

            // 23.4
            x = new GenericTrainingCycle();
            x.CycleNo = 4;
            x.CycleTitleLong = "Cycle 23.4";
            x.CycleTitleNumOnly = "4";
            x.CycleTitleShort = "23.4";
            x.start = DateTime.Parse("6/18/2023");
            x.end = DateTime.Parse("8/5/2023");
            ret.Add(x);

            // 23.5
            x = new GenericTrainingCycle();
            x.CycleNo = 5;
            x.CycleTitleLong = "Cycle 23.5";
            x.CycleTitleNumOnly = "5";
            x.CycleTitleShort = "23.5";
            x.start = DateTime.Parse("8/6/2023");
            x.end = DateTime.Parse("11/18/2023");
            ret.Add(x);

            // 23.6
            x = new GenericTrainingCycle();
            x.CycleNo = 6;
            x.CycleTitleLong = "Cycle 23.6";
            x.CycleTitleNumOnly = "6";
            x.CycleTitleShort = "23.6";
            x.start = DateTime.Parse("11/19/2023");
            x.end = DateTime.Parse("12/31/2023");
            ret.Add(x);

            // 23.1
            x = new GenericTrainingCycle();
            x.CycleNo = 1;
            x.CycleTitleLong = "Cycle 23.1";
            x.CycleTitleNumOnly = "1";
            x.CycleTitleShort = "23.1";
            x.start = DateTime.Parse("1/8/2023");
            x.end = DateTime.Parse("3/4/2023");
            ret.Add(x);

            // 23.2
            x = new GenericTrainingCycle();
            x.CycleNo = 2;
            x.CycleTitleLong = "Cycle 23.2";
            x.CycleTitleNumOnly = "2";
            x.CycleTitleShort = "23.2";
            x.start = DateTime.Parse("3/5/2023");
            x.end = DateTime.Parse("4/23/2023");
            ret.Add(x);

            // 23.3
            x = new GenericTrainingCycle();
            x.CycleNo = 3;
            x.CycleTitleLong = "Cycle 23.3";
            x.CycleTitleNumOnly = "3";
            x.CycleTitleShort = "23.3";
            x.start = DateTime.Parse("4/24/2023");
            x.end = DateTime.Parse("6/17/2023");
            ret.Add(x);

            // 24.1
            x = new GenericTrainingCycle();
            x.CycleNo = 1;
            x.CycleTitleLong = "Cycle 24.1";
            x.CycleTitleNumOnly = "1";
            x.CycleTitleShort = "24.1";
            x.start = DateTime.Parse("1/1/2024");
            x.end = DateTime.Parse("1/28/2024");
            ret.Add(x);

            // 24.2
            x = new GenericTrainingCycle();
            x.CycleNo = 2;
            x.CycleTitleLong = "Cycle 24.2";
            x.CycleTitleNumOnly = "2";
            x.CycleTitleShort = "24.2";
            x.start = DateTime.Parse("1/29/2024");
            x.end = DateTime.Parse("3/24/2024");
            ret.Add(x);

            // 24.3
            x = new GenericTrainingCycle();
            x.CycleNo = 3;
            x.CycleTitleLong = "Cycle 24.3";
            x.CycleTitleNumOnly = "3";
            x.CycleTitleShort = "24.3";
            x.start = DateTime.Parse("3/25/2024");
            x.end = DateTime.Parse("5/19/2024");
            ret.Add(x);

            // 24.4
            x = new GenericTrainingCycle();
            x.CycleNo = 4;
            x.CycleTitleLong = "Cycle 24.4";
            x.CycleTitleNumOnly = "4";
            x.CycleTitleShort = "24.4";
            x.start = DateTime.Parse("5/20/2024");
            x.end = DateTime.Parse("9/1/2024");
            ret.Add(x);


            return ret;
        }
    }
}

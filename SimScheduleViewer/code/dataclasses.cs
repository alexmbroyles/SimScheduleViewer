using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimScheduleViewer.code
{
    public class Shortcut
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public bool ESM { get; set; }
        public bool CMD { get; set; }
        public string KeyWord1 { get; set; }
        public List<string> KeyWord2 { get; set; }
        public string Link { get; set; }
        public string Args { get; set; }
        public bool Fav { get; set; }
        public bool IEOnly { get; set; }

    }
    public class ErrorInfo
    {
        public string User { get; set; }
        public DateTime DT { get; set; }
        public string DTString { get; set; }
        public string Computer { get; set; }
        public string loc { get; set; }
        public string line { get; set; }
        public string col { get; set; }
        public string msg { get; set; }
        public string rev { get; set; }
        public string extra { get; set; }
        public string IP { get; set; }
    }
    public class LogEntry
    {
        public string User { get; set; }
        public DateTime DT { get; set; }
        public string Computer { get; set; }
        public string Text { get; set; }
    }
	public class MyLearningItem
	{
		public string ClassID;
		public string ItemType;
		public bool ActiveClass;
		public bool Cancelled;
		public DateTime StartTime;
		public DateTime EndTime;
		public string Description;
		public float TotalHours;
		public string PrimaryLocation;
		public string InstructorFirstName;
		public string InstructorLastName;
		public string ItemID;

		public static MyLearningItem FromCSV(string csvLine)
		{
			string[] values2 = csvLine.Split(',');
			List<string> values = new List<string>();
			foreach (string s in values2)
			{
				string t = s;
				if (s.Contains("US/Central"))
				{
					int i = s.IndexOf("US/Central");
					t = s.Substring(0, i);
				}
				t = t.Replace("\"", "");
				values.Add(t);
			}
			MyLearningItem mli = new MyLearningItem();
			mli.ClassID = values[0].ToString();
			mli.ItemType = values[1].ToString();
			mli.ItemID = values[2].ToString();
			//mli.ActiveClass = BoolFromString(values[4]);
			//mli.Cancelled = BoolFromString(values[5]);
			mli.StartTime = DateTime.Parse(values[7]);
			mli.Description = values[8].ToString();
			mli.TotalHours = float.Parse(values[9], CultureInfo.InvariantCulture.NumberFormat);
			mli.PrimaryLocation = values[10].ToString();
			mli.InstructorFirstName = values[11].ToString();
			mli.InstructorLastName = values[12].ToString();
			mli.EndTime = mli.StartTime + TimeSpan.FromHours(mli.TotalHours);
			return mli;

		}
		static bool BoolFromString(string str)
		{
			return Convert.ToBoolean(Enum.Parse(typeof(BooleanAliases), str));
		}
		enum BooleanAliases
		{
			Yes = 1,
			Aye = 1,
			Cool = 1,
			Naw = 0,
			No = 0
		}

	}
	public class StatusItem
	{
		public DateTime StartTime { get; set; }
		public TimeSpan RunTime { get; set; }
		public int ItemCount { get; set; }
		public DateTime LastUpdate { get; set; }
		public string ExtraInfo { get; set; }
		public int ErrorCount { get; set; }
		public int LogCount { get; set; }
		public long MemoryUsed { get; set; }
		public string CurrentCPUUsage { get; set; }
		public string CurrentAvailableRam { get; set; }
		public string MachineName { get; set; }
		public string IPAddr { get; set; }
		public string version { get; set; }
		public string maxtabs { get; set; }
		public string UITimer { get; set; }
	}
	// consider a file on desktop for different configs
	public class Config
    {
		public string Location { get; set; }
		public List<tab> tabList { get; set; }
    }
	public class tab
    {
		public string xName { get; set; }
		public string Hdr { get; set; }
		public string mode { get; set; }
    }

	public class GenericTrainingCycle
    {
		public string CycleTitleLong { get; set; }
		public string CycleTitleShort { get; set; }
		public string CycleTitleNumOnly { get; set; }
		public int CycleNo { get; set; }
		public DateTime start { get; set; }
		public DateTime end { get; set; }
		public int year { get; set; }

	}
	public class GenericTrainingWeek
    {
		public GenericTrainingCycle TrainingCycle { get; set; }
		public int WeekNumber { get; set; }
		public DateTime start { get; set; }
		public DateTime end { get; set; }
		public string Img { get; set; }

		//future - list of training items

	}


}

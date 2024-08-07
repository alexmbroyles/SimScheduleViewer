using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;

namespace SimScheduleViewer.code
{
    /// <summary>
    /// class of methods I expect to use in many different places throughout the project
    /// </summary>
    public class common
    {
        public static void error_logger(Exception ex, string extra = "")
        {
            if (globs.DevMode) { return; }
            string error_message = ex.Message;
            var e = new ErrorInfo();
            try
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                string rev = "Rev.: " + version.GetVersion();
                string loc = "Assembly Name: " + trace.GetFrame(0).GetMethod().ReflectedType.FullName;
                string line = "Line No.: " + trace.GetFrame(0).GetFileLineNumber();
                string col = "Column: " + trace.GetFrame(0).GetFileColumnNumber();
                error_message = generic.getUser() + "\t" + DateTime.Now.ToString("MM/dd/yy hh:mm:ss") + "\t" + generic.getComputer() + "\t" + "Error: " + error_message + " | " +
                                rev + " | " + loc + " | " + line + " | " + col + " | " + "Version: " + version.GetVersion() + " | Extra Info: " + extra;

                if (!globs.DoNotDBErrors)
                {
                    try
                    {
                        e.msg = ex.Message;
                        e.rev = rev;
                        e.loc = loc;
                        e.line = line;
                        e.col = col;
                        e.extra = extra;
                        e.DT = DateTime.Now;
                        e.DTString = DateTime.Now.ToString("MM/dd/yy hh:mm:ss");
                        e.IP = generic.LocalIPAddress().ToString();
                        e.Computer = generic.getComputer();
                        e.User = generic.getUser();
                        globs.ErrorList.Add(e);
                    }
                    catch (Exception ecac)
                    {
                        common.DoNothing(ecac);
                    }
                }

            }
            catch (Exception ey)
            {
                string rev = "Rev.: " + version.GetVersion();
                string loc = "Assembly Name: StackTrace not available";
                string line = "Line No.: StackTrace not available";
                string col = "Column: StackTrace not available | Additional Info: " + ey.Message;
                error_message = generic.getUser() + "\t" + DateTime.Now.ToString("MM/dd/yy hh:mm:ss") + "\t" + generic.getComputer() + "\t" + "Error: " + error_message + " | " +
                                rev + " | " + loc + " | " + line + " | " + col + " | " + "Version: " + version.GetVersion() + " | Extra Info: " + extra;
            }
            if (!File.Exists(globs.errPath))
            {
                using (StreamWriter sw = File.CreateText(globs.errPath))
                {
                    sw.WriteLine(error_message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(globs.errPath))
                {
                    sw.WriteLine(error_message);
                    try
                    {
                        code.common.AddToLogList("An error occurred: " + extra); // I don't remember adding this but it's not bad.
                    }
                    catch { }
                }
            }
            return;
        }
        /// <summary>
        /// This is an error catch that will do absolutely nothing, but will not kill your program.
        /// </summary>
        /// <param name="ey04">the error.  Does nothing.</param>
        public static void DoNothing(Exception ey04)
        {
            return;
        }
        public static void AddToLogList(string text)
        {
            if (globs.DevMode) { return; }
            try
            {
                var ent = new code.LogEntry();
                ent.Computer = generic.getComputer();
                ent.User = generic.getUser();
                ent.DT = DateTime.Now;
                ent.Text = text;
                globs.LogList.Add(ent);

            }
            catch (Exception ex)
            {
                common.error_logger(ex, "Error writing log entry.  Message: " + text + ". File: " + globs.LogList);
            }
            bool GoAheadAndLogThoseErrors = !globs.DoNotDBErrors && globs.LogList.Count > 6;
            if (globs.LogList.Count > 20 || GoAheadAndLogThoseErrors)
            {
                sqlitedbmethods.Log();
            }
        }
        public static List<MyLearningItem> RemoveDupsFromMyLearning(List<MyLearningItem> inp)
        {
            //return inp.GroupBy(x => x.ClassID).Select(x => x.First()).ToList(); // this was removing nrc validations which have the same class ID but on diff days
            return inp.GroupBy(x => new { x.ClassID, x.StartTime, x.ItemID }).Select(x => x.First()).ToList();
        }
        public static List<code.MyLearningItem> ParseCSVFile(string file)
        {
            List<code.MyLearningItem> ret = new List<code.MyLearningItem>();
            
            try
            {
                TextFieldParser parser = new TextFieldParser(file);
                parser.HasFieldsEnclosedInQuotes = true;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields();
                    if (fields[0] == "Class ID") continue;  // skip first line

                    var x = new code.MyLearningItem();

                    x.ClassID = fields[0].ToString();
                    x.ItemType = fields[1].ToString();
                    x.ItemID = fields[2].ToString();
                    int i = 0;
                    if(fields[7].Contains("US/Central"))
                    {
                        i = fields[7].IndexOf("US/Central");
                    }
                    else if(fields[7].Contains("America/CST"))
                    {
                        i = fields[7].IndexOf("America/CST");
                    }
                    x.StartTime = DateTime.Parse(fields[7].Substring(0, i).Replace("\"", ""));
                    x.Description = fields[8].ToString();
                    x.TotalHours = float.Parse(fields[9], CultureInfo.InvariantCulture.NumberFormat); 
                    x.PrimaryLocation = fields[10].ToString();
                    x.InstructorFirstName = fields[11].ToString();
                    x.InstructorLastName = fields[12].ToString();
                    x.EndTime = x.StartTime + TimeSpan.FromHours(x.TotalHours);

                    ret.Add(x);
                   

                }

                parser.Close();

            }
            catch(Exception ex)
            {
                code.common.error_logger(ex, "Error in parsing the csv file.");
            }


            return ret;
        }
    }
}

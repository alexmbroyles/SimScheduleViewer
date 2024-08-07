using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.IO;
using System.DirectoryServices.AccountManagement;
using System.Text;

namespace SimScheduleViewer.code
{
    public class StatusCheck
    {

        public static StatusItem GetStatus(string extra = "")
        {
            var x = new StatusItem();
            
            try
            {
                x.ItemCount = code.globs.MyLearningItemsList.Count;
                x.LastUpdate = code.generic.WhenWasFileCreated(code.globs.csvfile);
                x.StartTime = Process.GetCurrentProcess().StartTime;
                x.RunTime = DateTime.Now - x.StartTime;
                x.ExtraInfo = extra;

                //x.ErrorCount = code.sqlitedbmethods.GetRows("errorlogger"); // this method keeps failing
                //x.LogCount = code.sqlitedbmethods.GetRows("log"); // this method keeps failing
                x.ErrorCount = File.ReadLines(code.globs.errPath).Count();
                x.CurrentAvailableRam = getAvailableRAM();
                x.CurrentCPUUsage = getCurrentCpuUsage();
                Process currentProc = Process.GetCurrentProcess();
                x.MemoryUsed = currentProc.PrivateMemorySize64;
                x.IPAddr = code.generic.LocalIPAddress().ToString();
                x.MachineName = code.generic.getComputer();

                x.maxtabs = code.globs.NumberOfTabsOnMainWindow.ToString();
                x.UITimer = code.globs.TS_CycleUI.TotalSeconds.ToString() + " seconds";
                x.version = version.GetVersion();


            }
            catch (Exception ex)
            {
                code.common.error_logger(ex, "Error in creating a statusitem");
            }


            return x;
        }
        public static void SendStatusCheck(string extra = "")
        {
            var x = new StatusItem();
            x = GetStatus(extra);
            SmtpClient EntergySmtpClient = new SmtpClient("mail.prod.entergy.com");
            EntergySmtpClient.UseDefaultCredentials = true;
            try
            {
                MailMessage email = new MailMessage();
                email.To.Add(UserPrincipal.Current.EmailAddress); // Use the signed on user's email instead of hardcoding an email to use
                MailAddress from = new MailAddress("abroyle@entergy.com", "Simulator Scheduler");
                email.From = from;
                email.Subject = "Status Update from Simulator Scheduler";

                string body = "<H3>This is an update from the Simulator Scheduler software.</H3>" + "</br></br></br>";
                body = body + "The machine this instance is running on is: " + x.MachineName + "</br>";
                body = body + "The IP address is: " + x.IPAddr + "</br>";
                body = body + "The number of items currently on display on the agenda is: " + x.ItemCount + "</br>";
                body = body + "The number of items currently on display on the 'happening now' html page is: " +code.globs.NoHTMLItems + "</br>";
                body = body + "The scheduler is using MyLearning data from: " + x.LastUpdate.ToString("G") + "</br>";
                body = body + "This instance started on " + x.StartTime.ToString("G") + "</br>";
                double displayedTime = Math.Round(x.RunTime.TotalHours, 2);
                string displayedUnit = "hours";
                if (displayedTime < 1)
                {
                    displayedTime = Math.Round(x.RunTime.TotalMinutes, 2);
                    displayedUnit = "minutes";
                }
                if (displayedTime < 1)
                {
                    displayedTime = Math.Round(x.RunTime.TotalSeconds, 2);
                    displayedUnit = "seconds";
                }
                body = body + "This instance has been running for " + displayedTime + " " + displayedUnit + "</br>";
                body = body + "The number of items in the error log: " + x.ErrorCount + "</br>"; // this method keeps failing
                //body = body + "The number of items in the general log: " + x.LogCount + "</br>"; // this method keeps failing
                string memused = Convert.ToString(x.MemoryUsed / 1000000) + "MB";
                body = body + "The RAM used by SimScheduler: " + memused + "</br>";
                body = body + "The RAM available on this machine: " + x.CurrentAvailableRam + "</br>";
                body = body + "The CPU usage on this machine: " + x.CurrentCPUUsage + "</br>";
                body = body + "The number of tabs on this instance: " + x.maxtabs + "</br>";
                body = body + "The UI changes tabs every: " + x.UITimer + "</br>";
                body = body + "The version of this deployment: " + x.version + "</br>";
                body = body + "<b>Extra information: " + extra + "</b>";
                email.Body = body;

                email.BodyEncoding = System.Text.Encoding.UTF8;
                email.IsBodyHtml = true;
                EntergySmtpClient.Send(email);
                code.common.AddToLogList("A status check was performed and an email was sent to " + email.To[0].ToString());
            }
            catch (Exception ex)
            {
                code.common.error_logger(ex, "Error sending email for status check");
            }

        }

        public static string BuildWeekViewHTML()
        {
            StringBuilder html = new StringBuilder();

            DateTime now = DateTime.Now;
            DateTime nextMonday = now.AddDays(((int)DayOfWeek.Monday - (int)now.DayOfWeek + 7) % 7);
            DateTime nextSunday = nextMonday.AddDays(6);

            List<MyLearningItem> itemsThisWeek = code.globs.MyLearningItemsList.Where(x => x.StartTime > nextMonday && x.StartTime < nextSunday).ToList();

            html.Append("<table border='1'>");
            html.Append("<tr>");
            html.Append("<th>Monday</th>");
            html.Append("<th>Tuesday</th>");
            html.Append("<th>Wednesday</th>");
            html.Append("<th>Thursday</th>");
            html.Append("<th>Friday</th>");
            html.Append("<th>Saturday</th>");
            html.Append("<th>Sunday</th>");
            html.Append("</tr>");

            // TODO -- Make this look nicer with CSS trickery
            // This loop will make a table of events coming up. Appearance tries to mimmick the Weekly Agenda View.
            foreach (var item in itemsThisWeek)
            {
                int startDayOfWeek = (int)item.StartTime.DayOfWeek;
                int endDayOfWeek = (int)item.EndTime.DayOfWeek;
                if (endDayOfWeek < startDayOfWeek)
                {
                    endDayOfWeek += 7;
                }
                html.Append("<tr>");
                for (int i = 1; i <= 7; i++)
                {
                    if (i == startDayOfWeek)
                    {
                        int span = Math.Min(7, endDayOfWeek - startDayOfWeek + 1);
                        html.Append($"<td colspan='{span}'>");
                        html.Append($"<p>Name: {item.Description}</p>");
                        html.Append($"<p>ID: {item.ClassID}</p>");
                        html.Append($"<p>Begins: {item.StartTime}</p>");
                        html.Append($"<p>Ends: {item.EndTime}</p>");
                        html.Append($"<p>Duration: {item.TotalHours} hours</p>");
                        html.Append($"<p>Instructor: {item.InstructorFirstName} {item.InstructorLastName}</p>");
                        html.Append($"<p>Location: {item.PrimaryLocation}</p>");
                        html.Append("</td>"); i += span - 1;
                    }
                    else
                    {
                        html.Append("<td></td>");
                    }
                }
                html.Append("</tr>");
            }
            html.Append("</table>");

            return html.ToString();
        }

        public static void SendFancyStatusUpdate()
        {
            if (DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
            {
                Console.WriteLine("Today's not Sunday! This will be replaced with a return when the logic is done...");
            }
            SmtpClient EntergySmtpClient = new SmtpClient("mail.prod.entergy.com");
            EntergySmtpClient.UseDefaultCredentials = true;
            try
            {
                MailMessage email = new MailMessage();
                email.To.Add(UserPrincipal.Current.EmailAddress);    //TODO -- Send this to Training (All) instead of whoever's signed on
                email.From = new MailAddress("abroyle@entergy.com", "Simulator Scheduler");
                email.Subject = "Trainging Week Update from Simulator Scheduler";

                email.Body = BuildWeekViewHTML();

                email.BodyEncoding = System.Text.Encoding.UTF8;
                email.IsBodyHtml = true;
                EntergySmtpClient.Send(email);
                code.common.AddToLogList("The weekly update has been sent out (an email was sent to " + email.To[0].ToString() +")");
            }
            catch (Exception ex)
            {
                code.common.error_logger(ex, "Error sending email for weekly update!");
            }
        }
        public static string getCurrentCpuUsage()
        {
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            return cpuCounter.NextValue() + "%";
        }

        public static string getAvailableRAM()
        {
            PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            return ramCounter.NextValue() + "MB";
        }

    }

}

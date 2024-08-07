using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.IO;

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
                email.To.Add("abroyle@entergy.com");
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
                body = body + "This instance has been running for " + x.RunTime.TotalHours + " hours" + "</br>";
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

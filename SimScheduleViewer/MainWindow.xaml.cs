using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Timers;
using wpfs = WpfScheduler;
using System.Reflection;
using System.IO;
using SimScheduleViewer.code;

namespace SimScheduleViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        Timer Timer_Logger;
        Timer Timer_UpdateCurrentClass;
        Timer Timer_ReadMyLearningCSV;
        DispatcherTimer Timer_RefreshCalendars;
        DispatcherTimer Timer_UI;

        Random rnd21; //just some random number if needed
        public MainWindow()
        {
            InitializeComponent();
            bool goodstart = code.initialize.Startup();
            if(goodstart) { initTimers(); code.common.AddToLogList("Startup was successful.  MyLearningItemsList has " + code.globs.MyLearningItemsList.Count + " items."); }
            AddEventsToScheduler();
            code.StatusCheck.SendStatusCheck("New instance started up, this is the initial status check.");
            code.globs.PerformARefresh = false;
        }
        public void initTimers()
        {
            Timer_Logger = new Timer();
            Timer_Logger.Elapsed += Timer_Logger_Tick;
            Timer_Logger.Interval = code.globs.TS_Logger.TotalMilliseconds;
            Timer_Logger.Enabled = true;

            Timer_UpdateCurrentClass = new Timer();
            Timer_UpdateCurrentClass.Elapsed += Timer_UpdateCurrentClass_Tick;
            Timer_UpdateCurrentClass.Interval = code.globs.TS_UpdateCurrentClass.TotalMilliseconds;
            Timer_UpdateCurrentClass.Enabled = true;

            Timer_ReadMyLearningCSV = new Timer();
            Timer_ReadMyLearningCSV.Elapsed += Timer_ReadMyLearningCSV_Tick;
            Timer_ReadMyLearningCSV.Interval = code.globs.TS_ReadMyLearningCSV.TotalMilliseconds;
            Timer_ReadMyLearningCSV.Enabled = true;

            Timer_UI = new DispatcherTimer();
            Timer_UI.Tick += new EventHandler(Timer_UI_Elapsed);
            Timer_UI.Interval = code.globs.TS_CycleUI;
            Timer_UI.Start();

            Timer_RefreshCalendars = new DispatcherTimer();
            Timer_RefreshCalendars.Tick += new EventHandler(Timer_RefreshCalendars_Elapsed);
            Timer_RefreshCalendars.Interval = code.globs.TS_RefreshCalendars;
            Timer_RefreshCalendars.Start();

            code.globs.DT_Previous = DateTime.Now;


        }
        public void StopTimers()
        {
            Timer_Logger.Enabled = false;
            Timer_UpdateCurrentClass.Enabled = false;
            Timer_ReadMyLearningCSV.Enabled = false;
            Timer_RefreshCalendars.Stop();
            Timer_UI.Stop();

        }

        private void Timer_ReadMyLearningCSV_Tick(object sender, ElapsedEventArgs e)
        {
            if(!code.initialize.RefreshAll())
            {
                StopTimers();
                code.common.AddToLogList("Timer_ReadMyLearningCSV_Tick failed.  Check errors.");
                return;
            }
            //this.BeginInvoke(new Action(() => { AddEventsToScheduler(); })); // this is so slow

            bool rstTimeFrame11 = DateTime.Now.DayOfWeek == DayOfWeek.Monday;
            bool rstTimeFrame12 = DateTime.Now.Hour > 0 && DateTime.Now.Hour < 5;
            bool rstTimeFrame1 = rstTimeFrame11 && rstTimeFrame12;
            bool rstTimeFrame21 = false;// DateTime.Now.DayOfWeek == DayOfWeek.Sunday;
            bool rstTimeFrame22 = false; // DateTime.Now.Hour > 17 && DateTime.Now.Hour < 24;
            bool rstTimeFrame2 = false; // rstTimeFrame21 && rstTimeFrame22;
            bool rst = rstTimeFrame1 || rstTimeFrame2;



            if (rst)
            {
                code.globs.PerformARefresh = true;
                code.common.AddToLogList("Timer_ReadMyLearningCSV_Tick is telling me to refresh the calendar." + code.globs.MyLearningItemsList.Count + " items.");
            }
            else
            {
                try
                {
                    code.common.AddToLogList("Timer_ReadMyLearningCSV_Tick fired, but does not meet the conditions to do a full refresh.  Instead updating day of week." + code.globs.MyLearningItemsList.Count + " items.");
                    this.BeginInvoke(new Action(() => { UpdateDayofWeek(); }));
                }
                catch(Exception ex)
                {
                    code.common.error_logger(ex, "Error performing the following: this.BeginInvoke(new Action(() => { UpdateDayofWeek(); }));");
                }

            }



            code.StatusCheck.SendStatusCheck("This is a status check generated by Timer_ReadMyLearningCSV_Tick, which fires every " + code.globs.TS_ReadMyLearningCSV.TotalHours + " hours.");
        }

        private void Timer_UpdateCurrentClass_Tick(object sender, ElapsedEventArgs e)
        {


            string temp = code.generic.GetNewestFile();
            DateTime tempMod = code.generic.WhenWasFileModified(temp);
            DateTime tempCreate = code.generic.WhenWasFileCreated(temp);
            

            if(tempMod > code.globs.DTCSVFileWasModified || tempCreate > code.globs.DTCSVFileWasCreated)
            {
                code.common.AddToLogList("New CSV file detected: " + temp + " modified " + tempMod.ToString("G") + " created " + tempCreate.ToString("G") + ".  Old file: " + code.globs.csvfile);
                code.sqlitedbmethods.Log();
                code.initialize.RefreshAll();
                // this.BeginInvoke(new Action(() => { AddEventsToScheduler(); })); // i believe this is causing memory leaks
                code.globs.PerformARefresh = true; // try to fix memory leak
            }
            try
            {
                var templist = code.globs.MyLearningItemsList.Where(x => DateTime.Now.Ticks - x.StartTime.Ticks < TimeSpan.FromHours(9).Ticks && x.StartTime.Ticks - DateTime.Now.Ticks < TimeSpan.FromDays(1).Ticks).ToList();
                
                string html = code.writeHTML01.makeHTML(templist);
                if (code.globs.DevMode)
                {
                    if (File.Exists(code.globs.htmlLocation_dev))
                    {
                        File.Delete(code.globs.htmlLocation_dev);
                    }
                    File.WriteAllText(code.globs.htmlLocation_dev, html);
                }
                else
                {
                    if (File.Exists(code.globs.htmlLocation))
                    {
                        File.Delete(code.globs.htmlLocation);
                    }
                    File.WriteAllText(code.globs.htmlLocation, html);
                }

            }
            catch(Exception ex)
            {
                code.common.error_logger(ex, "Problem making html");
            }

            try
            {
                if(code.generic.DoesFileExist("SITREP.txt"))
                {
                    code.common.AddToLogList("A sitrep was requested.  Sending the status check...");
                    code.StatusCheck.SendStatusCheck("A status check was requested via sitrep.txt");
                    File.Delete("SITREP.txt");
                }
                if (code.generic.DoesFileExist("REFRESHALL.txt"))
                {
                    code.common.AddToLogList("A refreshall was requested.  Sending the status check...");
                    code.StatusCheck.SendStatusCheck("A refreshall was requested via REFRESHALL.txt");
                    //this.BeginInvoke(new Action(() => { AddEventsToScheduler(); })); // this is so slow
                    code.globs.PerformARefresh = true;
                    File.Delete("REFRESHALL.txt");
                }
            }
            catch(Exception ex)
            {
                code.common.error_logger(ex, "Error checking for or deleting status check txt");
            }
        }

        private void Timer_Logger_Tick(object sender, ElapsedEventArgs e)
        {
            code.sqlitedbmethods.Log();
        }
        private void Timer_RefreshCalendars_Elapsed(object sender, EventArgs e)
        {
            if(code.globs.PerformARefresh)
            {
                try
                {
                    code.common.AddToLogList("Timer_RefreshCalendars was told that a refresh is necessary. Doing it now...");
                    AddEventsToScheduler();
                    code.common.AddToLogList("AddEventsToScheduler() performed successfully.");
                    //System.Windows.Forms.Application.Restart(); //this doesn't work, because restarting gives warning about potentially unsafe program...

                }
                catch(Exception ex)
                {
                    code.common.error_logger(ex, "error refreshing calendars in the timer method");
                }
                code.globs.PerformARefresh = false;
            }
            else
            {
                DateTime dtTemp = DateTime.Now;
                if(dtTemp.DayOfWeek != code.globs.DT_Previous.DayOfWeek)
                {
                    try
                    {
                        code.common.AddToLogList("A need was detected to update the day of the week.  Doing that now...");
                        UpdateDayofWeek();
                    }
                    catch(Exception ex)
                    {
                        code.common.error_logger(ex, "Error updating day of week");
                    }
                }
                code.globs.DT_Previous = DateTime.Now;
            }
        }
        private void Timer_UI_Elapsed(object sender, EventArgs e)
        {
            if (code.globs.DevMode)
            {
                MainWin.Title = "DevMode | Timer_UI fired in: ";
            }
            else
            {
                MainWin.Title = "Training Cycle: " + code.globs.CurrentTrainingCycleNow.CycleTitleLong + " | Last Updated: " + DateTime.Now.ToString("G") + " | Using MyLearning Data from: " + code.generic.WhenWasFileCreated(code.globs.csvfile);
            }
            if(code.globs.PerformARefresh)
            {
                MainWin.Title = "MyLearning Data has changed.  Calendar will re-fresh with new data within " + code.globs.TS_RefreshCalendars.TotalMinutes + " minutes.";
            }
            //AddEventsToScheduler(); // this was resulting in slow performance.  I gave it to the dispatchertimers instead, which invoke it.
            browser.Load(code.globs.htmlLocation);

            var CurrentTab = tabControl.SelectedIndex;
            var maxTab = code.globs.NumberOfTabsOnMainWindow - 1;
            if (CurrentTab == maxTab)
            {
                Dispatcher.BeginInvoke((Action)(() => tabControl.SelectedIndex = 0));
            }
            else
            {
                Dispatcher.BeginInvoke((Action)(() => tabControl.SelectedIndex = CurrentTab + 1));
            }
            //Dispatcher.BeginInvoke((Action)(() => tabControl.SelectedIndex = CurrentTab.));

            //  throw new NotImplementedException();
        }
        public void UpdateDayofWeek()
        {
            try
            {
                DailyAgenda.SelectedDate = DateTime.Now;
                WeeklyAgenda.SelectedDate = DateTime.Now;
                MonthlyAgenda.SelectedDate = DateTime.Now;
                code.common.AddToLogList("Updated day of week");
            }
            catch(Exception ex)
            {
                code.common.error_logger(ex, "Unsuccessful attempt to update day of week");
            }
        }
        public void AddEventsToScheduler()
        {
            try
            {
                //DailyAgenda.Events = new System.Collections.ObjectModel.ObservableCollection<wpfs.Event>();
                DailyAgenda.Events.Clear();
            }
            catch(Exception ex)
            {
                code.common.error_logger(ex, "Error clearing daily agenda");
            }
            try
            {
                //WeeklyAgenda.Events = new System.Collections.ObjectModel.ObservableCollection<wpfs.Event>();
                WeeklyAgenda.Events.Clear();
            }
            catch(Exception ex)
            {
                code.common.error_logger(ex, "Error clearing weekly agenda");
            }
            try
            {
                //MonthlyAgenda.Events = new System.Collections.ObjectModel.ObservableCollection<wpfs.Event>();
                MonthlyAgenda.Events.Clear();
            }
            catch (Exception ex)
            {
                code.common.error_logger(ex, "Error clearing monthly agenda");
            }
            try 
            {
                string oldmemory = "About to run System.GC.Collect().  Initial free memory is : " + code.StatusCheck.getAvailableRAM();
                System.GC.Collect();
                code.common.AddToLogList(oldmemory + ".  System.GC.Collect() ran successfully.  New free memory is : " + code.StatusCheck.getAvailableRAM());
            }
            catch(Exception ex)
            {
                code.common.error_logger(ex, "Tried collecting garbage.  I know it's not recommended.  It apparently failed.");
            }

            DailyAgenda.SelectedDate = DateTime.Now;
            WeeklyAgenda.SelectedDate = DateTime.Now;
            MonthlyAgenda.SelectedDate = DateTime.Now;

            try
            {
                foreach (var item in code.globs.MyLearningItemsList)
                {
                    var evt = new wpfs.Event();
                    evt.Description = "DESCRIPTION: " + item.ItemID;

                    if (DateTime.Now > item.StartTime && DateTime.Now < item.EndTime)
                    {

                        evt.Subject = "IN PROGRESS: " + item.Description + Environment.NewLine; /// this is what you see
                        evt.Subject = evt.Subject + item.ItemID + "  |  Class ID: " + item.ClassID + Environment.NewLine;
                        evt.Subject = evt.Subject + "Class begins at: " + item.StartTime.ToString("F") + " Class ends at: " + item.EndTime.ToString("F") + Environment.NewLine;
                        evt.Subject = evt.Subject + "Duration: " + item.TotalHours.ToString() + " hours" + Environment.NewLine + "Instructor: " + item.InstructorFirstName + " " + item.InstructorLastName + Environment.NewLine + "Location: " + item.PrimaryLocation;


                    }
                    else
                    {
                        evt.Subject = item.Description + Environment.NewLine; /// this is what you see
                        evt.Subject = evt.Subject + item.ItemID + "  |  Class ID: " + item.ClassID + Environment.NewLine;
                        evt.Subject = evt.Subject + "Class begins at: " + item.StartTime.ToString("F") + " Class ends at: " + item.EndTime.ToString("F") + Environment.NewLine;
                        evt.Subject = evt.Subject + "Duration: " + item.TotalHours.ToString() + " hours" + Environment.NewLine + "Instructor: " + item.InstructorFirstName + " " + item.InstructorLastName + Environment.NewLine + "Location: " + item.PrimaryLocation;
                    }
                    evt.Start = item.StartTime;
                    evt.End = item.EndTime;




                    //// img brush
                    //ImageBrush imgBrush = new ImageBrush();
                    //imgBrush.ImageSource = new BitmapImage(new Uri(randImg(), UriKind.Relative));
                    //evt.Color = imgBrush;


                    evt.Color = SmartBrush(item.ItemType, item.ItemID, item.Description);
                    

                    DailyAgenda.AddEvent(evt);
                    WeeklyAgenda.AddEvent(evt);
                    MonthlyAgenda.AddEvent(evt);

                }
                code.common.AddToLogList("All scheduler agendas were updated - cleared then re-added.");
                code.StatusCheck.SendStatusCheck("A new CSV file was detected, and the schedules were updated successfully.");
            }
            catch(Exception ex)
            {
                code.common.error_logger(ex, "error drawing events to agendas");
            }
            code.globs.PerformARefresh = false;
            
        }
        /// <summary>
        /// Random brush generator
        /// </summary>
        /// <returns>returns a random brush</returns>
        public Brush PickBrush()
        {
            Brush result = Brushes.Transparent;
            Type brushesType = typeof(Brushes);
            PropertyInfo[] properties = brushesType.GetProperties();
            int random = rnd21.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);

            return result;
        }
        /// <summary>
        /// This is my attempt to put some logic behind the colors of the calendar items
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="item"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public Brush SmartBrush(string classType, string item, string desc)
        {

            if (classType.ToUpper() == "EXAM")  /// intent here is to make exam items red.  This will probably make exam validation stuff red.
            {
                return Brushes.Red;
            }
            if (item.ToUpper().Contains("NRC") || desc.ToUpper().Contains("VALIDATION")) // intent is to make exam validation items dark orange, but most will probably be red from above
            {
                return Brushes.DarkOrange;
            }
            if (desc.ToUpper().Contains("SRO CERTIFIED INSTRUCTOR")) // cert training is blue
            {
                return Brushes.Blue;
            }
            if (desc.ToUpper().Contains("MAINT") || item.ToUpper().Contains("MAINT")) // sim maintenance is grey
            {
                return Brushes.Gray;
            }
            if (item.ToUpper().Contains("LOR")) // all other lor is lime green
            {
                return Brushes.LimeGreen;
            }
            else // if i don't know what it is, it's gold.
            {
                return Brushes.Gold;
            }


            //return PickBrush();

        }

        private void MainWin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            code.common.AddToLogList("The program is shutting down...");
            code.sqlitedbmethods.Log();
        }
    }
}

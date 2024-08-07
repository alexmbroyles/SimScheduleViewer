using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Data.OleDb;
using System.Threading;
using System.Diagnostics;

namespace SimScheduleViewer.code
{
    /// <summary>
    /// Class of generic methods that can basically be copied and pasted to any project.  Might have a few project-specific things but largely generic.
    /// </summary>

    public static class generic
    {
        public static int GetCurrentWeekNumber(DateTime dateTime)
        {
            var culture = CultureInfo.CurrentCulture;
            var weekRule = culture.DateTimeFormat.CalendarWeekRule;
            var firstDayOfWeek = DayOfWeek.Sunday;
            int weekNumber = culture.Calendar.GetWeekOfYear(dateTime, weekRule, firstDayOfWeek);

            return weekNumber;
        }
        public static void AppendToText(string filename, string text)
        {
            using (StreamWriter w = File.AppendText(filename))
            {
                w.WriteLine(text);
            }
        }
        public static void Exec(string fullPath, string args, bool ESM = false, bool CMD = true)
        {
            if (!ESM && CMD)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = fullPath;
                startInfo.Arguments = args;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;

                Process processTemp = new Process();
                processTemp.StartInfo = startInfo;
                processTemp.EnableRaisingEvents = true;
                try
                {
                    processTemp.Start();
                    return;
                }
                catch (Exception ex)
                {
                    common.error_logger(ex, "Error executing: " + fullPath + " args: " + args);
                }
            }
            if (ESM && CMD)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = fullPath;
                startInfo.Arguments = args;
                startInfo.WorkingDirectory = @"C:\ETRProgramFiles\ESM";
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;

                Process processTemp = new Process();
                processTemp.StartInfo = startInfo;
                processTemp.EnableRaisingEvents = true;
                try
                {
                    processTemp.Start();
                    return;
                }
                catch (Exception ex)
                {
                    common.error_logger(ex, "Error executing (ESM): " + fullPath + " args: " + args);
                }
            }

        }
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        public static bool DictionaryExists(string s)
        {
            bool ret = false;
            if (File.Exists(s))
            {
                ret = true;
            }
            return ret;
        }
        public static DataSet GenericExecute(string sql, string conn, string TableName = "table")
        {
            DataSet ret = new DataSet();
            OleDbConnection myAccessConn = null;
            try
            {
                myAccessConn = new OleDbConnection(conn);
                OleDbCommand myAccessCommand = new OleDbCommand(sql, myAccessConn);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);
                myAccessConn.Open();
                myDataAdapter.Fill(ret, TableName);
                myAccessConn.Close();
            }
            catch (System.Data.OleDb.OleDbException olex) { common.error_logger(olex, "Error in database.GenericExecute(string sql, string conn, string TableName), retrying..."); if (code.globs.RetryCount <= code.globs.RetryMax) { Retry.Do(() => sqlitedbmethods.GenericExecute(sql, conn, TableName), TimeSpan.FromMilliseconds(250), 5); } }
            catch (Exception excptn0) { common.error_logger(excptn0, "Error in database.GenericExecute: sql: " + sql + " conn: " + conn); }
            finally { }
            return ret;
        }
        public static DateTime WhenWasFileModified(string file)
        {
            return System.IO.File.GetLastWriteTime(file);
        }
        public static DateTime WhenWasFileCreated(string file)
        {
            try
            {
                return System.IO.File.GetCreationTime(file);
            }
            catch(Exception ex)
            {
                return DateTime.MinValue;
            }
        }
        public static string GetNewestFile(string dir = @"\\wf3nj\groups\training\sim\Alex\SimSchedule\CSV\", string ext = ".csv")
        {
            if(code.globs.DevMode)
            {
                dir = @"C:\Users\abroyle\Downloads\";
            }
            string ret = "";
            var directory = new DirectoryInfo(dir);
            ret = directory.GetFiles().Where(f => f.Extension == ext).OrderByDescending(f => f.LastWriteTime).First().FullName.ToString();
            return ret;
        }
        public static bool DoesFileExist(string file)
        {
            try
            {
                return System.IO.File.Exists(file);
            }
            catch
            {
                return false;
            }
        }
        public static bool DoesFolderExist(string folder)
        {
            try
            {
                return System.IO.Directory.Exists(folder);
            }
            catch 
            {
                return false;
            }
        }
        public static string getUser()
        {
            return Environment.UserName;
        }
        public static string getComputer()
        {
            return System.Environment.MachineName;
        }
        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }
        public static bool IsEven(int value)
        {
            return value % 2 == 0;
        }

        public static void RemoveDuplicates<T>(IList<T> list)
        {
            if (list == null)
            {
                return;
            }
            int i = 1;
            while (i < list.Count)
            {
                int j = 0;
                bool remove = false;
                while (j < i && !remove)
                {
                    if (list[i].Equals(list[j]))
                    {
                        remove = true;
                    }
                    j++;
                }
                if (remove)
                {
                    list.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
        public static List<T> Distinct<T>(this List<T> list)
        {
            return (new HashSet<T>(list)).ToList();
        }
        public static IPAddress LocalIPAddress()
        {
            try
            {
                if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    return null;
                }

                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

                return host
                    .AddressList
                    .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            }
            catch (Exception ex)
            {
                common.error_logger(ex, "Error obtaining IP address");
                IPAddress ipret = IPAddress.Parse("0.0.0.0");
                return ipret;
            }
        }
    }
    #region Encrypt / Decrypt
    public static class StringCipher
    {

        private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");


        private const int keysize = 256;

        public static string Encrypt(string plainText, string passPhrase)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                byte[] cipherTextBytes = memoryStream.ToArray();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }
    }
    #endregion Encrypt / Decrypt
    public static class Retry
    {

        public static void Do(
            Action action,
            TimeSpan retryInterval,
            int retryCount = 3)
        {
            globs.RetryCount++;
            Do<object>(() =>
            {
                action();
                return null;
            }, retryInterval, retryCount);
        }

        public static T Do<T>(
            Func<T> action,
            TimeSpan retryInterval,
            int retryCount = 3)
        {
            globs.RetryCount++;
            var exceptions = new List<Exception>();

            for (int retry = 0; retry < retryCount; retry++)
            {
                try
                {
                    if (retry > 0)
                        Thread.Sleep(retryInterval);
                    return action();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            throw new AggregateException(exceptions);
        }
    }

    #region String Comparison
    public static class stringComp
    {
        public static bool Search(string word, List<string> wordList, double fuzzyness = 0.80)
        {
            bool foundWords = false;

            foreach (string s in wordList)
            {

                // Calculate the Levenshtein-distance:
                int levenshteinDistance = LevenshteinDistance(word, s);

                // Length of the longer string:
                int length = Math.Max(word.Length, s.Length);

                // Calculate the score:
                double score = 1.0 - (double)levenshteinDistance / length;

                // Match?
                if (score > fuzzyness)
                {
                    foundWords = true;
                }
            }
            return foundWords;
        }
        public static bool IsMatch(string word, string dest, double fuzzyness = 0.80)
        {
            bool foundWords = false;
            // Calculate the Levenshtein-distance:
            int levenshteinDistance = LevenshteinDistance(word, dest);

            // Length of the longer string:
            int length = Math.Max(word.Length, dest.Length);

            // Calculate the score:
            double score = 1.0 - (double)levenshteinDistance / length;

            // Match?
            if (score > fuzzyness)
            {
                foundWords = true;
            }
            return foundWords;
        }
        public static string Match(string word, List<string> wordList, double fuzzyness = 0.80)
        {
            string returnString = null;

            foreach (string s in wordList)
            {

                // Calculate the Levenshtein-distance:
                int levenshteinDistance = LevenshteinDistance(word, s);

                // Length of the longer string:
                int length = Math.Max(word.Length, s.Length);

                // Calculate the score:
                double score = 1.0 - (double)levenshteinDistance / length;

                // Match?
                if (score > fuzzyness)
                {
                    returnString = s;
                }
            }
            return returnString;
        }
        public static int MatchIndex(string word, List<string> wordList, double fuzzyness = 0.80)
        {

            int matchedIndex = 0;
            int i = 0;
            foreach (string s in wordList)
            {

                // Calculate the Levenshtein-distance:
                int levenshteinDistance = LevenshteinDistance(word, s);

                // Length of the longer string:
                int length = Math.Max(word.Length, s.Length);

                // Calculate the score:
                double score = 1.0 - (double)levenshteinDistance / length;

                // Match?
                if (score > fuzzyness)
                {
                    matchedIndex = i; ;
                }
                i = i + 1;

            }
            return matchedIndex;
        }


        public static int LevenshteinDistance(string src, string dest, bool caseSensitive = false)
        {
            //Default not case sensitive, specify true to override
            if (caseSensitive == false)
            {
                src = src.ToLower();
                dest = dest.ToLower();
            }

            int[,] d = new int[src.Length + 1, dest.Length + 1];
            int i, j, cost;
            char[] str1 = src.ToCharArray();
            char[] str2 = dest.ToCharArray();

            for (i = 0; i <= str1.Length; i++)
            {
                d[i, 0] = i;
            }
            for (j = 0; j <= str2.Length; j++)
            {
                d[0, j] = j;
            }
            for (i = 1; i <= str1.Length; i++)
            {
                for (j = 1; j <= str2.Length; j++)
                {

                    if (str1[i - 1] == str2[j - 1])
                        cost = 0;
                    else
                        cost = 1;

                    d[i, j] =
                        Math.Min(
                            d[i - 1, j] + 1,              // Deletion
                            Math.Min(
                                d[i, j - 1] + 1,          // Insertion
                                d[i - 1, j - 1] + cost)); // Substitution

                    if ((i > 1) && (j > 1) && (str1[i - 1] ==
                        str2[j - 2]) && (str1[i - 2] == str2[j - 1]))
                    {
                        d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
                    }
                }
            }

            return d[str1.Length, str2.Length];
        }
    }
    #endregion String Comparison
}

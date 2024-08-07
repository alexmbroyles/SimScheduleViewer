using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace SimScheduleViewer.code
{
    public class sqlitedbmethods
    {
        public static bool CreateDatase(string file)
        {
            try
            {
                SQLiteConnection.CreateFile(file);

                return true;
            }
            catch (Exception ex)
            {
                globs.DoNotDBErrors = true;
                common.error_logger(ex, "Error creating SQLite file.  Cancelling logging anything in db...");
                return false;
            }
        }
        public static bool CreateTable(string file, string tableName, string args, string conn = "")
        {
            if (String.IsNullOrEmpty(conn))
            {
                conn = "Data Source=" + file + ";Version = 3";
            }
            try
            {
                SQLiteConnection m_dbConnection = new SQLiteConnection(conn);
                m_dbConnection.Open();
                string sql = "Create Table If Not Exists " + tableName + " (" + args + ")";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                globs.DoNotDBErrors = true;
                common.error_logger(ex, "Error creating SQLite file.  Cancelling logging anything in db...");
                return false;
            }
        }
        public static DataSet GenericExecute(string sql, string conn, string TableName = "table")
        {
            DataSet ret = new DataSet();
            try
            {
                var con = new SQLiteConnection(conn);
                con.Open();
                var cmd = con.CreateCommand();
                var db = new SQLiteDataAdapter(sql, con);
                db.Fill(ret);
                con.Close();

            }
            catch (System.Data.SQLite.SQLiteException olex) { common.error_logger(olex, "Error in database.GenericExecute(string sql, string conn, string TableName), retrying..."); if (code.globs.RetryCount <= 5) { Retry.Do(() => sqlitedbmethods.GenericExecute(sql, conn, TableName), TimeSpan.FromMilliseconds(250), 3); } }
            catch (Exception excptn0) { common.error_logger(excptn0, "Error in database.GenericExecute: sql: " + sql + " conn: " + conn); }
            finally { }

            return ret;
        }
        public static void Log()
        {
            //if (globs.DevMode) { return; }
            string conn = "Data Source=" + code.globs.logDB + ";Version = 3";
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(conn))
                {
                    connection.Open();
                    foreach (code.LogEntry cc in globs.LogList)
                    {
                        try
                        {
                            string sqlText = "INSERT INTO log ([Time], [User], [Machine], [Event], [Version]) " +
                                                "VALUES (?, ?, ?, ?, ?)";
                            SQLiteCommand command = new SQLiteCommand(sqlText, connection);
                            command.Parameters.AddWithValue("@[Time]", cc.DT);
                            command.Parameters.AddWithValue("@[User]", cc.User);
                            command.Parameters.AddWithValue("@[Machine]", cc.Computer);
                            command.Parameters.AddWithValue("@[Event]", cc.Text);
                            command.Parameters.AddWithValue("@[Version]", code.version.GetVersion());
                            command.ExecuteNonQuery();
                        }
                        catch (Exception exx)
                        {
                            common.error_logger(exx, "Error executing query");
                        }

                    }
                    // Log Errors
                    if (!globs.DoNotDBErrors)
                    {
                        foreach (code.ErrorInfo ec in globs.ErrorList)
                        {
                            try
                            {
                                string sqlText = "INSERT INTO errorlogger ([User], [DateTime], [DTS], [Machine], [IP], [Error], [Version], [AssemblyName], [Line], [Column], [Extra]) " +
                                                 "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                                SQLiteCommand command = new SQLiteCommand(sqlText, connection);
                                command.Parameters.AddWithValue("@[User]", ec.User);
                                command.Parameters.AddWithValue("@[DateTime]", ec.DT);
                                command.Parameters.AddWithValue("@[DTS]", ec.DTString);
                                command.Parameters.AddWithValue("@[Machine]", ec.Computer);
                                command.Parameters.AddWithValue("@[IP]", ec.IP);
                                command.Parameters.AddWithValue("@[Error]", ec.msg);
                                command.Parameters.AddWithValue("@[Version]", ec.rev);
                                command.Parameters.AddWithValue("@[AssemblyName]", ec.loc);
                                command.Parameters.AddWithValue("@[Line]", ec.line);
                                command.Parameters.AddWithValue("@[Column]", ec.col);
                                command.Parameters.AddWithValue("@[Extra]", ec.extra);
                                command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                common.DoNothing(ex);
                            }
                        }
                    }

                    connection.Close();
                }
            }

            catch (Exception ex)
            {
                common.error_logger(ex, "Error writing to database");
            }
            globs.LogList.Clear();
            globs.LogList = new List<LogEntry>();

            globs.ErrorList.Clear();
            globs.ErrorList = new List<ErrorInfo>();

        }
        public static int GetRows(string table, string field = "User")
        {
            // THis method is failing.   I'm disabling it.
            return 0;
            /*
            if (globs.DevMode) { return 0; }

            string conn = "Data Source=" + code.globs.logDB + ";Version = 3";
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    SQLiteConnection comm = new SQLiteConnection(conn);
                    comm.Open();
                    cmd.CommandText = "select count(" + field + ") from " + table + ";";
                    cmd.CommandType = CommandType.Text;
                    int RowCount = 0;
                    RowCount = Convert.ToInt32(cmd.ExecuteScalar());
                    return RowCount;
                }
            }
            catch (Exception ex)
            {
                common.error_logger(ex);
                return 0;
            }*/
            
        }
    }

}


using RoRService.Repositories.Contracts;
using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace RoRService.Repositories.Helpers
{
    public class SQLiteHelper : ISQLiteHelper
    {
        private string _connectionStringROR = ConfigurationManager.ConnectionStrings["RORConnectionString"].ConnectionString;

        public long ExecuteNonQuery(SQLiteCommand command)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(_connectionStringROR))
                {
                    command.Connection = conn;
                    conn.Open();
                    var result = command.ExecuteNonQuery();
                    long rowId = conn.LastInsertRowId;
                    conn.Close();
                    
                    if (rowId > 0)
                    {
                        return rowId;
                    }
                    else
                    {
                        return (Int64)result;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.ToString());
                return 0;
            }
        }

        public DataTable ExecuteQuery(SQLiteCommand command)
        {
            var dt = new DataTable();

            using (SQLiteConnection conn = new SQLiteConnection(_connectionStringROR))
            {
                conn.Open();
                command.Connection = conn;           
                var da = new SQLiteDataAdapter(command);
                var ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
                conn.Close();
            }

            return dt;
        }
    }
}
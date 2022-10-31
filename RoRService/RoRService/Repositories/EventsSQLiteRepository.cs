using RoRService.Models.DataModels;
using RoRService.Repositories.Contracts;
using RoRService.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace RoRService.Repositories
{
    public class EventsSQLiteRepository : IEventsRepository
    {
        private SQLiteHelper sqliteHelper;

        public EventsSQLiteRepository(SQLiteHelper sqliteHelper)
        {
            this.sqliteHelper = sqliteHelper;
        }

        public Event GetEvent(int eventNo, int eventLevel)
        {
            var commandText = new StringBuilder();
            commandText.Append("SELECT No, ");
            commandText.Append("       Level, ");
            commandText.Append("       Name, ");
            commandText.Append("       DisplayText, ");
            commandText.Append("       DisplayImage ");
            commandText.Append("FROM   EventDetails ");
            commandText.Append("WHERE  No = @EventNo ");
            commandText.Append("AND    Level = @EventLevel ");

            SQLiteCommand command = new SQLiteCommand(commandText.ToString());
            command.Parameters.AddWithValue("EventNo", eventNo);
            command.Parameters.AddWithValue("EventLevel", eventLevel);
            DataTable dt = sqliteHelper.ExecuteQuery(command);

            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];

                var newEvent = new Event()
                {
                    No =  Convert.ToInt32(dr["No"]),
                    Level = Convert.ToInt32(dr["Level"]),
                    Name = Convert.ToString(dr["Name"]),
                    Text = Convert.ToString(dr["DisplayText"]),
                    Image = Convert.ToString(dr["DisplayImage"])
                };

                return newEvent;
            }
            else
            {
                return null;
            }
        }

        public List<Event> GetEventsForGame(int gameId)
        {
            var eventList = new List<Event>();

            var commandText = new StringBuilder();
            commandText.Append("SELECT ed.No, ");
            commandText.Append("       e.Level, ");
            commandText.Append("       ed.Name, ");
            commandText.Append("       ed.DisplayText, ");
            commandText.Append("       ed.DisplayImage ");
            commandText.Append("FROM   EventDetails ed ");
            commandText.Append("JOIN   Events e ON e.EventNo = ed.No AND e.Level = ed.Level ");
            commandText.Append("WHERE  e.GameId = @GameId ");

            SQLiteCommand command = new SQLiteCommand(commandText.ToString());
            command.Parameters.AddWithValue("GameId", gameId);
            DataTable dt = sqliteHelper.ExecuteQuery(command);

            foreach (DataRow dr in dt.Rows)
            {
                var newEvent = new Event()
                {
                    No = Convert.ToInt32(dr["No"]),
                    GameId = gameId,
                    Level = Convert.ToInt32(dr["Level"]),
                    Name = Convert.ToString(dr["Name"]),
                    Text = Convert.ToString(dr["DisplayText"]),
                    Image = Convert.ToString(dr["DisplayImage"])
                };

                eventList.Add(newEvent);
            }

            return eventList;
        }
    }
}
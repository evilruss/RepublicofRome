using RoRService.Models.DataModels;
using RoRService.Models.DataModels.Cards;
using RoRService.Models.DataModels.Enums;
using RoRService.Repositories.Contracts;
using RoRService.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Web;

namespace RoRService.Repositories
{
    public class LegionsSQLiteRepository : ILegionsRepository
    {
        private SQLiteHelper sqliteHelper;

        public LegionsSQLiteRepository(SQLiteHelper sqliteHelper)
        {
            this.sqliteHelper = sqliteHelper;
        }

        public IEnumerable<Legion> GetLegions(long gameId)
        {
            var legionList = new List<Legion>();

            var commandText = new StringBuilder();
            commandText.Append("SELECT No, ");
            commandText.Append("       Status, ");
            commandText.Append("       Owner ");
            commandText.Append("FROM   Legions ");
            commandText.Append("WHERE  GameId = @GameId ");

            SQLiteCommand command = new SQLiteCommand(commandText.ToString());
            command.Parameters.AddWithValue("GameId", gameId);
            DataTable dt = sqliteHelper.ExecuteQuery(command);

            foreach (DataRow dr in dt.Rows)
            {
                var legion = new Legion()
                {
                    No = Convert.ToInt32(dr["No"]),
                    GameId = gameId,
                    Status = (LegionStatus)Enum.Parse(typeof(LegionStatus), Convert.ToString(dr["Status"])),
                    Owner = Convert.ToInt32(dr["Owner"])
                };

                legionList.Add(legion);
            };

            return legionList;
        }

        public RepositoryActionResult<List<Legion>> CreateLegions(List<Legion> newLegionsList)
        {
            SQLiteCommand command;

            try
            {
                foreach (var legion in newLegionsList)
                {
                    command = new SQLiteCommand("INSERT INTO Legions (GameId, No, Status, Owner) VALUES (@GameId, @No, @No, @Status, @Owner)");
                    command.Parameters.AddWithValue("GameId", legion.GameId);
                    command.Parameters.AddWithValue("No", legion.No);
                    command.Parameters.AddWithValue("Status", legion.Status.ToString());
                    command.Parameters.AddWithValue("Owner", legion.Owner);
                    var result = sqliteHelper.ExecuteNonQuery(command);

                    if (result == 0)
                    {
                        return new RepositoryActionResult<List<Legion>>(newLegionsList, RepositoryActionStatus.NothingModified, null);
                    }
                }

                return new RepositoryActionResult<List<Legion>>(newLegionsList, RepositoryActionStatus.Created, null);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<List<Legion>>(null, RepositoryActionStatus.Error, ex);
            }
        }

    }
}
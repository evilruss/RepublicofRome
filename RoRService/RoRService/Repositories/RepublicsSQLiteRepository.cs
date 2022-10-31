using RoRService.Models.DataModels;
using RoRService.Repositories.Contracts;
using RoRService.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace RoRService.Repositories
{
    public class RepublicsSQLiteRepository : IRepublicsRepository
    {
        private SQLiteHelper sqliteHelper;

        public RepublicsSQLiteRepository(SQLiteHelper sqliteHelper)
        {
            this.sqliteHelper = sqliteHelper;
        }

        public Republic GetRepublic(long gameId)
        {
            var commandText = new StringBuilder();
            commandText.Append(" SELECT UnrestLevel, Treasury, Fleets ");
            commandText.Append(" FROM   Republics ");
            commandText.Append(" WHERE  GameId = @GameId ");

            SQLiteCommand command = new SQLiteCommand(commandText.ToString());
            command.Parameters.AddWithValue("GameId", gameId);
            DataTable dt = sqliteHelper.ExecuteQuery(command);

            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];

                var republic = new Republic()
                {
                    GameId = gameId,
                    UnrestLevel = Convert.ToInt32(dr["UnrestLevel"]),
                    StateTreasury = Convert.ToInt32(dr["Treasury"]),
                    Fleets = Convert.ToInt32(dr["Fleets"]),
                };

                return republic;
            }
            else
            {

                return null;
            }
        }

        public RepositoryActionResult<Republic> UpdateRepublic(long gameId, Republic republic)
        {
            var commandText = new StringBuilder();
            commandText.Append(" UPDATE Republics ");
            commandText.Append(" SET    UnrestLevel = @UnrestLevel, Treasury = @Treasury, Fleets = @Fleets ");
            commandText.Append(" WHERE  GameId = @GameId ");

            SQLiteCommand command = new SQLiteCommand(commandText.ToString());
            command.Parameters.AddWithValue("GameId", gameId);
            command.Parameters.AddWithValue("UnrestLevel", republic.UnrestLevel);
            command.Parameters.AddWithValue("Treasury", republic.StateTreasury);
            command.Parameters.AddWithValue("Fleets", republic.Fleets);
            var result = sqliteHelper.ExecuteNonQuery(command);

            if (result > 0)
            {
                return new RepositoryActionResult<Republic>(republic, RepositoryActionStatus.Updated);
            }
            else
            {
                return new RepositoryActionResult<Republic>(republic, RepositoryActionStatus.NothingModified);
            }
        }

        public RepositoryActionResult<Republic> CreateRepublic(long gameId, Republic republic)
        {
            try
            {
                var commandText = new StringBuilder();
                commandText.Append(" INSERT INTO Republics (GameId, UnrestLevel, Treasury, Fleets) ");
                commandText.Append(" VALUES (@GameId, @UnrestLevel, @Treasury, @Fleets) ");

                SQLiteCommand command = new SQLiteCommand(commandText.ToString());
                command.Parameters.AddWithValue("GameId", gameId);
                command.Parameters.AddWithValue("UnrestLevel", republic.UnrestLevel);
                command.Parameters.AddWithValue("Treasury", republic.StateTreasury);
                command.Parameters.AddWithValue("Fleets", republic.Fleets);
                var result = sqliteHelper.ExecuteNonQuery(command);

                if (result > 0)
                {
                    return new RepositoryActionResult<Republic>(republic, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Republic>(null, RepositoryActionStatus.NothingModified);
                }



            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Republic>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}
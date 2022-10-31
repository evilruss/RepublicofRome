using RoRService.Models.DataModels;
using RoRService.Repositories.Contracts;
using RoRService.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace RoRService.Repositories
{
    public class PlayersSQLiteRepository : IPlayersRepository
    {
        private SQLiteHelper sqliteHelper;

        public PlayersSQLiteRepository(SQLiteHelper sqliteHelper)
        {
            this.sqliteHelper = sqliteHelper;
        }

        public Player GetPlayer(long id)
        {
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Players WHERE Id = @id");
            command.Parameters.AddWithValue("id", id);

            DataTable dt = sqliteHelper.ExecuteQuery(command);

            var dr = dt.Rows[0];

            var player = new Player()
            {
                Id = Convert.ToInt64(dr["Id"]),
                FactionName = Convert.ToString(dr["FactionName"]),
                Name = Convert.ToString(dr["Name"])
            };

            return player;
        }

        public IEnumerable<Player> GetPlayersByGameId(long gameId)
        {
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Players where GameId = @GameId");
            command.Parameters.AddWithValue("GameId", gameId);

            DataTable dt = sqliteHelper.ExecuteQuery(command);

            List<Player> playerList = (from DataRow dr in dt.Rows

                                         select new Player()
                                         {
                                             Id = Convert.ToInt64(dr["Id"]),
                                             Name = Convert.ToString(dr["Name"]),
                                             FactionName = Convert.ToString(dr["FactionName"])
                                         }

                                  ).ToList();
            return playerList;
        }



        public RepositoryActionResult<Player> CreatePlayer(Player newPlayer)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand("INSERT INTO Players (GameId, Name, FactionName) VALUES (@GameId, @Name, @FactionName)");
                command.Parameters.AddWithValue("GameId", newPlayer.GameId);
                command.Parameters.AddWithValue("Name", newPlayer.Name);
                command.Parameters.AddWithValue("FactionName", newPlayer.FactionName);
                var playerRowId = sqliteHelper.ExecuteNonQuery(command);

                if (playerRowId > 0)
                {
                    newPlayer.Id = playerRowId;
                    return new RepositoryActionResult<Player>(newPlayer, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Player>(newPlayer, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Player>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}
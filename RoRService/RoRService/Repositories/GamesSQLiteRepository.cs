using RoRService.Models.DataModels;
using RoRService.Repositories.Helpers;
using RoRService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using RoRService.Models.DataModels.Enums;

namespace RoRService.Repositories
{
    public class GamesSQLiteRepository : IGamesRepository
    {
        private SQLiteHelper sqliteHelper;

        public GamesSQLiteRepository(SQLiteHelper sqliteHelper)
        {
            this.sqliteHelper = sqliteHelper;
        }

        public IEnumerable<Game> GetGames()
        {
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Games");

            DataTable dt = sqliteHelper.ExecuteQuery(command);

            List<Game> gameList = (from DataRow dr in dt.Rows

                                   select new Game()
                                   {
                                       Id = Convert.ToInt64(dr["Id"]),
                                       Name = Convert.ToString(dr["Name"]),
                                       Owner = Convert.ToString(dr["Owner"]),
                                       Status = (GameStatus)Enum.Parse(typeof(GameStatus), Convert.ToString(dr["Status"])),
                                       Description = Convert.ToString(dr["Description"]),
                                       DateOfLastAction = Convert.ToString(dr["CreateDate"]),
                                       Era = (Era)Enum.Parse(typeof(Era), Convert.ToString(dr["Era"]))
                                       //Era = dr.Table.Columns.Contains("Era") ? Convert.ToString(dr["Era"]) : string.Empty
                                   }

                                  ).ToList();
            return gameList;
        }

        public Game GetGame(long id)
        {
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Games WHERE Id = @id");
            command.Parameters.AddWithValue("id", id);

            DataTable dt = sqliteHelper.ExecuteQuery(command);

            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];

                var game = new Game()
                {
                    Id = Convert.ToInt64(dr["Id"]),
                    Name = Convert.ToString(dr["Name"]),
                    Owner = Convert.ToString(dr["Owner"]),
                    Status = (GameStatus)Enum.Parse(typeof(GameStatus), Convert.ToString(dr["Status"])),
                    Description = Convert.ToString(dr["Description"]),
                    DateOfLastAction = Convert.ToString(dr["CreateDate"]),
                    Era = (Era)Enum.Parse(typeof(Era), Convert.ToString(dr["Era"]))
                };

                return game;
            }
            else
            {

                return null;
            }
        }

        public RepositoryActionResult<Game> CreateGame(Game newGame)
        {
            try
            {
                var commandText = new StringBuilder();
                commandText.Append(" INSERT INTO Games (Name, Owner, Status, Description, CreateDate, Era) ");
                commandText.Append(" VALUES (@Name, @Owner, @Status, @Description, @CreateDate, @Era) ");

                SQLiteCommand command = new SQLiteCommand(commandText.ToString());
                command.Parameters.AddWithValue("Name", newGame.Name);
                command.Parameters.AddWithValue("Owner", newGame.Owner);
                command.Parameters.AddWithValue("Status", newGame.Status.ToString());
                command.Parameters.AddWithValue("Description", newGame.Description);
                command.Parameters.AddWithValue("CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                command.Parameters.AddWithValue("Era", newGame.Era.ToString());
                var result = sqliteHelper.ExecuteNonQuery(command);

                if (result > 0)
                {
                    newGame.Id = result;
                    return new RepositoryActionResult<Game>(newGame, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Game>(newGame, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Game>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Game> UpdateGame(Game game)
        {
            try
            {
                var commandText = new StringBuilder();
                commandText.Append(" UPDATE Games ");
                commandText.Append(" SET    Name = @Name, ");
                commandText.Append("        Owner = @Owner, ");
                commandText.Append("        Status = @Status, ");
                commandText.Append("        Description = @Description, ");
                commandText.Append("        Era = @Era ");
                commandText.Append(" WHERE  Id = @GameId ");

                SQLiteCommand command = new SQLiteCommand(commandText.ToString());
                command.Parameters.AddWithValue("Name", game.Name);
                command.Parameters.AddWithValue("Owner", game.Owner);
                command.Parameters.AddWithValue("Status", game.Status.ToString());
                command.Parameters.AddWithValue("Description", game.Description);
                command.Parameters.AddWithValue("Era", game.Era.ToString());
                command.Parameters.AddWithValue("GameId", game.Id);
                var result = sqliteHelper.ExecuteNonQuery(command);

                if (result > 0)
                {
                    return new RepositoryActionResult<Game>(game, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Game>(game, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Game>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public void DeleteGame(long gameId)
        {
            SQLiteCommand command = new SQLiteCommand("DELETE FROM Games WHERE id = @id");
            command.Parameters.AddWithValue("id", gameId);
            sqliteHelper.ExecuteNonQuery(command);
        }
    }
}
using RoRService.Models.Contracts;
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
    public class FactionsSQLiteRepository : IFactionsRepository
    {
        private SQLiteHelper sqliteHelper;
        private CardsSQLiteRepository cardRepo;

        public FactionsSQLiteRepository(SQLiteHelper sqliteHelper, CardsSQLiteRepository cardRepo)
        {
            this.sqliteHelper = sqliteHelper;
            this.cardRepo = cardRepo;
        }

        public IEnumerable<Faction> GetFactions(long gameId)
        {
            var factionList = new List<Faction>();

            var commandText = new StringBuilder();
            commandText.Append("SELECT Factions.Id         as Id, ");
            commandText.Append("       Players.Id          as PlayerId, ");
            commandText.Append("       Players.FactionName as Name, ");
            commandText.Append("       Players.Name        as PlayerName, ");
            commandText.Append("       Players.Id          as PlayerId, ");
            commandText.Append("       Factions.No         as No, ");
            commandText.Append("       Factions.Treasury   as Treasury, ");
            commandText.Append("       Factions.Leader     as Leader ");
            commandText.Append("FROM   Factions ");
            commandText.Append("JOIN   Players       ON Factions.PlayerId = Players.Id ");
            commandText.Append("WHERE  Factions.GameId = @GameId ");

            SQLiteCommand command = new SQLiteCommand(commandText.ToString());
            command.Parameters.AddWithValue("GameId", gameId);
            DataTable dt = sqliteHelper.ExecuteQuery(command);

            foreach (DataRow dr in dt.Rows)
            {
                long factionId = Convert.ToInt64(dr["Id"]);
                List<Card> cardList = cardRepo.GetCardsForFaction(gameId, factionId);

                var faction = new Faction()
                {
                    Id = factionId,
                    GameId = gameId,
                    PlayerId = Convert.ToInt64(dr["PlayerId"]),
                    Name = Convert.ToString(dr["Name"]),
                    PlayerName = Convert.ToString(dr["PlayerName"]),
                    No = Convert.ToInt32(dr["No"]),
                    Treasury = Convert.ToInt32(dr["Treasury"]),
                    Leader = Convert.ToInt32(dr["Leader"]),
                    Hand = cardList.Where(c => c.Status == CardStatus.Hand).ToList(),
                    Senators = cardList.Where(c => c.Status != CardStatus.Hand && c.Status != CardStatus.Assigned).ToList()
                };

                factionList.Add(faction);
            }

            return factionList;
        }

        public Faction GetFaction(long gameId, int factionId)
        {
            var commandText = new StringBuilder();
            commandText.Append("SELECT f.Id          as Id, ");
            commandText.Append("       p.Id          as PlayerId, ");
            commandText.Append("       p.FactionName as Name, ");
            commandText.Append("       p.Name        as PlayerName, ");
            commandText.Append("       p.Id          as PlayerId, ");
            commandText.Append("       f.No          as No, ");
            commandText.Append("       f.Treasury    as Treasury, ");
            commandText.Append("       f.Leader      as Leader ");
            commandText.Append("FROM   Factions f ");
            commandText.Append("JOIN   Players p  ON f.PlayerId = p.Id ");
            commandText.Append("WHERE  f.GameId = @GameId ");
            commandText.Append("AND    f.Id = @FactionId ");

            SQLiteCommand command = new SQLiteCommand(commandText.ToString());
            command.Parameters.AddWithValue("GameId", gameId);
            command.Parameters.AddWithValue("FactionId", factionId);
            DataTable dt = sqliteHelper.ExecuteQuery(command);

            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];
                List<Card> cardList = cardRepo.GetCardsForFaction(gameId, factionId);

                var faction = new Faction()
                {
                    Id = factionId,
                    GameId = gameId,
                    PlayerId = Convert.ToInt64(dr["PlayerId"]),
                    Name = Convert.ToString(dr["Name"]),
                    PlayerName = Convert.ToString(dr["PlayerName"]),
                    No = Convert.ToInt32(dr["No"]),
                    Treasury = Convert.ToInt32(dr["Treasury"]),
                    Leader = Convert.ToInt32(dr["Leader"]),
                    Hand = cardList.Where(c => c.Status == CardStatus.Hand).ToList(),
                    Senators = cardList.Where(c => c.Status != CardStatus.Hand && c.Status != CardStatus.Assigned).ToList()
                };

                return faction;
            }
            else
            {

                return null;
            }
        }

        public RepositoryActionResult<List<Faction>> CreateFactions(List<Faction> newFactionsList)
        {
            SQLiteCommand command;

            try
            {
                foreach (var faction in newFactionsList)
                {
                    command = new SQLiteCommand("INSERT INTO Factions (GameId, PlayerId, No, Treasury, Leader) VALUES (@GameId, @PlayerId, @No, @Treasury, @Leader)");
                    command.Parameters.AddWithValue("GameId", faction.GameId);
                    command.Parameters.AddWithValue("PlayerId", faction.PlayerId);
                    command.Parameters.AddWithValue("No", faction.No);
                    command.Parameters.AddWithValue("Treasury", 0);
                    command.Parameters.AddWithValue("Leader", faction.Leader);
                    var result = sqliteHelper.ExecuteNonQuery(command);

                    if (result > 0)
                    {
                        faction.Id = result;

                        foreach (var card in faction.Senators)
                        {
                            command = new SQLiteCommand("INSERT INTO FactionCards (FactionId, CardNo) VALUES (@FactionId, @CardNo)");
                            command.Parameters.AddWithValue("FactionId", faction.Id);
                            command.Parameters.AddWithValue("CardNo", card.CardNo);
                            result = sqliteHelper.ExecuteNonQuery(command);
                        }

                        foreach (var card in faction.Hand)
                        {
                            command = new SQLiteCommand("INSERT INTO FactionCards (FactionId, CardNo) VALUES (@FactionId, @CardNo)");
                            command.Parameters.AddWithValue("FactionId", faction.Id);
                            command.Parameters.AddWithValue("CardNo", card.CardNo);
                            result = sqliteHelper.ExecuteNonQuery(command);
                        }
                    }
                    else
                    {
                        return new RepositoryActionResult<List<Faction>>(newFactionsList, RepositoryActionStatus.NothingModified, null);
                    }
                }

                return new RepositoryActionResult<List<Faction>>(newFactionsList, RepositoryActionStatus.Created, null);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<List<Faction>>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}
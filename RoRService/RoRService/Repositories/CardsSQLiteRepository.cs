using RoRService.Models.DataModels;
using RoRService.Models.DataModels.Cards;
using RoRService.Models.DataModels.Enums;
using RoRService.Repositories.Contracts;
using RoRService.Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace RoRService.Repositories
{
    public class CardsSQLiteRepository : ICardsRepository
    {
        private SQLiteHelper sqliteHelper;

        public readonly string senatorCardsCurrentSelect = " SELECT c.No as CardNo, c.Type, c.Status, sc.Name, sc.Era, "
                                                                + " sc.FamilyNo, sc.HasStatesman, null as FamilyLetter, sc.Military, sc.Oratory, sc.Loyalty, cv.Influence, cv.Popularity, cv.Knights, cv.PriorConsul, cv.Treasury, "
                                                                + " sc.DisplayText, sc.DisplayImage, 0 as Income, o.Title as OfficeTitle, o.Rank as OfficeRank, o.Influence as OfficeInfluence, "
                                                                + " null as WarName, 0 as Legions, 0 as FleetSupport, 0 as Fleets, 0 as Talents, null as Disasters, null as StandOffs, null as DefaultStatus, 0 as Bonus, 0 as Level, 0 as MaxLevel "
                                                         + " FROM   Cards c "
                                                         + " JOIN   SenatorCards sc ON c.No = sc.CardNo " 
                                                         + " JOIN   CardVariables cv ON c.No = cv.CardNo " 
                                                         + " LEFT JOIN OfficeHolders oh ON c.No = oh.CardNo AND oh.GameId = @GameId "
                                                         + " LEFT JOIN Offices o ON o.Id = oh.OfficeId "
                                                         + " WHERE  c.GameId = @GameId AND cv.GameId = @GameId ";
        public readonly string statesmanCardsCurrentSelect = " SELECT c.No as CardNo, c.Type, c.Status, sc.Name, sc.Era, "
                                                                  + " sc.FamilyNo, null, FamilyLetter, sc.Military, sc.Oratory, sc.Loyalty, cv.Influence, cv.Popularity, cv.Knights, cv.PriorConsul, cv.Treasury, "
                                                                  + " sc.DisplayText, sc.DisplayImage, 0 as Income, o.Title as OfficeTitle, o.Rank as OfficeRank, o.Influence as OfficeInfluence, "
                                                                  + " null, 0, 0, 0, 0, null, null, null, 0, 0, 0 "
                                                           + " FROM   Cards c "
                                                           + " JOIN   StatesmanCards sc ON c.No = sc.CardNo "
                                                           + " JOIN   CardVariables cv ON c.No = cv.CardNo "
                                                           + " LEFT JOIN OfficeHolders oh ON c.No = oh.CardNo AND oh.GameId = @GameId "
                                                           + " LEFT JOIN Offices o ON o.Id = oh.OfficeId "
                                                           + " WHERE  c.GameId = @GameId AND cv.GameId = @GameId ";
        public readonly string concessionCardsCurrentSelect = " SELECT c.No as CardNo, c.Type, c.Status, cc.Name, null, "
                                                                   + " 0, null, null, 0, 0, 0, 0, 0, 0, 0, 0, "
                                                                   + " cc.DisplayText, cc.DisplayImage, cc.Income, null, null, null, "
                                                                   + " null, 0, 0, 0, 0, null, null, null, 0, 0, 0 "
                                                            + " FROM   Cards c "
                                                            + " JOIN   ConcessionCards cc ON c.No = cc.CardNo "
                                                            + " WHERE  c.GameId = @GameId ";
        public readonly string intrigueCardsCurrentSelect = " SELECT c.No as CardNo, c.Type, c.Status, ic.Name, ic.Era, "
                                                                 + " 0, null, null, 0, 0, 0, 0, 0, 0, 0, 0, "
                                                                 + " ic.DisplayText, ic.DisplayImage, 0, null, null, null, "
                                                                 + " null, 0, 0, 0, 0, null, null, null, 0, 0, 0 "
                                                          + " FROM   Cards c "
                                                          + " JOIN IntrigueCards ic ON c.No = ic.CardNo "
                                                          + " WHERE  c.GameId = @GameId ";
        public readonly string warCardsCurrentSelect = " SELECT c.No as CardNo, c.Type, c.Status, wc.Name, wc.Era, "
                                                            + " 0, null, null, 0, 0, 0, 0, 0, 0, 0, 0, "
                                                            + " wc.DisplayText, wc.DisplayImage, 0, null, null, null, "
                                                            + " wc.WarName, wc.Legions, wc.FleetSupport, wc.Fleets, wc.Talents, wc.Disasters, wc.StandOffs, wc.DefaultStatus, 0, 0, 0 "
                                                     + " FROM   Cards c "
                                                     + " JOIN   WarCards wc ON c.No = wc.CardNo "
                                                     + " WHERE  c.GameId = @GameId ";
        public readonly string leaderCardsCurrentSelect = " SELECT c.No as CardNo, c.Type, c.Status, lc.Name, lc.Era, "
                                                               + " 0, null, null, 0, 0, 0, 0, 0, 0, 0, 0, "
                                                               + " lc.DisplayText, lc.DisplayImage, 0, null, null, null, "
                                                               + " lc.WarName, 0, 0, 0, 0, lc.Disasters, lc.StandOffs, null, lc.Bonus, 0, 0 "
                                                        + " FROM   Cards c "
                                                        + " JOIN   LeaderCards lc ON c.No = lc.CardNo "
                                                        + " WHERE  c.GameId = @GameId ";
        public readonly string eventCardsCurrentSelect = " SELECT c.No as CardNo, c.Type, c.Status, ec.Name, ec.Era, "
                                                              + " 0, null, null, 0, 0, 0, 0, 0, 0, 0, 0, "
                                                              + " ec.DisplayText, ec.DisplayImage, 0, null, null, null, "
                                                              + " null, 0, 0, 0, 0, null, null, null, 0, ec.Level, ec.MaxLevel "
                                                       + " FROM   Cards c "
                                                       + " JOIN   EventCards ec ON c.No = ec.CardNo "
                                                       + " WHERE  c.GameId = @GameId ";



        public readonly string senatorCardsDefaultSelect = " SELECT CardNo, Type, 'Deck' as Status, Name, Era, "
                                                                + " FamilyNo, HasStatesman, null as FamilyLetter, Military, Oratory, Loyalty, Influence, Popularity, 'N' as PriorConsul, "
                                                                + " DisplayText, DisplayImage, 0 as Income, null as OfficeTitle, null as OfficeRank, null as OfficeInfluence, "
                                                                + " null as WarName, 0 as Legions, 0 as FleetSupport, 0 as Fleets, 0 as Talents, null as Disasters, null as StandOffs, null as DefaultStatus, 0 as Bonus, 0 as Level, 0 as MaxLevel "
                                                         + " FROM   SenatorCards ";
        public readonly string statesmanCardsDefaultSelect = " SELECT CardNo, Type, 'Deck', Name, Era, "
                                                                  + " FamilyNo, null as HasStatesman, FamilyLetter, Military, Oratory, Loyalty, Influence, Popularity, 'N', "
                                                                  + " DisplayText, DisplayImage, 0, null, null, null, "
                                                                  + " null, 0, 0, 0, 0, null, null, null, 0, 0, 0 "
                                                           + " FROM StatesmanCards ";
        public readonly string concessionCardsDefaultSelect = " SELECT CardNo, Type, 'Deck', Name, null, "
                                                                   + " 0, null, null, 0, 0, 0, 0, 0, 'N', "
                                                                   + " DisplayText, DisplayImage, Income, null, null, null, "
                                                                   + " null, 0, 0, 0, 0, null, null, null, 0, 0, 0 "
                                                            + " FROM ConcessionCards ";
        public readonly string intrigueCardsDefaultSelect = " SELECT CardNo, Type, 'Deck', Name, Era, "
                                                                 + " 0, null, null, 0, 0, 0, 0, 0, 'N', "
                                                                 + " DisplayText, DisplayImage, 0, null, null, null, "
                                                                 + " null, 0, 0, 0, 0, null, null, null, 0, 0, 0 "
                                                          + " FROM   IntrigueCards ";
        public readonly string warCardsDefaultSelect = " SELECT CardNo, Type, 'Deck', Name, Era, "
                                                            + " 0, null, null, 0, 0, 0, 0, 0, 'N', "
                                                            + " DisplayText, DisplayImage, 0, null, null, null, "
                                                            + " WarName, Legions, FleetSupport, Fleets, Talents, Disasters, StandOffs, DefaultStatus, 0, 0, 0 "
                                                     + " FROM   WarCards ";
        public readonly string leaderCardsDefaultSelect = " SELECT CardNo, Type, 'Deck', Name, Era, "
                                                               + " 0, null, null, 0, 0, 0, 0, 0, 'N', "
                                                               + " DisplayText, DisplayImage, 0, null, null, null, "
                                                               + " WarName, 0, 0, 0, 0, Disasters, StandOffs, null, Bonus, 0, 0 "
                                                        + " FROM   LeaderCards ";
        public readonly string eventCardsDefaultSelect = " SELECT CardNo, 'Event', 'Deck', Name, Era, "
                                                            + " 0, null, null, 0, 0, 0, 0, 0, 'N', "
                                                            + " DisplayText, DisplayImage, 0, null, null, null, "
                                                            + " null, 0, 0, 0, 0, null, null, null, 0, Level, MaxLevel "
                                                       + " FROM   EventCards "
                                                       + " WHERE  Type = 'DeckEvent' ";

        public CardsSQLiteRepository(SQLiteHelper sqliteHelper)
        {
            this.sqliteHelper = sqliteHelper;
        }

        public List<Card> GetCards()
        {
            var commandText = new StringBuilder();
            commandText.Append(senatorCardsDefaultSelect);
            commandText.Append(" UNION ");
            commandText.Append(statesmanCardsDefaultSelect);
            commandText.Append(" UNION ");
            commandText.Append(concessionCardsDefaultSelect);
            commandText.Append(" UNION ");
            commandText.Append(intrigueCardsDefaultSelect);
            commandText.Append(" UNION ");
            commandText.Append(warCardsDefaultSelect);
            commandText.Append(" UNION ");
            commandText.Append(leaderCardsDefaultSelect);
            commandText.Append(" UNION ");
            commandText.Append(eventCardsDefaultSelect);

            SQLiteCommand command = new SQLiteCommand(commandText.ToString());
            DataTable dt = sqliteHelper.ExecuteQuery(command);
            List<Card> cardList = BuildCards(dt);

            return cardList;
        }

        public List<Card> GetCardsByEra(Era gameEra)
        {
            string defaultWhereClause = " WHERE  Era = '" + gameEra + "' ";
            string eventWhereClause = " AND    Era = '" + gameEra + "' ";
            string senatorWhereClause = string.Empty;

            switch (gameEra)
            {
                case Era.Early:
                    senatorWhereClause = " WHERE Era = '" + Era.Early + "' ";
                    break;
                case Era.Middle:
                    senatorWhereClause = " WHERE Era = '" + Era.Early + "' OR Era = '" + Era.Middle + "' ";
                    break;
                case Era.Late:
                    //No clause needed - return all senators
                    break;
            }

            var commandText = new StringBuilder();
            commandText.Append(senatorCardsDefaultSelect);
            commandText.Append(senatorWhereClause);
            commandText.Append(" UNION ");
            commandText.Append(statesmanCardsDefaultSelect);
            commandText.Append(defaultWhereClause);
            commandText.Append(" UNION ");
            commandText.Append(concessionCardsDefaultSelect);
            commandText.Append(" UNION ");
            commandText.Append(intrigueCardsDefaultSelect);
            commandText.Append(defaultWhereClause);
            commandText.Append(" UNION ");
            commandText.Append(warCardsDefaultSelect);
            commandText.Append(defaultWhereClause);
            commandText.Append(" UNION ");
            commandText.Append(leaderCardsDefaultSelect);
            commandText.Append(defaultWhereClause);
            commandText.Append(" UNION ");
            commandText.Append(eventCardsDefaultSelect);
            commandText.Append(eventWhereClause);

            SQLiteCommand command = new SQLiteCommand(commandText.ToString());
            DataTable dt = sqliteHelper.ExecuteQuery(command);
            List<Card> cardList = BuildCards(dt);

            return cardList;
        }

        public List<Card> GetAllCardsForGame(long gameId)
        {
            var commandText = new StringBuilder();
            commandText.Append(senatorCardsCurrentSelect);
            commandText.Append(" UNION ");
            commandText.Append(statesmanCardsCurrentSelect);
            commandText.Append(" UNION ");
            commandText.Append(concessionCardsCurrentSelect);
            commandText.Append(" UNION ");
            commandText.Append(intrigueCardsCurrentSelect);
            commandText.Append(" UNION ");
            commandText.Append(warCardsCurrentSelect);
            commandText.Append(" UNION ");
            commandText.Append(leaderCardsCurrentSelect);
            commandText.Append(" UNION ");
            commandText.Append(eventCardsCurrentSelect);

            SQLiteCommand command = new SQLiteCommand(commandText.ToString());
            command.Parameters.AddWithValue("GameId", gameId);
            DataTable dt = sqliteHelper.ExecuteQuery(command);
            List<Card> cardList = BuildCards(dt);

            return cardList;
        }

        public List<Card> GetCardsForFaction(long gameId, long factionId)
        {
            var commandText = new StringBuilder();
            commandText.Append(senatorCardsCurrentSelect);
            commandText.Append(" AND c.No in (SELECT CardNo FROM FactionCards WHERE factionId = @FactionId) ");
            commandText.Append(" UNION ");
            commandText.Append(statesmanCardsCurrentSelect);
            commandText.Append(" AND c.No in (SELECT CardNo FROM FactionCards WHERE factionId = @FactionId) ");
            commandText.Append(" UNION ");
            commandText.Append(concessionCardsCurrentSelect);
            commandText.Append(" AND c.No in (SELECT CardNo FROM FactionCards WHERE factionId = @FactionId) ");
            commandText.Append(" UNION ");
            commandText.Append(intrigueCardsCurrentSelect);
            commandText.Append(" AND c.No in (SELECT CardNo FROM FactionCards WHERE factionId = @FactionId) ");
            commandText.Append(" UNION ");
            commandText.Append(eventCardsCurrentSelect);
            commandText.Append(" AND c.No in (SELECT CardNo FROM FactionCards WHERE factionId = @FactionId) ");

            SQLiteCommand command = new SQLiteCommand(commandText.ToString());
            command.Parameters.AddWithValue("GameId", gameId);
            command.Parameters.AddWithValue("FactionId", factionId);
            DataTable dt = sqliteHelper.ExecuteQuery(command);
            List<Card> cardList = BuildCards(dt);

            return cardList;
        }

        public RepositoryActionResult<CardVariables> UpdateCardVariables(CardVariables cardVariables)
        {
            var commandText = new StringBuilder();
            commandText.Append(" UPDATE CardVariables ");
            commandText.Append(" SET    Influence = @Influence, Popularity = @Popularity, Knights = @Knights, PriorConsul = @PriorConsul, Treasury = @Treasury ");
            commandText.Append(" WHERE  CardNo = @CardNo AND GameId = @GameId");

            SQLiteCommand command = new SQLiteCommand(commandText.ToString());
            command.Parameters.AddWithValue("GameId", cardVariables.GameId);
            command.Parameters.AddWithValue("CardNo", cardVariables.CardNo);
            command.Parameters.AddWithValue("Influence", cardVariables.Influence);
            command.Parameters.AddWithValue("Popularity", cardVariables.Popularity);
            command.Parameters.AddWithValue("Knights", cardVariables.Knights);
            command.Parameters.AddWithValue("PriorConsul", cardVariables.PriorConsul ? "Y" : "N");
            command.Parameters.AddWithValue("Treasury", cardVariables.Treasury);
            var result = sqliteHelper.ExecuteNonQuery(command);

            if (result > 0)
            {
                return new RepositoryActionResult<CardVariables>(cardVariables, RepositoryActionStatus.Updated);
            }
            else
            {
                return new RepositoryActionResult<CardVariables>(cardVariables, RepositoryActionStatus.NothingModified);
            }
        }

        public RepositoryActionResult<List<CardVariables>> CreateCardVariables(List<CardVariables> cardVariablesList, long gameId)
        {
            SQLiteCommand command;
            long result;
            try
            {
                foreach (var card in cardVariablesList)
                {
                    command = new SQLiteCommand("INSERT INTO Cards (GameId, No, Type, Status) VALUES (@GameId, @No, @Type, @Status)");
                    command.Parameters.AddWithValue("GameId", gameId);
                    command.Parameters.AddWithValue("No", card.CardNo);
                    command.Parameters.AddWithValue("Type", card.Type.ToString());
                    command.Parameters.AddWithValue("Status", card.Status.ToString());
                    result = sqliteHelper.ExecuteNonQuery(command);

                    command = new SQLiteCommand("INSERT INTO CardVariables (GameId, CardNo, Influence, Popularity, Knights, PriorConsul, Treasury) VALUES (@GameId, @CardNo, @Influence, @Popularity, @Knights, @PriorConsul, @Treasury)");
                    command.Parameters.AddWithValue("GameId", gameId);
                    command.Parameters.AddWithValue("CardNo", card.CardNo);
                    command.Parameters.AddWithValue("Influence", card.Influence);
                    command.Parameters.AddWithValue("Popularity", card.Popularity);
                    command.Parameters.AddWithValue("Knights", card.Knights);
                    command.Parameters.AddWithValue("PriorConsul", card.PriorConsul ? "Y" : "N");
                    command.Parameters.AddWithValue("Treasury", card.Treasury);
                    result = sqliteHelper.ExecuteNonQuery(command);
                }

                return new RepositoryActionResult<List<CardVariables>>(cardVariablesList, RepositoryActionStatus.Created);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<List<CardVariables>>(null, RepositoryActionStatus.Error, ex);
            }
        }

        private List<Card> BuildCards(DataTable dt)
        {
            var cardList = new List<Card>();

            foreach (DataRow dr in dt.Rows)
            {
                switch ((CardType)Enum.Parse(typeof(CardType), Convert.ToString(dr["Type"])))
                {
                    case CardType.Senator:
                        var senator = BuildSenator(dr);
                        cardList.Add(senator);
                        break;
                    case CardType.Statesman:
                        var statesman = BuildStatesman(dr);
                        cardList.Add(statesman);
                        break;
                    case CardType.Concession:
                        var concession = BuildConcession(dr);
                        cardList.Add(concession);
                        break;
                    case CardType.Intrigue:
                        var intrigue = BuildIntrigue(dr);
                        cardList.Add(intrigue);
                        break;
                    case CardType.War:
                        var war = BuildWar(dr);
                        cardList.Add(war);
                        break;
                    case CardType.Leader:
                        var leader = BuildLeader(dr);
                        cardList.Add(leader);
                        break;
                    case CardType.Event:
                        var eventCard = BuildEvent(dr);
                        cardList.Add(eventCard);
                        break;
                }
            }

            return cardList;
        }

        private Senator BuildSenator(DataRow dr)
        {
            return new Senator()
            {
                CardNo = Convert.ToInt32(dr["CardNo"]),
                Type = (CardType)Enum.Parse(typeof(CardType), Convert.ToString(dr["Type"])),
                Status = (CardStatus)Enum.Parse(typeof(CardStatus), Convert.ToString(dr["Status"])),
                Name = Convert.ToString(dr["Name"]),
                Era = (Era)Enum.Parse(typeof(Era), Convert.ToString(dr["Era"])),
                FamilyNo = Convert.ToInt32(dr["FamilyNo"]),
                HasStatesman = Convert.ToString(dr["HasStatesman"]) == "Y" ? true : false,
                Military = Convert.ToInt32(dr["Military"]),
                Oratory = Convert.ToInt32(dr["Oratory"]),
                Loyalty = Convert.ToInt32(dr["Loyalty"]),
                Influence = Convert.ToInt32(dr["Influence"]),
                Popularity = Convert.ToInt32(dr["Popularity"]),
                PriorConsul = Convert.ToString(dr["PriorConsul"]) == "Y" ? true : false,
                Text = Convert.ToString(dr["DisplayText"]),
                Image = Convert.ToString(dr["DisplayImage"]),
                Office = dr["OfficeTitle"] is DBNull ? null : new Office()
                {
                    Title = Convert.ToString(dr["OfficeTitle"]),
                    Rank = Convert.ToInt32(dr["OfficeRank"]),
                    Influence = Convert.ToInt32(dr["OfficeInfluence"])
                }
            };
        }

        private Statesman BuildStatesman(DataRow dr)
        {
            return new Statesman()
            {
                CardNo = Convert.ToInt32(dr["CardNo"]),
                Type = (CardType)Enum.Parse(typeof(CardType), Convert.ToString(dr["Type"])),
                Status = (CardStatus)Enum.Parse(typeof(CardStatus), Convert.ToString(dr["Status"])),
                Name = Convert.ToString(dr["Name"]),
                Era = (Era)Enum.Parse(typeof(Era), Convert.ToString(dr["Era"])),
                FamilyNo = Convert.ToInt32(dr["FamilyNo"]),
                FamilyLetter = Convert.ToString(dr["FamilyLetter"]),
                Military = Convert.ToInt32(dr["Military"]),
                Oratory = Convert.ToInt32(dr["Oratory"]),
                Loyalty = Convert.ToInt32(dr["Loyalty"]),
                Influence = Convert.ToInt32(dr["Influence"]),
                Popularity = Convert.ToInt32(dr["Popularity"]),
                PriorConsul = Convert.ToString(dr["PriorConsul"]) == "Y" ? true : false,
                Text = Convert.ToString(dr["DisplayText"]),
                Image = Convert.ToString(dr["DisplayImage"]),
                Office = dr["OfficeTitle"] is DBNull ? null : new Office()
                {
                    Title = Convert.ToString(dr["OfficeTitle"]),
                    Rank = Convert.ToInt32(dr["OfficeRank"]),
                    Influence = Convert.ToInt32(dr["OfficeInfluence"])
                }
            };
        }

        private Concession BuildConcession(DataRow dr)
        {
            return new Concession()
            {
                CardNo = Convert.ToInt32(dr["CardNo"]),
                Type = (CardType)Enum.Parse(typeof(CardType), Convert.ToString(dr["Type"])),
                Status = (CardStatus)Enum.Parse(typeof(CardStatus), Convert.ToString(dr["Status"])),
                Name = Convert.ToString(dr["Name"]),
                Era = Era.Early,
                Text = Convert.ToString(dr["DisplayText"]),
                Image = Convert.ToString(dr["DisplayImage"]),
                Income = Convert.ToInt32(dr["Income"])
            };
        }

        private Intrigue BuildIntrigue(DataRow dr)
        {
            return new Intrigue()
            {
                CardNo = Convert.ToInt32(dr["CardNo"]),
                Type = (CardType)Enum.Parse(typeof(CardType), Convert.ToString(dr["Type"])),
                Status = (CardStatus)Enum.Parse(typeof(CardStatus), Convert.ToString(dr["Status"])),
                Name = Convert.ToString(dr["Name"]),
                Era = (Era)Enum.Parse(typeof(Era), Convert.ToString(dr["Era"])),
                Text = Convert.ToString(dr["DisplayText"]),
                Image = Convert.ToString(dr["DisplayImage"])
            };
        }

        private War BuildWar(DataRow dr)
        {
            return new War()
            {
                CardNo = Convert.ToInt32(dr["CardNo"]),
                Type = (CardType)Enum.Parse(typeof(CardType), Convert.ToString(dr["Type"])),
                Status = (CardStatus)Enum.Parse(typeof(CardStatus), Convert.ToString(dr["Status"])),
                Name = Convert.ToString(dr["Name"]),
                Era = (Era)Enum.Parse(typeof(Era), Convert.ToString(dr["Era"])),
                Text = Convert.ToString(dr["DisplayText"]),
                Image = Convert.ToString(dr["DisplayImage"]),
                WarName = Convert.ToString(dr["WarName"]),
                Legions = Convert.ToInt32(dr["Legions"]),
                FleetSupport = Convert.ToInt32(dr["FleetSupport"]),
                Fleets = Convert.ToInt32(dr["Fleets"]),
                Talents = Convert.ToInt32(dr["Talents"]),
                Disasters = Convert.ToString(dr["Disasters"]),
                StandOffs = Convert.ToString(dr["StandOffs"]),
                DefaultStatus = (CardStatus)Enum.Parse(typeof(CardStatus), Convert.ToString(dr["DefaultStatus"])),
            };
        }

        private Leader BuildLeader(DataRow dr)
        {
            return new Leader()
            {
                CardNo = Convert.ToInt32(dr["CardNo"]),
                Type = (CardType)Enum.Parse(typeof(CardType), Convert.ToString(dr["Type"])),
                Status = (CardStatus)Enum.Parse(typeof(CardStatus), Convert.ToString(dr["Status"])),
                Name = Convert.ToString(dr["Name"]),
                Era = (Era)Enum.Parse(typeof(Era), Convert.ToString(dr["Era"])),
                Text = Convert.ToString(dr["DisplayText"]),
                Image = Convert.ToString(dr["DisplayImage"]),
                WarName = Convert.ToString(dr["WarName"]),
                Disasters = Convert.ToString(dr["Disasters"]),
                StandOffs = Convert.ToString(dr["StandOffs"]),
                Bonus = Convert.ToInt32(dr["Bonus"])
            };
        }

        private Event BuildEvent(DataRow dr)
        {
            return new Event()
            {
                CardNo = Convert.ToInt32(dr["CardNo"]),
                Type = (CardType)Enum.Parse(typeof(CardType), Convert.ToString(dr["Type"])),
                Status = (CardStatus)Enum.Parse(typeof(CardStatus), Convert.ToString(dr["Status"])),
                Name = Convert.ToString(dr["Name"]),
                Era = (Era)Enum.Parse(typeof(Era), Convert.ToString(dr["Era"])),
                Text = Convert.ToString(dr["DisplayText"]),
                Image = Convert.ToString(dr["DisplayImage"]),
                Level = Convert.ToInt32(dr["Level"]),
                MaxLevel = Convert.ToInt32(dr["MaxLevel"]),
            };
        }
    }
}
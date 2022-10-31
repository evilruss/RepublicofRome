using RoRService.Models.DataModels;
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
    public class OfficesSQLiteRepository : IOfficesRepository
    {
        private SQLiteHelper sqliteHelper;

        public OfficesSQLiteRepository(SQLiteHelper sqliteHelper)
        {
            this.sqliteHelper = sqliteHelper;
        }

        public IEnumerable<Office> GetOffices()
        {
            var commandText = new StringBuilder();
            commandText.Append(" SELECT Id, Title, Rank, Influence ");
            commandText.Append(" FROM   Offices o ");

            SQLiteCommand command = new SQLiteCommand(commandText.ToString());
            DataTable dt = sqliteHelper.ExecuteQuery(command);
            List<Office> officeList = (from DataRow dr in dt.Rows

                                       select new Office()
                                       {
                                           OfficeId = Convert.ToInt64(dr["Id"]),
                                           Title = Convert.ToString(dr["Title"]),
                                           Rank = Convert.ToInt32(dr["Rank"]),
                                           Influence = Convert.ToInt32(dr["Influence"])
                                       }

                                      ).ToList();

            return officeList;
        }

        public Office GetOffice(int officeId)
        {
            var commandText = new StringBuilder();
            commandText.Append(" SELECT Id, Title, Rank, Influence ");
            commandText.Append(" FROM   Offices o ");
            commandText.Append(" WHERE  o.Id = @OfficeId ");

            SQLiteCommand command = new SQLiteCommand(commandText.ToString());
            command.Parameters.AddWithValue("OfficeId", officeId);
            DataTable dt = sqliteHelper.ExecuteQuery(command);

            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];

                var office = new Office()
                {
                    OfficeId = Convert.ToInt64(dr["Id"]),
                    Title = Convert.ToString(dr["Title"]),
                    Rank = Convert.ToInt32(dr["Rank"]),
                    Influence = Convert.ToInt32(dr["Influence"])
                };

                return office;
            }
            else
            {

                return null;
            }
        }

        public IEnumerable<Office> GetOfficesForGame(long gameId)
        {
            var commandText = new StringBuilder();
            commandText.Append(" SELECT o.Id, oh.CardNo, o.Title, o.Rank, o.Influence ");
            commandText.Append("       ,(SELECT sc.Name FROM SenatorCards sc WHERE sc.CardNo = c.No UNION SELECT sc1.Name FROM StatesmanCards sc1 WHERE sc1.CardNo = c.No) as Name ");
            commandText.Append(" FROM   Offices o ");
            commandText.Append(" LEFT JOIN OfficeHolders oh ON o.Id = oh.OfficeId AND oh.GameId = @GameId ");
            commandText.Append(" LEFT JOIN Cards c          ON c.No = oh.CardNo   AND c.GameId = @GameId ");

            SQLiteCommand command = new SQLiteCommand(commandText.ToString());
            command.Parameters.AddWithValue("GameId", gameId);
            DataTable dt = sqliteHelper.ExecuteQuery(command);
            List<Office> officeList = (from DataRow dr in dt.Rows

                                       select new Office()
                                       {
                                           OfficeId = Convert.ToInt64(dr["Id"]),
                                           Title = Convert.ToString(dr["Title"]),
                                           Rank = Convert.ToInt32(dr["Rank"]),
                                           Influence = Convert.ToInt32(dr["Influence"]),
                                           Name = Convert.ToString(dr["Name"])
                                       }

                                      ).ToList();

            return officeList;

        }

        public Office GetOfficeForGame(long gameId, int officeId)
        {
            var commandText = new StringBuilder();
            commandText.Append(" SELECT o.Id, oh.CardNo, o.Title, o.Rank, o.Influence ");
            commandText.Append("       ,(SELECT sc.Name FROM SenatorCards sc WHERE sc.CardNo = c.No UNION SELECT sc1.Name FROM StatesmanCards sc1 WHERE sc1.CardNo = c.No) as Name ");
            commandText.Append(" FROM   Offices o ");
            commandText.Append(" LEFT JOIN OfficeHolders oh ON o.Id = oh.OfficeId AND oh.GameId = @GameId ");
            commandText.Append(" LEFT JOIN Cards c          ON c.No = oh.CardNo   AND c.GameId = @GameId ");
            commandText.Append(" WHERE  o.Id = @OfficeId ");

            SQLiteCommand command = new SQLiteCommand(commandText.ToString());
            command.Parameters.AddWithValue("GameId", gameId);
            command.Parameters.AddWithValue("OfficeId", officeId);
            DataTable dt = sqliteHelper.ExecuteQuery(command);

            if (dt.Rows.Count > 0)
            {
                var dr = dt.Rows[0];

                var office = new Office()
                {
                    OfficeId = Convert.ToInt64(dr["Id"]),
                    Title = Convert.ToString(dr["Title"]),
                    Rank = Convert.ToInt32(dr["Rank"]),
                    Influence = Convert.ToInt32(dr["Influence"]),
                    Name = Convert.ToString(dr["Name"])
                };

                return office;
            }
            else
            {

                return null;
            }
        }

        public RepositoryActionResult<Office> UpdateOffice(long gameId, int officeId, Office office)
        {
            try
            {
                var commandText = new StringBuilder();
                commandText.Append(" UPDATE OfficeHolders ");
                commandText.Append(" SET    CardNo = @CardNo ");
                commandText.Append(" WHERE  OfficeId = @OfficeId ");
                commandText.Append(" AND    GameId = @GameId ");

                SQLiteCommand command = new SQLiteCommand(commandText.ToString());
                command.Parameters.AddWithValue("CardNo", office.CardNo);
                command.Parameters.AddWithValue("GameId", gameId);
                command.Parameters.AddWithValue("OfficeId", officeId);
                var result = sqliteHelper.ExecuteNonQuery(command);

                if (result > 0)
                {
                    return new RepositoryActionResult<Office>(office, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Office>(office, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Office>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Office> CreateOffice(Office office)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand("INSERT INTO OfficeHolders (GameId, OfficeId, CardNo) VALUES (@GameId, @OfficeId, @CardNo)");
                command.Parameters.AddWithValue("GameId", office.GameId);
                command.Parameters.AddWithValue("OfficeId", office.OfficeId);
                command.Parameters.AddWithValue("CardNo", 0);
                var result = sqliteHelper.ExecuteNonQuery(command);

                if (result > 0)
                {
                    office.OfficeId = result;
                    return new RepositoryActionResult<Office>(office, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Office>(office, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Office>(null, RepositoryActionStatus.Error, ex);
            }
        }
    }
}
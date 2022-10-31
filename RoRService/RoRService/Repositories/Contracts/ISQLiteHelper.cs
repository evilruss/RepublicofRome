using System.Data;
using System.Data.SQLite;

namespace RoRService.Repositories.Contracts
{
    public interface ISQLiteHelper
    {
        long ExecuteNonQuery(SQLiteCommand command);
        DataTable ExecuteQuery(SQLiteCommand command);
    }
}

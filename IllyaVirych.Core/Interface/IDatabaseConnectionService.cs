using SQLite;

namespace IllyaVirych.Core.Interface
{
    public interface IDatabaseConnectionService
    {
        SQLiteConnection GetDatebaseConnection();
    }
}

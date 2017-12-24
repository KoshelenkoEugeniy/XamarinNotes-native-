using System.IO;
using SQLite;

namespace XamarinNotesNew.Classes
{
    public static class DBConnection
    {
        public static SQLiteConnection databaseConnection { get; set; }

        public static string appDbPath { get; set; }

        private const string DBName = "NoteDB.db3";

        public static void CreateConnection()
        {
            if (databaseConnection == null)
            {
                databaseConnection = new SQLiteConnection(GetDatabasePath(appDbPath));

                databaseConnection.CreateTable<NoteForDB>();
                databaseConnection.CreateTable<Picture>();
            }
        }

        private static string GetDatabasePath(string path)
        {
            path = Path.Combine(path, DBName);
            return path;
        }

    }
}

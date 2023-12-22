namespace Database;

using Microsoft.Data.Sqlite;
using System.IO;

public class DatabaseInitializer
{
    public void InitializeDatabase(string scriptPath, string connectionString)
    {
        string script = File.ReadAllText(scriptPath);

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = script;
            command.ExecuteNonQuery();
        }
    }
}



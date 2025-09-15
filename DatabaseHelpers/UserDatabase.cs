using Microsoft.Data.Sqlite;

namespace PrivateChat.DatabaseHelpers
{
    class UserDatabase
    {
        private string _filename = "Users.db";
        private string _userTable = "user";
        private string _connectionTable = "connections";
        private string _messagesTable = "messages";

        private async Task<SqliteConnection> OpenConnection()
        {
            var connection = new SqliteConnection($"Data Source={_filename}");
            await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA foreign_keys = ON;";

                await command.ExecuteNonQueryAsync();
            }
            return connection;
        }
        private async Task InitializeTables()
        {
            using (var connection = await OpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @$"
                        CREATE TABLE IF NOT EXISTS {_userTable} (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL UNIQUE,
                        age INTEGER NOT NULL,
                        sex TEXT NOT NULL,
                        password TEXT NOT NULL,
                        username TEXT NOT NULL UNIQUE
                        );
                    ";

                    await command.ExecuteNonQueryAsync();
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @$"
                        CREATE TABLE IF NOT EXISTS {_connectionTable} (
                        connection_id INTEGER PRIMARY KEY AUTOINCREMENT,
                        user1_id INTEGER NOT NULL,
                        user2_id INTEGER NOT NULL,

                        FOREIGN KEY (user1_id) REFERENCES {_userTable}(id),
                        FOREIGN KEY (user2_id) REFERENCES {_userTable}(id)
                        );
                    ";

                    await command.ExecuteNonQueryAsync();
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @$"
                        CREATE TABLE IF NOT EXISTS {_messagesTable} (
                        message_id INTEGER PRIMARY KEY AUTOINCREMENT,
                        connection_id INTEGER NOT NULL,
                        sender INTEGER NOT NULL,
                        message TEXT NOT NULL,
                        date_sent DATETIME DEFAULT CURRENT_TIMESTAMP,

                        FOREIGN KEY (connection_id) REFERENCES {_connectionTable}(connection_id),
                        FOREIGN KEY (sender) REFERENCES {_userTable}(id)
                        );
                    ";

                    await command.ExecuteNonQueryAsync();
                }
            }
        }



    }
}
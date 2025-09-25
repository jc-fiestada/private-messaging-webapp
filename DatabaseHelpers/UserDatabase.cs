using BCrypt.Net;
using Microsoft.Data.Sqlite;
using PrivateChat.Models;

namespace PrivateChat.DatabaseHelpers
{
    class UserDatabase
    {
        private string _filename = "Users.db";
        private string _userTable = "user";
        private string _connectionTable = "connections";
        private string _messagesTable = "messages";


        // Enable Pragma Keys - Default Open Connection
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

        // Run this for every method in database, initializes tables for user, connections, and messages
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
                        sender_id INTEGER NOT NULL,
                        message TEXT NOT NULL,
                        date_sent DATETIME DEFAULT CURRENT_TIMESTAMP,

                        FOREIGN KEY (connection_id) REFERENCES {_connectionTable}(connection_id),
                        FOREIGN KEY (sender_id) REFERENCES {_userTable}(id)
                        );
                    ";

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        // user related
        public async Task InsertUser(User user)
        {
            await InitializeTables();
            using (var connection = await OpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @$"
                        INSERT INTO {_userTable} (name, age, sex, password, username)
                        VALUES (@name, @age, @sex, @password, @username)
                    ";

                    command.Parameters.AddWithValue("@name", user.Name);
                    command.Parameters.AddWithValue("@age", user.Age);
                    command.Parameters.AddWithValue("@sex", user.Sex);
                    command.Parameters.AddWithValue("@password", user.HashedPassword);
                    command.Parameters.AddWithValue("@username", user.Username);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<int> GetUserId(string name)
        {
            int userId = 0;

            await InitializeTables();

            using (var connection = await OpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT id FROM {_userTable} WHERE name = @name";
                    command.Parameters.AddWithValue("@name", name);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userId = Convert.ToInt32(reader["id"]);
                        }
                    }
                }
            }

            return userId;
        }
        public async Task<User?> GetUserInfo(int id)
        {
            await InitializeTables();
            UserDTO? user = new UserDTO();
            using (var connection = await OpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @$"
                        SELECT * FROM {_userTable}
                        WHERE id = @id
                    ";

                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user.ID = Convert.ToInt32(reader["id"]);
                            user.Name = reader["name"].ToString();
                            user.Age = Convert.ToInt32(reader["age"]);
                            user.Sex = reader["sex"].ToString();
                            user.Username = reader["username"].ToString();
                            user.HashedPassword = reader["password"].ToString();
                        }
                    }
                }
            }
            (User? verifiedUser, List<string> errors) = User.ValidateUser(user);
            return verifiedUser;
        }

        public async Task<(bool, int)> ValidateUserSignin(string username, string password)
        {
            await InitializeTables();

            using (var connection = await OpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @$"SELECT password, id FROM {_userTable} WHERE username = @username";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            string hashedPassword = reader["password"].ToString()!;
                            int userId = Convert.ToInt32(reader["id"]);

                            if (BCrypt.Net.BCrypt.Verify(password, hashedPassword))
                            {
                                return (true, userId);
                            }
                            else
                            {
                                return (false, 0);
                            }
                        }
                        else
                        {
                            return (false, 0);
                        }
                    }
                }
            }

        }
        

        // connection related methods
        public async Task InsertConnection(int user1, int user2)
        {
            await InitializeTables();
            using (var connection = await OpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @$"
                        INSERT INTO {_connectionTable} (user1_id, user2_id) 
                        VALUES (@user1, @user2);
                    ";

                    command.Parameters.AddWithValue("@user1", user1);
                    command.Parameters.AddWithValue("@user2", user2);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task InsertMessage(int connectionID, int senderID, string message, DateTime dateSent)
        {
            await InitializeTables();
            using (var connection = await OpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @$"
                        INSERT INTO {_messagesTable} (connection_id, sender_id, message, date_sent)
                        VALUES (@connectionId, @senderId, @message, @dateSent)
                    ";

                    command.Parameters.AddWithValue("@connectionId", connectionID);
                    command.Parameters.AddWithValue("@senderId", senderID);
                    command.Parameters.AddWithValue("@message", message);
                    command.Parameters.AddWithValue("@dateSent", dateSent);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
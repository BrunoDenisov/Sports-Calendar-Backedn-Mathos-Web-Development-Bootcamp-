using Npgsql;
using SportCalendar.Model;
using SportCalendar.RepositoryCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.Repository
{
    public class UserRepository :IUserRepository
    {
        private static string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        public async Task<List<User>> GetAllAsync()
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString); 
            List<User> usersList = new List<User>();
            using (connection)
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM public.\"User\";";
                NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                
                while(await reader.ReadAsync())
                {
                    usersList.Add(
                        new User()
                        {
                            Id = (Guid)reader["Id"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Password = (string)reader["Password"],
                            Email = (string)reader["Email"],
                            RoleId = (Guid)reader["RoleId"],
                            IsActive = (bool)reader["IsActive"],
                            UpdatedByUserId = (Guid)reader["UpdatedByUserId"],
                            DateCreated = (DateTime)reader["DateCreated"],
                            DateUpdated = (DateTime)reader["DateUpdated"],
                            Username = (string)reader["Username"]

                        }) ;
                }
            }
            
            
            return usersList;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            bool isUser = await CheckEntryByUsernameAsync(username);

            if (isUser)
            {
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);

                using (connection)
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM public.\"User\" WHERE \"Username\" = @username";
                    command.Parameters.AddWithValue("@username", username);

                    NpgsqlDataReader reader = command.ExecuteReader();
                    User user = new User();
                    while (await reader.ReadAsync())
                    {
                        user.Id = (Guid)reader["Id"];
                        user.FirstName = (string)reader["FirstName"];
                        user.LastName = (string)reader["LastName"];
                        user.Password = (string)reader["Password"];
                        user.Email = (string)reader["Email"];
                        user.RoleId = (Guid)reader["RoleId"];
                        user.IsActive = (bool)reader["IsActive"];
                        user.UpdatedByUserId = (Guid)reader["UpdatedByUserId"];
                        user.DateCreated = (DateTime)reader["DateCreated"];
                        user.DateUpdated = (DateTime)reader["DateUpdated"];
                        user.Username = (string)reader["Username"];

                                             
                    };
                    return user;
                }
            }
            return null;
        }
        public async Task<List<User>> InsertUserAsync(Guid id, User newUser)
        {
            return null;
        }
        public async Task<List<User>> UpdateUserAsync(string username, User updateUser)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            using (connection)
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
            }
            return null;

        }

        public async Task<List<User>> DeleteUserAsync(string username)
        {
            return null;
        }

        public async Task<bool> CheckEntryByUsernameAsync(string username)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    command.CommandText = "SELECT * FROM public.\"User\" WHERE \"Username\" = @username";
                    command.Parameters.AddWithValue("@username", username);

                    NpgsqlDataReader reader = command.ExecuteReader();

                    return reader.HasRows;
                }
            }
            catch
            { return false; }                       
        }
    }
}

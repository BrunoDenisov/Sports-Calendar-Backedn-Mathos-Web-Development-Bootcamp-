using Npgsql;
using NpgsqlTypes;
using SportCalendar.Common;
using SportCalendar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.Repository
{
    public class AuthRepository
    {
        private static string connectionString = Environment.GetEnvironmentVariable("ConnectionString");

        //This method is used to check and validate the user credentials
        public static AuthUser ValidateUser(string username, string password)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            using (connection)
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;

                string hashPassword = PasswordHasher.HashPassword(password);
                // Set the query with parameters
                command.CommandText = "SELECT public.\"User\".*, \"Role\".\"Access\" FROM \"User\" JOIN \"Role\" ON \"User\".\"RoleId\" = \"Role\".\"Id\" WHERE \"Username\" = @username AND \"Password\" = @password";
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", hashPassword);

                // Execute the query
                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Create a UserMaster object from the data reader
                    AuthUser user = new AuthUser()
                    {
                        // Map the properties from the data reader
                        Username = reader.GetString(reader.GetOrdinal("Username")),
                        Password = reader.GetString(reader.GetOrdinal("Password")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        Role = reader.GetString(reader.GetOrdinal("Access"))
                       
                    };

                    return user;
                }

            }

            return null;
        }
    }
}


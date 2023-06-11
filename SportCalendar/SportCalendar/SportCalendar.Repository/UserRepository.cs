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
    public class UserRepository : IUserRepository
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
        public async Task<User> InsertUserAsync(Guid id, User newUser)
        {
            bool isUser = await CheckEntryByUsernameAsync(newUser.Username);

            if(!isUser)
            {
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);

                using (connection)
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    StringBuilder insertQuery = new StringBuilder("INSERT INTO public.\"User\" VALUES ");
                    insertQuery.Append("(@id, @firstname, @lastname, @password, @email, @roleid, @isactive, @updatedbyuserid, @datecreated, @dateupdated, @username)");
                    command.CommandText = insertQuery.ToString();

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@firstname", newUser.FirstName);
                    command.Parameters.AddWithValue("@lastname", newUser.LastName);
                    command.Parameters.AddWithValue("@password", newUser.Password);
                    command.Parameters.AddWithValue("@email", newUser.Email);
                    command.Parameters.AddWithValue("@roleid", newUser.RoleId);
                    command.Parameters.AddWithValue("@isActive", newUser.IsActive);
                    command.Parameters.AddWithValue("@updatedbyuserid", newUser.UpdatedByUserId);
                    command.Parameters.AddWithValue("@datecreated", DateTime.Now);
                    command.Parameters.AddWithValue("@dateupdated", DateTime.Now);
                    command.Parameters.AddWithValue("@username", newUser.Username);

                    int rowsAffected = command.ExecuteNonQuery();
                    if(rowsAffected > 0)
                    {
                        User addedUser = new User();
                        addedUser = await GetByUsernameAsync(newUser.Username);
                        return addedUser;
                    }
                    return null;
                }
            }
            return null;
        }
        public async Task<User> UpdateUserAsync(string username, User updateUser)
        {
            User user = await GetByUsernameAsync(username);
            if (user != null)
            {
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                using (connection)
                {
                    connection.Open();

                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;

                    StringBuilder updateQuery = new StringBuilder("UPDATE public.\"User\" SET ");

                    if (updateUser.FirstName != null && updateUser.FirstName != user.FirstName)
                    {
                        updateQuery.Append(", \"FirstName\" = @firstname");
                        command.Parameters.AddWithValue("@firstname", updateUser.FirstName);
                    }
                    if (updateUser.LastName != null && updateUser.LastName != user.LastName)
                    {
                        updateQuery.Append(", \"LastName\" = @lastname");
                        command.Parameters.AddWithValue("@lastname", updateUser.LastName);
                    }
                    if (updateUser.Password != null && updateUser.Password != user.Password)
                    {
                        updateQuery.Append(", \"Password\" = @password");
                        command.Parameters.AddWithValue("@password", updateUser.Password);
                    }
                    if (updateUser.Email != null && updateUser.Email != user.Email)
                    {
                        updateQuery.Append(", \"Email\" = @email");
                        command.Parameters.AddWithValue("@email", updateUser.Email);
                    }
                    if (updateUser.RoleId != Guid.Empty && updateUser.RoleId != user.RoleId)
                    {
                        updateQuery.Append(", \"RoleId\" = @roleid");
                        command.Parameters.AddWithValue("@roleid", updateUser.RoleId);
                    }
                    if (updateUser.IsActive != user.IsActive)
                    {
                        updateQuery.Append(", \"IsActive\" = @isactive");
                        command.Parameters.AddWithValue("@isactive", updateUser.IsActive);
                    }
                    // ovdje treba implementirati GetCurrentlyLoggedInUserId, ali tek nakon što se riješi autorizacija
                    //dodati za prvi append
                    if (updateUser.UpdatedByUserId != null && updateUser.UpdatedByUserId != user.UpdatedByUserId)
                    {
                        updateQuery.Append(", \"UpdatedByUserId\" = @updatedbyuserid");
                        command.Parameters.AddWithValue("@updatedbyuserid", updateUser.UpdatedByUserId);
                    }
                    if (updateUser.Username != null && updateUser.Username != user.Username)
                    {
                        updateQuery.Append(", \"Username\" = @username" );
                        command.Parameters.AddWithValue("@username", updateUser.Username);
                    }
                    updateQuery.Append(", \"DateUpdated\" = @dateupdated ");
                    command.Parameters.AddWithValue("@dateupdated", DateTime.Now);

                    updateQuery.Append("WHERE \"Username\" = @username");
                    command.Parameters.AddWithValue("@username", username);

                    //kad se implementira UpdatedByUserId, Replace() je višak
                    updateQuery.Replace("SET ,", "SET ");
                    string query = updateQuery.ToString();
                    command.CommandText = query;
                    
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                    {
                        User updatedUser = new User();
                        updatedUser = await GetByUsernameAsync(username);
                        return updatedUser;                        
                    }
                }
            }            
            return null;
        }

        public async Task<User> DeleteUserAsync(string username)
        {
            User user = await GetByUsernameAsync(username);

            if (user != null)
            {
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);

                using (connection)
                {
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM public.\"User\" WHERE \"Username\" = @username";
                    command.Parameters.AddWithValue("@username", username);

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    if(rowsAffected > 0)
                    {
                        return user;
                    }
                }
            }
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

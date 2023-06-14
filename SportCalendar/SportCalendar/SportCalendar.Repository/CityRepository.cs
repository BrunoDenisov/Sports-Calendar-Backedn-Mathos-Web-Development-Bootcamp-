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
    public class CityRepository : ICityRepository
    {
        //Enviroment varijabla 
        private static string connectionString = Environment.GetEnvironmentVariable("ConnectionString");

        public async Task<List<City>> GetAll()
        {
            List<City> city = new List<City>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM \"City\"";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            City cityatributes = new City();
                            cityatributes.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                            cityatributes.Name = reader.GetString(reader.GetOrdinal("Name"));
                            cityatributes.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                            cityatributes.UpdatedByUserId = reader.GetGuid(reader.GetOrdinal("UpdatedByUserId"));
                            cityatributes.CreatedByUserId = reader.GetGuid(reader.GetOrdinal("CreatedByUserId"));
                            cityatributes.DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated"));
                            cityatributes.DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated"));

                            city.Add(cityatributes);
                        }
                        reader.Close();
                    }
                }
            }
            return city;
        }
    }
}

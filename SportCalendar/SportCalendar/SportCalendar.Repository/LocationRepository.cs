using Npgsql;
using SportCalendar.Common;
using SportCalendar.Model;
using SportCalendar.RepositoryCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.Repository
{
    public class LocationRepository : ILocationRepository
    {

        //Enviroment varijabla 
        private static string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        public async Task<List<Location>> GetAllREST()
        {
            List<Location> locations = new List<Location>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT l.\"Id\", l.\"Venue\", l.\"IsActive\", co.\"Name\" AS CountyName, ci.\"Name\" AS CityName " +
                        "FROM public.\"Location\" l " +
                        "JOIN public.\"County\" co ON l.\"CountyId\" = co.\"Id\" " +
                        "JOIN public.\"City\" ci ON l.\"CityId\" = ci.\"Id\"";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                   
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            Location location = new Location();

                            location.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                            location.Venue = reader.GetString(reader.GetOrdinal("Venue"));
                            location.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                            location.CityName = reader.GetString(reader.GetOrdinal("CityName"));
                            location.CountyName = reader.GetString(reader.GetOrdinal("CountyName"));


                            locations.Add(location);
                        }
                        reader.Close();
                    }
                }
            }
            return locations;
        }
     }
}


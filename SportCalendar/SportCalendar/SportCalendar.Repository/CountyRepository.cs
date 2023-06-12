using Npgsql;
using SportCalendar.Model;
using SportCalendar.ModelCommon;
using SportCalendar.RepositoryCommon;
using SportCalendar.ServiceCommon;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.Repository
{
    public class CountyRepository : ICountyRepository
    {

        public CountyRepository(ICountyService context)
        {
            Context = context;
        }
        public ICountyService Context { get; private set; }

        private static readonly string connectionString = " Server=localhost;Port=44380;User Id=postgres;Password=root;Database='Sport_Calendar'";

        public async Task<List<County>> GetAll()
        {
            List<County> counties = new List<County>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM County";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            County county = new County();
                            county.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                            county.Name = reader.GetString(reader.GetOrdinal("Name"));
                            county.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                            county.UpdatedByUserId = reader.GetGuid(reader.GetOrdinal("UpdatedByUserId"));
                            county.CreatedByUserId = reader.GetGuid(reader.GetOrdinal("CreatedByUserId"));
                            county.DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated"));
                            county.DateUpdated = reader.GetDateTime(reader.GetOrdinal("DateUpdated"));

                            counties.Add(county);
                        }
                        reader.Close();
                    }
                }
            }
            return counties;
        }
    }
}


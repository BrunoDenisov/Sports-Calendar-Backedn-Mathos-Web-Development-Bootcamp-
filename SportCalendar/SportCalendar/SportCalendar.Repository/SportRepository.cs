using SportCalendar.RepositoryCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Npgsql;
using System.Web;
using SportCalendar.Model;

namespace SportCalendar.Repository
{
    
    public class SportRepository : ISportRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DinoConnectionString"].ConnectionString;
        
        public bool Get()
        {
            List<Sport> sports = new List<Sport>();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM \"Sport\"");
                    command.Connection = connection;
                    connection.Open();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        return false;
                    }
                    while (reader.Read())
                    {
                        Sport sport = new Sport();

                        sport.Id = (Guid)reader["Id"];
                        sport.Name = (string)reader["Name"];
                        sport.Type = (string)reader["Type"];
                        sport.IsActive = (bool)reader["IsActive"];
                        sport.CreatedByUserId = (Guid)reader["CreatedByUserId"];
                        sport.UpdatedByUserId = (Guid)reader["UpdatedByUserId"];
                        sport.DateCreated = (DateTime)reader["DateCreated"];
                        sport.DateUpdated = (DateTime)reader["DateUpdated"];

                        sports.Add(sport);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

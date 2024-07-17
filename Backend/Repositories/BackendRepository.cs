using Dapper;
using Microsoft.Data.SqlClient;

namespace Backend.Repositories
{
    public class BackendRepository : IBackendRepository
    {
        private readonly IConfiguration _configuration;

        public BackendRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<TravelAgentInfo>> GetTravelAgentsWithoutObservations()
        {
            try
            {
                string sql =
                    @"
                    SELECT TravelAgent.* FROM TravelAgent
                    LEFT JOIN Observation ON TravelAgent.TravelAgent = Observation.TravelAgent
                    WHERE Observation.TravelAgent IS NULL;
                    "; 
                
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                // Typically connection strings should be in best practice encrypted in appsettings.json, then decrypted by an internal library/nuget package, alternatively System.Security.Cryptography
                await using (var connection = new SqlConnection(connectionString))
                {
                    var travelAgents = (await connection.QueryAsync<TravelAgentInfo>(sql)).ToList();
                    return travelAgents;
                }
            } catch(SqlException ex)
            {
                return null;
            }
        }
        public async Task<List<TravelAgentInfo>> GetTravelAgentsObservedTwiceOrMore()
        {
            try
            {
                string sql =
                    @"
                    SELECT TravelAgent.TravelAgent, COUNT(Observation.TravelAgent) AS ObservationCount
                    FROM TravelAgent
                    JOIN Observation ON TravelAgent.TravelAgent = Observation.TravelAgent
                    GROUP BY TravelAgent.TravelAgent HAVING ObservationCount >= 2;
                    "; 

                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                await using (var connection = new SqlConnection(connectionString))
                {
                    var travelAgents = (await connection.QueryAsync<TravelAgentInfo>(sql)).ToList();
                    return travelAgents;
                }
            }
            catch (SqlException ex)
            {
                return null;
            }
        }
    }
}

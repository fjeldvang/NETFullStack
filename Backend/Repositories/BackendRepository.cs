using Dapper;
using Microsoft.Data.SqlClient;

namespace Backend.Repositories
{
    public class BackendRepository : IBackendRepository
    {
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
                await using (var connection = new SqlConnection("connectionStringGoesHere"))
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
                await using (var connection = new SqlConnection("connectionStringGoesHere"))
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

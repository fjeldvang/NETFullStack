namespace Backend.Repositories
{
    public interface IBackendRepository
    {
        Task<List<TravelAgentInfo>> GetTravelAgentsWithoutObservations();
        Task<List<TravelAgentInfo>> GetTravelAgentsObservedTwiceOrMore();
    }
}
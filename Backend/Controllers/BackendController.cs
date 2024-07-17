using Microsoft.AspNetCore.Mvc;
using Backend.Repositories;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BackendController : Controller
    {
        private readonly IBackendRepository _backendRepository;
        public BackendController(IBackendRepository backendRepository) {
            _backendRepository = backendRepository;
        }

        [HttpGet("GetTravelAgentsWithoutObservations")]
        public async Task<List<TravelAgentInfo>> TravelAgentsWithoutObservations()
        {
            return await _backendRepository.GetTravelAgentsWithoutObservations();
        }

        [HttpGet("GetTravelAgentsObservedTwiceOrMore")]
        public async Task<List<TravelAgentInfo>> TravelAgentsObservedTwiceOrMore()
        {
            return await _backendRepository.GetTravelAgentsObservedTwiceOrMore();
        }
    }
}

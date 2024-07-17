namespace Backend.Services
{
    public interface IBackendService
    {
        IEnumerable<string> GetRepeatedGuestNames(List<InvoiceGroup> invoiceGroups);
        IEnumerable<TravelAgentInfo> GetNumOfNightsByTravelAgent(List<InvoiceGroup> invoiceGroups);
    }
}
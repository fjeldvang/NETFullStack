namespace Backend.Services
{
    public class BackendService : IBackendService
    {
        public IEnumerable<string> GetRepeatedGuestNames(List<InvoiceGroup> invoiceGroups)
        {
            return invoiceGroups
                .SelectMany(InvoiceGroup => InvoiceGroup.Invoices.SelectMany(i => i.Observations.Select(o => o.GuestName)))
                .GroupBy(guestName => guestName)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key);
        }

        public IEnumerable<TravelAgentInfo> GetNumOfNightsByTravelAgent(List<InvoiceGroup> invoiceGroups)
        {
            return invoiceGroups
                .Where(InvoiceGroup => InvoiceGroup.IssueDate.Year == 2015)
                .SelectMany(InvoiceGroup => InvoiceGroup.Invoices.SelectMany(inv => inv.Observations))
                .GroupBy(o => o.TravelAgent)
                .Select(group => new TravelAgentInfo
                {
                    TravelAgent = group.Key,
                    TotalNumberOfNights = group.Sum(o => o.NumberOfNights)
                });
        }
    }
}

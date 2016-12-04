using BikeSharing.Clients.Core.Models.ReportIncident;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.DataServices.Interfaces
{
    public interface IFeedbackService
    {
        Task SendIssueAsync(ReportedIssue issue);
    }
}

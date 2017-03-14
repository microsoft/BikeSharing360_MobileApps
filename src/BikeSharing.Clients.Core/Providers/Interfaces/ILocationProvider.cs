using BikeSharing.Clients.Core.Models;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.Services
{
    public interface ILocationProvider
    {
        Task<ILocationResponse> GetPositionAsync();
    }
}

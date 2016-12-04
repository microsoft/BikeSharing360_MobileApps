using BikeSharing.Clients.Core.Models;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.DataServices.Interfaces
{
    public interface IWeatherService
    {
        Task<IWeatherResponse> GetWeatherInfoAsync();
    }
}

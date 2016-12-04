using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.Services.Interfaces
{
    public interface IMediaPickerService
    {
        Task<string> PickImageAsBase64String();
    }
}

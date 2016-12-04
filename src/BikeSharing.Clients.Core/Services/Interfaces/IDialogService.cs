using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.Services
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
    }
}

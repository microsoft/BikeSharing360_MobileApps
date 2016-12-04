using Acr.UserDialogs;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.Services
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }
    }
}

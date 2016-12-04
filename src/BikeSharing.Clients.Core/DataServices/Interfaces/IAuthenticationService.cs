using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.DataServices
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }

        Task<bool> LoginAsync(string userName, string password);

        Task LogoutAsync();

        int GetCurrentUserId();
    }
}

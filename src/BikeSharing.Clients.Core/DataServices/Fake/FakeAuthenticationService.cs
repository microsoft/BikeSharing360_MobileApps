using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.DataServices
{
    public class FakeAuthenticationService : IAuthenticationService
    {
        private static bool AuthSucceded;

        public bool IsAuthenticated
        {
            get
            {
                return AuthSucceded;
            }
        }

        public Task<bool> LoginAsync(string userName, string password)
        {
            bool succeeded = false;

            if (userName == "aaa")
            {
                succeeded = true;
            }

            AuthSucceded = succeeded;

            return Task.FromResult(succeeded);
        }

        public Task LogoutAsync()
        {
            AuthSucceded = false;

            return Task.FromResult(false);
        }

        public int GetCurrentUserId()
        {
            return 1;
        }
    }
}

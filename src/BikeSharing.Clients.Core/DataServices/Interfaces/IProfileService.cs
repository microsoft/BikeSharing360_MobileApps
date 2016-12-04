using BikeSharing.Clients.Core.Models.Users;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.DataServices
{
    public interface IProfileService
    {
        Task<UserProfile> GetCurrentProfileAsync();

        Task<UserAndProfileModel> SignUp(UserAndProfileModel profile);

        Task UploadUserImageAsync(UserProfile profile, string imageAsBase64);
    }
}
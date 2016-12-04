using BikeSharing.Clients.Core.Models;
using BikeSharing.Clients.Core.Models.Users;
using System;
using System.Threading.Tasks;

namespace BikeSharing.Clients.Core.DataServices
{
    public class FakeProfileService : IProfileService
    {
        public Task<UserProfile> GetCurrentProfileAsync()
        {
            var userProfile = new UserProfile
            {
                Id = 1,
                BirthDate = DateTime.Now,
                FirstName = "John",
                LastName = "Doe",
                Gender = 1,
                UserId = 1
            };

            return Task.FromResult(userProfile);
        }

        public Task<UserAndProfileModel> SignUp(UserAndProfileModel profile)
        {
            var userProfile = new UserAndProfileModel
            {
                UserName = "johndoe",
                Password = "12345",
                Email = "johndoe@mail.com",
                Skype = "me.skype",
                BirthDate = DateTime.Now,
                FirstName = "John",
                LastName = "Doe",
                Gender = "Male"
            };

            return Task.FromResult(userProfile);
        }

        public Task UploadUserImageAsync(UserProfile profile, string imageAsBase64)
        {
            return Task.FromResult(true);
        }
    }
}
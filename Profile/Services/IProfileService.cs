using Profile.Models;
using Profile.ResponsResults;

namespace Profile.Services
{
    public interface IProfileService
    {
        Task<ProfileResult<bool>> CreateAsync(ProfileRegistrationFrom form);
        Task<ProfileResult<UserProfile>> GetByIdAsync(string id);
        Task<ProfileResult<UserProfile>> UpdateAsync(ProfileRegistrationFrom form);
    }
}
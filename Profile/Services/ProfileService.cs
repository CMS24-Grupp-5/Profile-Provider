using Profile.Data;
using Profile.Data.Repositories;
using Profile.Extentions;
using Profile.Models;
using Profile.ResponsResults;

namespace Profile.Services
{
    public class ProfileService(IProfileRepository profileRepository) : IProfileService
    {
        private readonly IProfileRepository _profileRepository = profileRepository;

        public async Task<ProfileResult<bool>> CreateAsync(ProfileRegistrationFrom form)
        {


            var entity = form.MapTo<UserProfileEntity>();
            var result = await _profileRepository.CreateAsync(entity);

            return new ProfileResult<bool>
            {
                Message = result.Message,
                StatusCode = result.StatusCode,
                Success = result.Succeeded,
            };

        }

        public async Task<ProfileResult<UserProfile>> UpdateAsync(ProfileRegistrationFrom form)
        {


            var entity = form.MapTo<UserProfileEntity>();
            var result = await _profileRepository.UpdateAsync(entity);

            return new ProfileResult<UserProfile>
            {
                Message = result.Message,
                StatusCode = result.StatusCode,
                Success = result.Succeeded,
                Result = result.Result,
            };

        }


        public async Task<ProfileResult<UserProfile>> GetByIdAsync(string id)
        {


            var result = await _profileRepository.GetByIdAsync(id);

            return new ProfileResult<UserProfile>
            {
                Message = result.Message,
                StatusCode = result.StatusCode,
                Success = result.Succeeded,
                Result = result.Result
            };

        }
    }
}

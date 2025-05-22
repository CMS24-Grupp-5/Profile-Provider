using Profile.Models;
using Profile.ResponsResults;

namespace Profile.Data.Repositories
{
    public interface IProfileRepository
    {
        Task<RepositoryResult<bool>> CreateAsync(UserProfileEntity entity);
        Task<RepositoryResult<UserProfile>> GetByIdAsync(string id);
        Task<RepositoryResult<UserProfile>> UpdateAsync(UserProfileEntity entity);
    }
}
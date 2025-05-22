using Microsoft.EntityFrameworkCore;
using Profile.Extentions;
using Profile.Models;
using Profile.ResponsResults;

namespace Profile.Data.Repositories;

public class ProfileRepository(DataContext dataContext) : IProfileRepository
{
    private readonly DataContext _DataContext = dataContext;

    public async Task<RepositoryResult<bool>> CreateAsync(UserProfileEntity entity)
    {
        if (entity == null)
        {
            return new RepositoryResult<bool>
            {
                Succeeded = false,
                StatusCode = 400,
                Message = "Entity can't be null.",
                Result = false
            };
        }

        try
        {
            _DataContext.Add(entity);
            await _DataContext.SaveChangesAsync();

            return new RepositoryResult<bool>
            {
                Succeeded = true,
                StatusCode = 201, 
                Message = "User profile created successfully",
            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<bool>
            {
                Succeeded = false,
                StatusCode = 500,
                Message = ex.Message
            };
        }
    }

    public async Task<RepositoryResult<UserProfile>> UpdateAsync(UserProfileEntity entity)
    {
        if (entity == null)
        {
            return new RepositoryResult<UserProfile>
            {
                Succeeded = false,
                StatusCode = 400,
                Message = "Entity can't be null."
            };
        }

        try
        {
            _DataContext.Update(entity);
            await _DataContext.SaveChangesAsync();

            return new RepositoryResult<UserProfile>
            {
                Succeeded = true,
                StatusCode = 200,
                Message = "User profile updated successfully",
                Result = entity.MapTo<UserProfile>()
            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<UserProfile>
            {
                Succeeded = false,
                StatusCode = 500,
                Message = ex.Message
            };
        }
    }

    public async Task<RepositoryResult<UserProfile>> GetByIdAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return new RepositoryResult<UserProfile>
            {
                Succeeded = false,
                StatusCode = 400,
                Message = "Id can't be null or empty"
            };
        }
        try
        {
            var entity = await _DataContext.UserProfiles.FindAsync(id);
            if (entity == null)
            {
                return new RepositoryResult<UserProfile>
                {
                    Succeeded = false,
                    StatusCode = 404,
                    Message = "Profile not found"
                };
            }

            return new RepositoryResult<UserProfile>
            {
                Succeeded = true,
                StatusCode = 200,
                Message = "Profile found",
                Result = entity.MapTo<UserProfile>()
            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<UserProfile>
            {
                Succeeded = false,
                StatusCode = 500,
                Message = ex.Message
            };
        }
    }
}

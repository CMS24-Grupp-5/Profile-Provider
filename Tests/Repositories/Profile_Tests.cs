using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Profile.Data;
using Profile.Data.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Tests.Repositories;

public class Profile_Tests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly IProfileRepository _profileRepository;

    public Profile_Tests(WebApplicationFactory<Program> factory)
    {
        var scopeFactory = factory.Services.CreateScope();
        _profileRepository = scopeFactory.ServiceProvider.GetRequiredService<IProfileRepository>();
    }

    [Fact]
    public async Task Create_UserProfile__Should_Return_Ture()
    {
        var entity = new UserProfileEntity
        {
            FirstName = "Yaarub",
            LastName = "Nassr",
            PhoneNumber = "1234567890",
            UserId = Guid.NewGuid().ToString()
        };

        var result = await _profileRepository.CreateAsync(entity);

        Assert.True(result.Succeeded);
    }

    [Fact]
    public async Task Update_UserProfile__Should_Return_Ture()
    {
        var entity = new UserProfileEntity
        {
            FirstName = "Yaarub",
            LastName = "Nassr",
            PhoneNumber = "1234567890",
            UserId = "5"
        };

        var updateEntity = new UserProfileEntity
        {
            FirstName = "Hadil",
            LastName = "Linda",
            PhoneNumber = "1234567890",
            UserId = "5"
        };

        var result = await _profileRepository.CreateAsync(entity);
        var updateResult = await _profileRepository.UpdateAsync(updateEntity);
        var updated = await _profileRepository.GetByIdAsync("5");
        Assert.Equal("Hadil", updated.Result!.FirstName);
        Assert.Equal("Linda", updated.Result.LastName);
    }


    [Fact]
    public async Task GetById__Should_Return_UserProfile_WithSameId()
    {
        var entity = new UserProfileEntity
        {
            FirstName = "Yaarub",
            LastName = "Nassr",
            PhoneNumber = "1234567890",
            UserId = "6"
        };
        
    

        var result = await _profileRepository.CreateAsync(entity);
  
        var updated = await _profileRepository.GetByIdAsync("6");
        Assert.Equal("Yaarub", updated.Result!.FirstName);
        Assert.Equal("Nassr", updated.Result.LastName);
        Assert.Equal("1234567890", updated.Result.PhoneNumber);
        Assert.Equal("6", updated.Result.UserId);
    }
}

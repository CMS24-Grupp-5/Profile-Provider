using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Profile.Models;
using Profile.Services;

namespace Tests.Services;

public class ProfileService_Tests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly IProfileService _profileService;

    public ProfileService_Tests(WebApplicationFactory<Program> factory)
    {
        var scope = factory.Services.CreateScope();
        _profileService = scope.ServiceProvider.GetRequiredService<IProfileService>();
    }

    [Fact]
    public async Task CreateAsync__Should_CreateAProfileAnd_Return_True()
    {
        var profile = new ProfileRegistrationFrom
        {
            FirstName = "Yaarub",
            LastName = "Nassr",
            PhoneNumber = "0700295829",
            UserId = "9"
        };

        var result = await _profileService.CreateAsync(profile);

        Assert.True(result.Success);
        Assert.Equal(201, result.StatusCode);
    }

    [Fact]
    public async Task UpdateAsync__Should_UpdateAProfileAnd_Return_True()
    {
        var profile = new ProfileRegistrationFrom
        {
            FirstName = "Yaarub",
            LastName = "Nassr",
            PhoneNumber = "0700295829",
            UserId = "1"
        };

        var result = await _profileService.CreateAsync(profile);


        var newProfile = new ProfileRegistrationFrom
        {
            FirstName = "Hadil",
            LastName = "Linda",
            PhoneNumber = "0736726523",
            UserId = "1"
        };

        var resultUpdate = await _profileService.UpdateAsync(newProfile);
        Assert.True(result.Success);         
        Assert.True(result.Success);
        Assert.Equal(201, result.StatusCode);
        Assert.Equal(200, resultUpdate.StatusCode);
        Assert.Equal("Hadil", resultUpdate.Result!.FirstName);
        Assert.Equal("Linda", resultUpdate.Result!.LastName);
        Assert.Equal("1", resultUpdate.Result!.UserId);

    }
    [Fact]
    public async Task GetByIdAsync__Should_GetAProfileByIdAnd_Return_True()
    {
        var profile = new ProfileRegistrationFrom
        {
            FirstName = "Yaarub",
            LastName = "Nassr",
            PhoneNumber = "0700295829",
            UserId = "11"
        };

        var result = await _profileService.CreateAsync(profile);



        var resultProfile = await _profileService.GetByIdAsync("11");
        Assert.True(result.Success);         
        Assert.True(result.Success);
        Assert.Equal(201, result.StatusCode);
        Assert.Equal(200, resultProfile.StatusCode);
        Assert.Equal("Yaarub", resultProfile.Result!.FirstName);
        Assert.Equal("Nassr", resultProfile.Result!.LastName);
        Assert.Equal("11", resultProfile.Result!.UserId);

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Tests.Repositories;

public class ProfileApiTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Create_Profile__Should_ReturnSuccess()
    {
        var profile = new
        {
            firstName = "Yaarub",
            lastName = "Nassr",
            phoneNumber = "1234567890",
            userId = Guid.NewGuid().ToString()
        };

        var response = await _client.PostAsJsonAsync("/api/profiles/create", profile);

        response.EnsureSuccessStatusCode();
        Assert.Equal(StatusCodes.Status201Created, (int)response.StatusCode);
    }


    [Fact]
    public async Task Update_Profile__Should_ReturnSuccess()
    {
        var profile = new
        {
            firstName = "Yaarub",
            lastName = "Nassr",
            phoneNumber = "1234567890",
            userId = "1"
        };

        var createresponse = await _client.PostAsJsonAsync("/api/profiles/create", profile);

        var updateProfile = new
        {
            firstName = "Hadil",
            lastName = "Linda",
            phoneNumber = "0987645543",
            userId = "1"
        };

        var updateresponse= await _client.PutAsJsonAsync("/api/profiles/update", updateProfile);
        Assert.Equal(StatusCodes.Status201Created, (int)createresponse.StatusCode);
        updateresponse.EnsureSuccessStatusCode();
        Assert.Equal(StatusCodes.Status200OK, (int)updateresponse.StatusCode);
    }

    [Fact]
    public async Task GetById_Profile__Should_ProfileList()
    {
        var payload = new
        {
            firstName = "Yaarub",
            lastName = "Nassr",
            phoneNumber = "1234567890",
            userId = "2"
        };

        var userId = "2";
        

        var response = await _client.PostAsJsonAsync("/api/profiles/create", payload);
     
        var getResponse = await _client.GetAsync($"/api/profiles/{userId}");


        Assert.Equal(StatusCodes.Status201Created, (int)response.StatusCode);

        getResponse.EnsureSuccessStatusCode();
        Assert.Equal(StatusCodes.Status200OK, (int)getResponse.StatusCode);
    
    }
}
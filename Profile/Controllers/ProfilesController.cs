using Microsoft.AspNetCore.Mvc;
using Profile.Models;
using Profile.Services;

namespace Profile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController(IProfileService profileService) : ControllerBase
    {
        private readonly IProfileService _profileService = profileService;

        [HttpPost("create")]
        public async Task<IActionResult> Create(ProfileRegistrationFrom form)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _profileService.CreateAsync(form);

            return StatusCode(result.StatusCode, result.Success);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Id is required.");

            var result = await _profileService.GetByIdAsync(id);

            if (!result.Success)
                return StatusCode(result.StatusCode,result.Result);

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(ProfileRegistrationFrom form)
        {                                                 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _profileService.UpdateAsync(form);

            return StatusCode(result.StatusCode,result.Result);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profile.Data;
using Profile.Models;

namespace Profile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpPost("create")]

        public async Task <IActionResult> Create(ProfileRegistrationFrom form)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profileEntity = new UserProfileEntity
            {
                UserId = form.UserId,
                FirstName = form.FirstName,
                LastName = form.LastName,
                PhoneNumber = form.PhoneNumber
            };


            _context.Add(profileEntity);
            await _context.SaveChangesAsync();

            return Ok();

        }

      
    }
}

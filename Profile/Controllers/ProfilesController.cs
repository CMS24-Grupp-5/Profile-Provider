using Microsoft.AspNetCore.Mvc;
using Profile.Extentions.Attributes;
using Profile.Models;
using Profile.Services;

namespace Profile.Controllers
{
    /// <summary>
    /// API-kontroller för hantering av användarprofiler.
    /// Skyddas med API-nyckel via <see cref="ApiKeyAttribute"/>.
    /// </summary>
    [ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController(IProfileService profileService) : ControllerBase
    {
        private readonly IProfileService _profileService = profileService;

        /// <summary>
        /// Skapar en ny användarprofil.
        /// </summary>
        /// <param name="form">Formulärdata för registrering av profil.</param>
        /// <returns>
        /// HTTP 201 Created vid lyckad skapelse eller relevant HTTP-felkod om misslyckad.
        /// </returns>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(ProfileRegistrationFrom form)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _profileService.CreateAsync(form);
            return StatusCode(result.StatusCode, result.Success);
        }

        /// <summary>
        /// Hämtar en användarprofil baserat på angivet ID.
        /// </summary>
        /// <param name="id">Unikt ID för den profil som ska hämtas.</param>
        /// <returns>
        /// HTTP 200 OK med profilinformation vid lyckad hämtning,
        /// HTTP 400 vid ogiltigt ID,
        /// HTTP 404 om profil saknas.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Id is required.");

            var result = await _profileService.GetByIdAsync(id);

            if (!result.Success)
                return StatusCode(result.StatusCode, result.Result);

            return Ok(result.Result);
        }

        /// <summary>
        /// Uppdaterar en befintlig användarprofil.
        /// </summary>
        /// <param name="form">Formulärdata för uppdatering av profil.</param>
        /// <returns>
        /// HTTP 200 OK vid lyckad uppdatering,
        /// HTTP 400 Bad Request vid valideringsfel,
        /// HTTP 404 Not Found om profil saknas,
        /// eller annan relevant HTTP-statuskod vid fel.
        /// </returns>
        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(ProfileRegistrationFrom form)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _profileService.UpdateAsync(form);

            return StatusCode(result.StatusCode, result.Result);
        }
    }
}

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
        /// <see cref="IActionResult"/> som representerar resultatet av operationen.
        /// Returnerar HTTP 200 eller annan statuskod beroende på resultatet.
        /// </returns>
        [HttpPost("create")]
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
        /// <param name="id">Sträng-ID för den profil som ska hämtas.</param>
        /// <returns>
        /// <see cref="IActionResult"/> med användarprofildata om den finns.
        /// Returnerar 400 om ID saknas, 404 eller annan statuskod om profil inte hittas.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Id is required.");

            var result = await _profileService.GetByIdAsync(id);

            if (!result.Success)
                return StatusCode(result.StatusCode, result.Result);

            return Ok(result);
        }

        /// <summary>
        /// Uppdaterar en befintlig användarprofil.
        /// </summary>
        /// <param name="form">Formulärdata för uppdatering av profil.</param>
        /// <returns>
        /// <see cref="IActionResult"/> med uppdateringsresultat.
        /// Returnerar lämplig HTTP-statuskod och eventuell felinformation.
        /// </returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update(ProfileRegistrationFrom form)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _profileService.UpdateAsync(form);

            return StatusCode(result.StatusCode, result.Result);
        }
    }
}

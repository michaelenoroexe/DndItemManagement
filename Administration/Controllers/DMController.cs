using Administration.Service.Contracts;
using Administration.Shared.DataTransferObjects.DM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Administration.Controllers
{
    [ApiController]
    [Route("/api/dm")]
    public class DMController : ControllerBase
    {
        private readonly IDMService dmService;
        private readonly IAuthenticationService authService;

        public DMController(IDMService dmService, IAuthenticationService authService)
        {
            this.dmService = dmService;
            this.authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDms() 
        {
            var dms = await dmService.GetAllDMs(false);
            return Ok(dms);
        }
        [HttpGet("full")]
        public async Task<IActionResult> GetDm()
        {
            var name = User.FindFirst(ClaimTypes.Name)?.Value;
            if (name is null) return NoContent();
            if (!int.TryParse(name, out int dmId)) return BadRequest("Not valid dm id in token.");
            var dm = await dmService.GetDMAsync(dmId, false);

            return Ok(dm);
        }
        [HttpPost]
        public async Task<IActionResult> RegisterDm([FromBody] DMForRegistrationDto dMForRegistration) 
        {
            var dm = await dmService.RegisterDMAsync(dMForRegistration);

            return Created("/api/dm", dm);
        }
        [HttpPost("login")]
        public async Task<IActionResult> SignInDm([FromBody] DMForAuthenticationDto dMForAuth)
        {
            var valDm = await authService.ValidateDM(dMForAuth);
            if (valDm is null) return ValidationProblem();
            var character = User.FindFirst(ClaimTypes.Actor)?.Value;
            var token = authService
                .CreateToken(Convert.ToInt32(valDm.Id), (character is null)? null : Convert.ToInt32(character));
            
            return Ok(new { token = $"{token}" });
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDm(int id) 
        {
            await dmService.DeleteDMAsync(id);

            return NoContent();
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDm(int id, [FromBody] DMForUpdateDto dMForUpdate) 
        {
            await dmService.UpdateDMAsync(id, dMForUpdate, true);

            return NoContent();
        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateDm(int id,
            [FromBody] DMForUpdateDto dMForUpdate) 
        {
            await dmService.PartialUpdateDMAsync(id, dMForUpdate, true);
            
            return NoContent();
        }
        [HttpOptions]
        public IActionResult DmOptions()
        {
            Response.Headers.Add("Allow",
                "GET, " +
                "POST, " +
                "PUT, " +
                "DELETE, " +
                "OPTIONS"
                );
            return Ok();
        }
    }
}

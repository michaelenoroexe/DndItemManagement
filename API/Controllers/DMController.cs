using API.ActionFilters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.DM;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/dm")]
    public class DMController : ControllerBase
    {
        private readonly IServiceManager service;

        public DMController(IServiceManager service) => this.service = service;

        [HttpGet]
        public async Task<IActionResult> GetDms() 
        {
            var dms = await service.DMService.GetAllDMs(false);
            return Ok(dms);
        }
        [HttpGet("full")]
        public async Task<IActionResult> GetDm()
        {
            var name = User.FindFirst(ClaimTypes.Name)?.Value;
            if (name is null) return NoContent();
            var dm = await service.DMService.GetDMAsync(name, false);
            return Ok(dm);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterDm([FromBody] DMForRegistrationDto dMForRegistration) 
        { 
            var dm = await service.DMService.RegisterDMAsync(dMForRegistration);

            return Created("/api/dm", dm);
        }
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> SignInDm([FromBody] DMForAuthenticationDto dMForAuth)
        {
            var res = await service.AuthenticationService.ValidateDM(dMForAuth);
            if (res == false) return ValidationProblem();
            var character = User.FindFirst(ClaimTypes.Actor)?.Value;
            var token = service.AuthenticationService
                .CreateToken(dMForAuth.Login, (character is null)? null : Convert.ToInt32(character));

            return Ok(new { token = $"{token}" });
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDm(int id) 
        {
            await service.DMService.DeleteDMAsync(id);

            return NoContent();
        }
        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateDm(int id, [FromBody] DMForUpdateDto dMForUpdate) 
        {
            await service.DMService.UpdateDMAsync(id, dMForUpdate, true);

            return NoContent();
        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PartiallyUpdateDm(int id,
            [FromBody] DMForUpdateDto dMForUpdate) 
        {
            await service.DMService.PartialUpdateDMAsync(id, dMForUpdate, true);
            
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

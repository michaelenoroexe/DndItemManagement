using API.ActionFilters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.DM;

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
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterDm([FromBody] DMForRegistrationDto dMForRegistration) 
        { 
            var dm = await service.DMService.RegisterDMAsync(dMForRegistration);

            return Created("/api/dm", dm);
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
            [FromBody] JsonPatchDocument<DMForUpdateDto> patchDoc) 
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");

            var result = await service.DMService.GetDMForPatchAsync(id, true);

            patchDoc.ApplyTo(result.dmToPatch, ModelState);

            TryValidateModel(result.dmToPatch);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await service.DMService.SaveChangesForPatchAsync(result.dmToPatch, result.dmEntity);

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

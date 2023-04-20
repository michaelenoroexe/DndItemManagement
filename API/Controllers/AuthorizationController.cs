using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        [HttpPost]
        [Authorize]
        public IActionResult SignIn()
        {
            

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, )
        }
    }
}

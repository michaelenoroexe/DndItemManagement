using Microsoft.AspNetCore.Mvc.Filters;

namespace API.ActionFilters
{
    public class DMAuthorizeAttribute : IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //var currDmLogin = context.HttpContext.User.;
        }
    }
}

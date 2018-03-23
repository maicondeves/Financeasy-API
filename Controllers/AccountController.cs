using System.Web.Http;
using Financeasy.Api.Applications;
using Financeasy.Api.Authentication;
using Financeasy.Api.Core;
using Financeasy.Api.Core.DI;

namespace Financeasy.Api.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : WebApiController
    {
        [Inject]
        private UserApplication _userApplication { get; set; }

        [Inject]
        private AuthenticationProvider _authProvider { get; set; }

        //[Route("register")]
        //[Route("authenticate")]
    }
}

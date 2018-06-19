using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Financeasy.Api.Applications;
using Financeasy.Api.Authentication;
using Financeasy.Api.Core;
using Financeasy.Api.Core.DI;

namespace Financeasy.Api.Controllers
{
    [RoutePrefix("dashboard")]
    public class DashboardController : WebApiController
    {
        [Inject]
        private DashboardApplication _dashboardApplication { get; set; }
        
        [Inject]
        private AuthenticationProvider _authProvider { get; set; }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var auth = _authProvider.Authenticate(Request);

            if (!auth.IsAuthenticated)
                return Response(auth.StatusCode, auth.Message);

            return Response(HttpStatusCode.OK, _dashboardApplication.GetDashboard(auth.UserId));
        }

    }
}

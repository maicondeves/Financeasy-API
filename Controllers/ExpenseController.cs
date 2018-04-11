using System.Web.Http;
using Financeasy.Api.Applications;
using Financeasy.Api.Authentication;
using Financeasy.Api.Core;
using Financeasy.Api.Core.DI;

namespace Financeasy.Api.Controllers
{
    [RoutePrefix("expenses")]
    public class ExpenseController : WebApiController
    {
        [Inject]
        private ExpenseApplication _expenseApplication { get; set; }

        [Inject]
        private AuthenticationProvider _authProvider { get; set; }
    }
}

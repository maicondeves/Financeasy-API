using System.Net;

namespace Financeasy.Api.Authentication
{
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; }
        public long UserId { get; set; }

        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
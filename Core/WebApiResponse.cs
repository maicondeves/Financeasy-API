using System.Net;

namespace Financeasy.Api.Core
{
    public class WebApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public object Content { get; set; }
    }
}
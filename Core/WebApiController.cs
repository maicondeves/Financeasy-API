using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;

namespace Financeasy.Api.Core
{
    public class WebApiController : ApiController
    {
        protected HttpResponseMessage Response(HttpStatusCode status, object entity)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);

            if (status == HttpStatusCode.Unauthorized)
            {
                //response.StatusCode = HttpStatusCode.Unauthorized;
                response.Headers.Add("WWW-Authenticate", "Basic realm=\"Financeasy\"");
            }
            
            var content = new WebApiResponse()
            {
                StatusCode = status,
                Content = entity
            };
            
            response.Content = new StringContent(JsonConvert.SerializeObject(content, Formatting.Indented), Encoding.UTF8, "application/json");
            return response;
        }
    }
}
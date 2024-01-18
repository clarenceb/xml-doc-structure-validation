using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace XmlDocStructureValidation
{
    public class MessageSizeLimitFunc
    {
        private readonly ILogger _logger;

        public MessageSizeLimitFunc(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MessageSizeLimitFunc>();
        }

        [Function("MessageSizeLimitFunc")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // Check the request message size does not exceed 2560KB.
            // If it does, return HTTP 400 Bad Request with a reason.
            // Otherwise, return HTTP 200 OK.

            if (req.Body.Length > 2560 * 1024)
            {
                var response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.WriteString("The request message size exceeds the limit (2560KB).");
                return response;
            }
            else
            {
                var response = req.CreateResponse(HttpStatusCode.OK);
                return response;
            }
        }
    }
}

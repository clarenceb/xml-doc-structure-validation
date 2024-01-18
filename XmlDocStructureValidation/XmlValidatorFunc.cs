using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace XmlDocStructureValidation
{
    public class XmlValidatorFunc
    {
        private readonly ILogger _logger;

        public XmlValidatorFunc(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<XmlValidatorFunc>();
        }

        [Function("XmlValidatorFunc")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("XmlValidatorFunc received a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            XmlValidator validator = new XmlValidator();

            try
            {
                validator.ValidateXml(requestBody);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}

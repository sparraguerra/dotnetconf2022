using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using System.Net;

namespace FunctionsExampleSdkBinding
{
    public partial class BlobInputBindingSamples
    {
        private readonly ILogger _logger;

        public BlobInputBindingSamples(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<BlobInputBindingSamples>();
        }

        [Function(nameof(BlobInputStringFunctionAsync))]
        [OpenApiOperation(operationId: "blobInputStringFunction", tags: new[] { "blobInputStringFunction" },
                    Summary = "BlobInput string example", Description = "Integrate SDK Binding in Azure function.",
                    Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/html; charset=utf-8", bodyType: typeof(Dictionary<string, object?>),
                    Summary = "The response", Description = "This returns the response")]
        public async Task<HttpResponseData> BlobInputStringFunctionAsync(
                    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req,
                    [BlobInput("dot-net-conf-2022/sample.txt", Connection = "AzureWebJobsStorage")] string content)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync(content);
            return response;
        }         

        [Function(nameof(BlobInputByteArrayFunctionAsync))]
        [OpenApiOperation(operationId: "blobInputByteArrayFunction", tags: new[] { "blobInputByteArrayFunction" },
                   Summary = "BlobInput byte array example", Description = "Integrate SDK Binding in Azure function.",
                   Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/html; charset=utf-8", bodyType: typeof(Dictionary<string, object?>),
                   Summary = "The response", Description = "This returns the response")]
        public async Task<HttpResponseData> BlobInputByteArrayFunctionAsync(
                   [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req,
                   [BlobInput("dot-net-conf-2022/sample.txt", Connection = "AzureWebJobsStorage")] byte[] bytearray)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK); 
            await response.WriteBytesAsync(bytearray);
            return response;
        }
    }
}

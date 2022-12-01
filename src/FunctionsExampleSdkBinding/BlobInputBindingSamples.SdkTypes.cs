using Azure.Storage.Blobs;
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
        [Function(nameof(BlobInputClientFunctionAsync))]
        [OpenApiOperation(operationId: "blobInputClientFunction", tags: new[] { "blobInputClientFunction" },
                    Summary = "BlobInput client example", Description = "Integrate SDK Binding in Azure function.",
                    Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/html; charset=utf-8", bodyType: typeof(Dictionary<string, object?>),
                    Summary = "The response", Description = "This returns the response")]
        public async Task<HttpResponseData> BlobInputClientFunctionAsync(
                    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req,
                    [BlobInput("dot-net-conf-2022/sample.txt", Connection = "AzureWebJobsStorage")] BlobClient client)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var downloadResult = await client.DownloadContentAsync();
            await response.Body.WriteAsync(downloadResult.Value.Content); 
            return response;
        }

        [Function(nameof(BlobInputStreamFunctionAsync))]
        [OpenApiOperation(operationId: "blobInputStreamFunction", tags: new[] { "blobInputStreamFunction" },
                    Summary = "BlobInput stream example", Description = "Integrate SDK Binding in Azure function.",
                    Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/html; charset=utf-8", bodyType: typeof(Dictionary<string, object?>),
                    Summary = "The response", Description = "This returns the response")]
        public async Task<HttpResponseData> BlobInputStreamFunctionAsync(
                    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req,
                    [BlobInput("dot-net-conf-2022/sample.txt", Connection = "AzureWebJobsStorage")] Stream stream)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK); 
            using MemoryStream ms = new();
            await stream.CopyToAsync(ms);
            await response.Body.WriteAsync(ms.ToArray());
            return response;
        }

        [Function(nameof(BlobInputPOCOFunctionAsync))]
        [OpenApiOperation(operationId: "blobInputPOCOFunction", tags: new[] { "blobInputPOCOFunction" },
                    Summary = "BlobInput POCO example", Description = "Integrate SDK Binding in Azure function.",
                    Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/html; charset=utf-8", bodyType: typeof(Dictionary<string, object?>),
                    Summary = "The response", Description = "This returns the response")]
        public async Task<HttpResponseData> BlobInputPOCOFunctionAsync(
                    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req,
                    [BlobInput("dot-net-conf-2022/sample.json", Connection = "AzureWebJobsStorage")] Customer data)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK); 
            await response.WriteAsJsonAsync(data);
            return response;
        }
    }

    public class Customer
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Alias { get; set; }
    }
}

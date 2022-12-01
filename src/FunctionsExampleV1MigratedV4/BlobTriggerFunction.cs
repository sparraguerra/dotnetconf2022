using System;
using System.IO;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionsExampleV1MigratedV4
{
    public class BlobTriggerFunction
    {
        private readonly ILogger _logger;

        public BlobTriggerFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<BlobTriggerFunction>();
        }

        [Function(nameof(BlobTriggerFunctionAsync))]
        public void BlobTriggerFunctionAsync([BlobTrigger("dot-net-conf-2022/{name}", Connection = "AzureWebJobsStorage")] string myBlob, string name)
        {
            _logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name} \n Data: {myBlob}");
        }
    }
}

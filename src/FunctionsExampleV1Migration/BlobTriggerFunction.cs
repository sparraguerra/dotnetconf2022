using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace FunctionsExampleV1Migration
{
    public static class BlobTriggerFunction
    {
        [FunctionName(nameof(BlobTriggerFunctionAsync))]
        public static void BlobTriggerFunctionAsync(
                    [BlobTrigger("dot-net-conf-2022/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob,
                    string name, TraceWriter log)
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}

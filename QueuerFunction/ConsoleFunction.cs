using System;
using System.Text.Json;
using System.Threading.Tasks;
using Common;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace QueuerFunction
{
    public class ConsoleFunction
    {
        [FunctionName("ProcessQueueMessage")]
        public static async Task Run([QueueTrigger("myqueue", Connection = "AzureWebJobsStorage")] string message, ILogger log)
        {
            try
            {
                // Deserialize the message
                var request = JsonSerializer.Deserialize<PostDataRequest>(message);

                if (request == null)
                {
                    log.LogError("Failed to deserialize the message.");
                    return;
                }

                // Log the data for debugging
                log.LogInformation($"Processing message: UserId={request.UserId}, Data={request.Data}");

                // Simulate some processing
                await Task.Delay(100); // Simulate processing delay if needed

            }
            catch (Exception ex)
            {
                log.LogError($"Error processing queue message: {ex.Message}");
            }
        }
    }
}

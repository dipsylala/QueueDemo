# QueueDemo

## potential updates
Opportunity to throw the service into a lambda funciton

```C#
using Azure.Storage.Queues.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

public class QueueFunction
{
    [FunctionName("ProcessQueueMessage")]
    public void Run([QueueTrigger("myqueue", Connection = "AzureWebJobsStorage")] string message, ILogger log)
    {
        log.LogInformation($"Processing message: {message}");
    }
}
```

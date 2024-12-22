using System.Text.Json;
using Azure.Storage.Queues;
using Common;
using Microsoft.AspNetCore.Mvc;

namespace Queuer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DataController(QueueClient queueClient) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostDataRequest request)
    {
        if (string.IsNullOrEmpty(request.UserId) || string.IsNullOrEmpty(request.Data))
        {
            return BadRequest("Both UserId and Data are required.");
        }

        var messageBody = JsonSerializer.Serialize(request);
        await queueClient.SendMessageAsync(messageBody);
        return Accepted();
    }
}
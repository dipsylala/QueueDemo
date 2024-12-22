using System.Text.Json;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Common;

namespace Worker.Services;

public class WorkerService(QueueClient queueClient) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            QueueMessage[] messages = await queueClient.ReceiveMessagesAsync(10, TimeSpan.FromSeconds(10), stoppingToken);

            foreach (var message in messages)
            {
                try
                {
                    // Deserialize the message body
                    var request = JsonSerializer.Deserialize<PostDataRequest>(message.MessageText);

                    if (request != null)
                    {
                        // Process the message
                        Console.WriteLine($"Processing UserId: {request.UserId}, Data: {request.Data}");
                    }

                    // Delete the message after successful processing
                    await queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt, stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing message: {ex.Message}");
                }
            }

            await Task.Delay(1000); // Optional delay to control polling frequency
        }
    }
}

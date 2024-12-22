using Azure.Storage.Queues;
using Worker.Services;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<WorkerService>();
        services.AddSingleton(provider =>
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddUserSecrets<Program>();
            var connectionString = builder.Configuration["AzureStorage:ConnectionString"];
            var queueClient = new QueueClient(connectionString, "myqueue");
            queueClient.CreateIfNotExists();
            return queueClient;
        });
    })
    .Build()
    .Run();
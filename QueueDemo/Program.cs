using Azure.Storage.Queues;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddSingleton(provider =>
{
    var connectionString = builder.Configuration.GetSection("AzureStorage:ConnectionString").Value;
    var queueClient = new QueueClient(connectionString, "myqueue");
    queueClient.CreateIfNotExists();
    return queueClient;
});

var app = builder.Build();

app.MapControllers();
app.Run();
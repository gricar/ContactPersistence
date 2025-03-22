using ContactPersistence.Consumers;
using ContactPersistence.Data;
using ContactPersistence.Data.Extensions;
using ContactPersistence.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddScoped<IContactRepository, ContactRepository>();

var factory = new ConnectionFactory
{
    HostName = builder.Configuration["MessageBroker:Host"],
    ClientProvidedName = builder.Configuration["MessageBroker:ConnectionName"]
};

var connection = factory.CreateConnection();
var channel = connection.CreateModel();

builder.Services.AddSingleton(channel);
builder.Services.AddHostedService<ContactCreatedConsumer>();
builder.Services.AddHostedService<ContactDeletedConsumer>();
builder.Services.AddHostedService<ContactUpdatedConsumer>();

var app = builder.Build();

await app.InitializeDatabaseAsync();

app.Run();

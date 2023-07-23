using MessageHub.Configuration;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços
var rabbitMQConfig = builder.Configuration.GetSection("RabbitMQ").Get<RabbitConfiguration>();
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ConnectionFactory>(sp =>
{
    var connectionFactory = new ConnectionFactory()
    {
        HostName = rabbitMQConfig.HostName,
        Port = rabbitMQConfig.Port,
        UserName = rabbitMQConfig.Username,
        Password = rabbitMQConfig.Password
    };

    return connectionFactory;
});

var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Message}/{action=Index}/{id?}");

app.Run();
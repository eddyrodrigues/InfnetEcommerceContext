using InfnetEcommerceContext.Notification.API.services;
using InfnetEcommerceContext.Payment.API.Repository;
using InfnetEcommerceContext.Payment.API.Repository.DataContext;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<PaymentContext>(c => c.UseInMemoryDatabase("payments"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<PaymentRepository>();
builder.Services.AddScoped<PaymentService>();

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    var entryAssembly = Assembly.GetEntryAssembly();

    x.AddConsumers(entryAssembly);
    x.AddActivities(entryAssembly);
    x.UsingRabbitMq((cxt, cfg) =>
    {
        cfg.Host("localhost", h =>
        {
            h.Username(builder.Configuration.GetValue<string>("rabbitmq.login"));
            h.Password(builder.Configuration.GetValue<string>("rabbitmq.password"));
        });

        cfg.ConfigureEndpoints(cxt);
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

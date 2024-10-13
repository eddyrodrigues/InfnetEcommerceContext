using InfnetEcommerceContext.Email.API.Services;
using MassTransit;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<SendEmailService>();
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

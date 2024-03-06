using InfnetEcommerceContext.Notification.API.services;
using InfnetEcommerceContext.Payment.API.Repository;
using InfnetEcommerceContext.Payment.API.Repository.DataContext;
using Microsoft.EntityFrameworkCore;
using Steeltoe.Discovery.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<PaymentContext>(c => c.UseInMemoryDatabase("payments"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<PaymentRepository>();
builder.Services.AddScoped<PaymentService>();

builder.Services.AddDiscoveryClient();
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

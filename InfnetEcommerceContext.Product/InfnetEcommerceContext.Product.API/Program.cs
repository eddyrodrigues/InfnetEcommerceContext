using InfnetEcommerceContext.Product.API.Repository;
using InfnetEcommerceContext.Product.API.Repository.DataContext;
using InfnetEcommerceContext.Product.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var hostDataBase = Environment.GetEnvironmentVariable("PRODUCTAPI_DB_SERVICE_SERVICE_HOST");
//UseInMemoryDatabase("products")
builder.Services.AddDbContext<ProductContext>(c => c.UseSqlServer($"Server={hostDataBase};Database=production;User Id=sa;Password=*summoner593;"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Export metrics from all HTTP clients registered in services


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Capture metrics about all received HTTP requests.
app.UseRouting();
app.UseHttpMetrics();
app.UseAuthorization();

app.MapControllers();
app.UseEndpoints(endpoints =>
{
    // Enable the /metrics page to export Prometheus metrics.
    // Open http://localhost:5099/metrics to see the metrics.
    //
    // Metrics published in this sample:
    // * built-in process metrics giving basic information about the .NET runtime (enabled by default)
    // * metrics from .NET Event Counters (enabled by default, updated every 10 seconds)
    // * metrics from .NET Meters (enabled by default)
    // * metrics about requests made by registered HTTP clients used in SampleService (configured above)
    // * metrics about requests handled by the web app (configured above)
    // * ASP.NET health check statuses (configured above)
    // * custom business logic metrics published by the SampleService class
    endpoints.MapMetrics("/hcappfollow");
});
app.Run();

using InfnetEcommerceContext.Product.API.Dependencies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using Steeltoe.Extensions.Configuration.CloudFoundry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Export metrics from all HTTP clients registered in services

//builder.AddDiscoveryClient();
builder.AddCloudFoundryConfiguration();
builder.AddServiceDiscovery(c => c.UseEureka());

builder.Services.AddServices();
builder.Services.AddProductsDatabase(builder.Environment.EnvironmentName);
builder.Services.AddJwtConfiguration(builder.Environment.EnvironmentName);

builder.Services.AddAuthorization();    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Capture metrics about all received HTTP requests.
app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseRouting();
app.UseHttpMetrics();

app.UseAuthentication();
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
    endpoints.MapMetrics("/metrics");
});
app.Run();

using InfnetEcommerceContext.Cart.API.Repository.DataContext;
using InfnetEcommerceContext.Cart.API.Repository.Repositories;
using InfnetEcommerceContext.Cart.API.Services;
using Microsoft.EntityFrameworkCore;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using InfnetEcommerceContext.Shared;
using System;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddCloudFoundryConfiguration();
//builder.Services.AddDiscoveryClient();
builder.Services.AddServiceDiscovery(c => c.UseEureka());

builder.Services.AddDbContext<CartContext>(c =>
{
    c.UseInMemoryDatabase("carts");
});

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<CheckoutService>();

var tokenSecret = Environment.GetEnvironmentVariable("SECRET_TOKEN_JWT");
builder.Services.AddJwtTokenConfiguration(tokenSecret);
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
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

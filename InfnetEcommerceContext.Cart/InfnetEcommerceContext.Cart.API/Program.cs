using InfnetEcommerceContext.Cart.API.Repository.DataContext;
using InfnetEcommerceContext.Cart.API.Repository.Repositories;
using InfnetEcommerceContext.Cart.API.Services;
using Microsoft.EntityFrameworkCore;
using Steeltoe.Discovery.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDiscoveryClient();

builder.Services.AddDbContext<CartContext>(c =>
{
    c.UseInMemoryDatabase("carts");
});

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<CartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

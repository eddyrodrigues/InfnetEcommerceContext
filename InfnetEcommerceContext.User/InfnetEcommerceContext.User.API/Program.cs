using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using InfnetEcommerceContext.Shared;
using InfnetEcommerceContext.User.API;
using InfnetEcommerceContext.User.API.dependencies;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using Steeltoe.Extensions.Configuration.CloudFoundry;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.AddDiscoveryClient();
builder.AddCloudFoundryConfiguration();
builder.AddServiceDiscovery(c => c.UseEureka());

var hostDataBase = Environment.GetEnvironmentVariable("DB_CONN_STRING");
var tokenSecret = Environment.GetEnvironmentVariable("SECRET_TOKEN_JWT");

if (builder.Environment.IsProduction())
{
    var client = new SecretClient(vaultUri: new Uri(Environment.GetEnvironmentVariable("azureapiskeyvault")), credential: new DefaultAzureCredential());
    var secret = client.GetSecret("dbusuarios");
    hostDataBase = secret.Value?.Value;
    var tokenSecretSecret = client.GetSecret("tokensecret");
    tokenSecret = tokenSecretSecret.Value?.Value;
}

builder.Services.AddUserDatabase(hostDataBase);
builder.Services.AddUserService();
SettingsApi.Secret = tokenSecret;
builder.Services.AddJwtTokenConfiguration(tokenSecret);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

//app.UseHttpsRedirection();
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

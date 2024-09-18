using ApiGateway;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.ConfigureGateway(builder.Configuration);
var app = builder.Build();

await app.LaunchGateway();
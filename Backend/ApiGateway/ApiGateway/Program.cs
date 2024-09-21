using ApiGateway;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureGateway(builder.Configuration);
var app = builder.Build();

await app.LaunchGateway();
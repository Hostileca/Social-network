using System.Text;
using Application;
using Infrastructure;
using Infrastructure.MesssageBroker.Consumers;
using Infrastructure.SignalR.Hubs;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Presentation;
using SharedResources.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddPresentation(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapHub<ChatHub>("/chatHub");

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
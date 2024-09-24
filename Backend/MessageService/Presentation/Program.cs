using System.Text;
using Application;
using Hangfire;
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

app.StartApplication();
using BusinessLogicLayer;
using DataAccessLayer;
using IdentityService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();
builder.Services.AddPresentationLayer();
var app = builder.Build();
app.StartApplication();

using Dotnet.Homeworks.Data.DatabaseContext;
using Dotnet.Homeworks.MainProject.Configuration;
using Dotnet.Homeworks.MainProject.Services;
using Dotnet.Homeworks.MainProject.ServicesExtensions.Masstransit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var rabbitMqConfig = builder.Configuration
    .GetSection(nameof(RabbitMqConfig))
    .Get<RabbitMqConfig>()
    ?? throw new ApplicationException("Not supported rabbitMq settings");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<ICommunicationService, CommunicationService>();
builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection(nameof(RabbitMqConfig)));
builder.Services.AddMasstransitRabbitMq(rabbitMqConfig);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
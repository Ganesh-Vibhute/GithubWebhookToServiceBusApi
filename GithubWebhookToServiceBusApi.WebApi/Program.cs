using GithubWebhookToServiceBusApi.Adapter.Contracts;
using GithubWebhookToServiceBusApi.Adapter.Repository;
using GithubWebhookToServiceBusApi.BLL.Contracts;
using GithubWebhookToServiceBusApi.BLL.Services;
using System.Text.Json.Nodes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IRepositories<JsonObject>, AddToServiceBus>();
builder.Services.AddScoped<IServiceBusService,ServiceBusService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

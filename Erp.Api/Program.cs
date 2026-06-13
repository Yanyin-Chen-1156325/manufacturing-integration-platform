using Erp.Api.Data;
using Microsoft.EntityFrameworkCore;
using Erp.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add Connection
builder.Services.AddDbContext<ErpDbContext>(options => 
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Postgres")
        );
});

// DI
builder.Services.AddSingleton<IServiceBusPublisher, ServiceBusPublisher>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

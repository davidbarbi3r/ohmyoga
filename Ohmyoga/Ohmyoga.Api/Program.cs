using Dapper;
using Npgsql;
using Ohmyoga.Application;
using Ohmyoga.Application.Database;

var builder = WebApplication.CreateBuilder(args);
var dbConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
if (string.IsNullOrEmpty(dbConnectionString))
    throw new Exception("DB_CONNECTION_STRING not found");


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication(); 
builder.Services.AddDatabase(dbConnectionString);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization(); 

app.MapControllers();

var dbInitializer = app.Services.GetRequiredService<DbInitializer>();
await dbInitializer.InitializeAsync();

app.Run();
using CRUDOperation.Model;
using CRUDOperation.Service;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// MongoDB Settings
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB"));

builder.Services.AddSingleton<UserService>(sp =>
{
    var settings = builder.Configuration
        .GetSection("MongoDB")
        .Get<MongoDBSettings>();

    return new UserService(settings);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

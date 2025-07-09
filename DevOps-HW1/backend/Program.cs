using backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Connection string not found.");


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Products API", Version = "v1" });
});

var app = builder.Build();



    app.UseSwagger();
    app.UseSwaggerUI();


app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    
    if (db.Database.GetPendingMigrations().Any())
    {
        Console.WriteLine("Migration...");
        db.Database.Migrate();
    }
    else
    {
        Console.WriteLine("Db Already Migrated");
    }
}


app.Run();

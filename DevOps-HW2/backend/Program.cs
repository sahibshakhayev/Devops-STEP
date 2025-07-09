using backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder
            .WithOrigins("http://localhost:3000") // 🔥 Разрешаем React
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Products API", Version = "v1" });
});


var app = builder.Build();
app.UseCors("AllowFrontend");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var retryPolicy = Policy
        .Handle<Exception>()
        .WaitAndRetry(10, retryAttempt => TimeSpan.FromSeconds(5));

  


    retryPolicy.Execute(() => db.Database.EnsureCreated());
}


app.UseSwagger();

app.UseSwaggerUI();


app.MapControllers();
app.Run();

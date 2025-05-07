using api_db.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer(); // Necesario para APIs mínimas
builder.Services.AddSwaggerGen(); // Swagger/OpenAPI
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("Master"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Master"))));

// Services
builder.Services.AddControllers();
builder.Services.AddSingleton<IReadOnlyConnectionSelector, RoundRobinReadOnlyConnectionSelector>();
builder.Services.AddSingleton<AppDbContextFactory>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();

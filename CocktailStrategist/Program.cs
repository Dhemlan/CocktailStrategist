using CocktailStrategist.Data;
using CocktailStrategist.Repo;
using CocktailStrategist.Repo.Interfaces;
using CocktailStrategist.Services;
using CocktailStrategist.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IDrinkService, DrinkService>();
builder.Services.AddScoped<IIngredientService, IngredientService> ();
builder.Services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));

string connString;
if (builder.Environment.IsDevelopment()) {
    connString = "CocktailStrategistDatabase";
}
else if (builder.Environment.IsProduction())
{
    connString = "CSProdDatabase";
}
else {
        connString = "CSTestDatabase";
}

builder.Services.AddDbContextPool<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString(connString)));

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

public partial class Program { }

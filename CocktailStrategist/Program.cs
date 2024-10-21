using CocktailStrategist.Data;
using CocktailStrategist.Data.Enum;
using CocktailStrategist.Repo;
using CocktailStrategist.Repo.Interfaces;
using CocktailStrategist.Services;
using CocktailStrategist.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var defaultCors = "default";

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

builder.Services.AddCors(options =>
{
    options.AddPolicy(defaultCors, policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContextPool<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString(connString),
    o => o.MapEnum<IngredientCategory>("ingredientCategory")));

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

app.UseCors(defaultCors);

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }

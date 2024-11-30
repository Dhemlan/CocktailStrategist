using AutoMapper;
using CocktailStrategist.Data;
using CocktailStrategist.Data.Enum;
using CocktailStrategist.Data.Mapping;
using CocktailStrategist.Repo;
using CocktailStrategist.Repo.Interfaces;
using CocktailStrategist.Services;
using CocktailStrategist.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var defaultCors = "default";

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped<IDrinkService, DrinkService>();
builder.Services.AddScoped<IIngredientService, IngredientService> ();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
builder.Services.AddScoped<IIngredientRepo, IngredientRepo>();
builder.Services.AddScoped<IDrinkRepo, DrinkRepo>();
builder.Services.AddScoped<IIngredientUsageRepo, IngredientUsageRepo>();


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

//var config = new MapperConfiguration(cfg => cfg.CreateMap<Drink, DrinkDTO>());

builder.Services.AddAutoMapper(typeof(DrinkProfile));

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

//builder.Services.AddDbContextPool<AppDbContext>(opt =>
//    opt.UseNpgsql(builder.Configuration.GetConnectionString(connString)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// todo was causing CORS issue in prod
// to research importance 
//app.UseHttpsRedirection();

app.UseCors(defaultCors);

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }

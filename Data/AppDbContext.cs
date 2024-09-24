using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;

namespace CocktailStrategist.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("CocktailStrategistDatabase"));
        }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Ingredient> Ingredients { get; set;}
    }
}

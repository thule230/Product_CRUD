using Microsoft.EntityFrameworkCore;

namespace Product_CRUD.DataLayer
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Models.Drink> Drinks { get; set; }

        public DbSet<Models.Clothing> Clothings { get; set; }

        public DbSet<Models.Food> Foods { get; set; }

        public DbSet<Models.CapacityUnity> CapacityUnities { get; set; }

        public DbSet<Models.ClothingSize> ClothingSizes { get; set; }

        public DbSet<Models.WeightUnity> WeightUnities { get; set; }

        public DbSet<Models.ProductType> ProductTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryDB");
        }
    }
}
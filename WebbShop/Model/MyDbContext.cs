using Microsoft.EntityFrameworkCore;

namespace WebbShop.Model
{
    internal class MyDbContext : DbContext
    {
        public DbSet<Product> products { get; set; }

        public DbSet<User> users { get; set; }

        public DbSet<Stock> stocks { get; set; }

        public DbSet<Color> colors { get; set; }

        public DbSet<Brand> brands { get; set; }

        public DbSet<Admin> admins { get; set; }

        public DbSet<ShopingCart> shopingCart { get; set; }

        public DbSet<Exchange> exchange { get; set; }   

        public DbSet<Category> categories { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}

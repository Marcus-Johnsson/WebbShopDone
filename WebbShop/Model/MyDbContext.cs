using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=WebbShop3;Trusted_Connection=True; TrustServerCertificate=True;");
            optionsBuilder.UseSqlServer("Server=tcp:marcusdatabas.database.windows.net,1433;Initial Catalog=MarcusDatabas;Persist Security Info=False;User ID=MarcusAdmin;Password=Kanelbulle96;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PolmesarieWeb.Models;

namespace PolmesarieWeb.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Brand> ProductBrands { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

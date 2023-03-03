using Microsoft.EntityFrameworkCore;
using PultApp.Models;

namespace PultApp.Data

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Admin> Login { get; set; }

    }
}

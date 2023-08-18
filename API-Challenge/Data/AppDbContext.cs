using API_Challenge.Modelos;
using Microsoft.EntityFrameworkCore;

namespace API_Challenge.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 

        }
        public DbSet<Cliente> clientes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

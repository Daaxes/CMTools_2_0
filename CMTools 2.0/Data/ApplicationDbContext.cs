using CMTools_2_0.Models;
using Microsoft.EntityFrameworkCore;

namespace CMTools_2_0.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Organisation> Organizations { get; set; }
    }
}

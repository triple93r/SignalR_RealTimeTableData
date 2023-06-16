using Microsoft.EntityFrameworkCore;
using SignalRDemo3ytEFC.Models;

namespace SignalRDemo3ytEFC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Product> Product { get; set; } = null!;
    }
}

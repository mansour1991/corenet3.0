using Microsoft.EntityFrameworkCore;

namespace PvItemsAPI.Models
{
    public class PvContext : DbContext
    {
        public PvContext(DbContextOptions<PvContext> options)
            : base(options)
        {
        }

        public DbSet<PvItem> PvItems { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;

namespace JogoDaVelhaAPI.Models
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardItem>().HasKey(ba => new { ba.id });
        }

        public DbSet<BoardItem> BoardItems { get; set; }
    }
}

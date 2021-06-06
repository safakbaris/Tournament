using Microsoft.EntityFrameworkCore;
using Tournament.Core.Entities;

namespace Tournament.Infrastructure
{
    public class TournamentContext : DbContext
    {
        public TournamentContext(DbContextOptions<TournamentContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<Game>()
              .ToTable("Games")
              .HasOne(p => p.Player1)
              .WithMany(p => p.Player1Games)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Game>()
                .HasOne(p => p.Player2)
                .WithMany(p => p.Player2Games)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Game>()
              .HasMany(p => p.Logs)
              .WithOne(p => p.Game)
              .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Core.Entities.Tournament>()
              .ToTable("Tournaments")
              .HasMany(p => p.Players)
              .WithOne(p => p.Tournament)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Core.Entities.Tournament>()
            .HasMany(p => p.Games)
            .WithOne(p => p.Tournament)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TournamentPlayer>()
              .ToTable("TournamentPlayers")
              .HasOne(p => p.User)
            .WithMany(p => p.Tournaments)
            .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<GameLog>()
               .ToTable("GameLogs");




        }
        //entities
        public DbSet<User> Users { get; set; }

        public DbSet<Core.Entities.Tournament> Tournaments { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<TournamentPlayer> TournamentPlayers { get; set; }
    }
}

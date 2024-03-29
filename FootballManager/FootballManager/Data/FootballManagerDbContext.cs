﻿namespace FootballManager.Data
{
    using FootballManager.Data.Models;
    using Microsoft.EntityFrameworkCore;
    public class FootballManagerDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<UserPlayer> UserPlayers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=FootballManager;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPlayer>()
                .HasKey(x => new { x.UserId, x.PlayerId });

            modelBuilder.Entity<UserPlayer>()
                .HasOne(bc => bc.Player)
                .WithMany(b => b.UserPlayers)
                .HasForeignKey(bc => bc.PlayerId);
            modelBuilder.Entity<UserPlayer>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.UserPlayers)
                .HasForeignKey(bc => bc.UserId);
        }
    }
}

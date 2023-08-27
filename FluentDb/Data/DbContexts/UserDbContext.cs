using FluentDb.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace FluentDb.Data.DbContexts
{
    public class UserDbContext : DbContext
    {
        public 
            DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConfigurationBuilder builder = new();
            builder.AddJsonFile("appsettings.json");


            IConfigurationRoot config = builder.Build();
            var connectionString = config.GetConnectionString("UserDb");

            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.HasIndex(u => u.Username).IsUnique();

                entity.Property(u => u.Name).HasMaxLength(50);

                entity.Property(u => u.Surname).HasMaxLength(50);

                entity.Property(u => u.Password).HasMaxLength(70);

                entity.Property(u => u.Email).HasMaxLength(256).IsRequired();

                entity.Property(e => e.Age).IsRequired();
            }
            );
        }
    }
}

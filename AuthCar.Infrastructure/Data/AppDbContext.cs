using AuthCar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace AuthCar.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Login)
                .IsUnique();
        }
    }
}

using AuthCar.Domain.Entities;
using AuthCar.Domain.Enums;
using AuthCar.Infrastructure.Data.Mappings;
using Foundation.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using System;

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

            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new VeiculoMap());

            // Seed de usuário admin
            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id = 1,
                Codigo = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Nome = "admin",
                Login = "admin",
                Senha = StringHelper.ComputeArgon2Hash("senha123")
            });
        }
    }
}

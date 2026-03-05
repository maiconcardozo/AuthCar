using AuthCar.Domain.Entities;
using Foundation.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthCar.Infrastructure.Data.Mappings
{
    public class UsuarioMap : EntityMap<Usuario>, IEntityTypeConfiguration<Usuario>
    {
        public override void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            base.Configure(builder);

            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(e => e.Login)
                .IsUnique();

            builder.Property(e => e.Senha)
                .IsRequired();
        }
    }
}

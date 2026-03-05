using AuthCar.Domain.Entities;
using Foundation.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthCar.Infrastructure.Data.Mappings
{
    public class VeiculoMap : EntityMap<Veiculo>, IEntityTypeConfiguration<Veiculo>
    {
        public override void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.ToTable("Veiculo");
            base.Configure(builder);

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Marca)
                .IsRequired();

            builder.Property(e => e.Modelo)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.Valor)
                .HasColumnType("decimal(18,2)");
        }
    }
}

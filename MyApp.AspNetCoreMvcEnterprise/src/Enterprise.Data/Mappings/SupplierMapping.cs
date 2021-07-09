using Enterprise.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enterprise.Data.Mappings
{
    public class SupplierMapping : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(x => x.Documento)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.HasOne(x => x.Endereco)
                .WithOne(e => e.Supplier);

            builder.HasMany(x => x.Products)
                .WithOne(x => x.Supplier)
                .HasForeignKey(x => x.SupplierId);

            builder.ToTable("TB_Supplier", "Enterprise");
        }
    }
}

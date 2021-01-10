using LZMotel.Local.API.Models;
using LZMotel.Local.API.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LZMotel.Local.API.Data.Mappings
{
  public class SuiteMap : IEntityTypeConfiguration<Suite>
  {
    public void Configure(EntityTypeBuilder<Suite> builder)
    {
      builder.ToTable("Suite");
      builder.HasKey(keyExpression: c => c.Id);

      builder.Property(c => c.Nome)
        .IsRequired(false);

      builder.Property(c => c.SuiteNumero)
        .IsRequired(true);

      builder.Property(c => c.SuiteTransferida)
        .HasColumnType<int>("integer");

      builder.Property(c => c.Status)
        .IsRequired(true)
        .HasDefaultValue(SuiteEnums.Status.Livre);

      builder.Property(c => c.Comanda)
        .HasColumnType<int>("integer");

      //builder.Property(c => c.CategoriaId)
      //  .HasColumnName("categoriaid");

      //builder.HasOne(c => c.Categoria)
      //  .WithMany(c => c.Suites)
      //  .HasForeignKey(c => c.CategoriaId)
      //  .OnDelete(DeleteBehavior.NoAction);

      builder.HasIndex(c => c.SuiteNumero).IsUnique();
    }
  }
}

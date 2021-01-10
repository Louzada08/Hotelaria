using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LZMotel.Clientes.API.Models;
using LZMotel.Core.DomainObjects;

namespace LZMotel.Clientes.API.Data.Mappings
{
  public class ClienteMapping : IEntityTypeConfiguration<Cliente>
  {
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
      builder.HasKey(c => c.Id);

      builder.Property(c => c.Nome)
          .IsRequired(true)
          .HasColumnType("varchar(50)");

      builder.Property(c => c.Turno)
          .IsRequired(true)
          .HasColumnType("char(02)");

      builder.Property(c => c.Funcao)
          .IsRequired(true)
          .HasColumnType("char(15)");

      builder.OwnsOne(c => c.Cpf, tf =>
            {
              tf.Property(c => c.Numero)
                  .IsRequired()
                  .HasMaxLength(Cpf.CpfMaxLength)
                  .HasColumnName("Cpf")
                  .HasColumnType($"varchar({Cpf.CpfMaxLength})");
            });

      builder.OwnsOne(c => c.Email, tf =>
      {
        tf.Property(c => c.Endereco)
                  .IsRequired()
                  .HasColumnName("Email")
                  .HasColumnType($"varchar({Email.EnderecoMaxLength})");
      });

      // 1 : 1 => Aluno : Endereco
      builder.HasOne(c => c.Endereco)
          .WithOne(c => c.Cliente);

      builder.ToTable("Cliente");
    }
  }
}
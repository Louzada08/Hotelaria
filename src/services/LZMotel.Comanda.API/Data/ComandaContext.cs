using System.Linq;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using LZMotel.Carrinho.API.Model;

namespace LZMotel.Carrinho.API.Data
{
    public sealed class ComandaContext : DbContext
    {
        public ComandaContext(DbContextOptions<ComandaContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<ComandaItem> ComandaItens { get; set; }
        public DbSet<ComandaCliente> ComandaCliente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.Entity<ComandaCliente>()
                .HasIndex(c => c.ClienteId)
                .HasName("IDX_Cliente");

            modelBuilder.Entity<ComandaCliente>()
                .HasMany(c => c.Itens)
                .WithOne(i => i.ComandaCliente)
                .HasForeignKey(c => c.ComandaId);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
    }
}
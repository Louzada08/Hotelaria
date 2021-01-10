using Microsoft.EntityFrameworkCore;
using LZMotel.Local.API.Models;
using System.Linq;
using System.Threading.Tasks;
using LZMotel.Core.Data;
using FluentValidation.Results;
using LZMotel.Core.Messages;

namespace LZMotel.Local.API.Data
{
  public class LocalContext : DbContext, IUnitOfWork
  {
    public LocalContext(DbContextOptions<LocalContext> options) : base(options) { }

    //public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Suite> Suites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Ignore<ValidationResult>();
      modelBuilder.Ignore<Event>();

      foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
          e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
        property.SetColumnType("varchar(100)");

      modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocalContext).Assembly);
    }

    public async Task<bool> Commit()
    {
      return await base.SaveChangesAsync() > 0;
    }
  }
}

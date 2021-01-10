using LZMotel.Core.Data;
using LZMotel.Local.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LZMotel.Local.API.Data.Repository
{
  public class SuiteRepository : ISuiteRepository
  {
    private readonly LocalContext _context;

    public SuiteRepository(LocalContext context)
    {
      _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Adicionar(Suite suite)
    {
      _context.Suites.Add(suite);
    }

    public void Atualizar(Suite suite)
    {
      _context.Suites.Update(suite);
    }

    public async Task<Suite> ObterPorId(Guid id)
    {
      return await _context.Suites.FindAsync(id);
    }

    public async Task<IEnumerable<Suite>> ObterTodos()
    {
      var suites = await _context.Suites.AsNoTracking().ToListAsync();
      return suites;
    }

    public void Dispose()
    {
      _context?.Dispose();
    }
  }
}

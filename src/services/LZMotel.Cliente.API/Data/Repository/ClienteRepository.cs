using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LZMotel.Clientes.API.Models;
using LZMotel.Core.Data;
using LZMotel.Core.DomainObjects;
using System;

namespace LZMotel.Clientes.API.Data.Repository
{
  public class ClienteRepository : IClienteRepository
  {
    private readonly ClienteContext _context;

    public ClienteRepository(ClienteContext context)
    {
      _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Cliente>> ObterListaClientes()
    {

      return await _context.Clientes.AsNoTracking().ToListAsync();
    }

    public Task<Cliente> ObterPorCpf(string cpf)
    {
      return _context.Clientes.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
    }
    
    public async Task<Cliente> ObterClientePorId(Guid id)
    {
      var resposta = await _context.Clientes.FindAsync(id);
      return resposta;
    }
    
    public void Adicionar(Cliente cliente)
    {
      _context.Clientes.Add(cliente);
    }

    public async Task<Endereco> ObterEnderecoPorId(Guid id)
    {
      return await _context.Enderecos.FirstOrDefaultAsync(e => e.ClienteId == id);
    }

    public void Dispose()
    {
      _context.Dispose();
    }

  }
}
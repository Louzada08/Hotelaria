using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LZMotel.Core.Data;

namespace LZMotel.Clientes.API.Models
{
  public interface IUsuarioRepository : IRepository<Cliente>
  {
    void Adicionar(Cliente cliente);

    Task<IEnumerable<Cliente>> ObterListaClientes();
    Task<Cliente> ObterPorCpf(string cpf);
    Task<Cliente> ObterClientePorId(Guid id);
    Task<Endereco> ObterEnderecoPorId(Guid id);
  }
}
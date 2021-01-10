using LZMotel.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LZMotel.Local.API.Models
{
  public interface ISuiteRepository : IRepository<Suite>
  {
    Task<IEnumerable<Suite>> ObterTodos();
    Task<Suite> ObterPorId(Guid id);
    void Adicionar(Suite suite);
    void Atualizar(Suite suite);
  }
}

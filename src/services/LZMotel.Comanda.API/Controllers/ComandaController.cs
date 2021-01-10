using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LZMotel.Carrinho.API.Data;
using LZMotel.Carrinho.API.Model;
using LZMotel.WebAPI.Core.Controllers;
using LZMotel.WebAPI.Core.Usuario;

namespace LZMotel.Carrinho.API.Controllers
{
  [Authorize]
  public class ComandaController : MainController
  {
    private readonly IAspNetUser _user;
    private readonly ComandaContext _context;

    public ComandaController(IAspNetUser user, ComandaContext context)
    {
      _user = user;
      _context = context;
    }

    [HttpGet("comanda")]
    public async Task<ComandaCliente> ObterCarrinho()
    {
      return await ObterComandaCliente() ?? new ComandaCliente();
    }

    [HttpPost("comanda")]
    public async Task<IActionResult> AdicionarItemCarrinho(ComandaItem item)
    {
      var comanda = await ObterComandaCliente();

      if (comanda == null)
        ManipularNovoComanda(item);
      else
        ManipularComandaExistente(comanda, item);

      if (!OperacaoValida()) return CustomResponse();

      await PersistirDados();
      return CustomResponse();
    }

    [HttpPut("comanda/{produtoId}")]
    public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, ComandaItem item)
    {
      var comanda = await ObterComandaCliente();
      var itemComanda = await ObterItemComandaValidado(produtoId, comanda, item);
      if (itemComanda == null) return CustomResponse();

      comanda.AtualizarUnidades(itemComanda, item.Quantidade);

      ValidarComanda(comanda);
      if (!OperacaoValida()) return CustomResponse();

      _context.ComandaItens.Update(itemComanda);
      _context.ComandaCliente.Update(comanda);

      await PersistirDados();
      return CustomResponse();
    }

    [HttpDelete("comanda/{produtoId}")]
    public async Task<IActionResult> RemoverItemComanda(Guid produtoId)
    {
      var comanda = await ObterComandaCliente();

      var itemComanda = await ObterItemComandaValidado(produtoId, comanda);
      if (itemComanda == null) return CustomResponse();

      ValidarComanda(comanda);
      if (!OperacaoValida()) return CustomResponse();

      comanda.RemoverItem(itemComanda);

      _context.ComandaItens.Remove(itemComanda);
      _context.ComandaCliente.Update(comanda);

      await PersistirDados();
      return CustomResponse();
    }

    private async Task<ComandaCliente> ObterComandaCliente()
    {
      return await _context.ComandaCliente
          .Include(c => c.Itens)
          .FirstOrDefaultAsync(c => c.ClienteId == _user.ObterUserId());
    }
    private void ManipularNovoComanda(ComandaItem item)
    {
      var comanda = new ComandaCliente(_user.ObterUserId());
      comanda.AdicionarItem(item);

      ValidarComanda(comanda);
      _context.ComandaCliente.Add(comanda);
    }
    private void ManipularComandaExistente(ComandaCliente comanda, ComandaItem item)
    {
      var produtoItemExistente = comanda.ComandaItemExistente(item);

      comanda.AdicionarItem(item);
      ValidarComanda(comanda);

      if (produtoItemExistente)
      {
        _context.ComandaItens.Update(comanda.ObterPorProdutoId(item.ProdutoId));
      }
      else
      {
        _context.ComandaItens.Add(item);
      }

      _context.ComandaCliente.Update(comanda);
    }
    private async Task<ComandaItem> ObterItemComandaValidado(Guid produtoId, ComandaCliente comanda, ComandaItem item = null)
    {
      if (item != null && produtoId != item.ProdutoId)
      {
        AdicionarErroProcessamento("O item não corresponde ao informado");
        return null;
      }

      if (comanda == null)
      {
        AdicionarErroProcessamento("Comanda não encontrado");
        return null;
      }

      var itemComanda = await _context.ComandaItens
          .FirstOrDefaultAsync(i => i.ComandaId == comanda.Id && i.ProdutoId == produtoId);

      if (itemComanda == null || !comanda.ComandaItemExistente(itemComanda))
      {
        AdicionarErroProcessamento("O item não está no carrinho");
        return null;
      }

      return itemComanda;
    }
    private async Task PersistirDados()
    {
      var result = await _context.SaveChangesAsync();
      if (result <= 0) AdicionarErroProcessamento("Não foi possível persistir os dados no banco");
    }
    private bool ValidarComanda(ComandaCliente comanda)
    {
      if (comanda.EhValido()) return true;

      comanda.ValidationResult.Errors.ToList().ForEach(e => AdicionarErroProcessamento(e.ErrorMessage));
      return false;
    }
  }
}

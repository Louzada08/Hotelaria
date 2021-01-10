using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace LZMotel.Carrinho.API.Model
{
  public class ComandaCliente
  {
    internal const int MAX_QUANTIDADE_ITEM = 30;

    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public decimal ValorPermanencia { get; set; }
    public decimal ValorConsumo { get; set; }
    public List<ComandaItem> Itens { get; set; } = new List<ComandaItem>();
    public ValidationResult ValidationResult { get; set; }

    public ComandaCliente(Guid clienteId)
    {
      Id = Guid.NewGuid();
      ClienteId = clienteId;
    }

    public ComandaCliente() { }

    internal void CalcularValorComanda()
    {
      ValorPermanencia = Itens.Sum(p => p.CalcularValor());
    }

    internal bool ComandaItemExistente(ComandaItem item)
    {
      return Itens.Any(p => p.ProdutoId == item.ProdutoId);
    }

    internal ComandaItem ObterPorProdutoId(Guid produtoId)
    {
      return Itens.FirstOrDefault(p => p.ProdutoId == produtoId);
    }

    internal void AdicionarItem(ComandaItem item)
    {
      item.AssociarComanda(Id);

      if (ComandaItemExistente(item))
      {
        var itemExistente = ObterPorProdutoId(item.ProdutoId);
        itemExistente.AdicionarUnidades(item.Quantidade);

        item = itemExistente;
        Itens.Remove(itemExistente);
      }

      Itens.Add(item);
      CalcularValorComanda();
    }

    internal void AtualizarItem(ComandaItem item)
    {
      item.AssociarComanda(Id);

      var itemExistente = ObterPorProdutoId(item.ProdutoId);

      Itens.Remove(itemExistente);
      Itens.Add(item);

      CalcularValorComanda();
    }

    internal void AtualizarUnidades(ComandaItem item, int unidades)
    {
      item.AtualizarUnidades(unidades);
      AtualizarItem(item);
    }

    internal void RemoverItem(ComandaItem item)
    {
      Itens.Remove(ObterPorProdutoId(item.ProdutoId));
      CalcularValorComanda();
    }

    internal bool EhValido()
    {
      var erros = Itens.SelectMany(i => new ComandaItem.ItemComandaValidation().Validate(i).Errors).ToList();
      erros.AddRange(new ComandaClienteValidation().Validate(this).Errors);
      ValidationResult = new ValidationResult(erros);

      return ValidationResult.IsValid;
    }

    public class ComandaClienteValidation : AbstractValidator<ComandaCliente>
    {
      public ComandaClienteValidation()
      {
        RuleFor(c => c.ClienteId)
            .NotEqual(Guid.Empty)
            .WithMessage("Cliente não reconhecido");
      }
    }
  }
}
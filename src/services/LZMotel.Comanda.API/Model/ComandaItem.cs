using System;
using System.Text.Json.Serialization;
using FluentValidation;

namespace LZMotel.Carrinho.API.Model
{
    public class ComandaItem
    {
        public ComandaItem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public Guid ComandaId { get; set; }

        [JsonIgnore]
        public ComandaCliente ComandaCliente { get; set; }

        internal void AssociarComanda(Guid comandaId)
        {
            ComandaId = comandaId;
        }

        internal decimal CalcularValor()
        {
            return Quantidade * Valor;
        }

        internal void AdicionarUnidades(int unidades)
        {
            Quantidade += unidades;
        }

        internal void AtualizarUnidades(int unidades)
        {
            Quantidade = unidades;
        }

        internal bool EhValido()
        {
            return new ItemComandaValidation().Validate(this).IsValid;
        }

        public class ItemComandaValidation : AbstractValidator<ComandaItem>
        {
            public ItemComandaValidation()
            {
                RuleFor(c => c.ProdutoId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do produto inválido");

                RuleFor(c => c.Nome)
                    .NotEmpty()
                    .WithMessage("O nome do produto não foi informado");

                RuleFor(c => c.Quantidade)
                    .GreaterThan(0)
                    .WithMessage(item => $"A quantidade miníma para o {item.Nome} é 1");

                RuleFor(c => c.Quantidade)
                    .LessThanOrEqualTo(ComandaCliente.MAX_QUANTIDADE_ITEM)
                    .WithMessage(item => $"A quantidade máxima do {item.Nome} é {ComandaCliente.MAX_QUANTIDADE_ITEM}");
            }
        }
    }
}
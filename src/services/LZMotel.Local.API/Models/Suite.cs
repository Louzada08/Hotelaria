using LZMotel.Core.DomainObjects;
using System;
using static LZMotel.Local.API.Models.Enums.SuiteEnums;

namespace LZMotel.Local.API.Models
{
  public class Suite : Entity, IAggregateRoot
  {
    public string Nome { get; set; }
    public int SuiteNumero { get; set; }
    public Status Status { get; set; }
    public int SuiteTransferida { get; set; }
    public int Comanda { get; set; }
    public Guid CategoriaId { get; set; }

    //[JsonIgnore]
    //public Categoria Categoria { get; set; }

    //internal void AssociarCategoria(Guid categoriaId)
    //{
    //  CategoriaId = categoriaId;
    //}

    public Suite() 
    {
      Id = Guid.NewGuid();
    }

    //public Suite(int suiteNumero, Status status, 
    //  int suiteTransferencia, int comanda, Guid categoriaId)
    //{
    //  SuiteNumero = suiteNumero;
    //  Status = Status.Livre;
    //  SuiteTransferida = suiteTransferencia;
    //  Comanda = comanda;
    //  CategoriaId = categoriaId;
    //}

  }

}

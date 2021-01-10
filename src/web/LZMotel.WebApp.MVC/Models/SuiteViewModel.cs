using System;

namespace LZMotel.WebApp.MVC.Models
{
  public class SuiteViewModel
  {
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public int SuiteNumero { get; set; }
    public SuiteEnums Situacao { get; set; }
    public int SuiteTransferida { get; set; }
    public int Comanda { get; set; }
  }

  public class SuiteEnums
  {
    public enum Status
    {
      AguardandoLimpar = 1,
      AguardandoVistoria = 2,
      Limpando = 3,
      Livre = 4,
      EmManutencao = 5,
      Ocupado = 6,
      Inativo = 7
    }
  }
}
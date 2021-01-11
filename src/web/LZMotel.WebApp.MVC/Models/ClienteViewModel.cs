
using LZMotel.Core.DomainObjects;
using System;

namespace LZMotel.WebApp.MVC.Models
{
  public class ClienteViewModel
  {
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Turno { get; set; }
    //public string Email { get; set; }
    public Cpf Cpf { get; set; }
    public string Funcao { get; set; }
    public bool Excluido { get; set; }

  }
}

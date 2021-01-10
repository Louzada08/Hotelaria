
using System;

namespace LZMotel.WebApp.MVC.Models
{
  public class ClienteViewModel
  {
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Turno { get; set; }
    //public string Email { get; set; }
    //public string Cpf { get;  set; }
    public string Funcao { get; set; }
    public bool Excluido { get; set; }

  }
}

using LZMotel.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace LZMotel.Local.API.Models
{
  public class Categoria
  {
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Acronimo { get; set; }
    public List<Suite> Suites { get; set; } = new List<Suite>();

    public Categoria() 
    {
      Id = Guid.NewGuid();
    }

  }
}

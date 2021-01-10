using System;
using LZMotel.Core.DomainObjects;

namespace LZMotel.Clientes.API.Models
{
  public class Cliente : Entity, IAggregateRoot
  {
    public string Nome { get; private set; }
    public string Turno { get; private set; }
    public Email Email { get; private set; }
    public Cpf Cpf { get; private set; }
    public string Funcao { get; private set; }
    public bool Excluido { get; private set; }
    public Endereco Endereco { get; private set; }

    // EF Relation
    protected Cliente() { }

    public Cliente(Guid id, string nome, string turno, string email, string cpf, string funcao)
    {
      Id = id;
      Nome = nome;
      Email = new Email(email);
      Cpf = new Cpf(cpf);
      Turno = turno;
      Funcao = funcao;
      Excluido = false;
    }

    public void TrocarEmail(string email)
    {
      Email = new Email(email);
    }

    public void AtribuirEndereco(Endereco endereco)
    {
      Endereco = endereco;
    }
  }
}
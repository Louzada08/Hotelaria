using System;
using LZMotel.Core.Messages;

namespace LZMotel.Clientes.API.Application.Events
{
  public class ClienteRegistradoEvent : Event
  {
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Turno { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }

    public ClienteRegistradoEvent(Guid id, string nome, string turno, string email, string cpf)
    {
      AggregateId = id;
      Id = id;
      Nome = nome;
      Turno = turno;
      Email = email;
      Cpf = cpf;
    }
  }
}
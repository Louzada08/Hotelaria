using System;

namespace LZMotel.Core.Messages.Integration
{
  public class UsuarioRegistradoIntegrationEvent : IntegrationEvent
  {
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Turno { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }

    public UsuarioRegistradoIntegrationEvent(Guid id, string nome, string turno, string email, string cpf)
    {
      Id = id;
      Nome = nome;
      Turno = turno;
      Email = email;
      Cpf = cpf;
    }
  }
}
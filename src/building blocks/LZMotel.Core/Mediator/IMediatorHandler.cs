﻿using System.Threading.Tasks;
using FluentValidation.Results;
using LZMotel.Core.Messages;

namespace LZMotel.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
    }
}
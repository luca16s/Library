// -----------------------------------------------------------------------
// <copyright file="MediatorHandler.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Mediator.Handlers;

using Mediator.Commands;
using Mediator.Interfaces;
using Mediator.Notifications;

using MediatR;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using RabbitMQ.Client;

using System;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Classe de manipulação da mediação.
/// </summary>
public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator mediator;
    private readonly ConnectionFactory connectionFactory;

    /// <summary>
    /// Construtor da classe de manipulação da mediação.
    /// </summary>
    /// <param name="mediator">
    /// Interface de mediação.
    /// </param>
    /// <param name="configuration">
    /// Inteface da  configuração.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Lança exceção caso <paramref name="mediator"/> nulo.
    /// </exception>
    public MediatorHandler(
        IMediator mediator,
        IConfiguration configuration
    )
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        connectionFactory = new ConnectionFactory
        {
            Uri = new Uri(configuration["rabbitmq:uri"] ?? string.Empty)
        };
    }

    /// <summary>
    /// Enviar comando com retorno.
    /// </summary>
    /// <typeparam name="TCommand">
    /// Tipo do comando a ser enviado.
    /// </typeparam>
    /// <typeparam name="TReturn">
    /// Tipo do retorno.
    /// </typeparam>
    /// <param name="comando">
    /// Comando a ser enviado.
    /// </param>
    /// <param name="cancellation">
    /// Token de cancelamento.
    /// </param>
    public Task<TReturn> Send<TCommand, TReturn>(
        TCommand comando,
        CancellationToken cancellation = default
    ) where TReturn : notnull
      where TCommand : QueryCommand<TReturn>
    {
        return mediator.Send(comando, cancellation);
    }

    /// <summary>
    /// Enviar comando sem retorno.
    /// </summary>
    /// <typeparam name="TCommand">
    /// Tipo do comando a ser enviado.
    /// </typeparam>
    /// <param name="comando">
    /// Comando a ser enviado.
    /// </param>
    /// <param name="cancellation">
    /// Token de cancelamento.
    /// </param>
    public Task Send<TCommand>(
        TCommand comando,
        CancellationToken cancellation = default
    ) where TCommand : Command
    {
        return mediator.Send(comando, cancellation);
    }

    /// <summary>
    /// Lançar notificação.
    /// </summary>
    /// <typeparam name="TNotification">
    /// Tipo da notificação.
    /// </typeparam>
    /// <param name="evento">
    /// Evento a ser lançado.
    /// </param>
    /// <param name="enqueue">
    /// Deve enfileirar?
    /// </param>
    /// <param name="cancellation">
    /// Token de cancelamento.
    /// </param>
    public Task Raise<TNotification>(
        TNotification evento,
        CancellationToken cancellation = default
    ) where TNotification : DomainNotification
    {
        return mediator.Publish(evento, cancellation);
    }

    /// <summary>
    /// Lançar notificação de erro.
    /// </summary>
    /// <typeparam name="TNotification">
    /// Tipo da notificação.
    /// </typeparam>
    /// <param name="evento">
    /// Evento a ser lançado.
    /// </param>
    /// <param name="enqueue">
    /// Deve enfileirar?
    /// </param>
    /// <param name="cancellation">
    /// Token de cancelamento.
    /// </param>
    public Task RaiseError<TNotification>(
        TNotification evento,
        CancellationToken cancellation = default
    ) where TNotification : ErrorNotification
    {
        return mediator.Publish(evento, cancellation);
    }

    /// <summary>
    /// Publicar comando em uma fila.
    /// </summary>
    /// <typeparam name="TCommand">
    /// Tipo do comando.
    /// </typeparam>
    /// <param name="comando">
    /// Comando a ser publicado.
    /// </param>
    /// <param name="cancellation">
    /// Token de cancelamento.
    /// </param>
    public Task PublishQueue<TCommand>(
        TCommand comando,
        CancellationToken cancellation = default
    )
    {
        using (IConnection? connection = connectionFactory.CreateConnection())
        {
            using (IModel? channel = connection.CreateModel())
            {
                _ = channel.QueueDeclare(
                    queue: typeof(TCommand).FullName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                byte[]? bodyMessage = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(comando));

                channel.BasicPublish(exchange: "",
                                     routingKey: typeof(TCommand).FullName,
                                     basicProperties: null,
                                     body: bodyMessage);

                Console.WriteLine($"Mensagem do tipo {typeof(TCommand).FullName} enviada.");
            }
        }

        return Task.CompletedTask;
    }
}

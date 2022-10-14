// -----------------------------------------------------------------------
// <copyright file="MediatorHandler.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace CQRS.Handlers.Struct
{
    using CQRS.Events.Struct;
    using CQRS.Commands.Struct;
    using CQRS.Interfaces.Struct;

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
        private readonly IMediator _mediator;
        private readonly IConnectionFactory _connectionFactory;

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
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(configuration["rabbitmq:uri"])
            };
        }

        /// <summary>
        /// Enviar comandos.
        /// </summary>
        /// <typeparam name="TCommand">
        /// Tipo do comando a ser enviado.
        /// </typeparam>
        /// <typeparam name="TId">
        /// Tipo do identificador.
        /// </typeparam>
        /// <typeparam name="TResponse">
        /// Tipo do retorno.
        /// </typeparam>
        /// <param name="comando">
        /// Comando a ser enviado.
        /// </param>
        /// <param name="shouldEnqueue">
        /// Deve enfileirar?
        /// </param>
        /// <param name="cancellation">
        /// Token de cancelamento.
        /// </param>
        public Task SendCommand<TCommand, TId, TResponse>(TCommand comando,
                                   bool shouldEnqueue = false,
                                   CancellationToken cancellation = default)
            where TCommand : Command<TId, TResponse>
            where TResponse : struct
            where TId : struct
        {
            return shouldEnqueue ?
                PublishQueue(comando, cancellation) :
                _mediator.Send(comando, cancellation);
        }

        /// <summary>
        /// Lançar evento.
        /// </summary>
        /// <typeparam name="TCommand">
        /// Tipo do evento.
        /// </typeparam>
        /// <typeparam name="TId">
        /// Tipo do identificador.
        /// </typeparam>
        /// <typeparam name="TResponse">
        /// Tipo do retorno.
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
        public Task RaiseEvent<TCommand, TId, TResponse>(
            TCommand evento,
            bool enqueue = false,
            CancellationToken cancellation = default)
            where TResponse : struct
            where TCommand : Event<TId, TResponse>
            where TId : struct
            => _mediator.Publish(evento, cancellation);

        /// <summary>
        /// Publicar Fila
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
        public Task PublishQueue<TCommand>(TCommand comando,
                                    CancellationToken cancellation = default)
        {
            using (IConnection? connection = _connectionFactory.CreateConnection())
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
}

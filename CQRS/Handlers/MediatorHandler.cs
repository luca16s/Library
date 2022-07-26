// -----------------------------------------------------------------------
// <copyright file="MediatorHandler.cs" company="Îakaré Software'Oka">
//     Copyright (c) Îakaré Software'Oka.
//     All rights reserved.
//     Licensed under the MIT license.
//     See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace CQRS.Handlers
{
    using CQRS.Commands;
    using CQRS.Events;
    using CQRS.Interfaces;

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
        /// <typeparam name="T">
        /// Tipo do comando a ser enviado.
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
        /// <returns>
        /// </returns>
        public Task SendCommand<T>(T comando,
                                   bool shouldEnqueue = false,
                                   CancellationToken cancellation = default)
            where T : Command
        {
            return shouldEnqueue ?
                PublishQueue(comando, cancellation) :
                _mediator.Send(comando, cancellation);
        }

        /// <summary>
        /// Lançar evento.
        /// </summary>
        /// <typeparam name="T">
        /// Tipo do evento.
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
        /// <returns>
        /// </returns>
        public Task RaiseEvent<T>(T evento,
                                  bool enqueue = false,
                                  CancellationToken cancellation = default)
            where T : Event => _mediator.Publish(evento, cancellation);

        /// <summary>
        /// Publicar Fila
        /// </summary>
        /// <typeparam name="T">
        /// Tipo do comando.
        /// </typeparam>
        /// <param name="comando">
        /// Comando a ser publicado.
        /// </param>
        /// <param name="cancellation">
        /// Token de cancelamento.
        /// </param>
        /// <returns>
        /// </returns>
        public Task PublishQueue<T>(T comando,
                                    CancellationToken cancellation = default)
        {
            using (IConnection? connection = _connectionFactory.CreateConnection())
            {
                using (IModel? channel = connection.CreateModel())
                {
                    _ = channel.QueueDeclare(
                        queue: typeof(T).FullName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    byte[]? bodyMessage = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(comando));

                    channel.BasicPublish(exchange: "",
                                         routingKey: typeof(T).FullName,
                                         basicProperties: null,
                                         body: bodyMessage);

                    Console.WriteLine($"Mensagem do tipo {typeof(T).FullName} enviada.");
                }
            }

            return Task.CompletedTask;
        }
    }
}

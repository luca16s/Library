namespace Library.Tests.Web
{
    using AutoMapper;

    using CQRS.Handlers;
    using CQRS.Interfaces;
    using CQRS.Notifications;

    using FluentAssertions;

    using global::Core.Interfaces.Services;
    using global::Core.Models;
    using global::Web.Controller;

    using Moq;

    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    using Xunit;

    public class ApiControllerTests
    {
        public class Modelo : Entity<long>
        {
            public Modelo(long id) : base(id) { }

            public override bool IsConsistent() { throw new NotImplementedException(); }
        }

        public class Servico : IService<Modelo, long>
        {
            public Task Create(Modelo item)
            {
                throw new NotImplementedException();
            }

            public Task Delete(Modelo item)
            {
                throw new NotImplementedException();
            }

            public Task<Modelo> Get(long id)
            {
                throw new NotImplementedException();
            }

            public IQueryable<Modelo> GetAll(int amount)
            {
                throw new NotImplementedException();
            }

            public IQueryable<Modelo> Search(Expression<Func<Modelo, bool>> predicate)
            {
                throw new NotImplementedException();
            }

            public Task Update(long id, Modelo item)
            {
                throw new NotImplementedException();
            }
        }

        public class Controller : ApiController<Servico, Modelo, long, Modelo>
        {
            public Controller(
                IMapper mapper,
                IMediatorHandler mediator,
                IService<Modelo, long> service,
                IDomainNotificationHandler<long, Modelo> notifications
            ) : base(mapper, mediator, service, notifications) { }

            public bool IsOperacaoValida()
            {
                return IsOperationValid();
            }
        }

        [Fact]
        public void DeveInstanciarServicoBase()
        {
            var servico = new Servico();
            var mapper = new Mock<IMapper>().Object;
            var mediator = new Mock<IMediatorHandler>().Object;
            var notificationHandler = new DomainNotificationHandler<long, Modelo>();

            var controller = new Controller(
                mapper,
                mediator,
                servico,
                notificationHandler
            );

            _ = controller.Service.Should().NotBeNull();
        }

        [Fact]
        public void DeveRetornarFalseQuandoNotificacoesNaoInstanciadas()
        {
            var servico = new Servico();
            var mapper = new Mock<IMapper>().Object;
            var mediator = new Mock<IMediatorHandler>().Object;
            var notificationHandler = new DomainNotificationHandler<long, Modelo>();
            var notification = new DomainNotification<long, Modelo>("", "");

            _ = notificationHandler.Handle(notification, CancellationToken.None);

            var controller = new Controller(
                mapper,
                mediator,
                servico,
                notificationHandler
            );

            _ = controller.IsOperacaoValida().Should().BeFalse();
        }

        [Fact]
        public void DeveRetornarFalseQuandoExistemNotificacoes()
        {
            var servico = new Servico();
            var mapper = new Mock<IMapper>().Object;
            var mediator = new Mock<IMediatorHandler>().Object;
            var notificationHandler = new DomainNotificationHandler<long, Modelo>();
            var notification = new DomainNotification<long, Modelo>("", "");

            notificationHandler.Handle(notification, CancellationToken.None);

            var controller = new Controller(
                mapper,
                mediator,
                servico,
                notificationHandler
            );

            _ = controller.IsOperacaoValida().Should().BeFalse();
        }

        [Fact]
        public void DeveRetornarTrueQuandoNaoExistemNotificacoes()
        {
            var servico = new Servico();
            var mapper = new Mock<IMapper>().Object;
            var mediator = new Mock<IMediatorHandler>().Object;
            var notificationHandler = new DomainNotificationHandler<long, Modelo>();

            var controller = new Controller(
                mapper,
                mediator,
                servico,
                notificationHandler
            );

            _ = controller.IsOperacaoValida().Should().BeTrue();
        }

        [Fact]
        public void DeveRetornarTrueQuandoExistemNotificacoes()
        {
            var servico = new Servico();
            var mapper = new Mock<IMapper>().Object;
            var mediator = new Mock<IMediatorHandler>().Object;
            var notificationHandler = new DomainNotificationHandler<long, Modelo>();
            var notificacao = new DomainNotification<long, Modelo>("", "");
            _ = notificationHandler.Handle(notificacao, CancellationToken.None);

            var controller = new Controller(
                mapper,
                mediator,
                servico,
                notificationHandler
            );

            _ = controller.IsOperacaoValida().Should().BeFalse();
        }
    }
}
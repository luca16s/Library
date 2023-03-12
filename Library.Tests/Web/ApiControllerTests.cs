namespace Library.Tests.Web
{
    using AutoMapper;

    using FluentAssertions;

    using Library.Tests.Common;
    using Library.Tests.Common.Interfaces;

    using Mediator.Handlers;
    using Mediator.Interfaces;
    using Mediator.Notifications;

    using Moq;

    using System.Threading;

    using Xunit;

    public class ApiControllerTests
    {
        [Fact]
        public void DeveInstanciarServicoBase()
        {
            var repositorio = new Mock<IPessoaRepository>().Object;
            var servico = new PessoaService(repositorio);
            var mapper = new Mock<IMapper>().Object;
            var mediator = new Mock<IMediatorHandler>().Object;
            var notificationHandlerA = new DomainNotificationHandler<long>();
            var notificationHandlerB = new DomainNotificationHandler<long, Pessoa>();

            var controller = new PessoaController(
                mapper,
                mediator,
                servico,
                notificationHandlerA,
                notificationHandlerB
            );

            _ = controller.Service.Should().NotBeNull();
        }

        [Fact]
        public void DeveRetornarTrueQuandoExistemNotificacoes()
        {
            var repositorio = new Mock<IPessoaRepository>().Object;
            var servico = new PessoaService(repositorio);
            var mapper = new Mock<IMapper>().Object;
            var mediator = new Mock<IMediatorHandler>().Object;
            var notificationHandlerA = new DomainNotificationHandler<long>();
            var notificationHandlerB = new DomainNotificationHandler<long, Pessoa>();
            var notificacao = new DomainNotification<long, Pessoa>("", "");
            _ = notificationHandlerB.Handle(notificacao, CancellationToken.None);

            var controller = new PessoaController(
                mapper,
                mediator,
                servico,
                notificationHandlerA,
                notificationHandlerB
            );

            _ = controller.IsOperacaoValida().Should().BeFalse();
        }

        [Fact]
        public void DeveRetornarFalseQuandoExistemNotificacoes()
        {
            var repositorio = new Mock<IPessoaRepository>().Object;
            var servico = new PessoaService(repositorio);
            var mapper = new Mock<IMapper>().Object;
            var mediator = new Mock<IMediatorHandler>().Object;
            var notificationHandlerA = new DomainNotificationHandler<long>();
            var notificationHandlerB = new DomainNotificationHandler<long, Pessoa>();
            var notification = new DomainNotification<long, Pessoa>("", "");

            _ = notificationHandlerB.Handle(notification, CancellationToken.None);

            var controller = new PessoaController(
                mapper,
                mediator,
                servico,
                notificationHandlerA,
                notificationHandlerB
            );

            _ = controller.IsOperacaoValida().Should().BeFalse();
        }

        [Fact]
        public void DeveRetornarTrueQuandoNaoExistemNotificacoes()
        {
            var repositorio = new Mock<IPessoaRepository>().Object;
            var servico = new PessoaService(repositorio);
            var mapper = new Mock<IMapper>().Object;
            var mediator = new Mock<IMediatorHandler>().Object;
            var notificationHandlerA = new DomainNotificationHandler<long>();
            var notificationHandlerB = new DomainNotificationHandler<long, Pessoa>();

            var controller = new PessoaController(
                mapper,
                mediator,
                servico,
                notificationHandlerA,
                notificationHandlerB
            );

            _ = controller.IsOperacaoValida().Should().BeTrue();
        }

        [Fact]
        public void DeveVerificarSeMetodoDerivadoDoServicoFoiChamado()
        {
            var servico = new Mock<IPessoaService>();
            var mapper = new Mock<IMapper>().Object;
            var mediator = new Mock<IMediatorHandler>().Object;
            var notificationHandlerA = new DomainNotificationHandler<long>();
            var notificationHandlerB = new DomainNotificationHandler<long, Pessoa>();
            var notificacao = new DomainNotification<long, Pessoa>("", "");
            _ = notificationHandlerB.Handle(notificacao, CancellationToken.None);

            var controller = new PessoaController(
                mapper,
                mediator,
                servico.Object,
                notificationHandlerA,
                notificationHandlerB
            );

            _ = controller.GetValue();

            servico.Verify(x => x.GetStringValue(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void DeveRetornarFalseQuandoNotificacoesNaoInstanciadas()
        {
            var repositorio = new Mock<IPessoaRepository>().Object;
            var servico = new PessoaService(repositorio);
            var mapper = new Mock<IMapper>().Object;
            var mediator = new Mock<IMediatorHandler>().Object;
            var notificationHandlerA = new DomainNotificationHandler<long>();
            var notificationHandlerB = new DomainNotificationHandler<long, Pessoa>();
            var notification = new DomainNotification<long, Pessoa>("", "");

            _ = notificationHandlerB.Handle(notification, CancellationToken.None);

            var controller = new PessoaController(
                mapper,
                mediator,
                servico,
                notificationHandlerA,
                notificationHandlerB
            );

            _ = controller.IsOperacaoValida().Should().BeFalse();
        }
    }
}
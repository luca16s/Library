namespace Library.Tests.Api;

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
    public void DeveRetornarTrueQuandoExistemNotificacoes()
    {
        var repositorio = new Mock<IPessoaRepository>().Object;
        var servico = new PessoaService(repositorio);
        var mediator = new Mock<IMediatorHandler>().Object;
        var notificationHandler = new DomainNotificationHandler();
        var notificacao = new DomainNotification { Id = "", Value = "" };
        _ = notificationHandler.Handle(notificacao, CancellationToken.None);

        var controller = new PessoaController(
            servico,
            mediator,
            notificationHandler
        );

        _ = controller.IsOperacaoValida().Should().BeFalse();
    }

    [Fact]
    public void DeveRetornarFalseQuandoExistemNotificacoes()
    {
        var repositorio = new Mock<IPessoaRepository>().Object;
        var servico = new PessoaService(repositorio);
        var mediator = new Mock<IMediatorHandler>().Object;
        var notificationHandler = new DomainNotificationHandler();
        var notificacao = new DomainNotification { Id = "", Value = "" };

        _ = notificationHandler.Handle(notificacao, CancellationToken.None);

        var controller = new PessoaController(
            servico,
            mediator,
            notificationHandler
        );

        _ = controller.IsOperacaoValida().Should().BeFalse();
    }

    [Fact]
    public void DeveRetornarTrueQuandoNaoExistemNotificacoes()
    {
        var repositorio = new Mock<IPessoaRepository>().Object;
        var servico = new PessoaService(repositorio);
        var mediator = new Mock<IMediatorHandler>().Object;
        var notificationHandler = new DomainNotificationHandler();

        var controller = new PessoaController(
            servico,
            mediator,
            notificationHandler
        );

        _ = controller.IsOperacaoValida().Should().BeTrue();
    }

    [Fact]
    public void DeveVerificarSeMetodoDerivadoDoServicoFoiChamado()
    {
        var servico = new Mock<IPessoaService>();
        var mediator = new Mock<IMediatorHandler>().Object;
        var notificationHandler = new DomainNotificationHandler();
        var notificacao = new DomainNotification { Id = "", Value = "" };
        _ = notificationHandler.Handle(notificacao, CancellationToken.None);

        var controller = new PessoaController(
            servico.Object,
            mediator,
            notificationHandler
        );

        _ = controller.GetValue();

        servico.Verify(x => x.GetStringValue(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void DeveRetornarFalseQuandoNotificacoesNaoInstanciadas()
    {
        var repositorio = new Mock<IPessoaRepository>().Object;
        var servico = new PessoaService(repositorio);
        var mediator = new Mock<IMediatorHandler>().Object;
        var notificationHandler = new DomainNotificationHandler();
        var notificacao = new DomainNotification { Id = "", Value = "" };

        _ = notificationHandler.Handle(notificacao, CancellationToken.None);

        var controller = new PessoaController(
            servico,
            mediator,
            notificationHandler
        );

        _ = controller.IsOperacaoValida().Should().BeFalse();
    }
}
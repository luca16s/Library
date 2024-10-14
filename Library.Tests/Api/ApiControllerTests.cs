namespace Library.Tests.Api;

using FluentAssertions;

using Library.Tests.Common;
using Library.Tests.Common.Interfaces;

using Moq;

using System.Threading;

using Xunit;

public class ApiControllerTests
{
    [Fact]
    [Trait("Método: ", "IsOperacaoValida")]
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
    [Trait("Método: ", "IsOperacaoValida")]
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
    [Trait("Método: ", "IsOperacaoValida")]
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
    [Trait("Método: ", "GetValue")]
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
    [Trait("Método: ", "IsOperacaoValida")]
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
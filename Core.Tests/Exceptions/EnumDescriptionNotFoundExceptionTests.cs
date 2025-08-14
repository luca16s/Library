namespace Core.Tests.Exceptions;
using Core.Services.Exceptions;

using Shouldly;

using System.Diagnostics.CodeAnalysis;

using Test.Types;

[ExcludeFromCodeCoverage]
[Trait("Core", "Exceptions")]
public class EnumDescriptionNotFoundExceptionTests
{
    [Test]
    public void DeveLancarMensagemPadraoParaConstrutorBase()
    {
        const string expected = "Enum informado não contém descrição.";

        var exception = new EnumDescriptionNotFoundException();

        exception.Message.ShouldBe(expected);
    }

    [Test]
    public void DeveLancarMensagemPadraoComMensagemPersonalizadaDoUsuario()
    {
        var message = "Mensagem do usuário.";
        string expected = $"Enum informado não contém descrição. {message}";

        var exception = new EnumDescriptionNotFoundException(message);

        exception.Message.ShouldBe(expected);
    }

    [Test]
    public void DeveLancarMensagemPadraoComMensagemPersonalizadaDoUsuarioComInnerException()
    {
        var message = "Mensagem do usuário.";
        const string innerMessage = "Inner Exception.";
        string expected = $"Enum informado não contém descrição. {message}";

        var exception = new EnumDescriptionNotFoundException(message, new Exception(innerMessage));

        exception.Message.ShouldBe(expected);
        _ = exception.InnerException.ShouldNotBeNull();
        exception.InnerException?.Message.ShouldBe(innerMessage);
    }
}

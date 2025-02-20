namespace Core.Tests.Exceptions;

using Core.Services.Exceptions;

using Shouldly;

using System.Diagnostics.CodeAnalysis;

using Test.Types;

[ExcludeFromCodeCoverage]
[Trait("Core", "Exceptions")]
public class EnumItemNotFoundExceptionTests
{
    [Test]
    public void DeveLancarMensagemPadraoParaConstrutorBase()
    {
        const string expected = "Item não encontrado no enumerador.";

        var exception = new EnumItemNotFoundException();

        exception.Message.ShouldBe(expected);
    }

    [Test]
    public void DeveLancarMensagemPadraoComMensagemPersonalizadaDoUsuario()
    {
        var message = "Mensagem do usuário.";
        string expected = $"Item não encontrado no enumerador. {message}";

        var exception = new EnumItemNotFoundException(message);

        exception.Message.ShouldBe(expected);
    }

    [Test]
    public void DeveLancarMensagemPadraoComMensagemPersonalizadaDoUsuarioComInnerException()
    {
        var message = "Mensagem do usuário.";
        const string innerMessage = "Inner Exception.";
        string expected = $"Item não encontrado no enumerador. {message}";

        var exception = new EnumItemNotFoundException(message, new Exception(innerMessage));

        exception.Message.ShouldBe(expected);
        exception.InnerException.ShouldNotBeNull();
        exception.InnerException?.Message.ShouldBe(innerMessage);
    }
}

namespace Core.Tests.Exceptions;

using Core.Exceptions;

using FluentAssertions;

using System.Diagnostics.CodeAnalysis;

using Test.Types;

[ExcludeFromCodeCoverage]
[Trait("Core", "Exception")]
public class EnumItemNotFoundExceptionTests
{
    [Test]
    public void DeveLancarMensagemPadraoParaConstrutorBase()
    {
        const string expected = "Item não encontrado no enumerador.";

        var exception = new EnumItemNotFoundException();

        exception.Message.Should().Be(expected);
    }

    [Test]
    public void DeveLancarMensagemPadraoComMensagemPersonalizadaDoUsuario()
    {
        var message = "Mensagem do usuário.";
        string expected = $"Item não encontrado no enumerador. {message}";

        var exception = new EnumItemNotFoundException(message);

        exception.Message.Should().Be(expected);
    }

    [Test]
    public void DeveLancarMensagemPadraoComMensagemPersonalizadaDoUsuarioComInnerException()
    {
        var message = "Mensagem do usuário.";
        const string innerMessage = "Inner Exception.";
        string expected = $"Item não encontrado no enumerador. {message}";

        var exception = new EnumItemNotFoundException(message, new Exception(innerMessage));

        exception.Message.Should().Be(expected);
        exception.InnerException.Should().NotBeNull();
        exception.InnerException?.Message.Should().Be(innerMessage);
    }
}

namespace Core.Tests.Extensions;

using Core.Services.Exceptions;
using Core.Services.Extensions;

using FluentAssertions;

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

using Test.Types;

[ExcludeFromCodeCoverage]
[Trait("Core", "Extensions")]
public class StringExtensionsTests
{
    private enum EnumTeste
    {
        [Description("Item - A")]
        A,
        [Description]
        B,
        C,
    }

    [Test]
    public void DeveRetornarMensagemFormatada()
    {
        var expected = "Esta é a mensagem base. Esta é a mensagem do usuário.";

        var result = "Esta é a mensagem base. {0}".FormatMessage("Esta é a mensagem do usuário.");

        result.Should().Be(expected);
    }

    [Test]
    public void DeveRetornarMensagemBaseQuandoSemItensExtras()
    {
        var expected = "Esta é a mensagem base.";

        var result = "Esta é a mensagem base. {0}".FormatMessage();

        result.Should().Be(expected);
    }

    [Test]
    public void DeveRetornarMensagemBaseQuandoItemExtraNulo()
    {
        var expected = "Esta é a mensagem base.";

        var result = "Esta é a mensagem base. {0}".FormatMessage(null);

        result.Should().Be(expected);
    }

    [Test]
    public void DeveRetornarEnumEquivalenteTexto()
    {
        var expected = EnumTeste.A;

        var result = "Item - A".GetEnumFromDescription<EnumTeste>();

        result.Should().Be(expected);
    }

    [Test]
    public void DeveLancarExceptionCasoItemVazio()
    {
        _ = Assert.Throws<ArgumentException>(static () => {
            _ = string.Empty.GetEnumFromDescription<EnumTeste>();
        });
    }

    [Test]
    public void DeveLancarExceptionCasoItemNaoEncontrado()
    {
        _ = Assert.Throws<EnumItemNotFoundException>(static () => {
            _ = "Item - C".GetEnumFromDescription<EnumTeste>();
        });
    }
}

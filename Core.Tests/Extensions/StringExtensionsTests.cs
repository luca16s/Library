namespace Core.Tests.Extensions;

using Core.Services.Exceptions;
using Core.Services.Extensions;

using Shouldly;

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
    public void FormatTextDeveRetornarMensagemFormatada()
    {
        var expected = "Esta é a mensagem base. Esta é a mensagem do usuário.";

        var result = "Esta é a mensagem base. {0}".FormatText("Esta é a mensagem do usuário.");

        result.ShouldBe(expected);
    }

    [Test]
    public void FormatTextDeveRetornarMensagemBaseQuandoSemItensExtras()
    {
        var expected = "Esta é a mensagem base.";

        var result = "Esta é a mensagem base. {0}".FormatText();

        result.ShouldBe(expected);
    }

    [Test]
    public void FormatTextDeveRetornarMensagemBaseQuandoItemExtraNulo()
    {
        var expected = "Esta é a mensagem base.";

        var result = "Esta é a mensagem base. {0}".FormatText(null as string);

        result.ShouldBe(expected);
    }

    [Test]
    public void GetEnumFromDescriptionDeveRetornarEnumEquivalenteTexto()
    {
        var expected = EnumTeste.A;

        var result = "Item - A".GetEnumFromDescription<EnumTeste>();

        result.ShouldBe(expected);
    }

    [Test]
    public void GetEnumFromDescriptionDeveLancarExceptionCasoItemVazio()
    {
        _ = Assert.Throws<ArgumentException>(static () => {
            _ = string.Empty.GetEnumFromDescription<EnumTeste>();
        });
    }

    [Test]
    public void GetEnumFromDescriptionDeveLancarExceptionCasoItemNaoEncontrado()
    {
        _ = Assert.Throws<EnumItemNotFoundException>(static () => {
            _ = "Item - C".GetEnumFromDescription<EnumTeste>();
        });
    }
}

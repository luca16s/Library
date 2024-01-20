namespace Library.Tests.Core.Extensions;

using FluentAssertions;

using global::Core.Exceptions;
using global::Core.Extensions;

using Library.Tests.Core;

using Xunit;

public class StringExtensionsTest
{
    [Fact]
    public void ShouldReturnEnumValueFromText()
    {
        string texto = "TESTE 1";

        EOK result = texto.GetEnumValueFromDescription<EOK>();

        _ = result.Should().Be(EOK.TESTE1);
    }

    [Fact]
    public void ShouldThrowExceptionWhenItemNotEncoutered()
    {
        string texto = "ABC 1";

        _ = this.Invoking(_ => texto.GetEnumValueFromDescription<EOK>())
            .Should()
            .Throw<EnumItemNotFoundException>()
            .WithMessage($"Item não encontrado no enumerador.\n - {texto}");
    }

    [Fact]
    public void ShouldReturnFormatedMessage()
    {
        var expected = "Mensagem teste Nome";
        var actual = "Mensagem teste {0}".FormatMessage("Nome");

        _ = expected.Should().Be(actual);
    }
}
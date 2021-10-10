namespace CoreLibrary.Tests
{

    using FluentAssertions;

    using KapiCoreLib.Exceptions;
    using KapiCoreLib.Extensions;

    using Xunit;

    public class StringExtensionsTests
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
    }
}
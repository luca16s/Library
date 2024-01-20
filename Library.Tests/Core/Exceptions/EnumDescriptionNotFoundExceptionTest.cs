namespace Library.Tests.Core.Exceptions;

using FluentAssertions;

using global::Core.Exceptions;

using System;

using Xunit;

public class EnumDescriptionNotFoundExceptionTest
{
    [Fact]
    [Trait("Método: ", "Throw")]
    public void ShouldShowCustomizedMessage()
    {
        _ = this.Invoking(g => throw new EnumDescriptionNotFoundException("Teste"))
            .Should()
            .Throw<EnumDescriptionNotFoundException>()
            .WithMessage("Enum informado não contém descrição.\n - Teste");
    }

    [Fact]
    [Trait("Método: ", "Throw")]
    public void ShouldShowDefaultMessage()
    {
        _ = this.Invoking(g => throw new EnumDescriptionNotFoundException())
            .Should()
            .Throw<EnumDescriptionNotFoundException>()
            .WithMessage("Enum informado não contém descrição.");
    }

    [Fact]
    [Trait("Método: ", "Throw")]
    public void ShouldShowSecondMessageWithInnerException()
    {
        _ = this.Invoking(g => throw new EnumDescriptionNotFoundException("Teste", new ArgumentException()))
            .Should()
            .Throw<EnumDescriptionNotFoundException>()
            .WithMessage("Enum informado não contém descrição.\n - Teste");
    }
}
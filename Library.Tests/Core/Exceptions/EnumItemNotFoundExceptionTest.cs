namespace Library.Tests.Core.Exceptions;

using FluentAssertions;

using global::Core.Exceptions;

using System;

using Xunit;

public class EnumItemNotFoundExceptionTest
{
    [Fact]
    public void ShouldShowCustomizedMessage()
    {
        _ = this.Invoking(g => throw new EnumItemNotFoundException("Teste"))
            .Should()
            .Throw<EnumItemNotFoundException>()
            .WithMessage("Item não encontrado no enumerador.\n - Teste");
    }

    [Fact]
    public void ShouldShowDefaultMessage()
    {
        _ = this.Invoking(g => throw new EnumItemNotFoundException())
            .Should()
            .Throw<EnumItemNotFoundException>()
            .WithMessage("Item não encontrado no enumerador.");
    }

    [Fact]
    public void ShouldShowSecondMessageWithInnerException()
    {
        _ = this.Invoking(g => throw new EnumItemNotFoundException("Teste", new ArgumentException()))
            .Should()
            .Throw<EnumItemNotFoundException>()
            .WithMessage("Item não encontrado no enumerador.\n - Teste");
    }
}
namespace Library.Tests.Core.Models;

using Bogus;

using FluentAssertions;

using global::Core.Models;

using Library.Tests.Core;

using Xunit;

public class EnumModelTest
{
    [Fact]
    [Trait("Método: ", "Model")]
    public void EnumModelShouldNotBeNull()
    {
        EnumModel enumModelo = new Faker<EnumModel>()
            .RuleFor(g => g.Description, f => f.Lorem.Word())
            .Generate();

        _ = enumModelo.Should().NotBeNull();
    }

    [Fact]
    [Trait("Método: ", "Value")]
    public void EnumValueShouldNotReturnNull()
    {
        EnumModel enumModelo = new Faker<EnumModel>()
            .RuleFor(g => g.Value, f => EError.TESTE1)
            .Generate();

        _ = enumModelo.Value.Should().NotBeNull();
    }

    [Fact]
    [Trait("Método: ", "Description")]
    public void EnumDescriptionShouldNotReturnNull()
    {
        EnumModel enumModelo = new Faker<EnumModel>()
            .RuleFor(g => g.Description, f => f.Lorem.Word())
            .Generate();

        _ = enumModelo.Description.Should().NotBeNull();
    }
}
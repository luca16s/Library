namespace Core.Tests.Models
{
    using Bogus;

    using Core.Models;

    using FluentAssertions;

    using Xunit;

    public class EnumModelTests
    {
        [Fact]
        public void EnumModelShouldNotBeNull()
        {
            EnumModel enumModelo = new Faker<EnumModel>()
                .RuleFor(g => g.Description, f => f.Lorem.Word())
                .Generate();

            _ = enumModelo.Should().NotBeNull();
        }

        [Fact]
        public void EnumValueShouldNotReturnNull()
        {
            EnumModel enumModelo = new Faker<EnumModel>()
                .RuleFor(g => g.Value, f => EError.TESTE1)
                .Generate();

            _ = enumModelo.Value.Should().NotBeNull();
        }

        [Fact]
        public void EnumDescriptionShouldNotReturnNull()
        {
            EnumModel enumModelo = new Faker<EnumModel>()
                .RuleFor(g => g.Description, f => f.Lorem.Word())
                .Generate();

            _ = enumModelo.Description.Should().NotBeNull();
        }
    }
}
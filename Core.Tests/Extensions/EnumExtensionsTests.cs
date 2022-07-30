namespace Core.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Core.Exceptions;
    using Core.Extensions;
    using Core.Models;
    using FluentAssertions;

    using Microsoft.VisualStudio.CodeCoverage;

    using Xunit;

    public class EnumExtensionsTests
    {
        [Fact]
        public void DescriptionShouldStringEmptyWhenNull()
        {
            _ = this.Invoking(g => default(Enum).Description())
                 .Should()
                 .Throw<ArgumentNullException>()
                 .WithMessage("Valor do enum não pode ser nulo. (Parameter 'value')");
        }

        [Fact]
        public void GetAllValuesAndDescriptionsShouldReturnListOfItems()
        {
            EnumModel[] listaModelo = new EnumModel[]
            {
                new EnumModel { Value = EOK.TESTE1, Description ="TESTE 1" },
                new EnumModel { Value = EOK.TESTE2, Description ="TESTE 2" },
                new EnumModel { Value = EOK.TESTE3, Description ="TESTE 3" },
            };

            IEnumerable<EnumModel> retorno = EOK.TESTE1.GetAllValuesAndDescriptions();

            _ = retorno.Should().NotBeEmpty()
                .And.HaveCount(3)
                .And.OnlyHaveUniqueItems()
                .And.BeEquivalentTo(listaModelo);
        }

        [Fact]
        public void DescriptionShouldReturnValueOfDescriptionAnnotation()
        {
            string description = EOK.TESTE1.Description();

            _ = description.Should().Be("TESTE 1");
        }

        [Fact]
        public void DescriptionShouldThrowExceptionWhenNoDescriptionIsProvided()
        {
            _ = this.Invoking(g => EError.TESTE3.Description())
                .Should()
                .Throw<EnumDescriptionNotFoundException>()
                .WithMessage("Enum informado não contém descrição.");
        }
    }
}
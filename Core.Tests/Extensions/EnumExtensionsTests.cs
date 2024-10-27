namespace Core.Tests.Extensions;

using Core.Services.Exceptions;
using Core.Services.Extensions;
using Core.Models;

using FluentAssertions;

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

using Test.Types;

[ExcludeFromCodeCoverage]
[Trait("Core", value: "Extensions")]
public class EnumExtensionsTests
{
    private enum EnumTeste
    {
        [Description("Item - A")]
        A,
        [Description]
        B,
        C,
    }

    private enum EnumTeste2
    {
        [Description("Item - A")]
        A,
        [Description]
        B,
        C,
        D
    }

    private enum EnumTeste3
    {
        [Description("Item - A")]
        A,
        [Description("Item - B")]
        B,
        [Description("Item - C")]
        C,
        [Description("Item - D")]
        D
    }

    [Test]
    public void DescriptionDeveLancarExceptionCasoValorPassadoNulo()
    {
        EnumTeste? enumTeste = null;

        Assert.Throws<ArgumentNullException>(enumTeste.Description);
    }

    [Test]
    public void DescriptionDeveLancarExceptionCasoValorPassadoDeOutroTipo()
    {
        EnumTeste enumTeste = (EnumTeste)EnumTeste2.D;

        Assert.Throws<ArgumentNullException>(enumTeste.Description);
    }

    [Test]
    public void DescriptionDeveLancarExceptionCasoEnumSemDescricao()
    {
        Assert.Throws<EnumDescriptionNotFoundException>(EnumTeste.C.Description);
    }

    [Test]
    public void DescriptionDeveLancarExceptionCasoEnumDescricaoVazia()
    {
        Assert.Throws<EnumDescriptionNotFoundException>(EnumTeste.B.Description);
    }

    [Test]
    public void DescriptionDeveRetornarDescricaoEnum()
    {
        var result = EnumTeste.A.Description();

        result.Should().Be("Item - A");
    }

    [Test]
    public void GetValuesAndDescriptionsDeveRetornarTodosItensDoEnum()
    {
        var expected = new List<EnumModel>
        {
            new(
                EnumTeste3.A,
                "Item - A"
            ),
            new(
                EnumTeste3.B,
                "Item - B"
            ),
            new(
                EnumTeste3.C,
                "Item - C"
            ),
            new(
                EnumTeste3.D,
                "Item - D"
            ),
        };

        var result = EnumTeste3.A.GetValuesAndDescriptions();

        result.Should().BeEquivalentTo(expected);
    }
}

namespace Core.Tests.Extensions;

using Core.Exceptions;
using Core.Extensions;

using FluentAssertions;

using System;
using System.ComponentModel;

using Test.Types;

public enum EnumTeste
{
    [Description("Item - A")]
    A,
    [Description]
    B,
    C,
}

[Trait("Core", value: "Extensions")]
public class EnumExtensionTests
{
    [Test]
    public void DeveLancarExceptionCasoValorPassadoNulo()
    {
        EnumTeste? enumTeste = null;

        Assert.Throws<ArgumentNullException>(enumTeste.Description);
    }

    [Test]
    public void DeveLancarExceptionCasoEnumSemDescricao()
    {
        Assert.Throws<EnumDescriptionNotFoundException>(EnumTeste.C.Description);
    }

    [Test]
    public void DeveLancarExceptionCasoEnumDescricaoVazia()
    {
        Assert.Throws<EnumDescriptionNotFoundException>(EnumTeste.B.Description);
    }

    [Test]
    public void DeveRetornarDescricaoEnum()
    {
        var result = EnumTeste.A.Description();

        result.Should().Be("Item - A");
    }
}

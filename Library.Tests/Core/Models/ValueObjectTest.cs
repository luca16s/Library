namespace Library.Tests.Core.Models;

using global::Core.Models;

using Library.Tests.Common;

using Xunit;

public class ValueObjectTest
{
    [Fact]
    [Trait("Método: ", "Equals")]
    public void EqualsDeveRetornarVerdadeiroParaMesmaReferencia()
    {
        // Arrange
        var valueObject = new Endereco();

        // Act
        var result = valueObject.Equals(valueObject);

        // Assert
        Assert.True(result);
    }

    [Fact]
    [Trait("Método: ", "Equals")]
    public void EqualsDeveRetornarFalsoParaObjetosNulos()
    {
        // Arrange
        var valueObject = new Endereco();

        // Act
        var result = valueObject.Equals(null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    [Trait("Método: ", "Equals")]
    public void EqualsDeveRetornarFalsoParaTipoDiferente()
    {
        // Arrange
        var valueObject = new Endereco();
        var differentTypeObject = new Pessoa(1);

        // Act
        var result = valueObject.Equals(differentTypeObject);

        // Assert
        Assert.False(result);
    }

    [Fact]
    [Trait("Método: ", "Equals")]
    public void EqualsDeveRetornarVerdadeiroObjetoDeValorIgual()
    {
        // Arrange
        var valueObject1 = new Endereco();
        var valueObject2 = new Endereco();

        // Act
        var result = valueObject1.Equals(valueObject2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    [Trait("Método: ", "Equals")]
    public void EqualsDeveRetornarFalsoParaDiferentesObjetosDeValores()
    {
        // Arrange
        var valueObject1 = new Endereco();
        var valueObject2 = new Endereco()
        {
            Nome = "AAAA"
        };

        // Act
        var result = valueObject1.Equals(valueObject2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    [Trait("Método: ", "GetHashCode")]
    public void GetHashCode_ReturnsSameHashCodeForEqualObjects()
    {
        // Arrange
        var obj1 = new Endereco { Nome = "Test" };
        var obj2 = new Endereco { Nome = "Test" };

        // Act
        var hashCode1 = obj1.GetHashCode();
        var hashCode2 = obj2.GetHashCode();

        // Assert
        Assert.Equal(hashCode1, hashCode2);
    }

    [Fact]
    [Trait("Método: ", "GetHashCode")]
    public void GetHashCode_ReturnsDifferentHashCodeForDifferentObjects()
    {
        // Arrange
        var obj1 = new Endereco { Nome = "Test" };
        var obj2 = new Endereco { Nome = "Test" };

        // Act
        var hashCode1 = obj1.GetHashCode();
        var hashCode2 = obj2.GetHashCode();

        // Assert
        Assert.NotEqual(hashCode1, hashCode2);
    }

    [Fact]
    [Trait("Método: ", "EqualOperator")]
    public void EqualOperatorDeveRetornarVerdadeiroQuandoObjetosForemIguais()
    {
        // Arrange
        var valueObject1 = new Endereco { Nome = "Test" };
        var valueObject2 = new Endereco { Nome = "Test" };

        // Act
        var result = valueObject1 == valueObject2;

        // Assert
        Assert.True(result);
    }

    [Fact]
    [Trait("Método: ", "EqualOperator")]
    public void EqualOperatorDeveRetornarFalsoQuandoObjetosNaoSaoIguais()
    {
        // Arrange
        var valueObject1 = new Endereco { Nome = "Test" };
        var valueObject2 = new Endereco { Nome = "Test1" };

        // Act
        var result = valueObject1 == valueObject2;

        // Assert
        Assert.False(result);
    }

    [Fact]
    [Trait("Método: ", "EqualOperator")]
    public void EqualOperatorDeveRetornarVerdadeiroQuandoAmbosObjetosForemNulos()
    {
        // Arrange
        ValueObject valueObject1 = null;
        ValueObject valueObject2 = null;

        // Act
        var result = valueObject1 == valueObject2;

        // Assert
        Assert.True(result);
    }

    [Fact]
    [Trait("Método: ", "EqualOperator")]
    public void EqualOperatorDeveRetornarFalsoQuandoUmObjetoENulo()
    {
        // Arrange
        var valueObject1 = new Endereco { Nome = "Test" };
        ValueObject valueObject2 = null;

        // Act
        var result = valueObject1 == valueObject2;

        // Assert
        Assert.False(result);
    }

    [Fact]
    [Trait("Método: ", "NotEqualOperator")]
    public void NotEqualOperatorDeveRetornarVerdadeiroQuandoObjetosNaoSaoIguais()
    {
        // Arrange
        var left = new Endereco { Nome = "Test" };
        var right = new Endereco { Nome = "Test2" };

        // Act
        var result = left != right;

        // Assert
        Assert.True(result);
    }

    [Fact]
    [Trait("Método: ", "NotEqualOperator")]
    public void NotEqualOperatorDeveRetornarFalsoQuandoObjetosSaoIguais()
    {
        // Arrange
        var left = new Endereco { Nome = "Test" };
        var right = new Endereco { Nome = "Test" };

        // Act
        var result = left != right;

        // Assert
        Assert.False(result);
    }
}

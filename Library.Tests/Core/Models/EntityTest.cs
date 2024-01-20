namespace Library.Tests.Core.Models;

using FluentValidation.Results;

using global::Core.Models;
using global::Core.Validations;

using Library.Tests.Common;

using System.Collections.Generic;

using Xunit;

public partial class EntityTest
{
    [Fact]
    [Trait("Método: ", "GetHashCode")]
    public void GetHashCodeComMesmoIdDeveProduzirMesmoHash()
    {
        // Arrange
        var objeto1 = new Pessoa(1);
        var objeto2 = new Pessoa(1);

        // Act
        var hashCode1 = objeto1.GetHashCode();
        var hashCode2 = objeto2.GetHashCode();

        // Assert
        Assert.Equal(hashCode1, hashCode2);
    }

    [Fact]
    [Trait("Método: ", "GetHashCode")]
    public void GetHashCodeComIdDiferenteDeveProduzirHashDiferente()
    {
        // Arrange
        var objeto1 = new Pessoa(1);
        var objeto2 = new Pessoa(2);

        // Act
        var hashCode1 = objeto1.GetHashCode();
        var hashCode2 = objeto2.GetHashCode();

        // Assert
        Assert.NotEqual(hashCode1, hashCode2);
    }

    [Fact]
    [Trait("Método: ", "GetHashCode")]
    public void GetHashCodeDeveRetornarMesmoValor()
    {
        // Arrange
        var objeto = new Pessoa(1);

        // Act
        var hashCode1 = objeto.GetHashCode();
        var hashCode2 = objeto.GetHashCode();

        // Assert
        Assert.Equal(hashCode1, hashCode2);
    }

    [Fact]
    [Trait("Método: ", "Equals")]
    public void EqualsDeveRetornarVerdadeiroParaMesmaInstancia()
    {
        // Act / Assert
        Assert.True(new Pessoa(1).Equals(new Pessoa(1)));
    }

    [Fact]
    [Trait("Método: ", "Equals")]
    public void EqualsDeveRetornarVerdadeiroParaEntidadesIguais()
    {
        // Act / Assert
        Assert.True(new Pessoa(1).Equals(new Pessoa(1)));
        Assert.True(new Pessoa(1).Equals(new Pessoa(1)));
    }

    [Fact]
    [Trait("Método: ", "Equals")]
    public void EqualsDeveRetornarFalsoParaDiferentesEntidades()
    {
        // Act / Assert
        Assert.False(new Pessoa(1).Equals(new Pessoa(2)));
        Assert.False(new Pessoa(2).Equals(new Pessoa(1)));
    }

    [Fact]
    [Trait("Método: ", "Equals")]
    public void EqualsDeveRetornarFalsoParaObjetosNulos()
    {
        // Act / Assert
        Assert.False(new Pessoa(1).Equals(null));
    }

    [Fact]
    [Trait("Método: ", "Equals")]
    public void EqualsDeveRetornarFalsoParaTiposDiferentes()
    {
        // Act / Assert
        Assert.False(new Pessoa(1).Equals("not an Entity"));
    }

    [Fact]
    [Trait("Método: ", "AddValidationError")]
    public void AddValidationErrorDeveIgnorarErrosQuandoValidationResultNulo()
    {
        // Arrange
        Pessoa validator = new(1);
        ValidationResult validationResult = null;

        // Act
        validator.AddValidationError(validationResult);

        // Assert
        Assert.Empty(validator.ValidationResult.Errors);
    }

    [Fact]
    [Trait("Método: ", "AddValidationError")]
    public void AddValidationErrorDeveAdicionarErrosQuandoValidationResultTemErros()
    {
        // Arrange
        Pessoa validator = new(1);
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new("Property1", "Error message 1"),
            new("Property2", "Error message 2"),
        });

        // Act
        validator.AddValidationError(validationResult);

        // Assert
        Assert.Equal(2, validator.ValidationResult.Errors.Count);

        Assert.Equal("Property1", validator.ValidationResult.Errors[0].PropertyName);
        Assert.Equal("Error message 1", validator.ValidationResult.Errors[0].ErrorMessage);

        Assert.Equal("Property2", validator.ValidationResult.Errors[1].PropertyName);
        Assert.Equal("Error message 2", validator.ValidationResult.Errors[1].ErrorMessage);
    }

    [Fact]
    [Trait("Método: ", "AddValidationError")]
    public void AddValidationErrorDeveIgnorarErrosNulosCasoValidacaoComErrosNulos()
    {
        // Arrange
        Pessoa validator = new(1);
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            null,
            new("Property1", "Error message 1"),
            null,
            new("Property2", "Error message 2"),
            null,
        });

        // Act
        validator.AddValidationError(validationResult);

        // Assert
        Assert.Equal(2, validator.ValidationResult.Errors.Count);

        Assert.Equal("Property1", validator.ValidationResult.Errors[0].PropertyName);
        Assert.Equal("Error message 1", validator.ValidationResult.Errors[0].ErrorMessage);

        Assert.Equal("Property2", validator.ValidationResult.Errors[1].PropertyName);
        Assert.Equal("Error message 2", validator.ValidationResult.Errors[1].ErrorMessage);
    }

    [Fact]
    [Trait("Método: ", "EqualOperator")]
    public void EqualOperatorDeveRetornarVerdadeiroParaObjetosIguais()
    {
        // Arrange
        Entity entity1 = new Pessoa(1);
        Entity entity2 = new Pessoa(1);

        // Act
        bool result = entity1 == entity2;

        // Assert
        Assert.True(result);
    }

    [Fact]
    [Trait("Método: ", "EqualOperator")]
    public void EqualOperatorDeveRetornarFalsoParaDiferentesObjetos()
    {
        // Arrange
        Entity entity1 = new Pessoa(1);
        Entity entity2 = new Pessoa(2);

        // Act
        bool result = entity1 == entity2;

        // Assert
        Assert.False(result);
    }

    [Fact]
    [Trait("Método: ", "EqualOperator")]
    public void EqualOperatorDeveRetornarTrueQuandoAmbosObjetosForemNulos()
    {
        // Arrange
        Entity entity1 = null;
        Entity entity2 = null;

        // Act
        bool result = entity1 == entity2;

        // Assert
        Assert.True(result);
    }

    [Fact]
    [Trait("Método: ", "EqualOperator")]
    public void EqualOperatorDeveRetornarFalsoQuandoObjetoAEsquerdaENuloEObjetoADireitaNaoENulo()
    {
        // Arrange
        Entity entity1 = null;
        Entity entity2 = new Pessoa(2);

        // Act
        bool result = entity1 == entity2;

        // Assert
        Assert.False(result);
    }

    [Fact]
    [Trait("Método: ", "EqualOperator")]
    public void EqualOperatorDeveRetornarFalsoQuandoObjetoAEsquerdaNaoENuloEObjetoADireitaENulo()
    {
        // Arrange
        Entity entity1 = new Pessoa(1);
        Entity entity2 = null;

        // Act
        bool result = entity1 == entity2;

        // Assert
        Assert.False(result);
    }

    [Fact]
    [Trait("Método: ", "NotEqualOperator")]
    public void NotEqualOperatorDeveRetornarVerdadeiroParaEntidadesDiferentes()
    {
        // Arrange
        Entity entity1 = new Pessoa(1);
        Entity entity2 = new Pessoa(2);

        // Act
        bool result = entity1 != entity2;

        // Assert
        Assert.True(result);
    }

    [Fact]
    [Trait("Método: ", "NotEqualOperator")]
    public void NotEqualOperatorDeveRetornarFalsoParaEntidadesIguais()
    {
        // Arrange
        Entity entity1 = new Pessoa(1);
        Entity entity2 = new Pessoa(1);

        // Act
        bool result = entity1 != entity2;

        // Assert
        Assert.False(result);
    }

    [Fact]
    [Trait("Método: ", "NotEqualOperator")]
    public void NotEqualOperatorDeveRetornarFalsoParaMesmaReferencia()
    {
        // Arrange
        Entity entity1 = new Pessoa(1);
        Entity entity2 = entity1;

        // Act
        bool result = entity1 != entity2;

        // Assert
        Assert.False(result);
    }
}
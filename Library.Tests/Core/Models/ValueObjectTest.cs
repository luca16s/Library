namespace Library.Tests.Core.Models;

using FluentAssertions;

using global::Core.Models;

using System.Collections.Generic;

using Xunit;

public class ValueObjectTest
{
    public class ObjetoValor : ValueObject
    {
        public string Nome { get; set; }

        public ObjetoValor() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Nome;
        }
    }

    [Fact]
    public void NotEqualsShouldBeTrueIfComparedWithNull()
    {
        ObjetoValor objeto = new();

        var result = objeto != null;

        _ = result.Should().Be(true);
    }

    [Fact]
    public void EqualsShouldBeFalseIfComparedWithNullOnRight()
    {
        ObjetoValor objeto = new();

        var result = objeto == null;

        _ = result.Should().Be(false);
    }

    [Fact]
    public void EqualsShouldBeFalseIfComparedWithNullOnLeft()
    {
        ObjetoValor objeto = new();
        ObjetoValor objetoNull = null;

        var result = objetoNull == objeto;

        _ = result.Should().Be(false);
    }

    [Fact]
    public void EqualsShouldBeFalseIfComparedWithObjetoOfOtherValue()
    {
        ObjetoValor objetoA = new();
        ObjetoValor objetoB = new();

        objetoA.Nome = "";
        objetoB.Nome = "A";

        var result = objetoA == objetoB;

        _ = result.Should().Be(false);
    }

    [Fact]
    public void EqualsShouldBeTrueIfComparedWithObjetoOfSameValue()
    {
        ObjetoValor objetoA = new();
        ObjetoValor objetoB = new();

        objetoA.Nome = "A";
        objetoB.Nome = "A";

        var result = objetoA == objetoB;

        _ = result.Should().Be(true);
    }

    [Fact]
    public void EqualsShouldBeFalseIfComparedObjectIsNull()
    {
        ObjetoValor objeto = new();

        var result = objeto.Equals(null);

        _ = result.Should().Be(false);
    }

    [Fact]
    public void EqualsShouldBeFalseIfComparedOtherType()
    {
        ObjetoValor objeto = new();

        var result = objeto.Equals(10);

        _ = result.Should().Be(false);
    }

    [Fact]
    public void EqualsShouldBeTrueIfIdIsSame()
    {
        ObjetoValor objetoA = new();
        ObjetoValor objetoB = new();

        objetoA.Nome = "A";
        objetoB.Nome = "A";

        var result = objetoA.Equals(objetoB);

        _ = result.Should().Be(true);
    }

    [Fact]
    public void EqualsShouldBeTrueIfHasSameReference()
    {
        ObjetoValor objeto = new()
        {
            Nome = "A"
        };

        var result = objeto.Equals(objeto);

        _ = result.Should().Be(true);
    }

    [Fact]
    public void GetHashCodeShouldReturnZeroWhenNull()
    {
        const int expected = 0;

        ObjetoValor objeto = new();

        var result = objeto.GetHashCode();

        _ = result.Should().Be(expected);
    }

    [Fact]
    public void GetHashCodeShouldReturnHash()
    {
        int expected = "A".GetHashCode();

        ObjetoValor objeto = new()
        {
            Nome = "A"
        };

        var result = objeto.GetHashCode();

        _ = result.Should().Be(expected);
    }
}

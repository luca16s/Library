namespace Library.Tests.Common;

using global::Core.Models;

using System.Collections.Generic;

public class Endereco : ValueObject
{
    public string Nome { get; set; }

    public Endereco() { }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Nome;
    }
}

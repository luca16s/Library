namespace Library.Tests.Common;
public class Pessoa : Entity
{
    public Pessoa() : base(default) { }

    public Pessoa(long id) : base(id) { }

    public int Idade { get; set; }

    public string Nome { get; set; } = string.Empty;
}
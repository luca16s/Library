namespace Library.Tests.Data;

using Bogus;

using FluentAssertions;

using Library.Tests.Common;
using Library.Tests.Common.Interfaces;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

public class RepositoryTests
{
    private readonly PessoaContext Context;
    private readonly IPessoaRepository Repository;

    public RepositoryTests()
    {
        var option = new DbContextOptionsBuilder<PessoaContext>()
        .UseInMemoryDatabase(databaseName: "DB")
        .Options;
        Context = new(option);

        _ = Context.Database.EnsureDeleted();
        for (int i = 0; i < 30; i++)
        {
            var faker = new Faker<Pessoa>()
                .RuleFor(u => u.Nome, (f, u) => f.Name.FirstName())
                .RuleFor(u => u.Idade, (f, u) => f.Random.Number(0, 100));

            _ = Context.Pessoas.Add(new Pessoa(i + 1)
            {
                Nome = faker.Generate().Nome,
                Idade = faker.Generate().Idade,
            });
            _ = Context.SaveChanges();
        }

        Repository = new PessoaRepository(Context);
    }

    [Fact]
    [Trait("Method", "Get")]
    public async Task GetDeveRetornarNuloQuandoIdNaoEncontrado()
    {
        var expectedId = Context.Pessoas.Count() + 1;
        var result = await Repository.Get(expectedId);

        _ = result.Should().BeNull();
    }

    [Fact]
    [Trait("Method", "Get")]
    public async Task GetDeveRetornarModeloQuandoIdCorretoFornecido()
    {
        var id = 50;
        var expectedPessoa = new Pessoa(id) { Nome = "Jhon" };
        _ = Context.Pessoas.Add(expectedPessoa);
        _ = Context.SaveChanges();

        var result = await Repository.Get(id);

        _ = result.Should().NotBeNull();
        _ = result.Id.Should().Be(expectedPessoa.Id);
        _ = result.Nome.Should().Be(expectedPessoa.Nome);
    }

    [Fact]
    [Trait("Method", "GetAll")]
    public void GetAllDeveRetornarTodosItens()
    {
        var result = Repository.GetAll(0, 2);

        _ = result.Should().NotBeNull();
        _ = result.Should().HaveCount(2);
    }

    [Fact]
    [Trait("Method", "GetAll")]
    public void GetAllDeveRetornarQuantidadeIgualPassada()
    {
        var result = Repository.GetAll(0, 1);

        _ = result.Should().NotBeNull();
        _ = result.Should().HaveCount(1);
    }

    [Fact]
    [Trait("Method", "GetAll")]
    public void GetAllDeveRetornarQuantidadePadraoQuandoSemQuantidadeInformada()
    {
        var result = Repository.GetAll();

        _ = result.Should().NotBeNull();
        _ = result.Should().HaveCount(25);
        _ = result.Should().NotHaveCount(Context.Pessoas.Count());
    }

    [Fact]
    [Trait("Method", "GetAll")]
    public void GetAllDeveIgnorarItensPassados()
    {
        var expectedResult = Repository
            .GetAll(10, 30)
            .ToList();
        var result = Repository
            .GetAll(10)
            .ToList();

        _ = result.Should().NotBeNull();
        _ = result.Should().HaveCount(30 - 10);
        _ = expectedResult.SequenceEqual(result);
    }

    [Fact]
    [Trait("Method", "Create")]
    public async Task CreateDeveSalvarItemPassado()
    {
        var id = Context.Pessoas.Count() + 1;
        var expectedPessoa = new Pessoa(id) { Nome = "Jhon" };
        await Repository.Create(expectedPessoa);
        _ = Context.SaveChanges();

        var result = await Repository.Get(id);

        _ = result.Should().NotBeNull();
        _ = result.Id.Should().Be(expectedPessoa.Id);
        _ = result.Nome.Should().Be(expectedPessoa.Nome);
    }

    [Fact]
    [Trait("Method", "Create")]
    public async Task CreateDeveSalvarTodosItemsPassados()
    {
        var amountToSkip = 30;
        var amountToTake = 30;
        var expectedAmount = 30;
        var listaPessoas = new List<Pessoa>();

        for (int i = 30; i < amountToTake + amountToSkip; i++)
        {
            var faker = new Faker<Pessoa>()
                .RuleFor(u => u.Nome, (f, u) => f.Name.FirstName());

            listaPessoas.Add(new Pessoa(i + 1) { Nome = faker.Generate().Nome });
        }

        await Repository.Create(listaPessoas);
        _ = Context.SaveChanges();

        var result = Repository.GetAll(amountToSkip, amountToTake).ToList();

        _ = result.Should().NotBeNull();
        _ = result.SequenceEqual(listaPessoas);
        _ = result.Count.Should().Be(expectedAmount);
    }

    [Fact]
    [Trait("Method", "Delete")]
    public async Task DeleteDeveRemoverItemPassado()
    {
        var id = 1;
        var expectedPessoa = await Repository.Get(id);
        await Repository.Delete(expectedPessoa);
        _ = Context.SaveChanges();

        var result = await Repository.Get(id);

        _ = result.Should().BeNull();
    }

    [Fact]
    [Trait("Method", "Delete")]
    public async Task DeleteDeveLancarExcecaoQuandoParametroEstaNulo()
    {
        var id = 100;
        var expectedPessoa = await Repository.Get(id);

        var acao = async () => await Repository.Delete(expectedPessoa);

        _ = await acao.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("item");
    }

    [Fact]
    [Trait("Method", "Update")]
    public async Task UpdateDeveAtualizarQuandoItemPassado()
    {
        var id = 1;
        var expectedPessoa = await Repository.Get(id);
        expectedPessoa.Nome = "Nome Atualizado";

        await Repository.Update(id, expectedPessoa);
        _ = Context.SaveChanges();

        var result = await Repository.Get(id);

        _ = result.Should().NotBeNull();
        _ = result.Id.Should().Be(expectedPessoa.Id);
        _ = result.Nome.Should().Be(expectedPessoa.Nome);
    }

    [Fact]
    [Trait("Method", "Update")]
    public async Task UpdateDeveLancarExcecaoQuandoParametroNulo()
    {
        var id = 100;
        var expectedPessoa = await Repository.Get(id);

        var acao = async () => await Repository.Update(id, expectedPessoa);

        _ = await acao.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("item");
    }

    [Fact]
    [Trait("Method", "Update")]
    public async Task UpdateDeveLancarExcecaoQuandoIdDeItemPassadoInexistente()
    {
        var id = 100;
        var expectedPessoa = await Repository.Get(1);

        var acao = async () => await Repository.Update(id, expectedPessoa);

        _ = await acao.Should().ThrowAsync<NullReferenceException>()
            .WithMessage("Item pesquisado não existente no banco de dados.");
    }

    [Fact]
    [Trait("Method", "Search")]
    public void SearchDeveRetornarItemBuscadoQuandoExistente()
    {
        var id = 50;
        var expectedPessoa = new Pessoa(id) { Nome = "Jhon" };
        _ = Context.Pessoas.Add(expectedPessoa);
        _ = Context.SaveChanges();

        var result = Repository.Search(x => x.Nome == expectedPessoa.Nome).ToList();

        _ = result.Should().NotBeNull();
        _ = result.Should().Contain(expectedPessoa);
    }

    [Fact]
    [Trait("Method", "Search")]
    public void SearchDeveRetornarVazioQuandoItemBuscadoNaoExistente()
    {
        long id = 5340;

        var result = Repository.Search(x => x.Id == id).ToList();

        _ = result.Should().BeEmpty();
    }

    [Fact]
    [Trait("Method", "Count")]
    public async Task CountDeveRetornarZeroCasoTabelaVazia()
    {
        _ = Context.Database.EnsureDeleted();
        var result = await Repository.Count();

        _ = result.Should().Be(0);
    }

    [Fact]
    [Trait("Method", "Count")]
    public async Task CountDeveRetornarTotalDeItemsCasoTabelaPreenchida()
    {
        var expectedCount = Context.Pessoas.Count();
        var result = await Repository.Count();

        _ = result.Should().Be(expectedCount);
    }

    [Fact]
    [Trait("Method", "Max")]
    public async Task MaxDeveRetornarMensagemErroCasoTabelaVazia()
    {
        _ = Context.Database.EnsureDeleted();

        var acao = async () => await Repository.Max(x => x.Idade);

        _ = await acao.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Tabela não contém items ou foi passada uma entidade ao invés de uma propriedade.");
    }

    [Fact]
    [Trait("Method", "Max")]
    public async Task MaxDeveRetornarMaiorItemEncontrado()
    {
        var expectedAge = Context.Pessoas.Max(x => x.Idade);
        var result = await Repository.Max(x => x.Idade);
        _ = result.Should().Be(expectedAge);
    }

    [Fact]
    [Trait("Method", "Max")]
    public async Task MaxDeveRetornarMensagemErroCasoItemPassadoNaoSejaPropriedade()
    {
        var acao = async () => await Repository.Max(x => x);
        _ = await acao.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Tabela não contém items ou foi passada uma entidade ao invés de uma propriedade.");
    }

    [Fact]
    [Trait("Method", "Min")]
    public async Task MinDeveRetornarMensagemErroCasoTabelaVazia()
    {
        _ = Context.Database.EnsureDeleted();

        var acao = async () => await Repository.Min(x => x.Idade);

        _ = await acao.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Tabela não contém items ou foi passada uma entidade ao invés de uma propriedade.");
    }

    [Fact]
    [Trait("Method", "Min")]
    public async Task MinDeveRetornarMenorItemEncontrado()
    {
        var expectedAge = Context.Pessoas.Min(x => x.Idade);
        var result = await Repository.Min(x => x.Idade);
        _ = result.Should().Be(expectedAge);
    }

    [Fact]
    [Trait("Method", "Min")]
    public async Task MinDeveRetornarMensagemErroCasoItemPassadoNaoSejaPropriedade()
    {
        var acao = async () => await Repository.Min(x => x);
        _ = await acao.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Tabela não contém items ou foi passada uma entidade ao invés de uma propriedade.");
    }
}

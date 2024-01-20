namespace Library.Tests.Data;

using Bogus;

using FluentAssertions;

using Library.Tests.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

public class RepositoryTest
{
    private readonly PessoaContext context;
    private readonly UnitOfWork unitOfWork;
    private readonly PessoaRepository repository;

    public RepositoryTest()
    {
        var option = new DbContextOptionsBuilder<PessoaContext>()
        .UseInMemoryDatabase(databaseName: "DB")
        .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;
        context = new(option);
        unitOfWork = new(context);

        _ = context.Database.EnsureDeleted();
        for (int i = 0; i < 30; i++)
        {
            var faker = new Faker<Pessoa>()
                .RuleFor(u => u.Nome, (f, u) => f.Name.FirstName())
                .RuleFor(u => u.Idade, (f, u) => f.Random.Number(0, 100));

            _ = context.Pessoas.Add(new Pessoa(i + 1)
            {
                Nome = faker.Generate().Nome,
                Idade = faker.Generate().Idade,
            });
            _ = context.SaveChanges();
        }

        repository = new PessoaRepository(unitOfWork, context);
    }

    [Fact]
    [Trait("Método: ", "Get")]
    public async Task GetDeveRetornarNuloQuandoIdNaoEncontrado()
    {
        var expectedId = context.Pessoas.Count() + 1;
        var result = await repository.GetAsync(expectedId);

        _ = result.Should().BeNull();
    }

    [Fact]
    [Trait("Método: ", "Get")]
    public async Task GetDeveRetornarModeloQuandoIdCorretoFornecido()
    {
        var id = 50;
        var expectedPessoa = new Pessoa(id) { Nome = "Jhon" };
        _ = context.Pessoas.Add(expectedPessoa);
        _ = context.SaveChanges();

        var result = await repository.GetAsync(id);

        _ = result.Should().NotBeNull();
        _ = result.Id.Should().Be(expectedPessoa.Id);
        _ = result.Nome.Should().Be(expectedPessoa.Nome);
    }

    [Fact]
    [Trait("Método: ", "Get")]
    public async Task GetDeveCancelarBuscaComCancellationTokenPassado()
    {
        var expectedId = context.Pessoas.Count() + 1;

        CancellationTokenSource source = new();
        source.Cancel();

        var acao = async () => await repository.GetAsync(expectedId, source.Token);

        _ = await acao.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    [Trait("Método: ", "GetAll")]
    public void GetAllDeveRetornarTodosItens()
    {
        var result = repository.GetAll(0, 2);

        _ = result.Should().NotBeNull();
        _ = result.Should().HaveCount(2);
    }

    [Fact]
    [Trait("Método: ", "GetAll")]
    public void GetAllDeveRetornarQuantidadeIgualPassada()
    {
        var result = repository.GetAll(0, 1);

        _ = result.Should().NotBeNull();
        _ = result.Should().HaveCount(1);
    }

    [Fact]
    [Trait("Método: ", "GetAll")]
    public void GetAllDeveRetornarQuantidadePadraoQuandoSemQuantidadeInformada()
    {
        var result = repository.GetAll();

        _ = result.Should().NotBeNull();
        _ = result.Should().HaveCount(25);
        _ = result.Should().NotHaveCount(context.Pessoas.Count());
    }

    [Fact]
    [Trait("Método: ", "GetAll")]
    public void GetAllDeveIgnorarItensPassados()
    {
        var expectedResult = repository
            .GetAll(10, 30)
            .ToList();
        var result = repository
            .GetAll(10)
            .ToList();

        _ = result.Should().NotBeNull();
        _ = result.Should().HaveCount(30 - 10);
        _ = expectedResult.SequenceEqual(result);
    }

    [Fact]
    [Trait("Método: ", "Create")]
    public async Task CreateDeveSalvarItemPassado()
    {
        var id = context.Pessoas.Count() + 1;
        var expectedPessoa = new Pessoa(id) { Nome = "Jhon" };
        await repository.CreateAsync(expectedPessoa);
        _ = context.SaveChanges();

        var result = await repository.GetAsync(id);

        _ = result.Should().NotBeNull();
        _ = result.Id.Should().Be(expectedPessoa.Id);
        _ = result.Nome.Should().Be(expectedPessoa.Nome);
    }

    [Fact]
    [Trait("Método: ", "Create")]
    public async Task CreateDeveCancelarSaveComCancellationTokenPassado()
    {
        var id = context.Pessoas.Count() + 1;
        var expectedPessoa = new Pessoa(id) { Nome = "Jhon" };

        CancellationTokenSource source = new();
        source.Cancel();

        var acao = async () => await repository.CreateAsync(expectedPessoa, source.Token);

        _ = await acao.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    [Trait("Método: ", "Create")]
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

        await repository.CreateAsync(listaPessoas);
        _ = context.SaveChanges();

        var result = repository.GetAll(amountToSkip, amountToTake).ToList();

        _ = result.Should().NotBeNull();
        _ = result.SequenceEqual(listaPessoas);
        _ = result.Count.Should().Be(expectedAmount);
    }

    [Fact]
    [Trait("Método: ", "Create")]
    public async Task CreateDeveCancelarSaveVariosComCancellationTokenPassado()
    {
        var amountToSkip = 30;
        var amountToTake = 30;
        var listaPessoas = new List<Pessoa>();

        for (int i = 30; i < amountToTake + amountToSkip; i++)
        {
            var faker = new Faker<Pessoa>()
                .RuleFor(u => u.Nome, (f, u) => f.Name.FirstName());

            listaPessoas.Add(new Pessoa(i + 1) { Nome = faker.Generate().Nome });
        }

        CancellationTokenSource source = new();
        source.Cancel();

        var acao = async () => await repository.CreateAsync(listaPessoas, source.Token);

        _ = await acao.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    [Trait("Método: ", "Delete")]
    public async Task DeleteDeveRemoverItemPassado()
    {
        var id = 1;
        var expectedPessoa = await repository.GetAsync(id);
        await repository.DeleteAsync(expectedPessoa);
        _ = context.SaveChanges();

        var result = await repository.GetAsync(id);

        _ = result.Should().BeNull();
    }

    [Fact]
    [Trait("Método: ", "Delete")]
    public async Task DeleteDeveLancarExcecaoQuandoParametroEstaNulo()
    {
        var id = 100;
        var expectedPessoa = await repository.GetAsync(id);

        var acao = async () => await repository.DeleteAsync(expectedPessoa);

        _ = await acao.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("item");
    }

    [Fact]
    [Trait("Método: ", "Update")]
    public async Task UpdateDeveAtualizarQuandoItemPassado()
    {
        var id = 1;
        var expectedPessoa = await repository.GetAsync(id);
        expectedPessoa.Nome = "Nome Atualizado";

        await repository.UpdateAsync(id, expectedPessoa);
        _ = context.SaveChanges();

        var result = await repository.GetAsync(id);

        _ = result.Should().NotBeNull();
        _ = result.Id.Should().Be(expectedPessoa.Id);
        _ = result.Nome.Should().Be(expectedPessoa.Nome);
    }

    [Fact]
    [Trait("Método: ", "Update")]
    public async Task UpdateDeveLancarExcecaoQuandoParametroNulo()
    {
        var id = 100;
        var expectedPessoa = await repository.GetAsync(id);

        var acao = async () => await repository.UpdateAsync(id, expectedPessoa);

        _ = await acao.Should().ThrowAsync<ArgumentNullException>()
            .WithParameterName("item");
    }

    [Fact]
    [Trait("Método: ", "Update")]
    public async Task UpdateDeveLancarExcecaoQuandoIdDeItemPassadoInexistente()
    {
        var id = 100;
        var expectedPessoa = await repository.GetAsync(1);

        var acao = async () => await repository.UpdateAsync(id, expectedPessoa);

        _ = await acao.Should().ThrowAsync<NullReferenceException>()
            .WithMessage("Item pesquisado não existente no banco de dados.");
    }

    [Fact]
    [Trait("Método: ", "Search")]
    public void SearchDeveRetornarItemBuscadoQuandoExistente()
    {
        var id = 50;
        var expectedPessoa = new Pessoa(id) { Nome = "Jhon" };
        _ = context.Pessoas.Add(expectedPessoa);
        _ = context.SaveChanges();

        var result = repository.Search(x => x.Nome == expectedPessoa.Nome).ToList();

        _ = result.Should().NotBeNull();
        _ = result.Should().Contain(expectedPessoa);
    }

    [Fact]
    [Trait("Método: ", "Search")]
    public void SearchDeveRetornarVazioQuandoItemBuscadoNaoExistente()
    {
        long id = 5340;

        var result = repository.Search(x => x.Id == id).ToList();

        _ = result.Should().BeEmpty();
    }

    [Fact]
    [Trait("Método: ", "Count")]
    public async Task CountDeveRetornarZeroCasoTabelaVazia()
    {
        _ = context.Database.EnsureDeleted();
        var result = await repository.CountAsync();

        _ = result.Should().Be(0);
    }

    [Fact]
    [Trait("Método: ", "Count")]
    public async Task CountDeveCancelarContagemComCancellationTokenPassado()
    {
        CancellationTokenSource source = new();
        source.Cancel();

        var acao = async () => await repository.CountAsync(source.Token);

        _ = await acao.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    [Trait("Método: ", "Count")]
    public async Task CountDeveRetornarTotalDeItemsCasoTabelaPreenchida()
    {
        var expectedCount = context.Pessoas.Count();
        var result = await repository.CountAsync();

        _ = result.Should().Be(expectedCount);
    }

    [Fact]
    [Trait("Método: ", "Min")]
    public async Task MinDeveRetornarMensagemErroCasoTabelaVazia()
    {
        _ = context.Database.EnsureDeleted();

        var acao = async () => await repository.MinAsync(x => x.Idade);

        _ = await acao.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Tabela não contém items ou foi passada uma entidade ao invés de uma propriedade.");
    }

    [Fact]
    [Trait("Método: ", "Min")]
    public async Task MinDeveRetornarMenorItemEncontrado()
    {
        var expectedAge = context.Pessoas.Min(x => x.Idade);
        var result = await repository.MinAsync(x => x.Idade);
        _ = result.Should().Be(expectedAge);
    }

    [Fact]
    [Trait("Método: ", "Min")]
    public async Task MinDeveRetornarMensagemErroCasoItemPassadoNaoSejaPropriedade()
    {
        var acao = async () => await repository.MinAsync(x => x);
        _ = await acao.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Tabela não contém items ou foi passada uma entidade ao invés de uma propriedade.");
    }

    [Fact]
    [Trait("Método: ", "Min")]
    public async Task MinDeveCancelarBuscaMenorComCancellationTokenPassado()
    {
        CancellationTokenSource source = new();
        source.Cancel();

        var acao = async () => await repository.MinAsync(x => x.Idade, source.Token);

        _ = await acao.Should().ThrowAsync<OperationCanceledException>();
    }

    [Fact]
    [Trait("Método: ", "Max")]
    public async Task MaxDeveRetornarMensagemErroCasoTabelaVazia()
    {
        _ = context.Database.EnsureDeleted();

        var acao = async () => await repository.MaxAsync(x => x.Idade);

        _ = await acao.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Tabela não contém items ou foi passada uma entidade ao invés de uma propriedade.");
    }

    [Fact]
    [Trait("Método: ", "Max")]
    public async Task MaxDeveRetornarMaiorItemEncontrado()
    {
        var expectedAge = context.Pessoas.Max(x => x.Idade);
        var result = await repository.MaxAsync(x => x.Idade);
        _ = result.Should().Be(expectedAge);
    }

    [Fact]
    [Trait("Método: ", "Max")]
    public async Task MaxDeveRetornarMensagemErroCasoItemPassadoNaoSejaPropriedade()
    {
        var acao = async () => await repository.MaxAsync(x => x);
        _ = await acao.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Tabela não contém items ou foi passada uma entidade ao invés de uma propriedade.");
    }

    [Fact]
    [Trait("Método: ", "Max")]
    public async Task MaxDeveCancelarBuscaMaiorComCancellationTokenPassado()
    {
        CancellationTokenSource source = new();
        source.Cancel();

        var acao = async () => await repository.MaxAsync(x => x.Idade, source.Token);

        _ = await acao.Should().ThrowAsync<OperationCanceledException>();
    }
}

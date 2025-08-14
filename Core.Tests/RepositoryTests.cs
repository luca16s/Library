namespace Core.Tests;
using Core.Data.Repositories;
using Core.Interfaces;
using Core.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using Moq;

using System.Diagnostics.CodeAnalysis;

using Test.Types;

[ExcludeFromCodeCoverage]
[Trait("Core", "Repository")]
public class RepositoryTests
{
    private readonly Mock<DbContext> _mockContext;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<DbSet<TestEntity>> _mockDbSet;
    private readonly Repository<int, TestEntity, DbContext> _repository;

    public RepositoryTests()
    {
        _mockContext = new Mock<DbContext>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockDbSet = new Mock<DbSet<TestEntity>>();
        _ = _mockContext.Setup(m => m.Set<TestEntity>()).Returns(_mockDbSet.Object);
        _repository = new TestRepository(_mockContext.Object, _mockUnitOfWork.Object);
    }

    [Test]
    public async Task CreateAsyncDeveAdicionarEntidade()
    {
        var entity = new TestEntity { Id = 1 };

        await _repository.CreateAsync(entity);

        _mockDbSet.Verify(m => m.AddAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
        _mockUnitOfWork.Verify(m => m.CommitAsync(It.IsAny<IDbContextTransaction>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task DeleteAsyncDeveRemoverEntidade()
    {
        var entity = new TestEntity { Id = 1 };

        await _repository.DeleteAsync(entity);

        _mockDbSet.Verify(m => m.Remove(entity), Times.Once);
        _mockUnitOfWork.Verify(m => m.CommitAsync(It.IsAny<IDbContextTransaction>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateAsyncDeveUpdateEntidade()
    {
        var entity = new TestEntity { Id = 1 };
        _ = _mockDbSet.Setup(m => m.Find(It.IsAny<int>())).Returns(entity);

        await _repository.UpdateAsync(1, entity);

        _mockDbSet.Verify(m => m.Update(entity), Times.Once);
        _mockUnitOfWork.Verify(m => m.CommitAsync(It.IsAny<IDbContextTransaction>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetAsyncDeveRetornarEntidade()
    {
        var entity = new TestEntity { Id = 1 };
        _ = _mockDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);

        var result = await _repository.GetAsync(1);

        Assert.Equal(entity, result);
    }

    [Test]
    public async Task CreateAsyncComCancelamentoDeveLancarExcecao()
    {
        var entity = new TestEntity { Id = 1 };
        var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        _ = await Assert.ThrowsAsync<OperationCanceledException>(async () =>
            await _repository.CreateAsync(entity, cancellationTokenSource.Token));
    }

    [Test]
    public async Task CreateAsyncComErroDeBancoDeDadosDeveLancarExcecao()
    {
        var entity = new TestEntity { Id = 1 };
        _ = _mockDbSet.Setup(m => m.AddAsync(entity, It.IsAny<CancellationToken>())).ThrowsAsync(new DbUpdateException());

        _ = await Assert.ThrowsAsync<DbUpdateException>(async () =>
            await _repository.CreateAsync(entity));
    }

    [Test]
    public async Task CreateAsyncComVariosItensDeveAdicionarEntidades()
    {
        var entities = new List<TestEntity>
        {
            new() { Id = 1 },
            new() { Id = 2 }
        };

        await _repository.CreateAsync(entities);

        _mockDbSet.Verify(m => m.AddRangeAsync(entities, It.IsAny<CancellationToken>()), Times.Once);
        _mockUnitOfWork.Verify(m => m.CommitAsync(It.IsAny<IDbContextTransaction>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateAsyncComVariosItensEErroDeBancoDeDadosDeveLancarExcecao()
    {
        var entities = new List<TestEntity>
        {
            new() { Id = 1 },
            new() { Id = 2 }
        };
        _ = _mockDbSet.Setup(m => m.AddRangeAsync(entities, It.IsAny<CancellationToken>())).ThrowsAsync(new DbUpdateException());

        _ = await Assert.ThrowsAsync<DbUpdateException>(async () =>
            await _repository.CreateAsync(entities));
    }

    [Test]
    public async Task DeleteAsyncComEntidadeNulaDeveLancarExcecao()
    {
        TestEntity? entity = null;

        _ = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await _repository.DeleteAsync(entity!));
    }

    [Test]
    public async Task UpdateAsyncComEntidadeNulaDeveLancarExcecao()
    {
        TestEntity? entity = null;

        _ = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await _repository.UpdateAsync(1, entity!));
    }

    [Test]
    public async Task UpdateAsyncComEntidadeNaoExistenteDeveLancarExcecao()
    {
        var entity = new TestEntity { Id = 1 };
        _ = _mockDbSet.Setup(m => m.Find(It.IsAny<int>())).Returns((TestEntity?)null);

        _ = await Assert.ThrowsAsync<NullReferenceException>(async () =>
            await _repository.UpdateAsync(1, entity));
    }

    public class TestEntity : Entity<int>
    { }

    public class TestRepository(
        DbContext context,
        IUnitOfWork unitOfWork
    ) : Repository<int, TestEntity, DbContext>(context, unitOfWork)
    { }
}

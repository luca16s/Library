namespace Core.Tests;
using Core.Interfaces;
using Core.Services;

using Microsoft.EntityFrameworkCore.Storage;

using Moq;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Test.Types;

using Xunit;

[ExcludeFromCodeCoverage]
[Trait("Core", "Service")]
public class ServiceTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IRepository<int, TestEntity>> _repositoryMock;
    private readonly TestService _service;

    public ServiceTest()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _repositoryMock = new Mock<IRepository<int, TestEntity>>();
        _service = new TestService(_unitOfWorkMock.Object, _repositoryMock.Object);
    }

    [Test]
    public async Task CreateAsyncDeveChamarRepositoryAndUnitOfWork()
    {
        var entity = new TestEntity { Id = 1 };
        var transactionMock = new Mock<IDbContextTransaction>();

        _unitOfWorkMock.Setup(u => u.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(transactionMock.Object);
        _repositoryMock.Setup(r => r.CreateAsync(entity, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        await _service.CreateAsync(entity);

        _repositoryMock.Verify(r => r.CreateAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(transactionMock.Object, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateAsyncDeveDarRollbackOnException()
    {
        var entity = new TestEntity { Id = 1 };
        var transactionMock = new Mock<IDbContextTransaction>();

        _unitOfWorkMock.Setup(u => u.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(transactionMock.Object);
        _repositoryMock.Setup(r => r.CreateAsync(entity, It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreateAsync(entity));

        _unitOfWorkMock.Verify(u => u.RollbackTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task DeleteDeveChamarRepositoryAndUnitOfWork()
    {
        var entity = new TestEntity { Id = 1 };
        var transactionMock = new Mock<IDbContextTransaction>();

        _unitOfWorkMock.Setup(u => u.BeginTransactionAsync()).ReturnsAsync(transactionMock.Object);
        _repositoryMock.Setup(r => r.DeleteAsync(entity)).Returns(Task.CompletedTask);

        await _service.Delete(entity);

        _repositoryMock.Verify(r => r.DeleteAsync(entity), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(transactionMock.Object), Times.Once);
    }

    [Test]
    public async Task UpdateDeveChamarRepositoryAndUnitOfWork()
    {
        var entity = new TestEntity { Id = 1 };
        var transactionMock = new Mock<IDbContextTransaction>();

        _unitOfWorkMock.Setup(u => u.BeginTransactionAsync()).ReturnsAsync(transactionMock.Object);
        _repositoryMock.Setup(r => r.UpdateAsync(entity.Id, entity)).Returns(Task.CompletedTask);

        await _service.Update(entity.Id, entity);

        _repositoryMock.Verify(r => r.UpdateAsync(entity.Id, entity), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(transactionMock.Object), Times.Once);
    }

    [Test]
    public async Task UpdateDeveDarRollbackOnException()
    {
        var entity = new TestEntity { Id = 1 };
        var transactionMock = new Mock<IDbContextTransaction>();

        _unitOfWorkMock.Setup(u => u.BeginTransactionAsync()).ReturnsAsync(transactionMock.Object);
        _repositoryMock.Setup(r => r.UpdateAsync(entity.Id, entity)).ThrowsAsync(new Exception());

        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.Update(entity.Id, entity));

        _unitOfWorkMock.Verify(u => u.RollbackTransactionAsync(), Times.Once);
    }

    [Test]
    public async Task GetAsyncDeveRetornarEntidade()
    {
        var entity = new TestEntity { Id = 1 };

        _repositoryMock.Setup(r => r.GetAsync(entity.Id, It.IsAny<CancellationToken>())).ReturnsAsync(entity);

        var result = await _service.GetAsync(entity.Id);

        Assert.Equal(entity, result);
    }

    [Test]
    public void GetAllDeveRetornarEntidades()
    {
        var entities = new List<TestEntity> { new() { Id = 1 }, new() { Id = 2 } }.AsQueryable();

        _repositoryMock.Setup(r => r.GetAll(It.IsAny<int>(), It.IsAny<int>())).Returns(entities);

        var result = _service.GetAll();

        Assert.Equal(entities, result);
    }

    [Test]
    public void SearchDeveRetornarEntidades()
    {
        var entities = new List<TestEntity> { new() { Id = 1 }, new() { Id = 2 } }.AsQueryable();
        Expression<Func<TestEntity, bool>> predicate = e => e.Id > 0;

        _repositoryMock.Setup(r => r.Search(predicate)).Returns(entities);

        var result = _service.Search(predicate);

        Assert.Equal(entities, result);
    }

    [Test]
    public async Task CreateAsyncEnumerableDeveChamarRepositoryAndUnitOfWork()
    {
        var entities = new List<TestEntity> { new() { Id = 1 }, new() { Id = 2 } };
        var transactionMock = new Mock<IDbContextTransaction>();

        _unitOfWorkMock.Setup(u => u.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(transactionMock.Object);
        _repositoryMock.Setup(r => r.CreateAsync(entities, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        await _service.CreateAsync(entities);

        _repositoryMock.Verify(r => r.CreateAsync(entities, It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(transactionMock.Object, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateAsyncEnumerableDeveDarRollbackOnException()
    {
        var entities = new List<TestEntity> { new() { Id = 1 }, new() { Id = 2 } };
        var transactionMock = new Mock<IDbContextTransaction>();

        _unitOfWorkMock.Setup(u => u.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(transactionMock.Object);
        _repositoryMock.Setup(r => r.CreateAsync(entities, It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CreateAsync(entities));

        _unitOfWorkMock.Verify(u => u.RollbackTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    public class TestEntity : IEntity<int>
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public long Version { get; set; }
    }

    public class TestService(
        IUnitOfWork unitOfWork,
        IRepository<int, TestEntity> repository
    ) : Service<int, TestEntity, IRepository<int, TestEntity>>(unitOfWork, repository)
    { }
}

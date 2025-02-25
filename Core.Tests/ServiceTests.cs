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
    private readonly Mock<IRepository<int, TestEntity>> _repositoryMock;
    private readonly TestService _service;

    public ServiceTest()
    {
        _repositoryMock = new Mock<IRepository<int, TestEntity>>();
        _service = new TestService(_repositoryMock.Object);
    }

    [Test]
    public async Task CreateAsyncDeveChamarRepository()
    {
        var entity = new TestEntity { Id = 1 };

        _repositoryMock.Setup(r => r.CreateAsync(entity, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        await _service.CreateAsync(entity);

        _repositoryMock.Verify(r => r.CreateAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task DeleteDeveChamarRepository()
    {
        var entity = new TestEntity { Id = 1 };
        var transactionMock = new Mock<IDbContextTransaction>();

        _repositoryMock.Setup(r => r.DeleteAsync(entity)).Returns(Task.CompletedTask);

        await _service.DeleteAsync(entity);

        _repositoryMock.Verify(r => r.DeleteAsync(entity), Times.Once);
    }

    [Test]
    public async Task UpdateDeveChamarRepository()
    {
        var entity = new TestEntity { Id = 1 };
        var transactionMock = new Mock<IDbContextTransaction>();

        _repositoryMock.Setup(r => r.UpdateAsync(entity.Id, entity)).Returns(Task.CompletedTask);

        await _service.UpdateAsync(entity.Id, entity);

        _repositoryMock.Verify(r => r.UpdateAsync(entity.Id, entity), Times.Once);
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
    public async Task GetAllDeveRetornarEntidadesAsync()
    {
        var entities = new List<TestEntity> { new() { Id = 1 }, new() { Id = 2 } }.AsQueryable();

        _repositoryMock.Setup(r => r.GetAll(It.IsAny<int>(), It.IsAny<int>())).Returns(entities);

        var result = await _service.GetAllAsync();

        Assert.Equal(entities, result);
    }

    [Test]
    public async Task SearchDeveRetornarEntidadesAsync()
    {
        var entities = new List<TestEntity> { new() { Id = 1 }, new() { Id = 2 } }.AsQueryable();
        Expression<Func<TestEntity, bool>> predicate = e => e.Id > 0;

        _repositoryMock.Setup(r => r.Search(predicate)).Returns(entities);

        var result = await _service.SearchAsync(predicate);

        Assert.Equal(entities, result);
    }

    [Test]
    public async Task CreateAsyncEnumerableDeveChamarRepository()
    {
        var entities = new List<TestEntity> { new() { Id = 1 }, new() { Id = 2 } };
        var transactionMock = new Mock<IDbContextTransaction>();

        _repositoryMock.Setup(r => r.CreateAsync(entities, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        await _service.CreateAsync(entities);

        _repositoryMock.Verify(r => r.CreateAsync(entities, It.IsAny<CancellationToken>()), Times.Once);
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
        IRepository<int, TestEntity> repository
    ) : Service<int, TestEntity, IRepository<int, TestEntity>>(repository)
    { }
}

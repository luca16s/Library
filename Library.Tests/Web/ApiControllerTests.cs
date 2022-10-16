namespace Library.Tests.Web
{
    using AutoMapper;

    using CQRS.Interfaces;
    using CQRS.Notifications;

    using FluentAssertions;

    using global::Core.Interfaces.Services;
    using global::Core.Models;
    using global::Web.Controller;

    using MediatR;

    using Moq;

    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Xunit;

    public class ApiControllerTests
    {
        public class Modelo : Entity<long>
        {
            public Modelo(long id) : base(id) { }

            public override bool IsConsistent() { throw new NotImplementedException(); }
        }

        public class Servico : IService<Modelo, long>
        {
            public Task Create(Modelo item)
            {
                throw new NotImplementedException();
            }

            public Task Delete(Modelo item)
            {
                throw new NotImplementedException();
            }

            public Task<Modelo> Get(long id)
            {
                throw new NotImplementedException();
            }

            public IQueryable<Modelo> GetAll(int amount)
            {
                throw new NotImplementedException();
            }

            public IQueryable<Modelo> Search(Expression<Func<Modelo, bool>> predicate)
            {
                throw new NotImplementedException();
            }

            public Task Update(long id, Modelo item)
            {
                throw new NotImplementedException();
            }
        }

        public class Controller : ApiController<Servico, Modelo, long, Modelo>
        {
            public Controller(
                IMapper mapper,
                IMediatorHandler mediator,
                IService<Modelo, long> service,
                INotificationHandler<DomainNotification<long, Modelo>> notifications)
                : base(mapper, mediator, service, notifications) { }
        }

        [Fact]
        public void DeveInstanciarServicoBase()
        {
            var servico = new Servico();
            var mapper = new Mock<IMapper>().Object;
            var mediator = new Mock<IMediatorHandler>().Object;
            var notification = new DomainNotification<long, Modelo>("", "") as INotificationHandler<DomainNotification<long, Modelo>>;

            var controller = new Controller(
                mapper,
                mediator,
                servico,
                notification
            );

            _ = controller.Service.Should().NotBeNull();
        }
    }
}
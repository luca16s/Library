namespace Library.Tests.Common
{
    using AutoMapper;

    using global::Web.Controller;

    using Library.Tests.Common.Interfaces;

    using Mediator.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    public class PessoaController : ApiController<IPessoaService, Pessoa, long, Pessoa>
    {
        public PessoaController(
            IMapper mapper,
            IMediatorHandler mediator,
            IPessoaService service,
            IDomainNotificationHandler<long> handlerWithOutNotification,
            IDomainNotificationHandler<long, Pessoa> handlerWithNotification
        ) : base(
            mapper,
            service,
            mediator,
            handlerWithOutNotification,
            handlerWithNotification
        )
        { }

        public IActionResult GetValue()
        {
            return Ok(Service.GetStringValue(string.Empty));
        }

        public bool IsOperacaoValida()
        {
            return IsOperationValid();
        }
    }
}
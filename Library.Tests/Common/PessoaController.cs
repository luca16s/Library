namespace Library.Tests.Common;

using Api.Controller;

using Library.Tests.Common.Interfaces;

using Mediator.Interfaces;

using Microsoft.AspNetCore.Mvc;

public class PessoaController(
    IPessoaService service,
    IMediatorHandler mediator,
    IDomainNotificationHandler<Pessoa> notificationHandler
    ) : ApiController<Pessoa, IPessoaService>(
    service,
    mediator,
    notificationHandler
    )
{
    public bool IsOperacaoValida()
    {
        return IsOperationValid();
    }

    public IActionResult GetValue()
    {
        return Ok(service.GetStringValue(string.Empty));
    }
}
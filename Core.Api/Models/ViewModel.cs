namespace Core.Api.Models;
using Swashbuckle.AspNetCore.Annotations;

public class ViewModel<TId>
    where TId : notnull
{
    [SwaggerSchema("Identificador", ReadOnly = true)]
    public required TId Id { get; set; }
}

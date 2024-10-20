namespace Core.Api.Conventions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

using System.Linq;

/// <summary>
/// Convenção para se utilizar o namespace para versionar a API.
/// </summary>
public class ApiExplorerGroupPerVersionConvention : IControllerModelConvention
{
    /// <summary>
    /// Aplica a convenção.
    /// </summary>
    /// <param name="controller">
    /// Controller 
    /// </param>
    public void Apply(
        ControllerModel controller
    )
    {
        if (controller is null) return;

        var controllerNamespace = controller.ControllerType?.Namespace;

        if (string.IsNullOrWhiteSpace(controllerNamespace)) return;

        var apiVersion = controllerNamespace.Split('.').Last().ToLower();

        controller.ApiExplorer.GroupName = apiVersion;
    }
}

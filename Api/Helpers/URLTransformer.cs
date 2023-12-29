namespace Api.Helpers;

using Microsoft.AspNetCore.Routing;

using System.Text.RegularExpressions;

public partial class URLTransformer : IOutboundParameterTransformer
{
    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex ControllerRegex();

    public string? TransformOutbound(
        object? value
    ) => ControllerRegex()
            .Replace(
                value?.ToString() ?? string.Empty,
                "$1-$2"
            ).ToLower();
}

using System.Text.RegularExpressions;

namespace API.Configs;


public partial class ToKebabParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value) => value != null
        ? MyRegex().Replace(value.ToString() ?? "", "$1-$2").ToLower() // to kebab 
        : null;

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex MyRegex();
}
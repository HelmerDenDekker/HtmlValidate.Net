using System.ComponentModel.DataAnnotations;
using HtmlValidate.Net.Core.HtmlWrapper;

namespace HtmlValidate.Net.Core;

public class ValidationRequest
{
    public IEnumerable<Node> Nodes { get; }

    public bool IsValid => Results.Count == 0;

    public List<ValidationResult> Results { get; } = new();
}
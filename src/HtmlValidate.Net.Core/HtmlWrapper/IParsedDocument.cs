using System.ComponentModel.DataAnnotations;

namespace HtmlValidate.Net.Core.HtmlWrapper;

public interface IParsedDocument
{
    IEnumerable<Node> Nodes { get; }
    IEnumerable<ValidationResult> ValidationResults { get; }
}
using System.ComponentModel.DataAnnotations;

namespace HtmlValidate.Net.Core.HtmlWrapper;

public class ParsedDocument : IParsedDocument
{
    public ParsedDocument(IEnumerable<Node> nodes, IEnumerable<ValidationResult> validationResults)
    {
        Nodes = nodes;
        ValidationResults = validationResults;
    }
    
    public IEnumerable<Node> Nodes { get; }
    public IEnumerable<ValidationResult> ValidationResults { get; }
}
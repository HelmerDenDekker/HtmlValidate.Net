using System.ComponentModel.DataAnnotations;
using HtmlAgilityPack;

namespace HtmlValidate.Net.Rules;

public class ValidationRequest
{
    public ValidationRequest(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        Document = doc;
        HtmlNodes = doc.DocumentNode.Descendants();
    }

    public HtmlDocument Document { get; }

    public IEnumerable<HtmlNode> HtmlNodes { get; }

    public bool IsValid => Results.Count == 0;

    public List<ValidationResult> Results { get; set; } = new();
}
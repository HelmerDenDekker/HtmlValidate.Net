using System.ComponentModel.DataAnnotations;
using HtmlAgilityPack;
using HtmlValidate.Net.Core.HtmlWrapper;

namespace HtmlValidate.Net.Core.AgilityPackWrapper;

public class HtmlAgilityPackParser : IHtmlParser
{
    public IParsedDocument Parse(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var nodes = doc.DocumentNode
            .Descendants()
            .Select(n => new Node
            {
                Name = n.Name,
                InnerHtml = n.InnerHtml,
                InnerText = n.InnerText,
                Attributes = n.Attributes.ToDictionary(a => a.Name, a => a.Value)
            })
            .ToList();

        var errors = doc.ParseErrors.Any()
            ? doc.ParseErrors.ToValidationResults()
            : Enumerable.Empty<ValidationResult>();

        return new ParsedDocument(nodes, errors);
    }
}
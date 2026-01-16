using System.ComponentModel.DataAnnotations;
using HtmlAgilityPack;

namespace HtmlValidate.Net.Rules.Security;

public class DisallowScriptElementsRule : BaseValidator<IEnumerable<HtmlNode>>
{
    public override bool IsValid(IEnumerable<HtmlNode> model)
    {
        model
            .Where(n=>n.Name.Equals("script", StringComparison.InvariantCultureIgnoreCase))
            .ToList()
            .ForEach(n=>
        {
            Results.Add(new ValidationResult($"Disallowed HTML Element: <{n.Name}> found."));
        });
        return Results.Count == 0;
    }
}
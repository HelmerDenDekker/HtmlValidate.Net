using System.ComponentModel.DataAnnotations;
using HtmlAgilityPack;

namespace HtmlValidate.Net.Rules.Content;

public class RequireElementAncestorsRule : BaseValidator<IEnumerable<HtmlNode>>
{
    public override bool IsValid(IEnumerable<HtmlNode> model)
    {
        // start with documentNode here

        // a li child element, should have an <ul>, <ol> or <menu> direct parent

        model
            .Where(n => n.Name == "li")
            .ToList()
            .ForEach(li =>
            {
                var parent = li.ParentNode;
                var hasValidAncestor = parent.Name.Equals("ul", StringComparison.OrdinalIgnoreCase) ||
                                       parent.Name.Equals("ol", StringComparison.OrdinalIgnoreCase) ||
                                       parent.Name.Equals("menu", StringComparison.OrdinalIgnoreCase);
                if (!hasValidAncestor)
                    Results.Add(new ValidationResult("<li> element must have a <ul>, <ol> or <menu> ancestor."));
            });
        
        return Results.Count == 0;
    }
}
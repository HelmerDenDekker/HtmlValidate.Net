using System.ComponentModel.DataAnnotations;
using HtmlAgilityPack;

namespace HtmlValidate.Net.Rules.Content;

public class RequireElementAncestorsRule : BaseValidator<HtmlDocument>
{
    public override bool IsValid(HtmlDocument model)
    {
        // start with documentNode here

        // an li child element, should have an <ul>, <ol> or <menu> direct parent

        model.DocumentNode.Descendants()
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
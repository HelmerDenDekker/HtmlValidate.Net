using System.ComponentModel.DataAnnotations;
using HtmlAgilityPack;

namespace HtmlValidate.Net.Rules.Content;

public class AllowedChildElementsRule : BaseValidator<IEnumerable<HtmlNode>>
{
    public override bool IsValid(IEnumerable<HtmlNode> model)
    {
        // an ol, ul or menu element should only have li child elements

        model
            .Where(n => n.Name == "ol" || n.Name == "ul" || n.Name == "menu")
            .ToList()
            .ForEach(parent =>
            {
                var invalidChildren = parent.ChildNodes
                    .Where(c => c.NodeType == HtmlNodeType.Element && !c.Name.Equals("li", StringComparison.OrdinalIgnoreCase))
                    .ToList();
                if (invalidChildren.Any())
                {
                    Results.Add(new ValidationResult($"Only <li> elements are allowed as children of <{parent.Name}>."));
                }
            });
        
        return Results.Count == 0;
    }
}
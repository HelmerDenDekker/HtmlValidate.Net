using System.ComponentModel.DataAnnotations;
using HtmlAgilityPack;

namespace HtmlValidate.Net.Rules.Content;

public class AllowedChildElementsRule : HandlerBase<ValidationRequest>
{
    protected override void HandleInternal(ValidationRequest request)
    {
        request.HtmlNodes
            .Where(n => n.Name.Equals("ol", StringComparison.OrdinalIgnoreCase) ||
                        n.Name.Equals("ul", StringComparison.OrdinalIgnoreCase) ||
                        n.Name.Equals("menu", StringComparison.OrdinalIgnoreCase))
            .ToList()
            .ForEach(parent =>
            {
                var invalidChildren = parent.ChildNodes
                    .Where(c => c.NodeType == HtmlNodeType.Element &&
                                !c.Name.Equals("li", StringComparison.OrdinalIgnoreCase))
                    .ToList();
                if (invalidChildren.Any())
                    request.Results.Add(
                        new ValidationResult($"Only <li> elements are allowed as children of <{parent.Name}>."));
            });
    }
}
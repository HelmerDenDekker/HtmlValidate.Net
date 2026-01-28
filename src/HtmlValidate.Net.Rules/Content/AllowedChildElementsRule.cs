using System.ComponentModel.DataAnnotations;
using HtmlAgilityPack;

namespace HtmlValidate.Net.Rules.Content;

public class AllowedChildElementsRule : HandlerBase<ValidationRequest>
{
    protected override void HandleInternal(ValidationRequest request)
    {
        request.Nodes
            .Where(n => n.Name == "ol" || n.Name == "ul" || n.Name == "menu")
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
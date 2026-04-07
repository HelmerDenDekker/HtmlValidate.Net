using System.ComponentModel.DataAnnotations;

namespace HtmlValidate.Net.Rules.Content;

public class RequiredAncestorElementRule : HandlerBase<ValidationRequest>
{
    protected override void HandleInternal(ValidationRequest request)
    {
        // a li child element, should have an <ul>, <ol> or <menu> direct parent
        request.HtmlNodes
            .Where(n => n.Name.Equals("li", StringComparison.OrdinalIgnoreCase))
            .ToList()
            .ForEach(li =>
            {
                var parent = li.ParentNode;
                var hasValidAncestor = parent.Name.Equals("ul", StringComparison.OrdinalIgnoreCase) ||
                                       parent.Name.Equals("ol", StringComparison.OrdinalIgnoreCase) ||
                                       parent.Name.Equals("menu", StringComparison.OrdinalIgnoreCase);
                if (!hasValidAncestor)
                    request.Results.Add(
                        new ValidationResult("<li> element must have a <ul>, <ol> or <menu> ancestor."));
            });
    }
}
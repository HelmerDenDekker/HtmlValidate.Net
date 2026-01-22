using System.ComponentModel.DataAnnotations;

namespace HtmlValidate.Net.Rules.Accessibility;

/// <summary>
///     Providing descriptive headings: disallow empty heading elements.
///     https://www.w3.org/WAI/WCAG22/Techniques/general/G130
/// </summary>
public class RequireHeadingsTextContentRule : HandlerBase<ValidationRequest>
{
    protected override void HandleInternal(ValidationRequest request)
    {
        if (request.HtmlNodes.Any())
            ValidateHtmlNodes(request);
    }

    private void ValidateHtmlNodes(ValidationRequest request)
    {
        var headingNames = new[] { "h1", "h2", "h3", "h4", "h5", "h6" };

        foreach (var node in request.HtmlNodes.Where(n =>
                     n != null && headingNames.Any(h => h.Equals(n.Name, StringComparison.InvariantCultureIgnoreCase))))
        {
            if (HtmlStringHelper.HasMeaningfulText(node))
                continue;

            if (HtmlStringHelper.HasImageWithAlt(node))
                continue;

            request.Results.Add(new ValidationResult($"<{node.Name}> cannot be empty, it must have text content."));
        }
    }
}
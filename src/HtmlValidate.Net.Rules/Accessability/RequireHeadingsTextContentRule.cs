using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace HtmlValidate.Net.Rules.Accessability;

/// <summary>
/// Providing descriptive headings: disallow empty heading elements.
/// https://www.w3.org/WAI/WCAG22/Techniques/general/G130
/// </summary>
public class RequireHeadingsTextContentRule : BaseValidator<IEnumerable<HtmlNode>>
{
    public override bool IsValid(IEnumerable<HtmlNode> model)
    {
        // clear any previous results in case the same instance is reused
        Results.Clear();

        if (model == null)
        {
            return true;
        }

        var headingNames = new[] { "h1", "h2", "h3", "h4", "h5", "h6" };

        // explicit invisible/format characters to strip
        var invisibleChars = new HashSet<char>
        {
            '\u00A0', // NO-BREAK SPACE (&nbsp;)
            '\u200B', // ZERO WIDTH SPACE
            '\u200C', // ZERO WIDTH NON-JOINER (&zwnj;)
            '\u200D', // ZERO WIDTH JOINER (&zwj;)
            '\u2060', // WORD JOINER
            '\uFEFF', // ZERO WIDTH NO-BREAK SPACE (BOM)
            '\u00AD', // SOFT HYPHEN (&shy;)
            '\u200E', // LRM
            '\u200F'  // RLM
        };

        foreach (var node in model.Where(n => n != null && headingNames.Any(h => h.Equals(n.Name, StringComparison.InvariantCultureIgnoreCase))))
        {
            if (HasMeaningfulText(node, invisibleChars))
                continue;

            if (HasImageWithAlt(node))
                continue;

            Results.Add(new ValidationResult($"<{node.Name}> cannot be empty, it must have text content."));
        }

        return Results.Count == 0;
    }

    private static bool HasImageWithAlt(HtmlNode node)
    {
        return node.Descendants("img").Any(img =>
        {
            var alt = img.GetAttributeValue("alt", null);
            return !string.IsNullOrWhiteSpace(alt);
        });
    }

    private static bool HasMeaningfulText(HtmlNode node, HashSet<char> invisibleChars)
    {
        // decode HTML entities (e.g. &nbsp;, &shy;, &#8203;) to actual characters
        var text = HtmlEntity.DeEntitize(node.InnerText ?? string.Empty);

        foreach (var c in text)
        {
            // treat ordinary whitespace as empty
            if (char.IsWhiteSpace(c))
                continue;

            // treat known invisible chars as empty
            if (invisibleChars.Contains(c))
                continue;

            // treat format characters (category Cf) as invisible
            if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.Format)
                continue;

            // if we reach here it's a visible/meaningful character
            return true;
        }

        return false;
    }
}

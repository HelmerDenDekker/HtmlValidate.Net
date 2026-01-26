using System.Globalization;
using HtmlAgilityPack;

namespace HtmlValidate.Net.Rules;

public static class HtmlStringHelper
{
    public static readonly IEnumerable<char> InvisibleHtmlStrings = new List<char>
    {
        '\u00A0', // NO-BREAK SPACE (&nbsp;)
        '\u1680', // OGHAM SPACE MARK
        '\u2000', // EN QUAD
        '\u2001', // EM QUAD
        '\u2002', // EN SPACE
        '\u2003', // EM SPACE
        '\u2004', // THREE-PER-EM SPACE
        '\u2005', // FOUR-PER-EM SPACE
        '\u2006', // SIX-PER-EM SPACE
        '\u2007', // FIGURE SPACE
        '\u2008', // PUNCTUATION SPACE
        '\u2009', // THIN SPACE
        '\u200A', // HAIR SPACE
        '\u202F', // NARROW NO-BREAK SPACE
        '\u205F', // MEDIUM MATHEMATICAL SPACE
        '\u2060', // WORD JOINER
        '\uFEFF', // ZERO WIDTH NO-BREAK SPACE (BOM)
        '\u00AD', // SOFT HYPHEN (&shy;)
        '\u200B', // ZERO WIDTH SPACE
        '\u200C', // ZERO WIDTH NON-JOINER (&zwnj;)
        '\u200D', // ZERO WIDTH JOINER (&zwj;)
        '\u200E', // LRM
        '\u200F' // RLM
    };

    public static bool HasMeaningfulText(HtmlNode node)
    {
        // decode HTML entities (e.g. &nbsp;, &shy;, &#8203;) to actual characters
        var text = HtmlEntity.DeEntitize(node.InnerText);

        foreach (var c in text)
        {
            // treat ordinary whitespace as empty
            if (char.IsWhiteSpace(c))
                continue;

            // treat known invisible chars as empty
            if (InvisibleHtmlStrings.Contains(c))
                continue;

            // treat format characters (category Cf) as invisible
            if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.Format)
                continue;

            // if we reach here it's a visible/meaningful character
            return true;
        }

        return false;
    }

    public static bool HasImageWithAlt(HtmlNode node)
    {
        return node.Descendants("img").Any(img =>
        {
            var alt = img.GetAttributeValue("alt", string.Empty);
            return !string.IsNullOrWhiteSpace(alt);
        });
    }
}
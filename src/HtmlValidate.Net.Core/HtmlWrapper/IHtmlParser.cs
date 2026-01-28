namespace HtmlValidate.Net.Core.HtmlWrapper;

public interface IHtmlParser
{
    IParsedDocument Parse(string html);
}
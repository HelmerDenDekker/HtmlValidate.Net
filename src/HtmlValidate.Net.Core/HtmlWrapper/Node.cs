namespace HtmlValidate.Net.Core.HtmlWrapper;

/// <summary>
///     Html Node representation.
/// </summary>
public class Node
{
    public string Name { get; set; } = string.Empty;
    public string InnerHtml { get; set; } = string.Empty;
    public IDictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
    public string InnerText { get; set; } = string.Empty;
}
using System.Text.Json;
using HtmlAgilityPack;
using HtmlValidate.Net.Rules.Security;

namespace HtmlValidate.Net.Rules.Tests;


public class DisallowScriptElementsRuleTests
{
    [Test]
    public void DisallowScriptElementsRule_HtmlHasScript_IsValidShouldReturnFalse()
    {
        // Arrange
        var html = "<html><head><script>alert('test');</script></head><body></body></html>";
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var validator = new DisallowScriptElementsRule();
        
        // Act
        var result = validator.IsValid(doc.DocumentNode.Descendants());
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void DisallowScriptElementsRule_HtmlWithNoScript_IsValidShouldReturnTrue()
    {
        // Arrange
        var html = "<html><head></head><body></body></html>";
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var validator = new DisallowScriptElementsRule();
        
        // Act
        var result = validator.IsValid(doc.DocumentNode.Descendants());
        
        // Assert
        Assert.That(result, Is.True);
    }
}
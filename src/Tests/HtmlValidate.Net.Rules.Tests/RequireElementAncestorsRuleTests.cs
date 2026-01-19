using HtmlAgilityPack;
using HtmlValidate.Net.Rules.Content;

namespace HtmlValidate.Net.Rules.Tests;

public class RequireElementAncestorsRuleTests
{
    // an li child element, should have an <ul>, <ol> or <menu> ancestor
    [Test]
    public void IsValid_LiWithUlAncestor_ShouldReturnTrue()
    {
        // Arrange
        var html = "<ul><li>Item</li></ul>";
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        
        var validator = new RequireElementAncestorsRule();

        // Act
        var result = validator.IsValid(doc.DocumentNode.Descendants());

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsValid_LiWithoutUlAncestor_ShouldReturnFalse()
    {
        // Arrange
        var html = "<div><li>Item</li></div>";
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var validator = new RequireElementAncestorsRule();

        // Act
        var result = validator.IsValid(doc.DocumentNode.Descendants());

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsValid_LiWithOlAncestor_ShouldReturnTrue()
    {
        // Arrange
        var html = "<ol><li>Item</li></ol>";
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var validator = new RequireElementAncestorsRule();
        
        // Act
        var result = validator.IsValid(doc.DocumentNode.Descendants());
        
        // Assert
        Assert.That(result, Is.True);
    }
    
    /// <summary>
    /// A subitem in a list is parsed by a html parser as a member of the parent node ul or ol: https://html.spec.whatwg.org/multipage/syntax.html#writing
    /// </summary>
    [Test]
    public void IsValid_LiWithOlAncestorAndLiSubItem_ShouldReturnTrue()
    {
        // Arrange
        var html = "<ol><li>Item<li>SubItem</li></li></ol>";
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var validator = new RequireElementAncestorsRule();
        
        // Act
        var result = validator.IsValid(doc.DocumentNode.Descendants());
        
        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsValid_LiWithMenuAncestor_ShouldReturnTrue()
    {
        // Arrange
        var html = "<menu><li>Item</li></menu>";
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var validator = new RequireElementAncestorsRule();

        // Act
        var result = validator.IsValid(doc.DocumentNode.Descendants());

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsValid_NoLiElements_ShouldReturnTrue()
    {
        // Arrange
        var html = "<div><p>No list items here</p></div>";
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var validator = new RequireElementAncestorsRule();
        
        // Act
        var result = validator.IsValid(doc.DocumentNode.Descendants());
     
        // Assert
        Assert.That(result, Is.True);
    }
}
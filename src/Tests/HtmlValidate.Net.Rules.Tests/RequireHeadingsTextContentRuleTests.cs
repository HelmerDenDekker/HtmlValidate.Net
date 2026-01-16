using HtmlAgilityPack;
using HtmlValidate.Net.Rules.Accessability;

namespace HtmlValidate.Net.Rules.Tests;

public class RequireHeadingsTextContentRuleTests
{
    [Test]
    public void RequireHeadingsTextContentRule_HtmlWithNoHeadings_IsValidShouldReturnTrue()
    {
        // Arrange
        var html = "<html><body><p>No headings here</p></body></html>";
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var validator = new RequireHeadingsTextContentRule();
        
        // Act
        var result = validator.IsValid(doc.DocumentNode.Descendants());
        
        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void RequireHeadingsTextContentRule_HtmlWithHeadingsWithText_IsValidShouldReturnTrue()
    {
        // Arrange
        var html = "<html><body><h1>Title</h1><h2>Subtitle</h2></body></html>";
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var validator = new RequireHeadingsTextContentRule();

        // Act
        var result = validator.IsValid(doc.DocumentNode.Descendants());

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void RequireHeadingsTextContentRule_HtmlWithHeadingsWithoutText_IsValidShouldReturnFalse()
    {
        // Arrange
        var html = "<html><body><h1></h1></body></html>";
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var validator = new RequireHeadingsTextContentRule();

        // Act
        var result = validator.IsValid(doc.DocumentNode.Descendants());

        // Assert
        Assert.That(result, Is.False);
        Assert.That(validator.Results.Count, Is.EqualTo(1));
        Assert.That(validator.Results.First().ErrorMessage, Is.EqualTo("<h1> cannot be empty, it must have text content."));
    }

    [Test]
    public void RequireHeadingsTextContentRule_HeadingsWithEmptyChildNodes_IsValidShouldReturnFalse()
    {
        // Arrange
        var html = "<html><body><h1></h1><h2><span></span></h2></body></html>";
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var validator = new RequireHeadingsTextContentRule();

        // Act
        var result = validator.IsValid(doc.DocumentNode.Descendants());

        // Assert
        Assert.That(result, Is.False);
        Assert.That(validator.Results.Count, Is.EqualTo(2));
        Assert.That(validator.Results.First().ErrorMessage, Is.EqualTo("<h1> cannot be empty, it must have text content."));
        Assert.That(validator.Results.Last().ErrorMessage, Is.EqualTo("<h2> cannot be empty, it must have text content."));
    }
    
    // Images can be used if they have alternative text
    [Test]
    public void RequireHeadingsTextContentRule_HeadingsWithImageAndAltText_IsValidShouldReturnTrue()
    {
        // Arrange
        var html = "<html><body><h1><img src='image.jpg' alt='An image'></h1></body></html>";
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var validator = new RequireHeadingsTextContentRule();

        // Act
        var result = validator.IsValid(doc.DocumentNode.Descendants());

        // Assert
        Assert.That(result, Is.True);
    }
    
    // Images without alt text should not count as valid content
    [Test]
    public void RequireHeadingsTextContentRule_HeadingsWithImageWithoutAltText_IsValidShouldReturnFalse()
    {
        // Arrange
        var html = "<html><body><h1><img src='image.jpg'></h1></body></html>";
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var validator = new RequireHeadingsTextContentRule();
        
        // Act
        var result = validator.IsValid(doc.DocumentNode.Descendants());
        
        // Assert
        Assert.That(result, Is.False);
        Assert.That(validator.Results.Count, Is.EqualTo(1));
        Assert.That(validator.Results.First().ErrorMessage, Is.EqualTo("<h1> cannot be empty, it must have text content."));
    }
    
    // Text with whitespace should be considered empty
    [TestCase("<h1>   </h1>")]
    [TestCase("<h1>\n\t</h1>")]
    [TestCase("<h1>\r\n</h1>")]
    [TestCase("<h1> &nbsp; </h1>")]
    [TestCase("<h1> &#8203; </h1>")]
    [TestCase("<h1> &#x200B; </h1>")]
    [TestCase("<h1> &shy; </h1>")]
    [TestCase("<h1> &shy; &nbsp; </h1>")]
    [TestCase("<h1>&nbsp;&nbsp;&nbsp;&nbsp;</h1>")]
    public void RequireHeadingsTextContentRule_HeadingsWithWhitespaceOnly_IsValidShouldReturnFalse(string headingHtml)
    {
        // Arrange
        var html = $"<html><body>{headingHtml}</body></html>";
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var validator = new RequireHeadingsTextContentRule();

        // Act
        var result = validator.IsValid(doc.DocumentNode.Descendants());

        // Assert
        Assert.That(result, Is.False);
        Assert.That(validator.Results.Count, Is.EqualTo(1));
        Assert.That(validator.Results.First().ErrorMessage,
            Is.EqualTo("<h1> cannot be empty, it must have text content."));
    }
}
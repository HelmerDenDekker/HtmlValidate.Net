using HtmlValidate.Net.Rules.Accessibility;

namespace HtmlValidate.Net.Rules.Tests.Accessibility;

public class RequireHeadingsTextContentRuleTests
{
    [Test]
    public void RequireHeadingsTextContentRule_HtmlWithNoHeadings_IsValidShouldReturnTrue()
    {
        // Arrange
        var html = "<html><body><p>No headings here</p></body></html>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<RequireHeadingsTextContentRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.True);
    }

    [Test]
    public void RequireHeadingsTextContentRule_HtmlWithHeadingsWithText_IsValidShouldReturnTrue()
    {
        // Arrange
        var html = "<html><body><h1>Title</h1><h2>Subtitle</h2></body></html>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<RequireHeadingsTextContentRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.True);
    }

    [Test]
    public void RequireHeadingsTextContentRule_HtmlWithHeadingsWithoutText_IsValidShouldReturnFalse()
    {
        // Arrange
        var html = "<html><body><h1></h1></body></html>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<RequireHeadingsTextContentRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.False);
        Assert.That(validationRequest.Results.Count, Is.EqualTo(1));
        Assert.That(validationRequest.Results.First().ErrorMessage,
            Is.EqualTo("<h1> cannot be empty, it must have text content."));
    }

    [Test]
    public void RequireHeadingsTextContentRule_HeadingsWithEmptyChildNodes_IsValidShouldReturnFalse()
    {
        // Arrange
        var html = "<html><body><h1></h1><h2><span></span></h2></body></html>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<RequireHeadingsTextContentRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.False);
        Assert.That(validationRequest.Results.Count, Is.EqualTo(2));
        Assert.That(validationRequest.Results.First().ErrorMessage,
            Is.EqualTo("<h1> cannot be empty, it must have text content."));
        Assert.That(validationRequest.Results.Last().ErrorMessage,
            Is.EqualTo("<h2> cannot be empty, it must have text content."));
    }

    // Images can be used if they have alternative text
    [Test]
    public void RequireHeadingsTextContentRule_HeadingsWithImageAndAltText_IsValidShouldReturnTrue()
    {
        // Arrange
        var html = "<html><body><h1><img src='image.jpg' alt='An image'></h1></body></html>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<RequireHeadingsTextContentRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.True);
    }

    // Images without alt text should not count as valid content
    [Test]
    public void RequireHeadingsTextContentRule_HeadingsWithImageWithoutAltText_IsValidShouldReturnFalse()
    {
        // Arrange
        var html = "<html><body><h1><img src='image.jpg'></h1></body></html>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<RequireHeadingsTextContentRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.False);
        Assert.That(validationRequest.Results.Count, Is.EqualTo(1));
        Assert.That(validationRequest.Results.First().ErrorMessage,
            Is.EqualTo("<h1> cannot be empty, it must have text content."));
    }

    // Text with whitespace should be considered empty
    [TestCase("<h1>   </h1>")]
    [TestCase("<h1>\n\t</h1>")]
    [TestCase("<h1>\r\n</h1>")]
    [TestCase("<h1> &nbsp; </h1>")]
    [TestCase("<h1> &#8192; </h1>")]
    [TestCase("<h1> &#8193; </h1>")]
    [TestCase("<h1> &#8194; </h1>")]
    [TestCase("<h1> &#8195; </h1>")]
    [TestCase("<h1> &#8196; </h1>")]
    [TestCase("<h1> &#8197; </h1>")]
    [TestCase("<h1> &#8198; </h1>")]
    [TestCase("<h1> &#8199; </h1>")]
    [TestCase("<h1> &#8200; </h1>")]
    [TestCase("<h1> &#8201; </h1>")]
    [TestCase("<h1> &#8202; </h1>")]
    [TestCase("<h1> &#8203; </h1>")]
    [TestCase("<h1> &#8204; </h1>")]
    [TestCase("<h1> &#8205; </h1>")]
    [TestCase("<h1> &#8206; </h1>")]
    [TestCase("<h1> &#8207; </h1>")]
    [TestCase("<h1> &#8232; </h1>")]
    [TestCase("<h1> &#8239; </h1>")]
    [TestCase("<h1> &#8287; </h1>")]
    [TestCase("<h1> &#x200B; </h1>")]
    [TestCase("<h1> &shy; </h1>")]
    [TestCase("<h1> &shy; &nbsp; </h1>")]
    [TestCase("<h1>&nbsp;&nbsp;&nbsp;&nbsp;</h1>")]
    public void RequireHeadingsTextContentRule_HeadingsWithWhitespaceOnly_IsValidShouldReturnFalse(string headingHtml)
    {
        // Arrange
        var html = $"<html><body>{headingHtml}</body></html>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<RequireHeadingsTextContentRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.False);
        Assert.That(validationRequest.Results.Count, Is.EqualTo(1));
        Assert.That(validationRequest.Results.First().ErrorMessage,
            Is.EqualTo("<h1> cannot be empty, it must have text content."));
    }
}
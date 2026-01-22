using HtmlValidate.Net.Rules.Content;

namespace HtmlValidate.Net.Rules.Tests.Content;

public class AllowedChildElementsRuleTests
{
    [Test]
    public void IsValid_HtmlWithAllowedChildElements_ShouldReturnTrue()
    {
        // Arrange
        var html = "<div><p>Paragraph inside div</p></div>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<AllowedChildElementsRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.True);
    }

    [Test]
    public void IsValid_HtmlWithDisallowedChildElement_ShouldReturnFalse()
    {
        // Arrange
        var html = "<ul><div>Invalid child inside ul</div></ul>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<AllowedChildElementsRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.False);
        Assert.That(validationRequest.Results.Count, Is.EqualTo(1));
        Assert.That(validationRequest.Results.First().ErrorMessage,
            Is.EqualTo("Only <li> elements are allowed as children of <ul>."));
    }

    [Test]
    public void IsValid_HtmlWithAllowedAndDisallowedChildElements_ShouldReturnFalse()
    {
        // Arrange
        var html = "<ul><li>Valid</li><div>Invalid child inside ul</div></ul>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<AllowedChildElementsRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.False);
        Assert.That(validationRequest.Results.Count, Is.EqualTo(1));
        Assert.That(validationRequest.Results.First().ErrorMessage,
            Is.EqualTo("Only <li> elements are allowed as children of <ul>."));
    }

    [Test]
    public void IsValid_HtmlWithDisallowedChildElements_ShouldReturnFalse()
    {
        // Arrange
        var html = "<ul><br><div>Invalid child inside ul</div></ul>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<AllowedChildElementsRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.False);
        Assert.That(validationRequest.Results.Count, Is.EqualTo(1));
        Assert.That(validationRequest.Results.First().ErrorMessage,
            Is.EqualTo("Only <li> elements are allowed as children of <ul>."));
    }

    [Test]
    public void IsValid_HtmlWithSubListsInListItem_ShouldReturnTrue()
    {
        // Arrange
        var html = "<ol> <li> <ol> <li>sub</li> </ol> </li> </ol>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<AllowedChildElementsRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.True);
    }

    [Test]
    public void IsValid_HtmlWithDirectSubLists_ShouldReturnFalse()
    {
        // Arrange
        var html = "<ol> <ol> <li>sub</li> </ol> </ol>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<AllowedChildElementsRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.False);
    }

    [Test]
    public void IsValid_HtmlWithListItemsContainingFlowElements_ShouldReturnTrue()
    {
        // Arrange
        var html = "<ol>\n<li><span>txt</span><br>\n<ol><li>sub</li></ol>\n</li>\n</ol>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<AllowedChildElementsRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.True);
    }
}
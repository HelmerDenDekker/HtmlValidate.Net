using HtmlValidate.Net.Rules.Content;

namespace HtmlValidate.Net.Rules.Tests.Content;

public class RequiredAncestorElementRuleTests
{
    // a li child element, should have an <ul>, <ol> or <menu> ancestor
    [Test]
    public void IsValid_LiWithUlAncestor_ShouldReturnTrue()
    {
        // Arrange
        var html = "<ul><li>Item</li></ul>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<RequiredAncestorElementRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.True);
    }

    [Test]
    public void IsValid_LiWithoutUlAncestor_ShouldReturnFalse()
    {
        // Arrange
        var html = "<div><li>Item</li></div>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<RequiredAncestorElementRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.False);
        Assert.That(validationRequest.Results.Count, Is.EqualTo(1));
        Assert.That(validationRequest.Results[0].ErrorMessage,
            Is.EqualTo("<li> element must have a <ul>, <ol> or <menu> ancestor."));
    }

    [Test]
    public void IsValid_LiWithOlAncestor_ShouldReturnTrue()
    {
        // Arrange
        var html = "<ol><li>Item</li></ol>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<RequiredAncestorElementRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.True);
    }

    /// <summary>
    ///     A subitem in a list is parsed by a html parser as a member of the parent node ul or ol:
    ///     https://html.spec.whatwg.org/multipage/syntax.html#writing
    /// </summary>
    [Test]
    public void IsValid_LiWithOlAncestorAndLiSubItem_ShouldReturnTrue()
    {
        // Arrange
        var html = "<ol><li>Item<li>SubItem</li></li></ol>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<RequiredAncestorElementRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.True);
    }

    [Test]
    public void IsValid_LiWithMenuAncestor_ShouldReturnTrue()
    {
        // Arrange
        var html = "<menu><li>Item</li></menu>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<RequiredAncestorElementRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.True);
    }

    [Test]
    public void IsValid_NoLiElements_ShouldReturnTrue()
    {
        // Arrange
        var html = "<div><p>No list items here</p></div>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<RequiredAncestorElementRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.True);
    }
}
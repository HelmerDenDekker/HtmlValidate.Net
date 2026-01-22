using HtmlValidate.Net.Rules.Security;

namespace HtmlValidate.Net.Rules.Tests;

public class DisallowScriptElementsRuleTests
{
    [Test]
    public void IsValid_HtmlHasScript_ShouldReturnFalse()
    {
        // Arrange
        var html = "<html><head><script>alert('test');</script></head><body></body></html>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<DisallowScriptElementsRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.False);
        Assert.That(validationRequest.Results.Count, Is.EqualTo(1));
        Assert.That(validationRequest.Results[0].ErrorMessage, Is.EqualTo("Disallowed HTML Element: <script> found."));
    }

    [Test]
    public void IsValid_HtmlWithNoScript_ShouldReturnTrue()
    {
        // Arrange
        var html = "<html><head></head><body></body></html>";
        var validationRequest = new ValidationRequest(html);

        // Act
        new ChainBuilder<ValidationRequest>()
            .RegisterHandler<DisallowScriptElementsRule>()
            .Build()
            .Handle(validationRequest);

        // Assert
        Assert.That(validationRequest.IsValid, Is.True);
    }
}
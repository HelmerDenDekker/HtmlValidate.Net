using HtmlValidate.Net.Rules.Content;

namespace HtmlValidate.Net.Rules.Tests;

public class ValidationChainBuilderTests
{
    [Test]
    public void Build_NoHandlersRegistered_ThrowsException()
    {
        // Arrange
        var builder = new ChainBuilder<object>();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    // TODO rename test
    [Test]
    public void Build_HandlersRegistered_ReturnsFirstHandler()
    {
        // Arrange
        var html = "<html><body><h1><img src='image.jpg' alt='An image'></h1></body></html>";
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
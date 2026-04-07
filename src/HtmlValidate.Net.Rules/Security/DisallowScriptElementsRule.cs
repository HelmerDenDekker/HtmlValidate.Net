using System.ComponentModel.DataAnnotations;

namespace HtmlValidate.Net.Rules.Security;

public class DisallowScriptElementsRule : HandlerBase<ValidationRequest>
{
    protected override void HandleInternal(ValidationRequest request)
    {
        request.HtmlNodes
            .Where(n => n.Name.Equals("script", StringComparison.OrdinalIgnoreCase))
            .ToList()
            .ForEach(n =>
            {
                request.Results.Add(new ValidationResult($"Disallowed HTML Element: <{n.Name}> found."));
            });
    }
}
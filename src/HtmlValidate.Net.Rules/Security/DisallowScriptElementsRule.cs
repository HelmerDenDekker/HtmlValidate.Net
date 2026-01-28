using System.ComponentModel.DataAnnotations;

namespace HtmlValidate.Net.Rules.Security;

public class DisallowScriptElementsRule : HandlerBase<ValidationRequest>
{
    protected override void HandleInternal(ValidationRequest request)
    {
        request.Nodes
            .Where(n => n.Name.Equals("script", StringComparison.InvariantCultureIgnoreCase))
            .ToList()
            .ForEach(n =>
            {
                request.Results.Add(new ValidationResult($"Disallowed HTML Element: <{n.Name}> found."));
            });
    }
}
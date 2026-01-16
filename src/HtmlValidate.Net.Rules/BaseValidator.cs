using System.ComponentModel.DataAnnotations;

namespace HtmlValidate.Net.Rules;

public class BaseValidator<T> : IValidator<T>
{
    public List<ValidationResult> Results { get; } = new();

    public virtual bool IsValid(T model)
    {
        Validate(model);

        return Results.Count == 0;
    }
    
    private void Validate(T model)
    {
        var context = new ValidationContext(model);
        Validator.TryValidateObject(model, context, Results, true);
    }
}
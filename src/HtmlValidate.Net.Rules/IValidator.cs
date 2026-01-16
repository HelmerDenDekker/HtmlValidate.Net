using System.ComponentModel.DataAnnotations;

namespace HtmlValidate.Net.Rules;

public interface IValidator<T>
{
    bool IsValid(T model);
		
    List<ValidationResult> Results { get; }
}
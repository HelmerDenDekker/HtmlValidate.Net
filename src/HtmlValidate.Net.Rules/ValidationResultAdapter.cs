using System.ComponentModel.DataAnnotations;
using HtmlAgilityPack;

namespace HtmlValidate.Net.Rules;

public static class ValidationResultAdapter
{
    public static IEnumerable<ValidationResult> ToValidationResults(this IEnumerable<HtmlParseError> htmlParseErrors)
    {
        return htmlParseErrors.Select(error => new ValidationResult(error.Reason));
    }
}
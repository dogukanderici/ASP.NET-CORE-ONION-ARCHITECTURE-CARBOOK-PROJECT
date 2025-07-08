using FluentValidation.Results;

namespace CarBook.WebAPI.Utilities.Helper
{
    public interface IValidationResultMessageHelper
    {
        Dictionary<string, string[]> ValidationMessages(ValidationResult validationResult);
    }
}

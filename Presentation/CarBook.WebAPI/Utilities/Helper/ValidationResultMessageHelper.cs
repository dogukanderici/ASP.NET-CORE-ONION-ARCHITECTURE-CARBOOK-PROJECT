using FluentValidation.Results;

namespace CarBook.WebAPI.Utilities.Helper
{
    public class ValidationResultMessageHelper : IValidationResultMessageHelper
    {
        public Dictionary<string, string[]> ValidationMessages(ValidationResult validationResult)
        {
            return validationResult.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    e => e.Key,
                    e => e.Select(m => m.ErrorMessage).ToArray()
                );
        }
    }
}

using SHARED.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Client.Pages.Validation
{
    public class AddEditLocationValidator : AbstractValidator<Location>
    {
        public AddEditLocationValidator()
        {

            RuleFor(v => v.Name)
            .MaximumLength(256)
                .NotEmpty();

            RuleFor(v => v.Description)
         .MaximumLength(256)
             .NotEmpty();

        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<Location>.CreateWithOptions((Location)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}

using SHARED.Models;
using FluentValidation;

namespace Client.Pages.Validation
{
    public class AddEditProductCommandValidator : AbstractValidator<Product>
    {
        public AddEditProductCommandValidator()
        {

            RuleFor(v => v.Name)
            .MaximumLength(256)
                .NotEmpty();

            RuleFor(v => v.Productid)
         .MaximumLength(256)
             .NotEmpty();

            RuleFor(v => v.invoice_No)
    .MaximumLength(256)
        .NotEmpty();

            RuleFor(v => v.color)
        .MaximumLength(256)
            .NotEmpty();
        }
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<Product>.CreateWithOptions((Product)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}

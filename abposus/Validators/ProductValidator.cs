using abposus.Models;
using FluentValidation;

namespace abposus.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(4).WithMessage("name must have at least 4 characters");
            RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0).WithMessage("quantity must be a positive value");
            RuleFor(x => x.UnitPrice).NotEmpty().GreaterThan(0).WithMessage("unit price must be a positive value");
        }
    }
}

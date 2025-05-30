namespace CourseMicroservice.Discount.API.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Code).Length(10).WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Rate).NotEmpty().WithMessage("{PropertyName} is required.").InclusiveBetween(0, 1).WithMessage("{PropertyName} must be between {From} and {To}.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Expired).NotEmpty().WithMessage("{PropertyName} is required.").GreaterThan(DateTime.UtcNow).WithMessage("{PropertyName} must be in the future.");
        }
    }
}

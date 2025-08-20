using FluentValidation;

namespace CourseMicroservice.Payment.API.Features.Payments.Create
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(x => x.OrderCode)
                .NotEmpty().WithMessage("Order code is required.");

            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("Card number is required.")
                .CreditCard().WithMessage("Invalid card number format.");

            RuleFor(x => x.CardHolderName)
                .NotEmpty().WithMessage("Card holder name is required.")
                .MaximumLength(100).WithMessage("Card holder name cannot exceed 100 characters.");

            RuleFor(x => x.CardExpirationDate)
                .NotEmpty().WithMessage("Card expiration date is required.")
                .Matches(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$").WithMessage("Invalid card expiration date format. Use MM/YY.");

            RuleFor(x => x.CardSecurityNumber)
                .NotEmpty().WithMessage("Card security number is required.")
                .Length(3, 4).WithMessage("Card security number must be 3 or 4 digits.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
        }
    }
}

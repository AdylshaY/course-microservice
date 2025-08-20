using CourseMicroservice.Payment.API.Repositories;
using CourseMicroservice.Shared;
using CourseMicroservice.Shared.Services;
using MediatR;

namespace CourseMicroservice.Payment.API.Features.Payments.Create
{
    public class CreatePaymentCommandHandler(AppDbContext appDbContext, IIdentityService identityService) : IRequestHandler<CreatePaymentCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var (isSuccess, errorMessage) = await ExternalPaymentProcessAsync(request.CardNumber, request.CardHolderName, request.CardExpirationDate, request.CardSecurityNumber, request.Amount);

            if (!isSuccess) return ServiceResult<Guid>.Error("Payment Failed", errorMessage, System.Net.HttpStatusCode.BadRequest);

            var userId = identityService.GetUserId;

            var newPayment = new Repositories.Payment(userId, request.OrderCode, request.Amount);

            newPayment.SetStatus(PaymentStatus.Success);

            appDbContext.Payments.Add(newPayment);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.SuccessAsOk(newPayment.Id);
        }

        private async Task<(bool isSuccess, string errorMessage)> ExternalPaymentProcessAsync(string cardNumber, string cardHolderName, string cardExpirationDate, string cardSecurityNumber, decimal amount)
        {
            // Simulate an external payment processing service call
            await Task.Delay(1000);

            Random random = new();
            // Simulate a 90% success rate for payment processing
            bool isSuccess = random.Next(1, 11) <= 9;
            if (isSuccess)
            {
                return (true, "Ödeme işlemi başarıyla gerçekleştirildi.");
            }
            else
            {
                return (false, "Ödeme işlemi başarısız oldu. Lütfen kart bilgilerinizi kontrol edin.");
            }
        }
    }
}

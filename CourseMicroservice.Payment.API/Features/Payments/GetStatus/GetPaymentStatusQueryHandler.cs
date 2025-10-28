namespace CourseMicroservice.Payment.API.Features.Payments.GetStatus
{
    using CourseMicroservice.Payment.API.Repositories;
    using CourseMicroservice.Shared;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetPaymentStatusQueryHandler(AppDbContext dbContext) : IRequestHandler<GetPaymentStatusRequest, ServiceResult<GetPaymentStatusResponse>>
    {
        public async Task<ServiceResult<GetPaymentStatusResponse>> Handle(GetPaymentStatusRequest request, CancellationToken cancellationToken)
        {
            var payment = await dbContext.Payments.FirstOrDefaultAsync(x => x.OrderCode == request.OrderCode, cancellationToken: cancellationToken);

            if (payment is null) return ServiceResult<GetPaymentStatusResponse>.SuccessAsOk(new GetPaymentStatusResponse(null, false));

            return ServiceResult<GetPaymentStatusResponse>.SuccessAsOk(new GetPaymentStatusResponse(payment.Id, payment.Status == PaymentStatus.Success));
        }
    }
}

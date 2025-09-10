using CourseMicroservice.Payment.API.Repositories;
using CourseMicroservice.Shared;
using CourseMicroservice.Shared.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseMicroservice.Payment.API.Features.Payments.GetAllPaymentsByUserId
{
    public class GetAllPaymentsByUserIdQueryHandler(AppDbContext context, IIdentityService identityService) : IRequestHandler<GetAllPaymentsByUserIdQuery, ServiceResult<List<GetAllPaymentsByUserIdResponse>>>
    {
        public async Task<ServiceResult<List<GetAllPaymentsByUserIdResponse>>> Handle(GetAllPaymentsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = identityService.UserId;
            var payments = await context.Payments
                .Where(p => p.UserId == userId)
                .Select(p => new GetAllPaymentsByUserIdResponse(p.Id, p.OrderCode, p.Amount.ToString("C"), p.Created, p.Status))
                .ToListAsync(cancellationToken);

            return ServiceResult<List<GetAllPaymentsByUserIdResponse>>.SuccessAsOk(payments);
        }
    }
}

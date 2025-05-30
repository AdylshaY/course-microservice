using CourseMicroservice.Discount.API.Repositories;
using CourseMicroservice.Shared.Services;

namespace CourseMicroservice.Discount.API.Features.Discounts.GetDiscountByCode
{
    public class GetDiscountByCodeQueryHandler(AppDbContext context, IIdentityService identityService) : IRequestHandler<GetDiscountByCodeQuery, ServiceResult<GetDiscountByCodeQueryResponse>>
    {
        public async Task<ServiceResult<GetDiscountByCodeQueryResponse>> Handle(GetDiscountByCodeQuery request, CancellationToken cancellationToken)
        {
            var discount = await context.Discounts.SingleOrDefaultAsync(d => d.Code == request.Code, cancellationToken: cancellationToken);

            if (discount is null) return ServiceResult<GetDiscountByCodeQueryResponse>.Error("Discount not found.", HttpStatusCode.NotFound);

            if (discount.Expired < DateTime.Now) return ServiceResult<GetDiscountByCodeQueryResponse>.Error("Discount expired.", HttpStatusCode.BadRequest);

            return ServiceResult<GetDiscountByCodeQueryResponse>.SuccessAsOk(new GetDiscountByCodeQueryResponse(discount.Code, discount.Rate));
        }
    }
}

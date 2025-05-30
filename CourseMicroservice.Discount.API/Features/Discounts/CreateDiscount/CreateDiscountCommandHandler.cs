using CourseMicroservice.Discount.API.Repositories;

namespace CourseMicroservice.Discount.API.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandHandler(AppDbContext context) : IRequestHandler<CreateDiscountCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var hasCodeForUser = await context.Discounts.AnyAsync(x => x.Code == request.Code && x.UserId == request.UserId, cancellationToken);

            if (hasCodeForUser)
            {
                return ServiceResult.Error("Discount code already exists for this user.", "Please use a different code.", HttpStatusCode.BadRequest);
            }

            var discount = new DiscountEntity
            {
                Id = NewId.NextSequentialGuid(),
                Code = request.Code,
                Rate = request.Rate,
                UserId = request.UserId,
                Expired = request.Expired,
                Created = DateTime.Now,
            };

            await context.Discounts.AddAsync(discount, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}

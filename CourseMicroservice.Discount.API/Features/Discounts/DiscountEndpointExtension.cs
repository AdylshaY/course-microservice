using Asp.Versioning.Builder;
using CourseMicroservice.Discount.API.Features.Discounts.CreateDiscount;
using CourseMicroservice.Discount.API.Features.Discounts.GetDiscountByCode;

namespace CourseMicroservice.Discount.API.Features.Discounts
{
    public static class DiscountEndpointExtension
    {
        public static void AddDiscountGroupEndpointExtension(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/discounts")
                .WithTags("discounts")
                .WithApiVersionSet(apiVersionSet)
                .CreateDiscountGroupItemEndpoint()
                .GetDiscountByCodeGroupItemEndpoint();
        }
    }
}

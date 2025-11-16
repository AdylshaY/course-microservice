using CourseMicroservice.Web.Pages.Order.Dtos;
using Refit;

namespace CourseMicroservice.Web.Services.Refit
{
    public interface IOrderRefitService
    {
        [Post("/api/v1/orders")]
        Task<ApiResponse<object>> CreateOrder(CreateOrderRequest request);

        [Get("/api/v1/orders")]
        Task<ApiResponse<List<GetOrderHistoryResponse>>> GetOrders();
    }
}

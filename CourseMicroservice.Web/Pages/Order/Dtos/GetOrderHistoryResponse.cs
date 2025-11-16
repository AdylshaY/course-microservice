using CourseMicroservice.Web.Pages.Order.ViewModels;

namespace CourseMicroservice.Web.Pages.Order.Dtos
{
    public record GetOrderHistoryResponse(DateTime Created, decimal TotalPrice, List<OrderItemViewModel> Items);
}

using CourseMicroservice.Web.PageModels;
using CourseMicroservice.Web.Pages.Order.ViewModels;
using CourseMicroservice.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseMicroservice.Web.Pages.Order
{
    [Authorize]
    public class HistoryModel(OrderService orderService) : BasePageModel
    {
        public List<OrderHistoryViewModel> OrderHistoryList { get; set; } = null!;

        public async Task<IActionResult> OnGet()
        {
            var response = await orderService.GetHistory();
            if (response.IsFail) return ErrorPage(response);
            OrderHistoryList = response.Data!;
            return Page();
        }
    }
}

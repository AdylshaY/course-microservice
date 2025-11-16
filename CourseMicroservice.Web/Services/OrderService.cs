using CourseMicroservice.Web.Extensions;
using CourseMicroservice.Web.Pages.Order.Dtos;
using CourseMicroservice.Web.Pages.Order.ViewModels;
using CourseMicroservice.Web.Services.Refit;
using System.Net;

namespace CourseMicroservice.Web.Services
{
    public class OrderService(IOrderRefitService orderService, ILogger<OrderService> logger)
    {
        public async Task<ServiceResult> CreateOrder(CreateOrderViewModel viewModel)
        {
            var address = new AddressDto(viewModel.Address.Province, viewModel.Address.District,
                viewModel.Address.Street, viewModel.Address.ZipCode, viewModel.Address.Line);

            var payment = new PaymentDto(viewModel.Payment.CardNumber, viewModel.Payment.CardHolderName,
                viewModel.Payment.ExpiryDate, viewModel.Payment.Cvv, viewModel.TotalPrice);

            var orderItems = viewModel.OrderItems.Select(x => new OrderItemDto(x.ProductId, x.ProductName, x.UnitPrice))
                .ToList();

            var createOrderRequest = new CreateOrderRequest(viewModel.DiscountRate, address, payment, orderItems);

            var response = await orderService.CreateOrder(createOrderRequest);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    return ServiceResult.FailFromProblemDetails(response.Error);

                logger.LogProblemDetails(response.Error);
                return ServiceResult.Error("An error occurred while creating the order");
            }

            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<OrderHistoryViewModel>>> GetHistory()
        {
            var response = await orderService.GetOrders();

            if (!response.IsSuccessStatusCode || response.Content is null)
            {
                logger.LogProblemDetails(response.Error);
                return ServiceResult<List<OrderHistoryViewModel>>.Error(
                    "An error occurred while getting the order history");
            }

            var orderHistoryList = new List<OrderHistoryViewModel>();

            foreach (var orderResponse in response.Content)
            {
                var newOrderHistory =
                    new OrderHistoryViewModel(orderResponse.Created.ToLongDateString(),
                        orderResponse.TotalPrice.ToString("C"));

                foreach (var orderItem in orderResponse.Items)
                    newOrderHistory.AddItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice);

                orderHistoryList.Add(newOrderHistory);
            }

            return ServiceResult<List<OrderHistoryViewModel>>.Success(orderHistoryList);
        }
    }
}

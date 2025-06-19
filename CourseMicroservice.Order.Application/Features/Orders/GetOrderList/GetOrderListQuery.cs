using CourseMicroservice.Shared;

namespace CourseMicroservice.Order.Application.Features.Orders.GetOrderList;

public record GetOrderListQuery : IRequestByServiceResult<List<GetOrderListQueryResponse>>;

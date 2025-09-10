using AutoMapper;
using CourseMicroservice.Order.Application.Contracts.Repositories;
using CourseMicroservice.Order.Application.Features.Orders.Dtos;
using CourseMicroservice.Shared;
using CourseMicroservice.Shared.Services;
using MediatR;

namespace CourseMicroservice.Order.Application.Features.Orders.GetOrderList;

public class GetOrderListQueryHandler(IIdentityService identityService, IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetOrderListQuery, ServiceResult<List<GetOrderListQueryResponse>>>
{
    public async Task<ServiceResult<List<GetOrderListQueryResponse>>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var orderList = await orderRepository.GetOrderListByUserId(identityService.UserId);

        var response = orderList.Select(x => new GetOrderListQueryResponse(x.Created, x.TotalPrice, mapper.Map<List<OrderItemDto>>(x.OrderItemList))).ToList();

        return ServiceResult<List<GetOrderListQueryResponse>>.SuccessAsOk(response);
    }
}

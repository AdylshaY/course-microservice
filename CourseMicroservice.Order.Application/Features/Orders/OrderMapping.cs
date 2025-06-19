using AutoMapper;
using CourseMicroservice.Order.Application.Features.Orders.Dtos;
using CourseMicroservice.Order.Domain.Entities;

namespace CourseMicroservice.Order.Application.Features.Orders
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}

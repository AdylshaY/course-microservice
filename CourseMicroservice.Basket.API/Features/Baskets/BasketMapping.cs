using AutoMapper;
using CourseMicroservice.Basket.API.Dto;

namespace CourseMicroservice.Basket.API.Features.Baskets
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<BasketDto, Data.Basket>().ReverseMap();
            CreateMap<BasketItemDto, Data.BasketItem>().ReverseMap();
        }
    }
}

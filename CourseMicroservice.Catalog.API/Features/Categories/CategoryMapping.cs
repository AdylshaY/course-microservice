using AutoMapper;
using CourseMicroservice.Catalog.API.Features.Categories.Dtos;

namespace CourseMicroservice.Catalog.API.Features.Categories
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}

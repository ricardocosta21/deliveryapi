using AutoMapper;
using supermarketapi.Domain.Models;
using supermarketapi.Domain.Models.Queries;
using supermarketapi.Resources;

namespace supermarketapi.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCategoryResource, Category>();

            CreateMap<SaveProductResource, Product>();

            CreateMap<ProductsQueryResource, ProductsQuery>();
        }
    }
}
using AutoMapper;
using supermarketapi.Domain.Models;
using supermarketapi.Domain.Models.Queries;
using supermarketapi.Extensions;
using supermarketapi.Resources;

namespace supermarketapi.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Category, CategoryResource>();

            CreateMap<Product, ProductResource>();

            //CreateMap<Product, ProductResource>()
            //    .ForMember(src => src.UnitOfMeasurement,
            //               opt => opt.MapFrom(src => src.UnitOfMeasurement.ToDescriptionString()));

            CreateMap<QueryResult<Product>, QueryResultResource<Product>>();
        }
    }
}
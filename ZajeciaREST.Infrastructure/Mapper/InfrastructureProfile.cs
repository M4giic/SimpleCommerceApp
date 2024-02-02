using AutoMapper;
using ZajeciaREST.Domain.Dto;
using ZajeciaREST.Infrastructure.Entity;

namespace ZajeciaREST.Infrastructure.Mapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<ProductDto, ProductEntity>().ReverseMap();

        CreateMap<CartDto, CartEntity>().ReverseMap();
    }
}

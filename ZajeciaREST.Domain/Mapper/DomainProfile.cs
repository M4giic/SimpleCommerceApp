using AutoMapper;
using ZajeciaREST.Domain.Dto;
using ZajeciaREST.Domain.Request;
using ZajeciaREST.Domain.Response;

namespace ZajeciaREST.Domain.Mapper;

public class DomainProfile : Profile
{
	public DomainProfile()
	{
		CreateMap<AddProductRequest, ProductDto>();
		CreateMap<ProductDto, ProductResponse>();

        CreateMap<CartDto, CartResponse>();

        CreateMap<CartDto, CreateCartResponse>();
		CreateMap<CreateCartRequest, CartDto>().ForMember(x=>
			x.Products, opt => opt.Ignore());
	}
}

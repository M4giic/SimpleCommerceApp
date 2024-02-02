using AutoMapper;
using ZajeciaREST.Application.Infrastructure;
using ZajeciaREST.Application.Interfaces;
using ZajeciaREST.Domain.Dto;
using ZajeciaREST.Domain.Request;
using ZajeciaREST.Domain.Response;

namespace ZajeciaREST.Application.Services;

internal class ProductService : IProdcutService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public ProductResponse AddProduct(AddProductRequest request)
    {
        var dto = _mapper.Map<ProductDto>(request);

        var response = _repository.AddProduct(dto);

        return _mapper.Map<ProductResponse>(response);
    }

    public void DeleteProduct(int Id)
    {
        _repository.DeleteProduct(Id);
    }

    public ProductResponse GetProductById(int Id)
    {
        var response = _repository.GetProductById(Id);

        return _mapper.Map<ProductResponse>(response);
    }

    public List<ProductResponse> GetProducts(int? count = null, int? offset = null)
    {

        var products = new List<ProductDto>();
        if (count == null || offset == null)
        {
            products = _repository.GetProducts();
        }
        else
        {
            products = _repository.GetProductsWithPagination((int)count, (int)offset);
        }

        return _mapper.Map<List<ProductResponse>>(products);
    }

    public ProductResponse UpdateProduct(int Id, AddProductRequest request)
    {
        var dto = _mapper.Map<ProductDto>(request);
        var response = _repository.UpdateProduct(Id, dto);

        return _mapper.Map<ProductResponse>(response);
    }
}

using System.Net;
using ZajeciaREST.Domain.Dto;
using ZajeciaREST.Domain.Request;
using ZajeciaREST.Domain.Response;

namespace ZajeciaREST.Application.Infrastructure;

public interface IProductRepository
{
    ProductDto AddProduct(ProductDto product);
    ProductDto GetProductById(int Id);
    List<ProductDto> GetProducts();
    List<ProductDto> GetProductsWithPagination(int count, int offset);
    ProductDto UpdateProduct(int Id, ProductDto product);
    void DeleteProduct(int Id);
}

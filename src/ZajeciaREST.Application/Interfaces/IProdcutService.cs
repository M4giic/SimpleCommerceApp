using ZajeciaREST.Domain.Request;
using ZajeciaREST.Domain.Response;

namespace ZajeciaREST.Application.Interfaces;

public interface IProdcutService
{
    ProductResponse AddProduct(AddProductRequest request);
    ProductResponse GetProductById(int Id);
    List<ProductResponse> GetProducts(int? count = null, int? offset = null);
    ProductResponse UpdateProduct(int Id,AddProductRequest request);
    void DeleteProduct(int Id);
}

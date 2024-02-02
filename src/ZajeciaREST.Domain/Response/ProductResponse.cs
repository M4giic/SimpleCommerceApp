using ZajeciaREST.Domain.Request;

namespace ZajeciaREST.Domain.Response;

public class ProductResponse : AddProductRequest
{
    public int Id { get; set; }
}


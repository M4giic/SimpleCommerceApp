using AutoMapper;
using ZajeciaREST.Application.Infrastructure;
using ZajeciaREST.Domain.Dto;
using ZajeciaREST.Infrastructure.Entity;

namespace ZajeciaREST.Infrastructure.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;

    public ProductRepository(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _dataContext = context;
    }


    public ProductDto AddProduct(ProductDto product)
    {
        var entity = _mapper.Map<ProductEntity>(product);
        _dataContext.Add(entity);
        _dataContext.SaveChanges();
        return _mapper.Map<ProductDto>(entity);
    }

    public void DeleteProduct(int Id)
    {
        var product = _dataContext.Products.FirstOrDefault(x => x.Id == Id);
        if(product is null)
        {
            throw new Exception("Object not found");
        }

        _dataContext.Products.Remove(product);
        _dataContext.SaveChanges();
    }

    public ProductDto GetProductById(int Id)
    {
        var product = _dataContext.Products.FirstOrDefault(x => x.Id == Id);
        if (product is null)
        {
            throw new Exception("Object not found");
        }

        return _mapper.Map<ProductDto>(product);
    }

    public List<ProductDto> GetProducts()
    {
        var products = _dataContext.Products.ToList();

        return _mapper.Map<List<ProductDto>>(products);
    }

    public List<ProductDto> GetProductsWithPagination(int count, int offset)
    {

        var products = _dataContext.Products.ToList();

        if (products.Count > 0)
        {
            var paginatedProducts = products
                .Skip(offset)
                .Take(count);

            return _mapper.Map<List<ProductDto>>(paginatedProducts);
        }

        return new List<ProductDto>();
    }

    public ProductDto UpdateProduct(int Id, ProductDto product)
    {
        var entity = _dataContext.Products.FirstOrDefault(x => x.Id == Id);
        entity.Price = product.Price;
        entity.Name = product.Name;

        _dataContext.Update(entity);
        _dataContext.SaveChanges();

        return _mapper.Map<ProductDto>(entity);
    }
}

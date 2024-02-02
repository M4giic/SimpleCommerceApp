using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZajeciaREST.Application.Infrastructure;
using ZajeciaREST.Application.Interfaces;
using ZajeciaREST.Domain.Dto;
using ZajeciaREST.Domain.Request;
using ZajeciaREST.Domain.Response;

namespace ZajeciaREST.Application.Services;
internal class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public CartService(IProductRepository productRepository, 
        ICartRepository cartRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _cartRepository = cartRepository;
        _mapper = mapper;
    }
    public CartResponse AddProductToCart(int userId, int productId)
    {
        var productToAdd = _productRepository.GetProductById(productId);
        if(productToAdd == null)
        {
            throw new Exception($"Product with id: {productId} cannot be added");
        }

        var cart = _cartRepository.AddProductToCart(userId, productToAdd);

        return _mapper.Map<CartResponse>(cart);
    }

  
    public void DeleteCartByUserId(int userId)
    {
        _cartRepository.DeleteCartByUserId(userId);
    }

    public CartResponse DeleteProductFromCart(int userId, int productId)
    {
        var productToRemove = _productRepository.GetProductById(productId);
        if (productToRemove == null)
        {
            throw new Exception($"Product with id: {productId} cannot be removed cause it doesnt exist");
        }

        var cart = _cartRepository.RemoveProductFromCart(userId, productToRemove);

        return _mapper.Map<CartResponse>(cart);
    }

    public CartResponse GetCartByUserId(int userId)
    {
        var cart = _cartRepository.GetOrCreateCartByUserId(userId);
        return _mapper.Map<CartResponse>(cart);
    }
}

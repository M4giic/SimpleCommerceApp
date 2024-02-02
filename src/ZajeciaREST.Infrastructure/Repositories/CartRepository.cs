using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZajeciaREST.Application.Infrastructure;
using ZajeciaREST.Domain.Dto;
using ZajeciaREST.Infrastructure.Entity;

namespace ZajeciaREST.Infrastructure.Repositories;
internal class CartRepository : ICartRepository
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;
    
    public CartRepository(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public CartDto AddProductToCart(int userId, ProductDto product)
    {
        var cart = GetOrCreateCart(userId);

        var productEntity = _dataContext
            .Products
            .FirstOrDefault(x => x.Id == product.Id);

        cart.Products.Add(productEntity) ;

        _dataContext.SaveChanges();

        return _mapper.Map<CartDto>(cart);
    }

    public CartDto RemoveProductFromCart(int userId, ProductDto product)
    {
        var cart = GetOrCreateCart(userId);

        var productEntity = _dataContext
            .Products
            .FirstOrDefault(x => x.Id == product.Id);

        if (productEntity == null) throw new Exception($"Product: {product.Id} {product.Name} does not exist in Cart: {cart.Name}");

        cart.Products.Remove(productEntity);

        _dataContext.SaveChanges();

        return _mapper.Map<CartDto>(cart);
    }

    public CartDto GetOrCreateCartByUserId(int userId)
    { 
        return _mapper.Map<CartDto>(GetOrCreateCart(userId));
    }

    private CartEntity GetOrCreateCart(int userId)
    {
        var cart = _dataContext.Carts.Where(x => x.UserId == userId)
            .Include(x => x.Products)
            .FirstOrDefault();
        
        if(cart != null) return cart;

        var newCart = new CartEntity()
        {
            UserId = userId,
            Name = "Cart_Of_User_" + userId,
        };

        _dataContext.Carts.Add(newCart);    
        _dataContext.SaveChanges(true);

        return newCart;

    }

    public CartDto GetCartByUserIdNoTracking(int userId)
    {
        var cart = _dataContext.Carts.Where(x => x.UserId == userId)
          .Include(x => x.Products)
          .AsNoTracking()
          .FirstOrDefault();
        
        if(cart == null) throw new Exception($"Cart for userId: {userId} does not exist");

        return _mapper.Map<CartDto>(cart);
    }

    public void DeleteCartByUserId(int userId)
    {
        var cart = _dataContext.Carts.FirstOrDefault(x => x.UserId == userId);
        if (cart == null)
        {
            throw new Exception($"Cart for UserId: {userId} does not exist");
        }

        _dataContext.Remove(cart);
        _dataContext.SaveChanges();
    }
}

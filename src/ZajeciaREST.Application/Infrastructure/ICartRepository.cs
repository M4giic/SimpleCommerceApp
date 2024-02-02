using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZajeciaREST.Domain.Dto;
using ZajeciaREST.Domain.Request;

namespace ZajeciaREST.Application.Infrastructure;
public interface ICartRepository
{
    CartDto AddProductToCart(int userId, ProductDto product);

    CartDto RemoveProductFromCart(int userId, ProductDto productId);
    CartDto GetOrCreateCartByUserId(int userId);
    CartDto GetCartByUserIdNoTracking(int userId);
    void DeleteCartByUserId(int userId);
}

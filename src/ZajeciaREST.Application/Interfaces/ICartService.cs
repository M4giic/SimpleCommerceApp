using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZajeciaREST.Domain.Dto;
using ZajeciaREST.Domain.Request;
using ZajeciaREST.Domain.Response;

namespace ZajeciaREST.Application.Interfaces;
public interface ICartService
{
    CartResponse AddProductToCart(int userId, int productId);
    CartResponse DeleteProductFromCart(int userId, int productId);
    CartResponse GetCartByUserId(int userId);
    void DeleteCartByUserId(int userId);
}

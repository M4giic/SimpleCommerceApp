using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ZajeciaREST.Application.Interfaces;
using ZajeciaREST.Domain.Request;
using System.Security.Claims;
using Microsoft.AspNetCore.Routing;

namespace ZajeciaREST.WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }


    [HttpPut("products")]
    public IActionResult AddProductToCart(ProductCartRequest product)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (userId == null)
            {
                return Unauthorized("UserId cannot be null");
            }
            var cart = _cartService.AddProductToCart(int.Parse(userId), product.ProductId);
          
            return Ok(cart);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }


    [HttpDelete("products")]
    public IActionResult RemoveProductFromCart(ProductCartRequest product)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (userId == null)
            {
                return Unauthorized("UserId cannot be null");
            }
            var cart = _cartService.AddProductToCart(int.Parse(userId), product.ProductId);
            
            return Ok(cart);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [HttpGet("")]
    public IActionResult GetCartByUserId()
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var cart = _cartService.GetCartByUserId(int.Parse(userId));
            if (userId == null)
            {
                return Unauthorized("UserId cannot be null");
            }
            return Ok(cart);
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }

    [HttpDelete("")]
    public IActionResult DeleteCartById()
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if(userId == null)
            {
                return Unauthorized("UserId cannot be null");
            }
            _cartService.DeleteCartByUserId(int.Parse(userId));

            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ZajeciaREST.Application.Interfaces;
using ZajeciaREST.Domain.Request;
using System.Security.Claims;

namespace ZajeciaREST.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProdcutService _productService;

    public ProductController(IProdcutService prodcutService)
    {
        _productService = prodcutService;
    }

    [HttpPost]
    [Authorize]
    public IActionResult AddProduct(AddProductRequest request)
    {
        var response = _productService.AddProduct(request);
        return Ok(response);
    }

    [HttpGet]
    public IActionResult GetProducts([FromQuery]int? count,[FromQuery] int? offset)
    {
        var response = _productService.GetProducts(count, offset);

        if(response.Any() == false)
        {
            return NotFound();
        }

        return Ok(response);
    }


    [HttpGet("{Id:int}")]
    public IActionResult GetProductById(int Id)
    {
        try
        {
            var response = _productService.GetProductById(Id);

            if (response is null)
            {
                return NotFound();
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpPut("{Id:int}")]
    [Authorize]
    public IActionResult UpdateProductById(int Id, AddProductRequest request)
    {
        var response = _productService.UpdateProduct(Id, request);

        if (response is null)
        {
            return NotFound();
        }

        return Ok(response);
    }


    [HttpDelete("{Id:int}")]
    [Authorize]
    public IActionResult DeleteProducyById(int Id)
    {
        try
        {
            _productService.DeleteProduct(Id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



}


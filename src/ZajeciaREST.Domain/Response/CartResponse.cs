using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZajeciaREST.Domain.Response;
public class CartResponse : CreateCartResponse
{
    public List<ProductResponse> Products { get; set; }
}

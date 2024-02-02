using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZajeciaREST.Infrastructure.Entity;
internal class CartEntity
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public List<ProductEntity> Products { get; } = new();
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZajeciaREST.Infrastructure.Entity;
internal class CartEntityProductEntity
{
    public CartEntity Cart { get; set; }
    public ProductEntity Product { get; set; }
    public int Count { get; set; }
}

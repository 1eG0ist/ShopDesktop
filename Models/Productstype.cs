using System;
using System.Collections.Generic;

namespace ShopDesktop.Models;

public partial class Productstype
{
    public int TypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }
}

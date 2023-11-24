using System;
using System.Collections.Generic;

namespace ShopDesktop.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int? ProductType { get; set; }

    public float? ProductPrice { get; set; }

    public byte[]? ProductImg { get; set; }

    public string? ProductDescription { get; set; }

    public int? ProductCount { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}

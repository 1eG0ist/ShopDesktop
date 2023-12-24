using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopDesktop.Models;

public partial class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int? ProductType { get; set; }

    public float? ProductPrice { get; set; }

    public string? ProductDescription { get; set; }

    public int? ProductCount { get; set; }

    public int? ProductAuthor { get; set; }

    public virtual User? ProductAuthorNavigation { get; set; }

    public virtual Productstype? ProductTypeNavigation { get; set; }
}

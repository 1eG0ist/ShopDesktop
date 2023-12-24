using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopDesktop.Models;

public partial class ProductImage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProdImgId { get; set; }

    public int? ProductId { get; set; }

    public byte[]? ProductImg { get; set; }

    public virtual Product? Product { get; set; }
}

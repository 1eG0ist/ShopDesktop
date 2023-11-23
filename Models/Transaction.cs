using System;
using System.Collections.Generic;

namespace ShopDesktop.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public DateTime? TransactionData { get; set; }

    public int? TransactionProductCount { get; set; }

    public virtual Product? Product { get; set; }
}

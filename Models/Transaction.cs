﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopDesktop.Models;

public partial class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionId { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public DateTime? TransactionData { get; set; }

    public int? TransactionProductCount { get; set; }

    public virtual Product? Product { get; set; }
}

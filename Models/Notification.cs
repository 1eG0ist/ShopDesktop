using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopDesktop.Models;

public partial class Notification
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NotificationId { get; set; }

    public int? UserId { get; set; }

    public string? NotificationText { get; set; }

    public int? NotificationAuthor { get; set; }

    public DateTime? NotificationData { get; set; }

    public bool? IsNew { get; set; }
}

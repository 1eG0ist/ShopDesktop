using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopDesktop.Models;

public partial class UserRole
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserRoleId { get; set; }

    public int? UserId { get; set; }

    public int? RoleId { get; set; }

    public virtual User? Role { get; set; }

    public virtual User? User { get; set; }
}

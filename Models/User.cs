using System;
using System.Collections.Generic;

namespace ShopDesktop.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public int? Age { get; set; }

    public byte[]? Photo { get; set; }

    public int? UserRole { get; set; }

    public virtual Role? UserRoleNavigation { get; set; }

    public User(int userId, string userName, string userPassword, string userEmail, int? age, byte[]? photo, int? userRole)
    {
        UserId = userId;
        UserName = userName;
        UserPassword = userPassword;
        UserEmail = userEmail;
        Age = age;
        Photo = photo;
        UserRole = userRole;
    }
}

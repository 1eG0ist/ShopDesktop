using ReactiveUI;
using ShopDesktop.Models;

namespace Shop.DBModels;

public class DBUser: User
{
    public DBUser(int userId, string userName, string userPassword, string userEmail, int? age, byte[]? photo, int? userRole)
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
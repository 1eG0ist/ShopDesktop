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

    public DBUser(User autoCreatingUser)
    {
        UserId = autoCreatingUser.UserId;
        UserName = autoCreatingUser.UserName;
        UserPassword = autoCreatingUser.UserPassword;
        UserEmail = autoCreatingUser.UserEmail;
        Age = autoCreatingUser.Age;
        Photo = autoCreatingUser.Photo;
        UserRole = autoCreatingUser.UserRole;
    }
}
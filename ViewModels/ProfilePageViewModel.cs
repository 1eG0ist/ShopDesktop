using ShopDesktop.ViewModels;

namespace ShopDesktop.ViewModels;

public class ProfilePageViewModel : ViewModelBase
{
    public string UserNickName => SessionData.registeredUser.UserName;
    public string UserEmail => SessionData.registeredUser.UserEmail;
    public string UserAge => SessionData.registeredUser.Age == null ? "No info" : SessionData.registeredUser.Age.ToString();

    public string UserRoles => SessionData.registeredUser.UserRole == 1 ? "User" :
        SessionData.registeredUser.UserRole == 2 ? "User | Seller" : "User | Seller | Admin";

    public string UserPassword => new string('·', SessionData.registeredUser.UserPassword.Length);
    
}
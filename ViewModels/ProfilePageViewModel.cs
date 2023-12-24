using System;
using ShopDesktop.ViewModels;

namespace ShopDesktop.ViewModels;

public class ProfilePageViewModel : ViewModelBase
{
    public string UserNickName => SessionData.registeredUser.UserName;
    public string UserEmail => SessionData.registeredUser.UserEmail;
    public string UserAge => SessionData.registeredUser.Birthdate == null ? null : SessionData.registeredUser.Birthdate.ToString();

    public string UserRoles => String.Join(" | ", SessionData.userRoles);

    public string UserPassword => new string('·', SessionData.registeredUser.UserPassword.Length);
    
}
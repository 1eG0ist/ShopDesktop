using System.Data.SqlTypes;
using System.Drawing;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using ShopDesktop.DBModels;
using ShopDesktop.ViewModels;
using Image = Avalonia.Controls.Image;

namespace ShopDesktop.Views;

public partial class ShopWindow : Window
{
    private DBUser _user;
    private bool _isPaneOpen = true;
    
    public ShopWindow()
    {
        InitializeComponent();
        if (SessionData.registeredUser != null)
        {
            _user = new DBUser(SessionData.registeredUser);
            UserName.Text = _user.UserName;
            SwapToAdminPanelBtn.IsVisible = _user.UserRole > 2; 
            SwapToSellerPanelBtn.IsVisible = _user.UserRole > 1;
            PutUserProfilePhoto();
        }
    }
    
    
    public void PutUserProfilePhoto()
    {
        Image UserProfilePhoto = ConnectionBD.GetUserProfilePhoto();
        if (UserProfilePhoto != null) ProfileImage.Source = UserProfilePhoto.Source;
    }
    
    private void ChangePaneVisibilityBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        ShopSplitView.IsPaneOpen = !ShopSplitView.IsPaneOpen;
        OnPaneModeChange();
    }

    private void OnPaneModeChange()
    {
        ProfileImage.IsVisible = !ProfileImage.IsVisible;
        UserName.IsVisible = !UserName.IsVisible;
        if (_isPaneOpen)
        {
            HomeItem.Content = new PathIcon
            {
                Data = this.FindResource("HomeRegular") as Geometry
            };
            ProfileItem.Content = new PathIcon
            {
                Data = this.FindResource("PersonRegular") as Geometry
            };
            HistoryItem.Content = new PathIcon
            {
                Data = this.FindResource("DocumentOnePageRegular") as Geometry
            };
            SettingsItem.Content = new PathIcon
            {
                Data = this.FindResource("SettingsRegular") as Geometry
            };
            LogOutItem.Content = new PathIcon
            {
                Data = this.FindResource("SignOutRegular") as Geometry
            };
            SwapToAdminPanelBtn.Content = new PathIcon
            {
                Data = this.FindResource("AdminPersonRegular") as Geometry
            };
            SwapToSellerPanelBtn.Content = new PathIcon
            {
                Data = this.FindResource("SellerShopRegular") as Geometry
            };
        }
        else
        {
            HomeItem.Content = "Home";
            ProfileItem.Content = "Profile";
            HistoryItem.Content = "Buy history";
            SettingsItem.Content = "Settings";
            LogOutItem.Content = "Log out";
            SwapToAdminPanelBtn.Content = "Admin Panel";
            SwapToSellerPanelBtn.Content = "Seller Panel";
        }
        _isPaneOpen = !_isPaneOpen;
    }

    private void LogOutItem_OnTapped(object? sender, TappedEventArgs e)
    {
        SessionData.registeredUser = null;
        new LogInWindow().Show();
        Close();
    }
}
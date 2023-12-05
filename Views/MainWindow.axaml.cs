using Avalonia.Controls;
using Avalonia.Interactivity;
using Shop;
using static Npgsql.NpgsqlDataSource;
using ShopDesktop.DBModels;
using ShopDesktop.ViewModels;

namespace ShopDesktop.Views;

public partial class MainWindow : Window
{
    private DBUser _user = new DBUser(SessionData.registeredUser);
    public MainWindow()
    {
        InitializeComponent();
        UserName.Text = _user.UserName;
        if (_user.UserRole != 3) SwapToAdminPanel.IsVisible = false;
        if (_user.UserRole == 1) SwapToSellerPanel.IsVisible = false;
        PutUserProfilePhoto();
    }

    private void ConfirmFilters_OnClick(object? sender, RoutedEventArgs e)
    {
        
    }
    
    public void PutUserProfilePhoto()
    {
        Image UserProfilePhoto = ConnectionBD.GetUserProfilePhoto();
        if (UserProfilePhoto != null) ProfileImage.Source = UserProfilePhoto.Source;
    }

    private void ProfileBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        ProfileWindow profileWindow = new ProfileWindow();
        profileWindow.DataContext = new ProfileWindowViewModel();
        profileWindow.Show();
        Close();
    }

    private void SettingsBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void BuyHistoryBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void OpenProfileMenuBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        ProfileMenu.IsVisible = !ProfileMenu.IsVisible;
    }
}
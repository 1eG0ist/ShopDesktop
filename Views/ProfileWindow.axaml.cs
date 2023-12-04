using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace ShopDesktop.Views;

public partial class ProfileWindow : Window
{
    public ProfileWindow()
    {
        InitializeComponent();
        PutUserProfilePhoto();
    }

    public void PutUserProfilePhoto()
    {
        Image UserProfilePhoto = ConnectionBD.GetUserProfilePhoto();
        if (UserProfilePhoto != null) ProfileImage.Source = UserProfilePhoto.Source;
    }

    private void GoBackBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Close();
    }

    async private void ChangeProfilePhoto_OnClick(object? sender, RoutedEventArgs e)
    {
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Title = "Choose new photo";
        var result = await dialog.ShowAsync(this);
        if (result != null && result.Length != 0)
        {
            // Выбранный файл доступен по пути result
            bool res = ConnectionBD.ChangeProfilePhoto(result[0]); ;

            if (res)
            {
                PutUserProfilePhoto(); // TODO make changes to the photo on the fly
            }
        }
    }
}
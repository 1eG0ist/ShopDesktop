using System;
using System.Runtime.InteropServices.ComTypes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace ShopDesktop.Views;

public partial class ProfileWindow : Window
{
    private SolidColorBrush _errorBorder = new SolidColorBrush(Colors.Red);
    private Thickness _errorThickness = new Thickness(2);
    private SolidColorBrush _rightBorder = new SolidColorBrush(Colors.Indigo);
    private Thickness _rightThickness = new Thickness(1);
    
    public ProfileWindow()
    {
        InitializeComponent();
        PutUserProfilePhoto();
        ConfirmPassword.IsVisible = false;
        OldPassword.IsVisible = false;
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
            // the selected file is available in the result path
            bool res = ConnectionBD.ChangeProfilePhoto(result[0]); ;

            if (res)
            {
                PutUserProfilePhoto(); // TODO make changes to the photo on the fly
            }
        }
    }

    private void ChangePassword_OnClick(object? sender, RoutedEventArgs e)
    {
        string pass = Password.Text;
        string confirmPass = ConfirmPassword.Text;
        string oldPass = OldPassword.Text;
        if ( String.IsNullOrEmpty(pass) || pass.Length < 6 )
        {
            Password.BorderBrush = _errorBorder;
            Password.BorderThickness = _errorThickness;
        } else if (pass != confirmPass)
        {
            Password.BorderBrush = _errorBorder;
            Password.BorderThickness = _errorThickness;
            ConfirmPassword.BorderBrush = _errorBorder;
            ConfirmPassword.BorderThickness = _errorThickness;
        } else if (oldPass != SessionData.registeredUser.UserPassword)
        {
            OldPassword.BorderBrush = _errorBorder;
            OldPassword.BorderThickness = _errorThickness;
        } else
        {
            Password.BorderBrush = _rightBorder;
            Password.BorderThickness = _rightThickness;
            OldPassword.BorderBrush = _rightBorder;
            OldPassword.BorderThickness = _rightThickness;
            string? answer = ConnectionBD.UpdateUserPassword(pass);
            if (answer == null)
            {
                GoodMsg.Text = "All changes saved";
                ShowGoodArea.IsVisible = true;
                ShowErrorArea.IsVisible = false;
                ConfirmPassword.IsVisible = false;
                OldPassword.IsVisible = false;
            } else
            {
                ShowGoodArea.IsVisible = false;
                ErrorMsg.Text = "Something went wrong";
                ShowErrorArea.IsVisible = true;
            }
        }
    }

    private void ChangeAge_OnClick(object? sender, RoutedEventArgs e)
    {
        string text = Age.Text;
        int num;
        if ( String.IsNullOrEmpty(text) || !int.TryParse(text, out num))
        {
            Age.BorderBrush = _errorBorder;
            Age.BorderThickness = _errorThickness;
        }
        else
        {
            Age.BorderBrush = _rightBorder;
            Age.BorderThickness = _rightThickness;
            string? answer = ConnectionBD.UpdateUserAge(int.Parse(text));

            if (answer == null)
            {
                GoodMsg.Text = "All changes saved";
                ShowGoodArea.IsVisible = true;
                ShowErrorArea.IsVisible = false;
            } else
            {
                ShowGoodArea.IsVisible = false;
                ErrorMsg.Text = "Something went wrong";
                ShowErrorArea.IsVisible = true;
            }
        }
    }

    private void ChangeNickName_OnClick(object? sender, RoutedEventArgs e)
    {
        string text = NickName.Text;
        if ( String.IsNullOrEmpty(text) || text.Length <= 3)
        {
            NickName.BorderBrush = _errorBorder;
            NickName.BorderThickness = _errorThickness;
        }
        else
        {
            NickName.BorderBrush = _rightBorder;
            NickName.BorderThickness = _rightThickness;
            string? answer = ConnectionBD.UpdateUserName(text);

            if (answer == null)
            {
                GoodMsg.Text = "All changes saved";
                ShowGoodArea.IsVisible = true;
                ShowErrorArea.IsVisible = false;
            } else
            {
                if (answer.Contains("23505"))
                {
                    if (answer.Contains("users_user_name_key"))
                    {
                        ShowGoodArea.IsVisible = false;
                        ErrorMsg.Text = "User with with nickname already exists";
                        ShowErrorArea.IsVisible = true;
                    }
                }
            }
        }
    }

    private void LogOutBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        SessionData.registeredUser = null;
        new LogInWindow().Show();
        Close();
    }

    private void Password_TextChanged(object? sender, TextChangedEventArgs e)
    {
        if (Password.Text.Length > 5 && Password.Text != SessionData.registeredUser.UserPassword)
        {
            ConfirmPassword.IsVisible = true;
            OldPassword.IsVisible = true;
        }
        else
        {
            ConfirmPassword.IsVisible = false;
            OldPassword.IsVisible = false;
        }
    }
}
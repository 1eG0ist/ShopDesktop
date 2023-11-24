using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Npgsql;
using Shop.Exceptions;
using ShopDesktop.Models;

namespace Shop;

public partial class LogInWindow : Window
{
    private SolidColorBrush _errorBorder = new SolidColorBrush(Colors.Red);
    private Thickness _errorThickness = new Thickness(5);
    private Thickness _rightThickness = new Thickness(0);
    
    private Dictionary<string, bool> _signInState = new Dictionary<string, bool>()
    {
        ["Email"] = false,
        ["Password"] = false
    };
    
    private Dictionary<string, bool> _signUpState = new Dictionary<string, bool>()
    {
        ["UserName"] = false,
        ["Email"] = false,
        ["Password"] = false,
        ["ConfirmPassword"] = false
    };
    
    public LogInWindow()
    {
        InitializeComponent();
    }

    private void ChangeOnSignUp_OnClick(object? sender, RoutedEventArgs e)
    {
        SignInArea.IsVisible = false;
        SignUpArea.IsVisible = true;
    }

    private void ChangeOnSignIn_OnClick(object? sender, RoutedEventArgs e)
    {
        SignUpArea.IsVisible = false;
        SignInArea.IsVisible = true;
    }
    
    private void SignInEmail_OnLostFocus(object? sender, RoutedEventArgs e)
    {
        string text = SignInEmail.Text;
        if ( String.IsNullOrEmpty(text) || text.Length <= 5 ||
             text.Count(x => x == '@') == 0 || text.Split('@')[1].Count(x => x == '.') != 1 )
        {
            SignInEmail.BorderBrush = _errorBorder;
            SignInEmail.BorderThickness = _errorThickness;
            _signInState["Email"] = false;
        }
        else
        {
            SignInEmail.BorderBrush = null;
            SignInEmail.BorderThickness = _rightThickness;
            _signInState["Email"] = true;
        }
    }

    private void SignInPassword_OnLostFocus(object? sender, RoutedEventArgs e)
    {
        string text = SignInPassword.Text;
        if ( String.IsNullOrEmpty(text) || text.Length <= 5)
        {
            SignInPassword.BorderBrush = _errorBorder;
            SignInPassword.BorderThickness = _errorThickness;
            _signInState["Password"] = false;
        }
        else
        {
            SignInPassword.BorderBrush = null;
            SignInPassword.BorderThickness = _rightThickness;
            _signInState["Password"] = true;
        }
    }

    private void SignUpUserName_OnLostFocus(object? sender, RoutedEventArgs e)
    {
        string text = SignUpUserName.Text;
        if ( String.IsNullOrEmpty(text) || text.Length <= 3)
        {
            SignUpUserName.BorderBrush = _errorBorder;
            SignUpUserName.BorderThickness = _errorThickness;
            _signUpState["UserName"] = false;
        }
        else
        {
            SignUpUserName.BorderBrush = null;
            SignUpUserName.BorderThickness = _rightThickness;
            _signUpState["UserName"] = true;
        }
    }

    private void SignUpEmail_OnLostFocus(object? sender, RoutedEventArgs e)
    {
        string text = SignUpEmail.Text;
        if ( String.IsNullOrEmpty(text) || text.Length <= 5 ||
             text.Count(x => x == '@') == 0 || text.Split('@')[1].Count(x => x == '.') != 1 )
        {
            SignUpEmail.BorderBrush = _errorBorder;
            SignUpEmail.BorderThickness = _errorThickness;
            _signUpState["Email"] = false;
        }
        else
        {
            SignUpEmail.BorderBrush = null;
            SignUpEmail.BorderThickness = _rightThickness;
            _signUpState["Email"] = true;
        }
    }

    private void SignUpPassword_OnLostFocus(object? sender, RoutedEventArgs e)
    {
        string text = SignUpPassword.Text;
        if ( String.IsNullOrEmpty(text) || text.Length <= 5)
        {
            SignUpPassword.BorderBrush = _errorBorder;
            SignUpPassword.BorderThickness = _errorThickness;
            _signUpState["Password"] = false;
        }
        else
        {
            SignUpPassword.BorderBrush = null;
            SignUpPassword.BorderThickness = _rightThickness;
            _signUpState["Password"] = true;
        }
    }

    private void SignUpConfirmPassword_OnLostFocus(object? sender, RoutedEventArgs e)
    {
        string text = SignUpConfirmPassword.Text;
        if ( String.IsNullOrEmpty(text) || text.Length <= 5 || text != SignUpPassword.Text )
        {
            SignUpConfirmPassword.BorderBrush = _errorBorder;
            SignUpConfirmPassword.BorderThickness = _errorThickness;
            _signUpState["ConfirmPassword"] = false;
        }
        else
        {
            SignUpConfirmPassword.BorderBrush = null;
            SignUpConfirmPassword.BorderThickness = _rightThickness;
            _signUpState["ConfirmPassword"] = true;
        }
    }

    private void AccessSignIn_OnClick(object? sender, RoutedEventArgs e)
    {
        if ( _signInState.Values.Any(b => b == false) )
        {
            SetErrorMessage("Some of filed have wrong filling");
        }
        else
        {
            User takenUser = ConnectionBD.GetUser(SignInEmail.Text);
                
            if (takenUser == null)
            {
                SetErrorMessage("user with this email not found");
            } else if (takenUser.UserPassword == SignInPassword.Text)
            {
                new MainWindow().Show();
                Close();
            }
            else
            {
                SetErrorMessage("Wrong password");
            }
        }
    }

    private void AccessSignUp_OnClick(object? sender, RoutedEventArgs e)
    {
        if ( _signUpState.Values.Any(b => b == false) )
        {
            SetErrorMessage("Some of filed have wrong filling");
        } else
        {
            string? answer = ConnectionBD.CreateUser(SignUpUserName.Text, SignUpEmail.Text, SignUpConfirmPassword.Text);
            if (answer == null)
            {
                new MainWindow().Show();
                Close();
            }
            else
            {
                if (answer.Contains("23505"))
                {
                    if (answer.Contains("users_user_name_key"))
                    {
                        SetErrorMessage("This nick is already exists");
                    } else if (answer.Contains("users_user_email_key"))
                    {
                        SetErrorMessage("This email is already exists");
                    }
                }
            }
        }
    }

    private void SetErrorMessage(string mess)
    {
        ErrorMessageField.Text = mess;
        ErrorArea.IsVisible = true;
    }
}
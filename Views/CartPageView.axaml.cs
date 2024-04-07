using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ShopDesktop.Models;
using ShopDesktop.ViewModels;

namespace ShopDesktop.Views;

public partial class CartPageView : UserControl
{
    public CartPageView()
    {
        InitializeComponent();
        DataContext = new CartPageViewModel();
    }

    private void RemoveFromCartCommand_Button(object? sender, RoutedEventArgs e)
    {
        var viewModel = new CartPageViewModel();
        if (sender is Button button && button.DataContext is Product product)
        {
            viewModel.RemoveFromCartCommand.Execute(product);
        }
    }

    private void BuyProducts_OnClick(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Both all products!");
        var viewModel = new CartPageViewModel();
        if (sender is Button button && button.DataContext is Product product)
        {
            viewModel.RemoveFromCartCommand.Execute(product);
        }
    }
}
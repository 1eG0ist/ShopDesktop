using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ShopDesktop.Models;
using ShopDesktop.ViewModels;

namespace ShopDesktop.Views;

public partial class HomePageView : UserControl
{
    public HomePageView()
    {
        InitializeComponent();
    }
    
    private void ConfirmFilters_OnClick(object? sender, RoutedEventArgs e)
    {
        
    }

    private void AddProductToCartButton_Click(object? sender, RoutedEventArgs e)
    {
        Product? product = (sender as Button)?.DataContext as Product;
    
        if (product != null)
        {
            Console.WriteLine(product.ProductName, product.ProductId);
            SessionData.cartProducts.Add(product);
        }
    }
}
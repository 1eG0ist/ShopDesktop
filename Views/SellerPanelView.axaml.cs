using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using ShopDesktop.Models;

namespace ShopDesktop.ViewModels;

public partial class SellerPanelView : Window
{
    public SellerPanelView()
    {
        InitializeComponent();
    }

    private void AddProduct_OnClick(object? sender, RoutedEventArgs e)
    {
        // Создаем новый объект Product на основе данных из текстовых полей и выпадающего списка
        SellerPanelViewModel viewModel = (SellerPanelViewModel)DataContext;
        Product newProduct = new Product
        {
            ProductName = ProductName.Text,
            ProductType = viewModel.SelectedType.TypeId,
            ProductPrice = float.Parse(ProductPrice.Text),
            ProductDescription = ProductDescription.Text,
            ProductCount = int.Parse(ProductCount.Text),
            ProductAuthor = SessionData.registeredUser.UserId
        };

        Product createdProduct = ConnectionBD.CreateNewProduct(newProduct);
        if (viewModel.ProductPhoto != null)
        {
            ConnectionBD.AddNewProductImage(createdProduct.ProductId, viewModel.ProductPhoto);
        }
        viewModel.IsAddProductBoxVisible = !viewModel.IsAddProductBoxVisible;
        viewModel.AllUserProducts.Add(createdProduct);
    }

    async private void SetPhotoButton_OnClick(object? sender, RoutedEventArgs e)
    {
        SellerPanelViewModel viewModel = (SellerPanelViewModel)DataContext;
        OpenFileDialog dialog = new OpenFileDialog();
        var result = await dialog.ShowAsync(new SellerPanelView());
        if (result != null && result.Length != 0)
        {
            var image = new Image();
            using (var stream = new MemoryStream(File.ReadAllBytes(result[0])))
            {
                var bitmap = new Bitmap(stream);
                image.Source = bitmap;
            }

            CreatingProductPhoto.Source = image.Source;
            
            viewModel.ProductPhoto = File.ReadAllBytes(result[0]);
        }
    }
    
    private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
    {
        
        var product = (sender as Button)?.DataContext as Product;
    
        if (product != null)
        {
            SellerPanelViewModel viewModel = (SellerPanelViewModel)DataContext;
            ConnectionBD.DeleteProduct(product);
            viewModel.AllUserProducts = ConnectionBD.GetSellerProducts(SessionData.registeredUser.UserId);
        }
        
    }

}
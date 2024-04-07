using System.Collections.ObjectModel;
using System.Reactive;
using System.Windows.Input;
using ReactiveUI;
using ShopDesktop.Models;

namespace ShopDesktop.ViewModels;

public class CartPageViewModel : ViewModelBase
{
    private ObservableCollection<Product> _cartProducts;
    public ICommand RemoveFromCartCommand { get; }

    public ObservableCollection<Product> CartProducts
    {
        get => _cartProducts;
        set => this.RaiseAndSetIfChanged(ref _cartProducts, value);
    }

    public CartPageViewModel()
    {
        var productsFromCart = SessionData.cartProducts;

        CartProducts = new ObservableCollection<Product>(productsFromCart);
        
        RemoveFromCartCommand = ReactiveCommand.Create<Product>(RemoveProductFromCart);
    }
    
    private void RemoveProductFromCart(Product product)
    {
        SessionData.cartProducts.Remove(product);
        CartProducts.Remove(product);
    }
}
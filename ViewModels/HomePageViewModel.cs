using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using ShopDesktop.Models;

namespace ShopDesktop.ViewModels;

public class HomePageViewModel : ViewModelBase
{
    private List<Product> _products;
    private int _currentPage = 1;
    private int _itemsPerPage = 20;
    private string _filterText;
    private string _fromPrice;
    private string _toPrice;
    private ObservableCollection<Product> _filteredProducts;

    public List<Product> Products
    {
        get => _products;
        set => this.RaiseAndSetIfChanged(ref _products, value);
    }

    public string FilterText
    {
        get => _filterText;
        set => this.RaiseAndSetIfChanged(ref _filterText, value);
    }
    
    public string FromPrice
    {
        get => _fromPrice;
        set => this.RaiseAndSetIfChanged(ref _fromPrice, value);
    }
    
    public string ToPrice
    {
        get => _toPrice;
        set => this.RaiseAndSetIfChanged(ref _toPrice, value);
    }

    public ObservableCollection<Product> FilteredProducts
    {
        get => _filteredProducts;
        set => this.RaiseAndSetIfChanged(ref _filteredProducts, value);
    }

    public ReactiveCommand<Unit, Unit> NextPageCommand { get; }
    public ReactiveCommand<Unit, Unit> PreviousPageCommand { get; }
    
    public HomePageViewModel()
    {
        Products = ConnectionBD.GetAllProducts();
        
        FilteredProducts = new ObservableCollection<Product>(Products);
        
        // Фильтрация списка товаров при изменении текста в поле фильтрации
        this.WhenAnyValue(x => x.FilterText, x => x.FromPrice, x => x.ToPrice)
            .Throttle(TimeSpan.FromMilliseconds(500))
            .Select(values => FilterProducts(values.Item1))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(filteredProducts => 
            {
                FilteredProducts = new ObservableCollection<Product>(filteredProducts);
                // _currentPage = 1; // Сброс текущей страницы при изменении фильтра
                // LoadPage();
            });
        

        // NextPageCommand = ReactiveCommand.Create(NextPage);
        // PreviousPageCommand = ReactiveCommand.Create(PreviousPage);

    }
    
    private IEnumerable<Product> FilterProducts(string filterText)
    {
        var filteredList = _products.Where(product =>
            string.IsNullOrWhiteSpace(filterText) || product.ProductName.Contains(filterText, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(FromPrice) && double.TryParse(FromPrice, out var fromPrice))
        {
            filteredList = filteredList.Where(product => product.ProductPrice >= fromPrice);
        }

        if (!string.IsNullOrWhiteSpace(ToPrice) && double.TryParse(ToPrice, out var toPrice))
        {
            filteredList = filteredList.Where(product => product.ProductPrice <= toPrice);
        }

        return filteredList;
    }



    // private void LoadPage()
    // {
    //     // Рассчитываем индекс начала и конца текущей страницы для отфильтрованных продуктов
    //     int startIndex = (_currentPage - 1) * _itemsPerPage;
    //     int endIndex = Math.Min(startIndex + _itemsPerPage, FilteredProducts.Count);
    //
    //     // Выбираем продукты для текущей страницы
    //     var currentPageProducts = FilteredProducts.Skip(startIndex).Take(endIndex - startIndex);
    //
    //     Products = new List<Product>(currentPageProducts);
    // }

    // private void NextPage()
    // {
    //     _currentPage++;
    //     LoadPage();
    // }
    //
    // private bool CanNavigateNext()
    // {
    //     return (_currentPage * _itemsPerPage) < FilteredProducts.Count;
    // }
    //
    // private void PreviousPage()
    // {
    //     _currentPage--;
    //     LoadPage();
    // }
    //
    // private bool CanNavigatePrevious()
    // {
    //     // Возвращает true, если текущая страница не является первой
    //     return _currentPage > 1;
    // }
}

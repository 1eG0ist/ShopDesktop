using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using ShopDesktop.Models;

namespace ShopDesktop.ViewModels
{
    public class SellerPanelViewModel : ViewModelBase
    {
        private bool _isAddProductBoxVisible;
        public bool IsAddProductBoxVisible
        {
            get => _isAddProductBoxVisible;
            set => this.RaiseAndSetIfChanged(ref _isAddProductBoxVisible, value);
        }
        
        private byte[] _productPhoto;
        public byte[] ProductPhoto
        {
            get => _productPhoto;
            set
            {
                _productPhoto = value;
                this.RaisePropertyChanged(nameof(ProductPhoto));
            }
        }

        private string _filterText;
        public string FilterText
        {
            get => _filterText;
            set => this.RaiseAndSetIfChanged(ref _filterText, value);
        }

        private ObservableCollection<Product> _userProducts;
        private List<Product> _allUserProducts;
        
        public List<Product> AllUserProducts
        {
            get => _allUserProducts;
            set => this.RaiseAndSetIfChanged(ref _allUserProducts, value);
        }

        public ObservableCollection<Product> UserProducts
        {
            get => _userProducts;
            set => this.RaiseAndSetIfChanged(ref _userProducts, value);
        }
        
        private ObservableCollection<Productstype> _productTypes;
        public ObservableCollection<Productstype> ProductTypes
        {
            get => _productTypes;
            set => this.RaiseAndSetIfChanged(ref _productTypes, value);
        }

        private Productstype _selectedType;
        public Productstype SelectedType
        {
            get => _selectedType;
            set => this.RaiseAndSetIfChanged(ref _selectedType, value);
        }

        public ICommand ChangeAddProductBoxVisibility { get; }

        public SellerPanelViewModel()
        {
            ChangeAddProductBoxVisibility = ReactiveCommand.Create(ChangeAddProductBoxVisibilityExecute);

            _allUserProducts = ConnectionBD.GetSellerProducts(SessionData.registeredUser.UserId);

            UserProducts = new ObservableCollection<Product>(_allUserProducts);

            ProductTypes = ConnectionBD.GetAllProductsTypes();

            // Фильтрация списка товаров по тексту из FilterText
            this.WhenAnyValue(x => x.FilterText)
                .Throttle(TimeSpan.FromMilliseconds(500))
                .Select(FilterProducts)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(filteredProducts => UserProducts = new ObservableCollection<Product>(filteredProducts));
        }

        private IEnumerable<Product> FilterProducts(string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
                return _allUserProducts;

            return _allUserProducts.Where(product => product.ProductName.Contains(filterText, StringComparison.OrdinalIgnoreCase));
        }

        private void ChangeAddProductBoxVisibilityExecute()
        {
            IsAddProductBoxVisible = !IsAddProductBoxVisible;
        }
    }
}

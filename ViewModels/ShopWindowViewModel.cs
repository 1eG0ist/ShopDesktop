using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI;

namespace ShopDesktop.ViewModels;

public class ShopWindowViewModel : ViewModelBase
{

    private ViewModelBase _currentPage = new HomePageViewModel();

    public ViewModelBase CurrentPage
    {
        get => _currentPage;
        set
        {
            _currentPage = value;
            this.RaisePropertyChanged();
        }
    }

    private ListItemTemplate? _selectedListItem;
    
    public ListItemTemplate? SelectedListItem
    {
        get => _selectedListItem;
        set
        {
            _selectedListItem = value;
            this.RaisePropertyChanged();
        }
    }

    public ReactiveCommand<ListItemTemplate?, Unit> OnSelectedListItemChangedCommand { get; }

    public ShopWindowViewModel()
    {
        OnSelectedListItemChangedCommand =
            ReactiveCommand.CreateFromObservable<ListItemTemplate?, Unit>(OnSelectedListItemChanged);
    }

    public IObservable<Unit> OnSelectedListItemChanged(ListItemTemplate? value)
    {
        Console.WriteLine("VALUE: " + value);
        if (value is null) return Observable.Return(Unit.Default);
        var instance = Activator.CreateInstance(value.ModelType);
        Console.WriteLine("INSTANCE: " + instance);
        if (instance is null) return Observable.Return(Unit.Default);
        CurrentPage = (ViewModelBase)instance;
        return Observable.Return(Unit.Default);
    }

    private static StreamGeometry GetIcon(string iconName)
    {
        var application = Application.Current;
        var streamGeometry = application.FindResource(iconName) as StreamGeometry;

        return streamGeometry;
    }

    public ObservableCollection<ListItemTemplate> Items { get; } = new()
    {
        new ListItemTemplate(typeof(HomePageViewModel), GetIcon("Home")),
        new ListItemTemplate(typeof(CartPageViewModel), GetIcon("Cart")),
        new ListItemTemplate(typeof(ProfilePageViewModel), GetIcon("Profile")),
        new ListItemTemplate(typeof(SettingsPageViewModel), GetIcon("Settings")),
    };
}

public class ListItemTemplate
{
    public ICommand ItemSelectedCommand { get; }

    public ListItemTemplate(Type type, StreamGeometry pageIcon)
    {
        // Инициализация команды
        ItemSelectedCommand = ReactiveCommand.CreateFromObservable(() => Observable.Return(this));

        ModelType = type;
        ItemName = type.Name.Replace("PageViewModel", "") + "Name";
        ItemLabel = type.Name.Replace("PageViewModel", "");
        ItemImg = pageIcon;
    }


    public string ItemName { get; }
    public string ItemLabel { get; set;  }
    public Geometry ItemImg { get; }
    public Type ModelType { get; }
}
using System;
using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ShopDesktop.ViewModels;

public partial class ShopWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentPage = new HomePageViewModel();

    [ObservableProperty] 
    private ListItemTemplate? _selectedListItem;

    partial void OnSelectedListItemChanged(ListItemTemplate? value)
    {
        if (value is null) return;
        var instance = Activator.CreateInstance(value.ModelType);
        if (instance is null) return;
        CurrentPage = (ViewModelBase)instance;
    }

    public ObservableCollection<ListItemTemplate> Items { get; } = new()
    {
        new ListItemTemplate(typeof(HomePageViewModel)),
        new ListItemTemplate(typeof(CartPageViewModel)),
        new ListItemTemplate(typeof(ProfilePageViewModel)),
        new ListItemTemplate(typeof(SettingsPageViewModel)),
    };
}

public class ListItemTemplate
{
    
    public ListItemTemplate( Type type)
    {
        ModelType = type;
        ItemName = type.Name.Replace("PageViewModel", "") + "Name"; 
        ItemLabel = type.Name.Replace("PageViewModel", "");
        // TODO: need ability to take icons from Icons.axaml by name for each item here
    }


    public string ItemName { get; }
    public string ItemLabel { get; set;  }
    public Geometry ItemImg { get; }
    public Type ModelType { get; }
}
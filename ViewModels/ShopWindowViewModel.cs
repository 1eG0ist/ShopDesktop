using System;
using System.Collections.ObjectModel;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Styling;
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
    // public var icons =
    
    public ListItemTemplate( Type type, StreamGeometry PageIcon)
    {
        ModelType = type;
        ItemName = type.Name.Replace("PageViewModel", "") + "Name"; 
        ItemLabel = type.Name.Replace("PageViewModel", "");
        ItemImg = PageIcon;
        // TODO: need ability to take icons from Icons.axaml by name for each item here
    }


    public string ItemName { get; }
    public string ItemLabel { get; set;  }
    public Geometry ItemImg { get; }
    public Type ModelType { get; }
}
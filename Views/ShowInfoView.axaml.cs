using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ShopDesktop.Views;

public partial class ShowInfoView : Window
{
    public ShowInfoView(string info)
    {
        InitializeComponent();
        InfoArea.Text = info;
    }
}
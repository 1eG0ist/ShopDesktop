using System.Windows.Input;
using ReactiveUI;

namespace ShopDesktop.ViewModels;

public class ShopWindowViewModel : ViewModelBase
{

    private bool _isPaneOpen = true;
    
    public bool IsPaneOpen
    {
        get { return _isPaneOpen; }
        set
        {
            _isPaneOpen = value;
            this.RaiseAndSetIfChanged(ref _isPaneOpen, value);
        }
    }

    public ICommand SwapPaneVisibilityCommand { get; }
    
    public ShopWindowViewModel()
    {
        SwapPaneVisibilityCommand = ReactiveCommand.Create(SwapPaneVisibility);
    }

    public void SwapPaneVisibility()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    public bool isSellerPanelBtnVisible => SessionData.registeredUser.UserRole > 1;
    public bool isAdminPanelBtnVisible => SessionData.registeredUser.UserRole > 2;

}
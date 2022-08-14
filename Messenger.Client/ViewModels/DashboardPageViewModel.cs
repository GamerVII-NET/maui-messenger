using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Messenger.Client.Views.Pages;

namespace Messenger.Client.ViewModels
{

    public partial class DashboardPageViewModel : ObservableObject
    {

        [ObservableProperty]
        object _currentPage;

        public DashboardPageViewModel()
        {
            
        }

        [RelayCommand]
        async Task GoToMessangerPage()
        {
            await Shell.Current.GoToAsync(nameof(MessangerPage));
        }

    }
}

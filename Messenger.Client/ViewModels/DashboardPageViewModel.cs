using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Messenger.Client.Views.Pages;
using Messenger.Domains.Models;
using System.Collections.ObjectModel;

namespace Messenger.Client.ViewModels
{

    public partial class DashboardPageViewModel : ObservableObject
    {

        [ObservableProperty]
        object _currentPage;

        [ObservableProperty]
        ObservableCollection<ChatModel> _chats = new ObservableCollection<ChatModel>();

        [ObservableProperty]
        ChatModel _selectedChat;

        public DashboardPageViewModel()
        {

        }

        [RelayCommand]
        async Task GoToMessangerPage()
        {
            await Shell.Current.GoToAsync(nameof(MessangerPage));
        }

        [RelayCommand]
        async Task LoadInfoByChatModel(ChatModel chatModel)
        {

        }

    }
}

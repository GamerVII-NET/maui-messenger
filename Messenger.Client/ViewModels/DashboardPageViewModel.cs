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
        ObservableCollection<ChatModel> _lastMessages = new ObservableCollection<ChatModel>();

        public DashboardPageViewModel()
        {

            Random rand = new Random();

            for (int i = 0; i < 10; i++)
            {
                _lastMessages.Add(new ChatModel
                {
                    User = new UserModel
                    {
                        FirstName = "Anton",
                        LastName = "Terentev",
                        Patronymic = "Alekseevich",
                        AvatarLink = "https://sun9-75.userapi.com/impg/DEOK5L2qIwif95nINZP2lMc0En_UhnPOUWgMNQ/07BCpJFoeHo.jpg?size=2160x2160&quality=96&sign=0706decc844288c1e4026a3ee4e72330&type=album"
                    },
                    LastMessage = "Hi everyone, this is MAUI. You can ignore it!",
                    LastMessageDate = DateTime.Now,
                    MessageType = Enums.MessageType.Direct,
                    MessageCount = rand.Next(0, 4)


                });
            }
        }

        [RelayCommand]
        async Task GoToMessangerPage()
        {
            await Shell.Current.GoToAsync(nameof(MessangerPage));
        }

    }
}

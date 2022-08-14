using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Client.ViewModels
{
    public partial class WelcomePageViewModel : ObservableObject
    {
        [ObservableProperty]
        string _name;

        [ObservableProperty]
        string _login;

        [ObservableProperty]
        string _password;

        public WelcomePageViewModel()
        {
            _name = "MAUI Messenger";
        }

        [RelayCommand]
        async Task OnLoginAsync()
        {
            if (string.IsNullOrEmpty(_login) ||
                string.IsNullOrEmpty(_password))
            {
                await Shell.Current.DisplayAlert("Error", "Login and password cannot be empty", "Ok");
                return;
            }

            

        }


    }
}

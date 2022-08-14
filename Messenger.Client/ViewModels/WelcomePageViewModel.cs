using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Messenger.Client.Services;
using Messenger.Client.Views.Pages;
using Messenger.Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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

        [ObservableProperty]
        bool _buttonIsActive = true;

        public WelcomePageViewModel()
        {
            string token = Preferences.Get("Token", "");

            // ToDo: Make a normal token check
            if (string.IsNullOrEmpty(token) == false)
            {
                Shell.Current.GoToAsync(nameof(DashboardPage));
            }
        }

        [RelayCommand]
        async Task OnLoginAsync()
        {
            _buttonIsActive = false;

            #region Audit
            if (string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password))
            {
                await Shell.Current.DisplayAlert("Error", "Login and password cannot be empty", "Ok");

                _buttonIsActive = true;
                return;
            } 
            #endregion

            var response = await AuthService.AuthAsync(_login, _password);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to log in, check the correctness of the entered data, or try again later", "Ok");
                return;
            }

            await Shell.Current.GoToAsync(nameof(DashboardPage));

            string token = await response.Content.ReadAsStringAsync();

            Preferences.Set("Token", token.Replace("\"", ""));

            _buttonIsActive = true;
        }



    }
}

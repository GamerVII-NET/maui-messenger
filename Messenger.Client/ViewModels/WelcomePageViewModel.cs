using CommunityToolkit.Mvvm.ComponentModel;
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

        public WelcomePageViewModel()
        {
            _name = "MAUI Messenger";
        }


    }
}

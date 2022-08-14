using Messenger.Client.Views.Pages;

namespace Messenger.Client
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(WelcomePage), typeof(WelcomePage));
            Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
        }
    }
}
using MyFriends.App.Views;

namespace MyFriends.App
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}

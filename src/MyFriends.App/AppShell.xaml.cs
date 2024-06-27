using MyFriends.App.Views;

namespace MyFriends.App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("//friends", typeof(FriendListView));
            Routing.RegisterRoute("//friends/detail", typeof(FriendDetailView));
            Routing.RegisterRoute("//friends/detail/edit", typeof(FriendEditView));
            Routing.RegisterRoute("//friends/new", typeof(FriendEditView));
        }


    }
}

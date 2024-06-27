using CommunityToolkit.Mvvm.ComponentModel;
using MyFriends.App.ViewModels;
using MyFriends.BL.Models;

namespace MyFriends.App.Views
{
    public partial class FriendListView : ContentPage
    {
        private readonly FriendListViewModel _viewModel;
        public FriendListView(FriendListViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Refresh();
        }
    }
}

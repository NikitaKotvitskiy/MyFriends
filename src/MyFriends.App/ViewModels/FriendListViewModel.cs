using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Bson;
using MyFriends.BL.Facades;
using MyFriends.BL.Models;
using System.Collections.ObjectModel;

namespace MyFriends.App.ViewModels
{
    public partial class FriendListViewModel : ViewModelBase
    {
        private readonly FriendFacade _friendFacade;

        [ObservableProperty]
        ObservableCollection<FriendListModel> friends = null!;

        public FriendListViewModel(FriendFacade friendFacade)
        {
            _friendFacade = friendFacade;
        }

        protected override async Task InitializeAsync()
        {
            Friends = new ObservableCollection<FriendListModel>();
            foreach (var friend in await _friendFacade.GetFriendsList())
                Friends.Add(friend);
        }

        [RelayCommand]
        async Task Add()
        {
            await Shell.Current.GoToAsync($"new", new ShellNavigationQueryParameters { { "Id", ObjectId.Empty } });
        }

        [RelayCommand]
        async Task ChooseFriend(ObjectId id)
        {
            await Shell.Current.GoToAsync($"detail", new ShellNavigationQueryParameters{ { "Id", id } });
        }
    }
}

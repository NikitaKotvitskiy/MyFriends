using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Bson;
using MyFriends.BL.Facades;
using MyFriends.BL.Models;

namespace MyFriends.App.ViewModels
{
    public partial class FriendDetailViewModel : ViewModelBase
    {
        private readonly FriendFacade _friendFacade;

        private ObjectId Id;

        [ObservableProperty]
        FriendDetailModel friend = FriendDetailModel.Empty;

        public FriendDetailViewModel(FriendFacade friendFacade)
        {
            _friendFacade = friendFacade;
        }

        [RelayCommand]
        async Task Edit()
        {
            await Shell.Current.GoToAsync($"edit", new ShellNavigationQueryParameters { { "Id", Id } });
        }

        public override void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Id = (ObjectId)query["Id"];
            _ = InitializeAsync();
        }

        protected override async Task InitializeAsync()
        {
            Friend = (await _friendFacade.GetFriend(Id))!;
        }

        [RelayCommand]
        async Task DeleteFriend()
        {
            await _friendFacade.DeleteFriend(Friend);
            Shell.Current.SendBackButtonPressed();
        }
    }
}

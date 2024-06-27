using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Bson;
using MyFriends.BL.Facades;
using MyFriends.BL.Models;

namespace MyFriends.App.ViewModels;

public partial class FriendEditViewModel : ViewModelBase
{
    public ObjectId Id;
    private readonly FriendFacade _friendFacade;

    [ObservableProperty]
    FriendDetailModel friend = FriendDetailModel.Empty;

    [ObservableProperty]
    DateTime? dateOfBirth = DateTime.Now;

    [ObservableProperty]
    bool isTimePickerActive = true;
    [ObservableProperty]
    bool isLeaveButtonActive = true;
    [ObservableProperty]
    bool isEnableButtonActive = false;

    public FriendEditViewModel(FriendFacade friendFacade)
    {
        _friendFacade = friendFacade;
    }

    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Id = (ObjectId)query["Id"];
        _ = InitializeAsync();
    }

    protected override async Task InitializeAsync()
    {
        if (Id != ObjectId.Empty)
        {
            Friend = (await _friendFacade.GetFriend(Id))!;
            if (Friend.DateOfBirth == null)
                DateOfBirth = null;
            else
                DateOfBirth = new DateTime((DateOnly)Friend.DateOfBirth, new TimeOnly());
        }
    }

    [RelayCommand]
    async Task Apply()
    {
        if (Friend.Name is "" or null)
            return;

        if (DateOfBirth == null)
            Friend.DateOfBirth = null;
        else
            Friend.DateOfBirth = new DateOnly(DateOfBirth.Value.Year, DateOfBirth.Value.Month, DateOfBirth.Value.Day);

        if (Friend.Id == default)
            await _friendFacade.InsertNewFriend(Friend);
        else
            await _friendFacade.UpdateFriend(Friend);

        Shell.Current.SendBackButtonPressed();
        return;
    }

    [RelayCommand]
    void SkipDateOfBirth()
    {
        DateOfBirth = null;
        IsTimePickerActive = false;
        IsLeaveButtonActive = false;
        IsEnableButtonActive = true;
    }

    [RelayCommand]
    void EnableDateOfBirth()
    {
        DateOfBirth = DateTime.Now;
        IsTimePickerActive = true;
        IsLeaveButtonActive = true;
        IsEnableButtonActive = false;
    }

    [RelayCommand]
    void CancelButton()
    {
        Shell.Current.SendBackButtonPressed();
    }
}
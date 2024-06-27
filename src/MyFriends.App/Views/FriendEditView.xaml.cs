using MyFriends.App.ViewModels;

namespace MyFriends.App.Views;

public partial class FriendEditView : ContentPage
{
	private readonly FriendEditViewModel _viewModel;

	public FriendEditView(FriendEditViewModel viewModel)
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
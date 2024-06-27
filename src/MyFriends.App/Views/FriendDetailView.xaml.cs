using MyFriends.App.ViewModels;

namespace MyFriends.App.Views;

public partial class FriendDetailView : ContentPage
{
	private readonly FriendDetailViewModel _viewModel;

	public FriendDetailView(FriendDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.Refresh();
    }
}
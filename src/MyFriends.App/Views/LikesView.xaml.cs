using MyFriends.App.ViewModels;

namespace MyFriends.App.Views;

public partial class LikesView : ContentPage
{
	public LikesView(LikesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
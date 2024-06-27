using MyFriends.App.ViewModels;

namespace MyFriends.App.Views;

public partial class RelationView : ContentPage
{
	public RelationView(RelationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
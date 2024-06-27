using MyFriends.App.ViewModels;
using MyFriends.App.Views;
using MyFriends.BL;

namespace MyFriends.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            var facades = BLFactory.GetFacades("mongodb://localhost:27017", "MyFriendsMongoDB");

            builder.Services.AddSingleton(facades.friendFacade);
            builder.Services.AddSingleton(facades.likesFacade);
            builder.Services.AddSingleton(facades.relationFacade);

            builder.Services.AddTransient<FriendDetailViewModel>();
            builder.Services.AddTransient<FriendEditViewModel>();
            builder.Services.AddTransient<FriendListViewModel>();
            builder.Services.AddTransient<LikesViewModel>();
            builder.Services.AddTransient<RelationViewModel>();

            builder.Services.AddTransient<FriendDetailView>();
            builder.Services.AddTransient<FriendEditView>();
            builder.Services.AddTransient<FriendListView>();
            builder.Services.AddTransient<LikesView>();
            builder.Services.AddTransient<RelationView>();

            var app = builder.Build();
            return app;
        }

    }
}

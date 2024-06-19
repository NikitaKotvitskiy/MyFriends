using Microsoft.Extensions.Logging;
using MyFriends.BL;
using MyFriends.BL.Facades;
using MyFriends.BL.Models;
using MyFriends.DAL;

namespace MyFriends.App
{
    public static class MauiProgram
    {
        public static FacadeSet facades { get; private set; } = null!;

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

            var app = builder.Build();

            facades = BLFactory.GetFacades("mongodb://localhost:27017", "MyFriendsMongoDB");

            return app;
        }

    }
}

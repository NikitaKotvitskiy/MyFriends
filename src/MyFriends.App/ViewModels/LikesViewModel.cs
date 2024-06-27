using MongoDB.Bson;
using MyFriends.BL.Facades;

namespace MyFriends.App.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class LikesViewModel(LikesFacade likesFacade)
    {
        public ObjectId Id;
        private LikesFacade _likesFacade = likesFacade;
    }
}

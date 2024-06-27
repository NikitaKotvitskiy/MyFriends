using MongoDB.Bson;
using MyFriends.BL.Facades;

namespace MyFriends.App.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class RelationViewModel(RelationFacade relationFacade)
    {
        public ObjectId Id;
        private RelationFacade _relationFacade = relationFacade;
    }
}

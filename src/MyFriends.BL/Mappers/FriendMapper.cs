using MyFriends.BL.Models;
using MyFriends.DAL.Entities;
using MyFriends.DAL.Mappers;

namespace MyFriends.BL.Mappers
{
    public class FriendMapper : IMapper
    {
        public FriendDetailModel MapToFriendDetailModel(FriendEntity entity) => 
            FriendDetailModel.Empty with
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                DateOfBirth = entity.DateOfBirth,
                Country = entity.Country,
                City = entity.City,
                Address = entity.Address
            };

        public FriendListModel MapToFriendListModel(FriendEntity entity) =>
            FriendListModel.Empty with
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname
            };

        public FriendEntity MapToFriendEntity(FriendDetailModel model) =>
            new()
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                DateOfBirth = model.DateOfBirth,
                Country = model.Country,
                City = model.City,
                Address = model.Address
            };
    }
}

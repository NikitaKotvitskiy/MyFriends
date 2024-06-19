using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFriends.BL.Facades
{
    public record FacadeSet (FriendFacade friendFacade, LikesFacade likesFacade, RelationFacade relationFacade)
    {
    }
}

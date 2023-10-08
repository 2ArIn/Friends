using FriendWithDB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendsWithDatabase.Services
{
    public interface IFriendsService
    {
      // FriendsRepository _friendsRepository { get; }
        Task<List<Friend>> GetAll();
        Task<Friend>Get(int id);
        Task Add(Friend friend);
        Task Edit(Friend friend);
        Task Delete(int id);
    }
}

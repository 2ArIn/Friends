using FriendWithDB.Models;
using FriendWithDB.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendsWithDatabase.Services
{
    public class FriendsSerivce:IFriendsService
    {
        FriendsRepository _friendsRepository;

        public FriendsSerivce(FriendsRepository friendsRepository)
        {
            _friendsRepository = friendsRepository;    
        }



        public async Task Add(Friend friend)
        {
            await _friendsRepository.AddNewAsync(friend);
        }

        

        public async Task Delete(int id)
        {
            await _friendsRepository.DeleteAsync(id);
        }

        public async Task Edit(Friend friend)
        {
           await _friendsRepository.UpdateAsync(friend);
        }


 

        Task<Friend> IFriendsService.Get(int id)
        {
            return _friendsRepository.GetByIdAsync(id);
        }

        Task<List<Friend>> IFriendsService.GetAll()
        {
            return _friendsRepository.GetAllAsync();
        }
    }
}

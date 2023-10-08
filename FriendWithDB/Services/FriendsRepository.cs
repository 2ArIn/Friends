using FriendsWithDatabase.Data;
using FriendWithDB.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendWithDB.Services
{
    public class FriendsRepository
    {
        AppDbContext _context;
        public FriendsRepository(AppDbContext context)
        {
            _context = context;
        }
        //public List<Friend> GetAll()
        //{
        //    return _context.Friends.ToList();
        //}
        public async Task<List<Friend>> GetAllAsync()
        {
            return await _context.Friends.ToListAsync();
        }
        public async Task<Friend> GetByIdAsync(int id)
        {
            return await _context.Friends.FindAsync(id);
        }
        //public async Task UpdateAsync(Friend friend)
        //{
        //    _context.Friends.Update(friend);
        //    await _context.SaveChangesAsync();
        //}
        //public async Task DeleteAsync(int id)
        //{
        //    Friend friend = await _context.Friends.FindAsync(id);
        //    _context.Friends.Remove(friend);
        //    await _context.SaveChangesAsync();
        //}
        public async Task AddNewAsync(Friend friend)
        {
            
            _context.Friends.Add(friend);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Friend friend)
        {
            _context.Friends.Update(friend);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Friend friend =await _context.Friends.FindAsync(id);
            _context.Friends.Remove(friend);
            await _context.SaveChangesAsync();
        }
    }
}

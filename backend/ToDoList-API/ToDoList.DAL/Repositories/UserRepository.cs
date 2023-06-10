using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Core;
using ToDoList.Services.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _db;
        public UserRepository(ApplicationContext db)
        {
            _db = db;
        }
        public async Task CreateUserAsync(UserEntity user)
        {
            user.Password = HashDataHelper.HashPassword(user.Password);
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userid)
        {
            var user = await _db.Users.FindAsync(userid);

            if (user != null) 
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<UserEntity> FindByEmailAsync(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(user => user.Email == email);

            if (user is null)
                return null!;
            
            return user!;
        }

        public async Task<UserEntity> FindByIdAsync(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(user => user.UserId == id);

            if (user is null)
                return null!;

            return user;
        }

        public async Task<UserEntity> FindByUserNameAsync(string username)
        {
            var user = await _db.Users.FirstOrDefaultAsync(user => user.UserName == username);

            if(user is null) 
                return null!;
            return user;
        }

        public async Task UpdateUserAsync(UserEntity user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}

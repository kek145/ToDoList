using ToDoList.DAL.Core;
using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using ToDoList.DAL.Interfaces;
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
        public async Task UpdateUserAsync(UserEntity user)
        {
            _db.UsersEntity.Update(user);
            await _db.SaveChangesAsync();
        }
        public async Task CreateUserAsync(UserEntity user)
        {
            _db.UsersEntity.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task<UserEntity> FindByEmailAsync(string email)
        {
            var user = await _db.UsersEntity.FirstOrDefaultAsync(user => user.Email == email);

            if (user is null)
                return null!;
            
            return user!;
        }

        public async Task<UserEntity> FindByIdAsync(int userId)
        {
            var user = await _db.UsersEntity.FirstOrDefaultAsync(user => user.UserId == userId);

            if (user is null)
                return null!;

            return user;
        }
    }
}

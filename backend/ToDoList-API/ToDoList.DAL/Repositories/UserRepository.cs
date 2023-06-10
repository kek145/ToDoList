using System.Text;
using ToDoList.DAL.Core;
using System.Threading.Tasks;
using ToDoList.Domain.Entity;
using ToDoList.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace ToDoList.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _db;
        public UserRepository(ApplicationContext db)
        {
            _db = db;
        }
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public async Task CreateUserAsync(UserEntity user)
        {
            user.Password = HashPassword(user.Password);
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

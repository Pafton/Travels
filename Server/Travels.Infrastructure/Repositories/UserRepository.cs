using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;
using Travels.Infrastructure.Presistance;

namespace Travels.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserRepository(AppDbContext appDbContext, IPasswordHasher<User> passwordHasher)
        {
            _appDbContext = appDbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            user.Password = _passwordHasher.HashPassword(user, user.Password);

            await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ChangeUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _appDbContext.Users.FindAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with id {id} not found.");

            _appDbContext.Users.Remove(user);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUser(int? id)
        {
            if (id == null)
                return null;

            return await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _appDbContext.Users.ToListAsync();
        }
        public async Task<User?> GetByEmail(string email)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}

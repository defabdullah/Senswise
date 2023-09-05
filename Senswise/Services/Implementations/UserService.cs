using Microsoft.EntityFrameworkCore;
using Senswise.Context;
using Senswise.Context.Entity;
using Senswise.Controllers.User;
using Senswise.Services.Abstractions;

namespace Senswise.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly SenswiseContext _context;

        public UserService(SenswiseContext context)
        {
            _context = context;
        }

        

        public async Task<ICollection<UserResponse>> GetListOfUsersAsync()
        {
            if (_context.Users == null)
            {
                throw new Exception("Table not exist.");
            }
            try {
                var response = await _context.Users.Select(x => new UserResponse
                {
                    Id = x.Id,
                    Address = x.Address,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).ToListAsync();
                return response;
            }
            catch
            {
                throw new Exception("Operation Failed.");
            }
        }

        public async Task<UserResponse> GetUserAsync(int id)
        {
            if (_context.Users == null)
            {
               throw new Exception();
            }
            
            try
            {
                var response = await _context.Users.Where(x => x.Id == id).Select(x => new UserResponse
                {
                    Id = x.Id,
                    Address = x.Address,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).FirstOrDefaultAsync();

                if (response == null)
                {
                    throw new Exception("User not found.");
                }

                return response;
            }
            catch
            {
                throw new Exception("Operation failed.");
            }
        }

        public async Task<UserResponse> UpdateUserAsync(int id,UserRequest user)
        {

            try
            {
                //_context.Entry(user).State = EntityState.Modified;
                var entity = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if(entity == null){
                    throw new Exception();
                }

                entity.Address = user.Address;
                entity.Email = user.Email;
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;

                _context.Update(entity);
                await _context.SaveChangesAsync();

                var response = await _context.Users.Where(x => x.Id == id).Select(x => new UserResponse
                {
                    Id = x.Id,
                    Address = x.Address,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).FirstOrDefaultAsync();

                if (response == null)
                {
                    throw new Exception();
                }

                return response;
            }
            catch
            {
                if (!UserExists(id))
                {
                    throw new Exception("Not Found.");
                }
                else
                {
                    throw new Exception("Database update error.");
                }
            }
        }
        public async Task<UserResponse> CreateUserAsync(UserRequest user)
        {
            if (_context.Users == null)
            {
                throw new Exception("Entity set 'SenswiseContext.Users'  is null.");
            }
            try
            {
                var entity = new User
                {
                    Address = user.Address,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };

                _context.Users.Add(entity);
                await _context.SaveChangesAsync();

                var response = await _context.Users.Where(x => x.Id == entity.Id).Select(x => new UserResponse
                {
                    Id = x.Id,
                    Address = x.Address,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).FirstOrDefaultAsync();

                if (response == null)
                {
                    throw new Exception();
                }

                return response;
            }
            catch
            {
                throw new Exception("Database update error.");
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            if (_context.Users == null)
            {
                throw new Exception("There is no table.");
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Database update error.");
            }
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

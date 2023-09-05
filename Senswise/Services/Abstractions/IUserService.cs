using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Senswise.Context.Entity;
using Senswise.Controllers.User;

namespace Senswise.Services.Abstractions
{
    public interface IUserService
    {
        Task<ICollection<UserResponse>> GetListOfUsersAsync();
        Task<UserResponse> GetUserAsync(int id);
        Task<UserResponse> UpdateUserAsync(int id, UserRequest user);
        Task<UserResponse> CreateUserAsync(UserRequest user);
        Task DeleteUserAsync(int id);
    }
}

using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using TeamManagement.Models;

namespace TeamManagement.Interfaces
{
    public interface IUserRepository
    {
        #region Method-Declarations
        Task<User> GetUser(string userEmail);

        Task<User> GetUserById(int userId);

        Task<User> GetUserByRoleId(int roleId);

        Task<List<User>> GetUsersListById(int id);

        Task<Login> GetLoginUser(string loginEmail);

        Task SaveUser(User user);

        Task SaveLoginUser(Login user);

        Task AddUser(User user);

        Task AddLoginUser(Login login);
        #endregion

    }
}

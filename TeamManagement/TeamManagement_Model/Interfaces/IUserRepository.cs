using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using TeamManagement.Models;

namespace TeamManagement.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(string userEmail);

        User GetUserById(int userId);

        User GetUserByRoleId(int roleId);

        List<User> GetUsersListById(int id);

        Login GetLoginUser(string loginEmail);

        void SaveUser(User user);

        void SaveLoginUser(Login user);


        void AddUser(User user);

        void AddLoginUser(Login login);


    }
}

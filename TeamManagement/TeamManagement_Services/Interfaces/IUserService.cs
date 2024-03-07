using TeamManagement.Models;

namespace TeamManagement.Interfaces
{
    public interface IUserService
    {
        Task<string> Register(User user);

        Task<string> ChangePwd(Login login,int id);
    }
}

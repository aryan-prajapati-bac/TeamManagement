using TeamManagement.Models;

namespace TeamManagement.Interfaces
{
    public interface IUserService
    {
        string Register(User user);

        string ChangePwd(Login login,int id);
    }
}

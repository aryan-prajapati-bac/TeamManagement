using TeamManagement.Models;

namespace TeamManagement.Interfaces
{
    public interface IUserService
    {
        #region Method-Declaration
        Task<string> Register(User user);
        Task<string> ChangePwd(Login login,int id);
        #endregion
    }
}

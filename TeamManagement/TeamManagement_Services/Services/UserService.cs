using TeamManagement.Interfaces;
using TeamManagement.Models;

namespace TeamManagement.Services
{
    public class UserService:IUserService
    {
        #region Services
        private readonly IUserRepository _userRepository;
        private readonly IMailServices _mailServices;
        #endregion

        #region DI
        public UserService(IUserRepository userRepo, IMailServices mailServices)
        {
            _userRepository = userRepo;
            _mailServices = mailServices;   

        }
        #endregion

        #region Methods
        public async Task<string> Register(User user)
        {
            string register = $"Welcome {user.FirstName}, You have registerd successfully!\n" +
                              $"Username:{user.Email}";            
            user.Password = PasswordHasher.HashPassword("team1234");
            Login loginObj = new Login()
            {
                Email = user.Email,
                Password = user.Password,

            };
            try
            {
                await _userRepository.AddUser(user);
                await _userRepository.AddLoginUser(loginObj);
            }
            catch (Exception ex) 
            { 
                return "User with this email id is already registered!"; 
            }

            await _mailServices.SendEmail(user.Email, "Registration", register);
            return "Registered Successfully";

        }

        public async Task<string> ChangePwd(Login obj,int id)
        {
            try
            {
                if (obj == null)
                    return "Provide data";

                User user = await _userRepository.GetUserById(id);

                if (!user.Email.Equals(obj.Email))
                    return "You have to log in first!!";

                Login login = await _userRepository.GetLoginUser(obj.Email);

                string changedPwd = $"Hello {user.FirstName}!\n" +
                                    $"Your Password has been successfully changed!";

                string newPwd = PasswordHasher.HashPassword(obj.Password);

                login.Password = newPwd;
                user.Password = newPwd;

                await _userRepository.SaveUser(user);
                await _userRepository.SaveLoginUser(login);

                await _mailServices.SendEmail(obj.Email, "Password changed!", changedPwd);
                return "Successfully Changed";
            }
            catch(Exception ex)
            {
                return "Error from changing password";
            }
        }
        #endregion

    }
}

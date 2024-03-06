using TeamManagement.Interfaces;
using TeamManagement.Models;

namespace TeamManagement.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMailServices _mailServices;
        public UserService(IUserRepository userRepo, IMailServices mailServices)
        {
            _userRepository = userRepo;
            _mailServices = mailServices;   

        }

        public string Register(User user)
        {
            user.Password = PasswordHasher.HashPassword("team1234");
            Login loginObj = new Login()
            {
                Email = user.Email,
                Password = user.Password,

            };
            try
            {
                _userRepository.AddUser(user);
                _userRepository.AddLoginUser(loginObj);
            }
            catch (Exception ex) 
            { 
                return "User with this email id is already registered!"; 
            }

            _mailServices.SendEmail(user.Email, "Registration", "You have registered successfully");
            return "Registered Successfully";

        }

        public string ChangePwd(Login obj,int id)
        {
            if (obj == null) return "Provide data";
            User user = _userRepository.GetUserById(id);
            if (!user.Email.Equals(obj.Email)) return "You have to log in first!!";
            Login login = _userRepository.GetLoginUser(obj.Email);
            //User user = _userRepository.GetUser(obj.Email);
            string newPwd = PasswordHasher.HashPassword(obj.Password);

            login.Password=newPwd;
            user.Password = newPwd;

             _userRepository.SaveUser(user);
             _userRepository.SaveLoginUser(login);

            _mailServices.SendEmail(obj.Email, "Password changed!", "Your Password has been changed!");
            return "Successfully Changed";
        }

    }
}

using Azure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeamManagement.ViewModals;
using TeamManagement.Interfaces;
using TeamManagement.Models;
using TeamManagement_Services;

namespace TeamManagement.Services
{
    public class LoginService
    {
        #region Services
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        #endregion

        #region EnumVariables
        static int enumUserId = (int)RoleEnum.User;
        static int enumPlayerId = (int)RoleEnum.Player;
        static int enumCaptainId = (int)RoleEnum.Captain;
        static int enumCoachId = (int)RoleEnum.Coach;
        static int enumTeamPlayerId = (int)RoleEnum.TeamPlayer;
        #endregion

        #region DI
        public LoginService(Microsoft.Extensions.Configuration.IConfiguration configuration,IUserRepository userRepo)
        {
            _userRepository= userRepo;
            _configuration = configuration;
            
        }
        #endregion

        #region Methods
        public string Authenticate(Login user)
        {
            User _user = _userRepository.GetUser(user.Email);
            if (_user == null) return "Failed";
            if (!_user.Password.Equals(PasswordHasher.HashPassword(user.Password))) return "Invalid Credentials";
            if (_user.RoleId == enumUserId)  return TokenGeneration("registerdUser", _user.UserId);
            if (_user.RoleId == enumPlayerId)  return TokenGeneration("AddedByCoach", _user.UserId);
            if (_user.RoleId == enumTeamPlayerId) return TokenGeneration("TeamPlayer", _user.UserId);
            if (_user.RoleId == enumCoachId)  return TokenGeneration("coach", _user.UserId); 

            if (_user.RoleId == enumCaptainId) return TokenGeneration("captain",_user.UserId);

            return "";
        }

        public string TokenGeneration(string role,int id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
             {
                new Claim(ClaimTypes.Role, role),
                new Claim("UserId",id.ToString())
            };

            var Sectoken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,  
              expires: DateTime.UtcNow.AddMinutes(30),
              signingCredentials: credentials);

            
            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
            

            return token;
        }

        public string ResponseObj(Login loginObj)
        {
            CoachView coachView = new CoachView();
            CaptainView captainView = new CaptainView();
            PlayerView playerView = new PlayerView();
            
            User _user = _userRepository.GetUser(loginObj.Email);
            if (_user == null) return "First register your self!";

            if (_user.RoleId == enumCoachId)
            {
                coachView.CoachName = _userRepository.GetUserByRoleId(enumCoachId).FirstName;
                coachView.CoachEmail = _userRepository.GetUserByRoleId(enumCoachId).Email;
                if (_userRepository.GetUserByRoleId(enumCaptainId) != null)
                {
                    coachView.CaptainName = _userRepository.GetUserByRoleId(enumCaptainId).FirstName;
                    coachView.CaptainEmail = _userRepository.GetUserByRoleId(enumCaptainId).Email;
                }
                else {
                    coachView.CaptainName = "Not assigned";
                    coachView.CaptainEmail = "Not assigned";
                }
                if (_userRepository.GetUserByRoleId(enumTeamPlayerId) != null)
                {
                    coachView.TeamPlayers = _userRepository.GetUsersListById(enumTeamPlayerId);
                }
                
                

                return coachView.ToString();
            }
            if (_user.RoleId == enumCaptainId)
            {
                captainView.CaptainName = _userRepository.GetUserByRoleId(enumCaptainId).FirstName;
                captainView.CaptainEmail = _userRepository.GetUserByRoleId(enumCaptainId).Email;
                captainView.TeamPlayers = _userRepository.GetUsersListById(enumTeamPlayerId);

                return captainView.ToString();
            }

            if (_user.RoleId == enumTeamPlayerId)
            {
                playerView.PlayerName = _user.FirstName;
                playerView.PlayerEmail=_user.Email;
                playerView.CoachName = _userRepository.GetUserByRoleId(enumCoachId).FirstName;
                playerView.CoachEmail = _userRepository.GetUserByRoleId(enumCoachId).Email;
                playerView.CaptainName = _userRepository.GetUserByRoleId(enumCaptainId).FirstName;
                playerView.CaptainEmail = _userRepository.GetUserByRoleId(enumCaptainId).Email;
                
                return playerView.ToString();
            }

            if (_user.RoleId == enumPlayerId) return $"Welcome {_user.FirstName}, You are added by Coach\n" +
                    $"Only after selected by Captain, you will be in his/her Team.";

            return $"Welcome {_user.FirstName},You have logged in successfully\n" +
                $"Only after added by Coach, you will be able to join any Team.";
            

        }
        #endregion
    }
}


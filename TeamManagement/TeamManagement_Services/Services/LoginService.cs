using Azure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeamManagement.ViewModals;
using TeamManagement.Interfaces;
using TeamManagement.Models;

namespace TeamManagement.Services
{
    public class LoginService
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public LoginService(Microsoft.Extensions.Configuration.IConfiguration configuration,IUserRepository userRepo)
        {
            _userRepository= userRepo;
            _configuration = configuration;
            
        }

        public string Authenticate(Login user)
        {
            User _user = _userRepository.GetUser(user.Email);
            if (_user == null) return "Failed";

            if (_user.RoleId == 0)  return TokenGeneration("registerdUser", _user.UserId);
            if (_user.RoleId == 1)  return TokenGeneration("AddedByCoach", _user.UserId);
            if (_user.RoleId == 11) return TokenGeneration("TeamPlayer", _user.UserId);
            if (_user.RoleId == 3)  return TokenGeneration("coach", _user.UserId); 

            if (_user.RoleId == 2) return TokenGeneration("captain",_user.UserId);

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
            CoachDTO coachDTO = new CoachDTO();
            CaptainDTO captainDTO = new CaptainDTO();
            PlayerDTO playerDTO = new PlayerDTO();
            
            User _user = _userRepository.GetUser(loginObj.Email);
            if (_user == null) return "First register your self!";

            if (_user.RoleId == 3)
            {
                coachDTO.CoachName = _userRepository.GetUserByRoleId(3).FirstName;
                coachDTO.CoachEmail = _userRepository.GetUserByRoleId(3).Email;
                if (_userRepository.GetUserByRoleId(2) != null)
                {
                    coachDTO.CaptainName = _userRepository.GetUserByRoleId(2).FirstName;
                    coachDTO.CaptainEmail = _userRepository.GetUserByRoleId(2).Email;
                }
                else { 
                     coachDTO.CaptainName = "Not assigned";
                     coachDTO.CaptainEmail = "Not assigned";
                }
                if (_userRepository.GetUserByRoleId(11) != null)
                {
                    coachDTO.TeamPlayers = _userRepository.GetUsersListById(11);
                }
                
                

                return coachDTO.ToString();
            }
            if (_user.RoleId == 2)
            {
                captainDTO.CaptainName = _userRepository.GetUserByRoleId(2).FirstName;
                captainDTO.CaptainEmail = _userRepository.GetUserByRoleId(2).Email;
                captainDTO.TeamPlayers = _userRepository.GetUsersListById(11);

                return captainDTO.ToString();
            }

            if (_user.RoleId == 11)
            {
                playerDTO.PlayerName = _user.FirstName;
                playerDTO.PlayerEmail=_user.Email;
                playerDTO.CoachName = _userRepository.GetUserByRoleId(3).FirstName;
                playerDTO.CoachEmail = _userRepository.GetUserByRoleId(3).Email;
                playerDTO.CaptainName = _userRepository.GetUserByRoleId(2).FirstName;
                playerDTO.CaptainEmail = _userRepository.GetUserByRoleId(2).Email;
                
                return playerDTO.ToString();
            }

            if (_user.RoleId == 1) return $"Welcome {_user.FirstName}, You are added by Coach\n" +
                    $"Only after selected by Captain, you will be in his/her Team.";

            return $"Welcome {_user.FirstName},You have logged in successfully\n" +
                $"Only after added by Coach, you will be able to join any Team.";
            

        }
    }
}


using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeamManagement.Interfaces;
using TeamManagement.Models;
using TeamManagement.ViewModals;
using TeamManagement_Services;

namespace TeamManagement.Services
{
    public class LoginService
    {
        #region Services
        private readonly IConfiguration _configuration;
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
        public async Task<string> Authenticate(Login user)
        {
            try { 
            User _user = await _userRepository.GetUser(user.Email);

            if (_user == null)
                return "Failed";

            if (!_user.Password.Equals(PasswordHasher.HashPassword(user.Password)))
                return "Invalid Credentials";

            if (_user.RoleId == enumUserId)
                return await TokenGeneration("registerdUser", _user.UserId);

            if (_user.RoleId == enumPlayerId)
                return await TokenGeneration("AddedByCoach", _user.UserId);

            if (_user.RoleId == enumTeamPlayerId)
                return await TokenGeneration("TeamPlayer", _user.UserId);

            if (_user.RoleId == enumCoachId)
                return await TokenGeneration("coach", _user.UserId);

            if (_user.RoleId == enumCaptainId)
                return await TokenGeneration("captain", _user.UserId);

            return "";
        }
            catch (Exception ex)
            {
                return "Error in Logging in";
            }
       
        }

        public async Task<string> TokenGeneration(string role,int id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
             {
                new Claim(ClaimTypes.Role, role),
                new Claim("UserId",id.ToString())
            };

            var Sectoken = new JwtSecurityToken(
                                 _configuration["Jwt:Issuer"],
                                 _configuration["Jwt:Issuer"],
                                 claims,  
                                 expires: DateTime.UtcNow.AddMinutes(30),
                                 signingCredentials: credentials
                                 );

            
            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
            

            return token;
        }

        public async Task<string> ResponseObj(Login loginObj)
        {
            try
            {
                CoachView coachView = new CoachView();
                CaptainView captainView = new CaptainView();
                PlayerView playerView = new PlayerView();

                User _user = await _userRepository.GetUser(loginObj.Email);
                if (!_user.Password.Equals(PasswordHasher.HashPassword(loginObj.Password)))
                    return "Invalid Credentials!";
                else
                {
                    if (_user == null) return "First register your self!";

                    if (_user.RoleId == enumCoachId)
                    {
                        coachView.CoachName = (await _userRepository.GetUserByRoleId(enumCoachId)).FirstName;
                        coachView.CoachEmail = (await _userRepository.GetUserByRoleId(enumCoachId)).Email;
                        if (await _userRepository.GetUserByRoleId(enumCaptainId) != null)
                        {
                            coachView.CaptainName = (await _userRepository.GetUserByRoleId(enumCaptainId)).FirstName;
                            coachView.CaptainEmail = (await _userRepository.GetUserByRoleId(enumCaptainId)).Email;
                        }
                        else
                        {
                            coachView.CaptainName = "Not assigned";
                            coachView.CaptainEmail = "Not assigned";
                        }
                        if (await _userRepository.GetUserByRoleId(enumTeamPlayerId) != null)
                        {
                            coachView.TeamPlayers = await _userRepository.GetUsersListById(enumTeamPlayerId);
                        }



                        return coachView.ToString();
                    }
                    if (_user.RoleId == enumCaptainId)
                    {
                        captainView.CaptainName = (await _userRepository.GetUserByRoleId(enumCaptainId)).FirstName;
                        captainView.CaptainEmail = (await _userRepository.GetUserByRoleId(enumCaptainId)).Email;
                        captainView.TeamPlayers = await _userRepository.GetUsersListById(enumTeamPlayerId);

                        return captainView.ToString();
                    }

                    if (_user.RoleId == enumTeamPlayerId)
                    {
                        playerView.PlayerName = _user.FirstName;
                        playerView.PlayerEmail = _user.Email;
                        playerView.CoachName = (await _userRepository.GetUserByRoleId(enumCoachId)).FirstName;
                        playerView.CoachEmail = (await _userRepository.GetUserByRoleId(enumCoachId)).Email;
                        playerView.CaptainName = (await _userRepository.GetUserByRoleId(enumCaptainId)).FirstName;
                        playerView.CaptainEmail = (await _userRepository.GetUserByRoleId(enumCaptainId)).Email;

                        return playerView.ToString();
                    }

                    if (_user.RoleId == enumPlayerId) return $"Welcome {_user.FirstName}, You are added by Coach\n" +
                            $"Only after selected by Captain, you will be in his/her Team.";

                    return $"Welcome {_user.FirstName},You have logged in successfully\n" +
                        $"Only after added by Coach, you will be able to join any Team.";
                }
            }
            catch (Exception ex)
            {
                return "Error in Dashboard";
            }
            }
            
       
        #endregion
    }
}


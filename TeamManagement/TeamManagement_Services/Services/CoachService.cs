using TeamManagement.Interfaces;
using TeamManagement.Models;
using TeamManagement_Services;

namespace TeamManagement.Services
{
    public class CoachService:ICoachService
    {
        #region Services
        private readonly IUserRepository _userRepository;
        private readonly IMailServices _mailServices;
        #endregion

        #region EnumVariables
        static int enumUserId = (int)RoleEnum.User;
        static int enumPlayerId = (int)RoleEnum.Player;
        static int enumCaptainId = (int)RoleEnum.Captain;
        static int enumCoachId = (int)RoleEnum.Coach;
        static int enumTeamPlayerId = (int)RoleEnum.TeamPlayer;
        static int enumCoachMaxCount = (int)RoleEnum.CoachMaxCount;
        #endregion

        #region DI
        public CoachService(IUserRepository userRepo, IMailServices mailServices)
        {
            _userRepository = userRepo;
            _mailServices = mailServices;

        }
        #endregion

        #region Methods
        public async Task<string> AddUser(string userEmail, int coachId)
        {
            User user_ = await _userRepository.GetUser(userEmail);
            User coach =  await _userRepository.GetUserByRoleId(coachId);

            if (user_ == null) return "First register yourself";
            if (user_.Equals(coach)) return "You are Coach.\n You are already Selected!";
            if (user_.RoleId == enumCaptainId) return "Provide user is Captain.He/she is already added.";
            if (user_.RoleId == enumPlayerId) return "Already selected by Coach!";
            if(user_.RoleId== enumTeamPlayerId) return $"{user_.FirstName} is already a Team-Player."; 
            if (coach.Count == enumCoachMaxCount) return "You as a Coach cannot add more than 15 players.";

            user_.RoleId = enumPlayerId;
            coach.Count += 1;

            await _userRepository.SaveUser(user_);
            await _userRepository.SaveUser(coach);
            await _mailServices.SendEmail(userEmail, "Selction By Coach", $"Hello {user_.FirstName}!\nYou are added as a Player by Coach.");
            return "Added successfully..";
        }

        public async Task<string> MakeCaptain(string captainEmail, int coachId)
        {
            List<User> userList = await _userRepository.GetUsersListById(enumCaptainId);

            if (userList.Count == 0)
            {
                User user1 = await _userRepository.GetUser(captainEmail);
                if (user1.RoleId == 1)
                {
                    User user = await _userRepository.GetUser(captainEmail);
                    user.RoleId = enumCaptainId;
                    await _userRepository.SaveUser(user);
                    await _mailServices.SendEmail(user.Email, "Captain", "You are Captain onwards");
                    return $"{user.FirstName} is Captain onwards...";
                }
                else
                {
                    return "First, select him/her in any Team and then only you can assign captaincy to him/her";
                }
            }
            else
            {
                return await UpdateCaptain(captainEmail, coachId);
            }
        }

        public async Task<string> UpdateCaptain(string captainEmail, int coachId)
        {
            User captain = await _userRepository.GetUserByRoleId(enumCaptainId);
            User user_ = await _userRepository.GetUser(captainEmail);
            if (captain.Equals(user_)) return $"{captain.FirstName} is already captain";
            if (user_.RoleId == enumUserId) return "First, select him/her in any Team and then only you can assign captaincy to him/her";
            if (user_.RoleId == enumPlayerId) return "Not in team! First let captain select him/her.";
            if (user_.RoleId == enumCoachId) return "Coach can not be a Captain";
            if (user_.RoleId == enumTeamPlayerId)
            {
                user_.Count = captain.Count;
                captain.Count = 0;
                captain.RoleId = enumTeamPlayerId;
                user_.RoleId = enumCaptainId;
                await _userRepository.SaveUser(user_);
                await _userRepository.SaveUser(captain);
                await _mailServices.SendEmail(captain.Email, "Removed Captain", $"Hello {captain.FirstName},You are no longer Captain now!");
                await _mailServices.SendEmail(user_.Email, "Captain", $"Hello {user_.FirstName},You are Captain onwards!");
            }
            return $"{user_.FirstName} is Captain onwards...";

        }
        #endregion

    }
}

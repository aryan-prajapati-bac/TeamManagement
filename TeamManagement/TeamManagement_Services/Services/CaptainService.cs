using TeamManagement.Interfaces;
using TeamManagement.Models;
using TeamManagement_Services;

namespace TeamManagement.Services
{
    public class CaptainService : ICaptainService
    {
        #region Services
        private readonly IUserRepository _userRepository;
        private readonly IMailServices _mailServices;
        #endregion

        #region EnumVariables
        static int enumUserId=(int)RoleEnum.User;
        static int enumPlayerId=(int)RoleEnum.Player;
        static int enumCaptainId = (int)RoleEnum.Captain;
        static int enumCoachId=(int)RoleEnum.Coach;
        static int enumTeamPlayerId=(int)RoleEnum.TeamPlayer;
        static int enumCaptainMaxCount=(int)RoleEnum.CaptainMaxCount;
        #endregion

        #region DI
        public CaptainService(IUserRepository userRepo,IMailServices mailservice)
        {
            _mailServices = mailservice;
            _userRepository = userRepo;
        }
        #endregion

        #region Methods
        public string SelectPlayer(string playerEmail, int captainId)
        {
            User user_ = _userRepository.GetUser(playerEmail);
            User captain = _userRepository.GetUserById(captainId);

            if (user_ == null) { return "Provided User is not available"; }            
            if (user_.RoleId == enumUserId) return $"First, {user_.FirstName} has to be added by Coach and then you can select {user_.FirstName} as Team-member";
            if (captain.Count == enumCaptainMaxCount) return "You cannot select more than 10 players";
            if (user_.RoleId == enumTeamPlayerId) return $"{user_.FirstName} is already in Team";
            if (user_.RoleId == enumPlayerId)
            {
                user_.RoleId = enumTeamPlayerId;
                captain.Count += 1;
                _userRepository.SaveUser(user_);
                 _userRepository.SaveUser(captain);
                _mailServices.SendEmail(user_.Email, "Selected in team!", "You are in a team now.");
                return "Selected in a team";
            }
            if (user_.RoleId == enumCaptainId) return "You are already in team";
            if (user_.RoleId == enumCoachId) return "Coach cannot be added in team";

            return $"{user_.FirstName} is already in Team";
        }
        #endregion
    }
}

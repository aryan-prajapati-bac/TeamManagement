using TeamManagement.Interfaces;
using TeamManagement.Models;

namespace TeamManagement.Services
{
    public class CaptainService : ICaptainService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMailServices _mailServices;
        public CaptainService(IUserRepository userRepo,IMailServices mailservice)
        {
            _mailServices = mailservice;
            _userRepository = userRepo;
        }


        public string SelectPlayer(string playerEmail, int captainId)
        {
            User user_ = _userRepository.GetUser(playerEmail);
            User captain = _userRepository.GetUserById(captainId);

            if (user_ == null) { return "Provided User is not available"; }            
            if (user_.RoleId == 0) return $"First, {user_.FirstName} has to be added by Coach and then you can select {user_.FirstName} as Team-member";
            if (captain.Count == 10) return "You cannot select more than 10 players";
            if (user_.RoleId == 11) return $"{user_.FirstName} is already in Team";
            if (user_.RoleId == 1)
            {
                user_.RoleId = 11;
                captain.Count += 1;
                _userRepository.SaveUser(user_);
                 _userRepository.SaveUser(captain);
                _mailServices.SendEmail(user_.Email, "Selected in team!", "You are in a team now.");
                return "Selected in a team";
            }
            if (user_.RoleId == 2) return "You are already in team";
            if (user_.RoleId == 3) return "Coach cannot be added in team";

            return $"{user_.FirstName} is already in Team";
        }
    }
}

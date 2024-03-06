using TeamManagement.Interfaces;
using TeamManagement.Models;

namespace TeamManagement.Services
{
    public class CoachService:ICoachService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMailServices _mailServices;

        public CoachService(IUserRepository userRepo, IMailServices mailServices)
        {
            _userRepository = userRepo;
            _mailServices = mailServices;

        }


        public string AddUser(string userEmail, int coachId)
        {
            User user_ = _userRepository.GetUser(userEmail);
            User coach = _userRepository.GetUserByRoleId(coachId);

            if (user_ == null) return "First register yourself";
            if (user_.Equals(coach)) return "You are Coach.\n You are already Selected!";
            if (user_.RoleId == 2) return "Provide user is Captain.He/she is already added.";
            if (user_.RoleId == 1) return "Already selected by Coach!";
            if(user_.RoleId==11) return $"{user_.FirstName} is already a Team-Player."; 
            if (coach.Count == 15) return "You as a Coach cannot add more than 15 players.";

            user_.RoleId = 1;
            coach.Count += 1;

            _userRepository.SaveUser(user_);
            _userRepository.SaveUser(coach);
            _mailServices.SendEmail(userEmail, "Selction By Coach", "You are now player assigned by Coach.");
            return "Added successfully..";
        }

        public string MakeCaptain(string captainEmail, int coachId)
        {
            List<User> userList = _userRepository.GetUsersListById(2);

            if (userList.Count == 0)
            {
                if (_userRepository.GetUser(captainEmail).RoleId == 1)
                {
                    User user = _userRepository.GetUser(captainEmail);
                    user.RoleId = 2;
                     _userRepository.SaveUser(user);
                    _mailServices.SendEmail(user.Email, "Captain", "You are Captain onwards");
                    return $"{user.FirstName} is Captain onwards...";
                }
                else
                {
                    return "First, select him/her in any Team and then only you can assign captaincy to him/her";
                }
            }
            else
            {
                return UpdateCaptain(captainEmail, coachId);
            }
        }

        public string UpdateCaptain(string captainEmail, int coachId)
        {
            User captain = _userRepository.GetUserByRoleId(2);
            User user_ = _userRepository.GetUser(captainEmail);
            if (captain.Equals(user_)) return $"{captain.FirstName} is already captain";
            if (user_.RoleId == 0) return "First, select him/her in any Team and then only you can assign captaincy to him/her";
            if (user_.RoleId == 1) return "Not in team! First let captain select him/her.";
            if (user_.RoleId == 3) return "Coach can not be a Captain";
            if (user_.RoleId == 11)
            {
                user_.Count = captain.Count;
                captain.Count = 0;
                captain.RoleId = 11;
                user_.RoleId = 2;
                _userRepository.SaveUser(user_);
                _userRepository.SaveUser(captain);
                _mailServices.SendEmail(captain.Email, "Removed Captain", "You are no longer Captain now...");
                _mailServices.SendEmail(user_.Email, "Captain", "You are Captain onwards");
            }
            return $"{user_.FirstName} is Captain onwards...";

        }

    }
}

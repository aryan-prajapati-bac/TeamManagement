using System.Text;
using TeamManagement.Models;

namespace TeamManagement.ViewModals
{
    public class CoachDTO
    {
        public string CoachName { get; set; }

        public string CoachEmail { get; set; }

        public string CaptainName { get; set; }
        public string CaptainEmail { get; set; }

        public List<User> TeamPlayers =new List<User>();

        public StringBuilder playerList = new StringBuilder("");

        public StringBuilder PlayerList(List<User> TeamPlayers)
        {
            if (TeamPlayers.Count > 0)
            {
                for (int i = 0; i < TeamPlayers.Count; i++)
                {
                    playerList.Append(TeamPlayers[i].FirstName + "(" + TeamPlayers[i].Email + ") ,");
                }
            }
            else
            {
                playerList.Append("Not created");
            }
            return playerList;
        }
        public override string ToString()
        {
            return $"Welcome {CoachName},You are coach.\n" +
                $"Your Email Id is {CoachEmail}\n" +
                $"Captain : {CaptainName}\n" +
                $"Captain Email : {CaptainEmail}\n" +
                $"Your Team : {PlayerList(TeamPlayers).ToString()}";
                
        }
    }
}

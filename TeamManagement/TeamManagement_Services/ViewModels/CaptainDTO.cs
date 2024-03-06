using System.Text;
using TeamManagement.Models;

namespace TeamManagement.ViewModals
{
    public class CaptainDTO
    {
        public string CaptainName { get; set; }
        public string CaptainEmail { get; set; }

        public List<User> TeamPlayers { get; set; }

        
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
                playerList.Append("You have not selected any player in your team");
            }
            return playerList;
        }
        

        public override string ToString()
        {
            return $"Welcome {CaptainName},You are Captain.\n" +
                $"Your Email Id is {CaptainEmail}\n" +
                $"Your Team : {PlayerList(TeamPlayers).ToString()}";
        }
    }
}

namespace TeamManagement.Interfaces
{
    public interface ICoachService
    {
        string AddUser(string userEmail, int coachId);

        string MakeCaptain(string captainEmail, int coachId);

    }
}

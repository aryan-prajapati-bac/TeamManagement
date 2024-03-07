namespace TeamManagement.Interfaces
{
    public interface ICoachService
    {
        Task<string> AddUser(string userEmail, int coachId);

        Task<string> MakeCaptain(string captainEmail, int coachId);

    }
}

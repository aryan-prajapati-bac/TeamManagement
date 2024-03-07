namespace TeamManagement.Interfaces
{
    public interface ICoachService
    {
        #region Method-Declaration
        Task<string> AddUser(string userEmail, int coachId);
        Task<string> MakeCaptain(string captainEmail, int coachId);
        #endregion

    }
}

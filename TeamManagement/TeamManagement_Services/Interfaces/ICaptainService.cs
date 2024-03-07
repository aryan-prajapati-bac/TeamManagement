namespace TeamManagement.Interfaces
{
    public interface ICaptainService
    {
        #region Method-Declaration
        Task<string> SelectPlayer(string playerEmail, int captainId);
        #endregion
    }
}

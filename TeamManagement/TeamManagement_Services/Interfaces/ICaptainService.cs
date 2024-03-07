namespace TeamManagement.Interfaces
{
    public interface ICaptainService
    {
        Task<string> SelectPlayer(string playerEmail, int captainId);
    }
}

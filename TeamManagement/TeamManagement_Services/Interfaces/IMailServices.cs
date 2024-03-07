namespace TeamManagement.Interfaces
{
    public interface IMailServices
    {
        #region Method-Declaration
        Task SendEmail(string to, string subject, string body);
        #endregion
    }
}

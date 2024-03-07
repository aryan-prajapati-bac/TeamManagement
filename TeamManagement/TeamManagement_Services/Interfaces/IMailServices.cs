namespace TeamManagement.Interfaces
{
    public interface IMailServices
    {
        Task SendEmail(string to, string subject, string body);
    }
}

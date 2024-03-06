namespace TeamManagement.Interfaces
{
    public interface IMailServices
    {
        void SendEmail(string to, string subject, string body);
    }
}

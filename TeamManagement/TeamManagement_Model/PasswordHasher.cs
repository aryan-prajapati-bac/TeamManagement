using System.Text;
using System.Security.Cryptography;

namespace TeamManagement
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            var sha256 = SHA256.Create();
            var byteValue = Encoding.UTF8.GetBytes(password);
            var byteHash = sha256.ComputeHash(byteValue);
            string hashedpwd = Convert.ToBase64String(byteHash);

            return hashedpwd;
        }
    }
}

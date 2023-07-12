using System.Security.Cryptography;
using System.Text;

namespace NeurofyMesh.Controllers
{
    public class PasswordController
    {
        public static string CreateTempPassword()
        {
            string tempPassword = "password123!";
            return HashPassword(tempPassword);
            
        }

        public static string HashPassword(string password)
        {
            SHA256 sha = SHA256.Create();
            byte[] asByteArray = Encoding.Default.GetBytes(password);
            byte[] hashedPassword = sha.ComputeHash(asByteArray);

            return Convert.ToBase64String(hashedPassword);
        }

        public static bool ValidatePassword(string password, string hashedPassword) 
        {
            string encodedPassword = HashPassword(password);

            return encodedPassword.Equals(hashedPassword); 
        }
    }
}

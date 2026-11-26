using System.Security.Cryptography;
using System.Text;

namespace Domain
{
    public static class HashGenerator
    {
        public static string GetHashForText(string text)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(text);
                byte[] hashedBytes = sha256.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2")); // Convert each byte to a hexadecimal string
                }
                return builder.ToString();
            }
        }
    }
}

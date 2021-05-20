using System.Security.Cryptography;
using System.Text;

namespace DataServices
{
    public class HashingService
    {
        public string Hashing(string text)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
            
            text = string.Empty;
            foreach (var item in bytes)
            {
                text+=string.Format("{0:x2}", item);
            }

            return text;
        }
    }
}
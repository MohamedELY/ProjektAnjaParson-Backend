using System.Security.Cryptography;
using System.Text;

namespace ProjektAnjaParson_Backend.Security
{

    public static class Hash
    {
        public static string Execute(string rawData)
        {
            // Create a SHA256 
            using (SHA256 mySHA256 = SHA256.Create())
            {
                //Get bytes from string and hash
                byte[] securedData = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < securedData.Length; i++)
                {
                    builder.Append(securedData[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}

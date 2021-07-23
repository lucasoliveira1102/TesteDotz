using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Dotz.Core.Infraestrutura.Seguranca
{
    public static class AES256Crypto
    {
        public static string Criptografar(string texto, string senha = "Z+C03BEJqlDi17cB4Re6cJY5XTpvya5FtP4AVDYyBzE=",
            string salt = "Kosher", string algoritmoHash = "SHA1",
            int iteracoesSenha = 2, string vectorInicial = "OFRna73m*aze01xY")
        {
            if (String.IsNullOrWhiteSpace(texto))
            {
                return texto;
            }   

            try
            {
                byte[] initialVectorBytes = Encoding.ASCII.GetBytes(vectorInicial);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(texto);
                var derivedPassword = new PasswordDeriveBytes(senha, saltValueBytes, algoritmoHash, iteracoesSenha);
                byte[] keyBytes = Convert.FromBase64String(senha);

                var rijndaelCipher = new RijndaelManaged();
                rijndaelCipher.Mode = CipherMode.ECB;
                byte[] cipherTextBytes;
                using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(keyBytes, initialVectorBytes))
                {
                    using (var memStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            cipherTextBytes = memStream.ToArray();
                            memStream.Close();
                            cryptoStream.Close();
                        }
                    }
                }
                rijndaelCipher.Clear();
                return Convert.ToBase64String(cipherTextBytes);
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        public static string Descriptografar(string texto, string senha = "Z+C03BEJqlDi17cB4Re6cJY5XTpvya5FtP4AVDYyBzE=",
            string salt = "Kosher", string algoritmoHash = "SHA1",
            int iteracoesSenha = 2, string vectorInicial = "OFRna73m*aze01xY")
        {
            if (String.IsNullOrWhiteSpace(texto))
            {
                return texto;
            }

            try
            {
                byte[] initialVectorBytes = Encoding.ASCII.GetBytes(vectorInicial);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
                byte[] cipherTextBytes = Convert.FromBase64String(texto);
                var derivedPassword = new PasswordDeriveBytes(senha, saltValueBytes, algoritmoHash, iteracoesSenha);
                byte[] keyBytes = Convert.FromBase64String(senha);

                var symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.ECB;
                var plainTextBytes = new byte[cipherTextBytes.Length];
                int byteCount;
                using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initialVectorBytes))
                {
                    using (var memStream = new MemoryStream(cipherTextBytes))
                    {
                        using (var cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read))
                        {

                            byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                            memStream.Close();
                            cryptoStream.Close();
                        }
                    }
                }
                symmetricKey.Clear();
                return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }
    }
}

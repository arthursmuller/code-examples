using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Infraestrutura
{
    public class AesCryptography
    {
        private byte[] _key;
        private byte[] _IV;

        public AesCryptography(string Key, string IV)
        {
            _key = Encoding.UTF8.GetBytes(Key);
            _IV = Encoding.UTF8.GetBytes(IV);
        }

        public string Encrypt(string data)
        {
            if (data == null || data.Length <= 0)
                throw new ArgumentNullException("data");
            if (_key == null || _key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (_IV == null || _IV.Length <= 0)
                throw new ArgumentNullException("IV");

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _key;
                aesAlg.IV = _IV;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                            swEncrypt.Write(data);

                        var encrypted = msEncrypt.ToArray();

                        return Convert.ToBase64String(encrypted);
                    }
                }
            }
        }

        public string Decrypt(string data)
        {
            var bytes = Convert.FromBase64String(data);

            if (bytes == null || bytes.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (_key == null || _key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (_IV == null || _IV.Length <= 0)
                throw new ArgumentNullException("IV");

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _key;
                aesAlg.IV = _IV;

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(bytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                            return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}

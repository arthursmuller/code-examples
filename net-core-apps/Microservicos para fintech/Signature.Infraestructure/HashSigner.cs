using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Signature.Infraestructure
{
    internal class HashDto
    {
        public string Original { get; set; }
        public string Signed{ get; set; }
    }

    internal class HashSigner
    {
        public static HashDto GenerateSignatureHash(byte[] dataToSign)
        {
            var hash = ComputeHash(dataToSign);
            var signedHash = Sign(hash);

            return new HashDto
            {
                Original = Convert.ToBase64String(hash),
                Signed = Convert.ToBase64String(signedHash)
            };
        }
        public static bool VerifySignatureHash(string originalHashBase64, string signedHashBase64)
        {
            var originalHash = Convert.FromBase64String(originalHashBase64);
            var signedHash = Convert.FromBase64String(signedHashBase64);
            var valid = Verify(originalHash, signedHash);
            return valid;
        }
        public static byte[] Sign(byte[] hashToSign)
        {
            var path = "Signature.Domain/Resources/Cert";
            var parentDir = Directory.GetParent(Environment.CurrentDirectory).FullName;
            var filePath = $"{parentDir}/{path}";
            var dir = Path.Combine(parentDir.Length > 1 ? filePath : path, "signing-cert.pfx");

            using (X509Certificate2 certificate = new X509Certificate2(dir, "+HVUsd22&%N85BFD=98234n*23lJadsmhHGHF=_"))
            {
                using (RSA rsa = certificate.GetRSAPrivateKey())
                {
                    byte[] signature = rsa.SignHash(hashToSign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    return signature;
                }
            }
        }
        public static bool Verify(byte[] original, byte[] hash)
        {
            var path = "Signature.Domain/Resources/Cert";
            var parentDir = Directory.GetParent(Environment.CurrentDirectory).FullName;
            var filePath = $"{parentDir}/{path}";
            var dir = Path.Combine(parentDir.Length > 1 ? filePath : path, "signing-cert.pfx");

            using (X509Certificate2 certificate = new X509Certificate2(dir, "+HVUsd22&%N85BFD=98234n*23lJadsmhHGHF=_"))
            {
                using (RSA rsa = certificate.GetRSAPublicKey())
                {

                    byte[] originalHash = ComputeHash(original);
                    bool isDataValid = rsa.VerifyHash(originalHash, hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    return isDataValid;
                }
            }
        }
        private static byte[] ComputeHash(byte[] data)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(data);
            }
        }
    }
}

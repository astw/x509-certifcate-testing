using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace WindowsFormsApplication1
{
    //Applies to PFX format certificates 
    public class DigitalSignManager
    {
        private readonly string certificateFile;
        private readonly X509Certificate2 certificate;
        private readonly RSACryptoServiceProvider privateKey; 
        private readonly RSACryptoServiceProvider publicKey; 
        private readonly string certificatePassword; 

        public DigitalSignManager(string certificateFile, string certificatePassword)
        {
            this.certificateFile = certificateFile;
            this.certificatePassword = certificatePassword;
            try
            {
                this.certificate = new X509Certificate2(certificateFile, certificatePassword);
                this.privateKey = (RSACryptoServiceProvider) this.certificate.PrivateKey;
                this.publicKey = (RSACryptoServiceProvider) this.certificate.PublicKey.Key;  

                if(privateKey == null || this.publicKey == null)
                {
                    throw new Exception(message: "Invalid certificate"); 
                }
            }
            catch (CryptographicException e)
            {
                System.Console.WriteLine(e.Message);
                throw;
            }
        }

        public string GetDataSignature(byte[] data)
        {
            return Convert.ToBase64String(this.SignData(data));
        }

        public string GetDataSignature(string base64Data)
        {
            var dataBytes = Convert.FromBase64String(base64Data);
            return this.GetDataSignature(dataBytes);
        }

        public byte[] SignData(byte[] data)
        {
            var halg = new SHA1CryptoServiceProvider();
            return this.privateKey.SignData(data, halg);
        }

        public bool VerifySignature(byte[] originData, byte[] signedData)
        {
            var halg = new SHA1CryptoServiceProvider();
            return this.publicKey.VerifyData(originData, halg, signedData);
        }

        public bool VerifySignature(string originData,  string signedData)
        {
            var originDataBytes = Convert.FromBase64String(originData);
            var signedDataBytes = Convert.FromBase64String(signedData);
            return VerifySignature(originDataBytes, signedDataBytes);
        } 
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void CertificateClicked(object sender, EventArgs e)
        {
            //var certFile = @"C:\work\certificate\openssl\fd-export-from-import.pfx";
            //var certPassword = "watertrax";
            //var plainText = "This is the text to be encrypted"; 

            //X509Certificate2 cert = new X509Certificate2(certFile, certPassword);

            //RSACryptoServiceProvider privateKeyRSACryptoServiceProvider = (RSACryptoServiceProvider) cert.PrivateKey; 
            //RSACryptoServiceProvider publicKeyRSACryptoServiceProvider = (RSACryptoServiceProvider) cert.PublicKey.Key; 


            //var encryptedBytes = publicKeyRSACryptoServiceProvider.Encrypt(plainBytes, false);  
            //var decryptBytes = privateKeyRSACryptoServiceProvider.Decrypt(encryptedBytes, false);
            //var decryptedText = Encoding.UTF8.GetString(decryptBytes);  


            Test_PFX();

            Test_CER();
        }

        private void Test_Cer()
        {
            var plainText = "This is the text to be encrypted";

            var cert = Get_PFX_Certificate();
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = Encrypt(plainBytes, cert);
             
        }

        private void Test_PFX()
        {
            var plainText = "This is the text to be encrypted";

            var cert = Get_PFX_Certificate();
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = Encrypt(plainBytes, cert);

            // ... 
            var decryptedBytes = Decrypt(encryptedBytes, cert);
            var decryptText = Encoding.UTF8.GetString(decryptedBytes);

            if(plainText.Equals(decryptText) == false)
            {
               Console.WriteLine("Encryption or decrypt failed -- PFX");
            } else
            {
                Console.WriteLine("Encryption or decrypt finished successfully -- PFX ");
            }
        }
        


        /// <summary>
        /// Here to create import the certificate which only include public key  (CRT) 
        /// </summary>
        private void Test_CER()
        {
            var plainText = "This is the text to be encrypted";

            /// Here to create import the certificate which only include public key  (CRT) 
            var cert = Get_CER_Certificate();
            var plainBytes = Encoding.UTF8.GetBytes(plainText); 
            var encryptedBytes = Encrypt(plainBytes, cert);


            /// here to load user private key
            var cert2 = Get_Certificate_Private();
            var decryptedBytes = Decrypt(encryptedBytes, cert2); 

            var decryptText = Encoding.UTF8.GetString(decryptedBytes);

            if (plainText.Equals(decryptText) == false)
            {
                Console.WriteLine("Encryption or decrypt failed -- CER");
            }
            else
            {
                Console.WriteLine("Encryption or decrypt finished successfully -- CER ");
            }

        }

        private X509Certificate2 Get_Certificate_Private()
        {
            var certFile = @"C:\work\certificate\openssl\fd\fd-private-exported.pfx";
            var certPassword = "watertrax"; 

            X509Certificate2 cert = new X509Certificate2(certFile, certPassword); 

            return cert;
        }

        private X509Certificate2 Get_CER_Certificate()
        {
            var certFile = @"C:\work\certificate\openssl\fd\fd.crt";
            var certPassword = "watertrax";

            X509Certificate2 cert = new X509Certificate2();
            cert.Import(certFile); 

            return cert;
        }
         
        private X509Certificate2 Get_PFX_Certificate()
        {
            var certFile = @"C:\work\certificate\openssl\fd\fd-export-from-import.pfx"; 
            var certPassword = "watertrax";
            
            X509Certificate2 cert = new X509Certificate2(certFile, certPassword);
            return cert;
        }

        private byte[] Encrypt(byte[] data, X509Certificate2 cert)
        {
            RSACryptoServiceProvider publicKeyRSACryptoServiceProvider = (RSACryptoServiceProvider) cert.PublicKey.Key;
            return publicKeyRSACryptoServiceProvider.Encrypt(data, false);
        }

        private byte[] Decrypt(byte[] data, X509Certificate2 cert)
        {
            RSACryptoServiceProvider privateKeyRSACryptoServiceProvider = (RSACryptoServiceProvider) cert.PrivateKey;
            return privateKeyRSACryptoServiceProvider.Decrypt(data, false); 
        }

        private void ZipStreamClicked(object sender, EventArgs e)
        {
            var file = @"C:\temp\test.txt";

            var data = System.IO.File.ReadAllBytes(file);

            var str = "1234";

            var byteUCS = Encoding.GetEncoding(1201);
            var strData = byteUCS.GetBytes(str);

            using (var sha256 = new SHA256Managed())
            {
                var freshDigestData = sha256.ComputeHash(strData); 
                var hashedBase64 = Convert.ToBase64String(freshDigestData); 
            }

            using (FileStream zipToOpen = new FileStream(@"c:\users\exampleuser\release.zip", FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry readmeEntry = archive.CreateEntry("Readme.txt");
                    using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                    {
                        writer.WriteLine("Information about this package.");
                        writer.WriteLine("========================");
                    }
                }
            }
        }
    }
}
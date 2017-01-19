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

        private void zipfolder_Click(object sender, EventArgs e)
        {
            var folder = @"..\..\fileStore\source\";
            var uniqueName = Guid.NewGuid().ToString();
            var targetFileName = @"..\..\fileStore\target\" + uniqueName + ".zip";

            var sourceFileHashes = GetFileHashes(folder); 
             
            ZipFile.CreateFromDirectory(folder, targetFileName);

            ZipFile.ExtractToDirectory(targetFileName, @"..\..\fileStore\target\" + uniqueName);

            var targetFileHashes = GetFileHashes(@"..\..\fileStore\target\" + uniqueName);

            for(int i=0; i< sourceFileHashes.Count; i++)
            {
                var sfh = sourceFileHashes[i]; 
                var tfs = targetFileHashes[i];
                if (sfh.Equals(tfs))
                {
                    Console.WriteLine("File:{0} passed", sfh.FileName);
                }
            }

        }

        private void CompressToZip(string outPutfileName, Dictionary<string, byte[]> fileList)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, leaveOpen: true))
                {
                    foreach (var file in fileList)
                    {
                        var demoFile = archive.CreateEntry(file.Key);

                        using (var entryStream = demoFile.Open())
                        using (var b = new BinaryWriter(entryStream))
                        {
                            b.Write(file.Value);
                        }
                    }
                }

                using (var fileStream = new FileStream(outPutfileName, FileMode.Create))
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    memoryStream.CopyTo(fileStream);
                }
            }
        }

        private List<FileHash> GetFileHashes(string folder)
        {
            var hashes = new List<FileHash>();

            var files = Directory.GetFiles(folder);  
            hashes = files.Select(fileName =>
            {
                return new FileHash(fileName, CalculateFileHash(fileName));
            }).ToList<FileHash>();

            return hashes;   
        }


        private string CalculateFileHash(string fileName)
        { 
            var data = File.ReadAllBytes(fileName);
            return ComputeHash(data);
        }

        private string ComputeHash(byte[] data)
        {
            using (var sha256 = new SHA256Managed())
            {
                var hash = sha256.ComputeHash(data);

                var hex = ByteArrayToString(hash).ToLowerInvariant();
                return hex;
            }
        }

        private string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        private void zipFolderAndEncryptHash_Click(object sender, EventArgs e)
        {
            var folder = @"..\..\fileStore\source\";
            var uniqueName = Guid.NewGuid().ToString();
            var targetFileName = @"..\..\fileStore\target\" + uniqueName + ".zip";

            var sourceFileHashes = GetFileHashes(folder);

            ZipFile.CreateFromDirectory(folder, targetFileName);

            var zipFileHash = CalculateFileHash(targetFileName);
            var hashBytes = Encoding.UTF8.GetBytes(zipFileHash); 

            var cert = Get_PFX_Certificate();
            RSACryptoServiceProvider privateKeyRSACryptoServiceProvider = (RSACryptoServiceProvider) cert.PrivateKey;
            RSACryptoServiceProvider publicKeyRSACryptoServiceProvider = (RSACryptoServiceProvider) cert.PublicKey.Key;

            //Other program is using public key to encrypt 
            var encryptedHashBytes = publicKeyRSACryptoServiceProvider.Encrypt(hashBytes, false);

            //This program is using private key to decrypt   
            var decryptedHashBytes = privateKeyRSACryptoServiceProvider.Decrypt(encryptedHashBytes, false); 

            var decryptedHashString = Encoding.UTF8.GetString(decryptedHashBytes);

            Console.WriteLine("Hash of the zipped file: {0}", zipFileHash);
            Console.WriteLine("decryptedHash: {0}", decryptedHashString);

            if (decryptedHashString.Equals(zipFileHash))
            {
                Console.WriteLine("Hash of the zipped file: {0}, decryptedHash {1} ", zipFileHash, decryptedHashString);
            }
        }

        private void zipFolderAndSignHash(object sender, EventArgs e)
        {
            var folder = @"..\..\fileStore\source\";
            var uniqueName = Guid.NewGuid().ToString();
            var targetFileName = @"..\..\fileStore\target\" + uniqueName + ".zip";

            var sourceFileHashes = GetFileHashes(folder);

            ZipFile.CreateFromDirectory(folder, targetFileName);

            var zipFileHash = CalculateFileHash(targetFileName);
            var originalHashBytes = Encoding.UTF8.GetBytes(zipFileHash);

            var cert = Get_PFX_Certificate();
            RSACryptoServiceProvider privateKeyRSACryptoServiceProvider = (RSACryptoServiceProvider) cert.PrivateKey;
            RSACryptoServiceProvider publicKeyRSACryptoServiceProvider = (RSACryptoServiceProvider) cert.PublicKey.Key;

            // https://msdn.microsoft.com/en-us/library/9tsc5d0z(v=vs.110).aspx  
            //-----------------------------------------------------
            byte[] signedHashBytes = null;
            var halg = new SHA1CryptoServiceProvider();

            string hashBytesSignature = null; 
            try
            {
               //---- sign data using private key    
                signedHashBytes = privateKeyRSACryptoServiceProvider.SignData(originalHashBytes, halg); 
                hashBytesSignature = Convert.ToBase64String(signedHashBytes);
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //--- Other program varify data using public key
            try
            {
               if( publicKeyRSACryptoServiceProvider.VerifyData(originalHashBytes, halg, signedHashBytes))
                {
                    Console.WriteLine("The data was verified 1.");
                }
                else
                {
                    Console.WriteLine("The data does not match the signature.");
                }
            } 
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }

            // verify the data from hashBytesSignature (string) 
            var hashBytesSignature2 = Convert.FromBase64String(hashBytesSignature);  
            if (publicKeyRSACryptoServiceProvider.VerifyData(originalHashBytes, halg, hashBytesSignature2))
            {
                Console.WriteLine("The data was verified 2.");
            }
            else
            {
                Console.WriteLine("The data does not match the signature.");
            } 
        }
    }

    class FileHash
    {
        public string FileName { get; set; }
        public string Hash { get; set; }
        public FileHash(string fileName, string hash)
        {
            this.FileName = Path.GetFileName(fileName); 
            this.Hash = hash;
        }
         
        public override bool Equals(object obj)
        {
            return this.FileName == ((FileHash) obj).FileName && this.Hash == ((FileHash) obj).Hash;
        }
    }
}
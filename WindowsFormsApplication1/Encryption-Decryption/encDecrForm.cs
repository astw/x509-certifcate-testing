using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Encryption_Decryption
{
    public partial class encDecrForm : Form
    {
        public encDecrForm()
        {
            InitializeComponent();
        }

        private void FindFolderBtn_Click(object sender, EventArgs e)
        {
            var sourceFolder = "";
            var targetFolder = ""; 

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    sourceFolder = fbd.SelectedPath; 
                }
            }
             

            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = sourceFolder;

                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    targetFolder = fbd.SelectedPath;
                }
            } 

            // zip the folder into a zip file  
            var zippedFile = ZipFolder(sourceFolder, targetFolder); 
            // encrypt the file  

            var encryptedFile = zippedFile + ".en";
            EncryptFile(zippedFile, encryptedFile);

            MessageBox.Show("zip finished"); 


        }

        private string ZipFolder(string sourceFolder, string targetFolder)
        {
            var uniqueName = Guid.NewGuid().ToString();
            var targetFile = Path.Combine(targetFolder, uniqueName + ".zip"); 

            ZipFile.CreateFromDirectory(sourceFolder, targetFile);
            return targetFile;
        } 

        private void encrypt_Click(object sender, EventArgs e)
        {
        }


        private void EncryptFile(string inputFile, string outputFile)
        {
            try
            {
                string password = this.passwordText.Text;
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte) data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch
            {
                MessageBox.Show("Encryption failed!", "Error");
            }
        }

        private void DecryptFile(string inputFile, string outputFile)
        {
            {
                string password = this.passwordText.Text;

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte) data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();

            }
        }

        private void decrypt_Click(object sender, EventArgs e)
        {

            // select the encryted file 
            var encryptedFile = "";

            OpenFileDialog od = new OpenFileDialog();
            od.InitialDirectory = "..\\..\\";

            if (od.ShowDialog() == DialogResult.OK)
            {
                encryptedFile = od.FileName;
            }

            // select the target folder for decryption  

            var decryptTargetFolder = ""; 

            var decryptionFile = encryptedFile + ".zip"; 
            DecryptFile(encryptedFile, decryptionFile);
        }

        private void passwordText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

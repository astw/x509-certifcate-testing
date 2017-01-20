using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.IO.Compression;
using System.Xml;
using System.Text;

namespace WindowsFormsApplication1
{
    public class CoreManager
    {
        private readonly DigitalSignManager digitalSignManager;

        public CoreManager(DigitalSignManager digitalSignManager)
        {
            this.digitalSignManager = digitalSignManager;
        }

        public CoreManifest CreateCoreFromFolder(string sourceFolder, string targetFolder)
        {
            var uniqueName = Guid.NewGuid().ToString();
            var coreFile = Path.Combine(targetFolder, uniqueName + ".zip");   
            var manifestFile = Path.Combine(targetFolder, uniqueName + "_manifest.xml");
             
            ZipFile.CreateFromDirectory(sourceFolder, coreFile);

            var coreFileElements = GetFileHashes(sourceFolder);

            var coreHash = HashHelper.CalculateFileHash(coreFile);

            var coreManifest =  new CoreManifest
            {
                CoreFileFullName = coreFile,
                CoreFileName = Path.GetFileName(coreFile),
                CoreHash = coreHash,
                FileNodes = coreFileElements,
                ManifestFile = manifestFile
            }; 

            coreManifest.CoreSignature  = GetCoreSignature(coreManifest);

            CreateCoreManifest(manifestFile, coreManifest);

            return coreManifest;
        }

        public string GetCoreSignature(CoreManifest manifest)
        {
            var hash = manifest.CoreHash;
            var hashBytes = Encoding.UTF8.GetBytes(hash);
            var hashBase64 = Convert.ToBase64String(hashBytes); 

            return this.digitalSignManager.GetDataSignature(hashBase64); 
        }

        public bool VerifyCoreSignature(CoreManifest manifest)
        {
            // Get New signature 
            var coreFile = manifest.CoreFileFullName; 
            var currentCoreFileHash = HashHelper.CalculateFileHash(coreFile);  

            var currentHashBytes = Encoding.UTF8.GetBytes(currentCoreFileHash);   
            var currentHashBase64 = Convert.ToBase64String(currentHashBytes);

            var orginHash = manifest.CoreHash;
            var orginHashBytes = Encoding.UTF8.GetBytes(orginHash);
            var originHashBase64 = Convert.ToBase64String(orginHashBytes); 

            // compare with old signature
            return digitalSignManager.VerifySignature(originHashBase64, manifest.CoreSignature); 
        }

        private List<CoreFileElement> GetFileHashes(string folder)
        {
            var hashes = new List<CoreFileElement>();

            var files = Directory.GetFiles(folder);
            hashes = files.Select(fileName =>
            {
                return new CoreFileElement(fileName, HashHelper.CalculateFileHash(fileName));
            }).ToList<CoreFileElement>();

            return hashes;
        }

        private void CreateCoreManifest(string manifestFile, CoreManifest coreManifest)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration(version: "1.0", encoding: "UTF-8", standalone: null);
            doc.AppendChild(docNode);

            XmlNode coreNode = doc.CreateElement(name: "Core");
            doc.AppendChild(coreNode);

            XmlNode coreFileName = doc.CreateElement(name: "FileName");
            coreFileName.AppendChild(doc.CreateTextNode(coreManifest.CoreFileName));
            coreNode.AppendChild(coreFileName);

            XmlNode coreHash = doc.CreateElement(name: "CoreHash");
            coreHash.AppendChild(doc.CreateTextNode(coreManifest.CoreHash));
            coreNode.AppendChild(coreHash);

            XmlNode coreSignature = doc.CreateElement(name: "Signature");
            coreSignature.AppendChild(doc.CreateTextNode(coreManifest.CoreSignature));
            coreNode.AppendChild(coreSignature);

            XmlNode manifest = doc.CreateElement(name: "Manifest");
            coreNode.AppendChild(manifest);

            XmlNode files = doc.CreateElement(name: "Files");
            manifest.AppendChild(files);

            foreach (var fileInfo in coreManifest.FileNodes)
            {
                XmlNode file = doc.CreateElement(name: "File");
                var fileName = doc.CreateElement(name: "Name");
                fileName.AppendChild(doc.CreateTextNode(fileInfo.FileName));
                file.AppendChild(fileName);

                var fileType = doc.CreateElement(name: "Type");
                fileType.AppendChild(doc.CreateTextNode(fileInfo.FileType));
                file.AppendChild(fileType);

                var Hash = doc.CreateElement(name: "Hash");
                Hash.AppendChild(doc.CreateTextNode(fileInfo.Hash));
                file.AppendChild(Hash);


                var size = doc.CreateElement(name: "Size");
                size.AppendChild(doc.CreateTextNode(fileInfo.Size.ToString()));
                file.AppendChild(size);

                files.AppendChild(file);
            }


            doc.Save(manifestFile);
        }
    }
}
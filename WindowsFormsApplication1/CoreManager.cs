using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.IO.Compression;

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
            var coreFile = targetFolder + uniqueName + ".zip";
            coreFile = coreFile.Replace(oldValue: @"\\", newValue: @"\");

            ZipFile.CreateFromDirectory(sourceFolder, coreFile);

            var coreFileElements = GetFileHashes(sourceFolder);

            var coreHash = HashHelper.CalculateFileHash(coreFile);

            return new CoreManifest
            {
                CoreFileFullName = coreFile,
                CoreFileName = Path.GetFileName(coreFile),
                CoreHash = coreHash,
                FileNodes = coreFileElements
            };
        }

        public string GetCoreSignature(CoreManifest manifest)
        {   
            return this.digitalSignManager.GetDataSignature(manifest.CoreHash); 
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
    }
}
using System.IO;

namespace WindowsFormsApplication1
{
    public class CoreFileElement
    {
        public string FileName { get; set; }
        public string FullFileName { get; set; }
        public string Hash { get; set; }
        public string FileType { get; set; }
        public long Size { get; set; }

        public CoreFileElement() { }
        public CoreFileElement(string fileName, string hash)
        {
            this.FullFileName = fileName;
            this.FileName = Path.GetFileName(fileName);
            this.Hash = hash;

            this.FileType = Path.GetExtension(fileName); 
            var fileInfo = new FileInfo(fileName);
            this.Size = fileInfo.Length;  
        }

        public CoreFileElement(string fileName, string hash, string fileType, long size)
        {
            this.FullFileName = fileName;
            this.FileName = Path.GetFileName(fileName);
            this.Hash = hash;
            this.FileType = fileType;
            this.Size = size; 
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return this.FileName == ((CoreFileElement) obj).FileName && this.Hash == ((CoreFileElement) obj).Hash;
        }
    }
}
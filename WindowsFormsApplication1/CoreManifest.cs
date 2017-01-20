using System.Collections.Generic;

namespace WindowsFormsApplication1
{
    public class CoreManifest
    {
        public List<CoreFileElement> FileNodes { get; set; }
        public string CoreFileName { get; set; }
        public string CoreFileFullName { get; set; }
        public string CoreHash { get; set; }
    }
}
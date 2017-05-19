using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApplication1
{
    public partial class XmlSortFrm : Form
    {
        public XmlSortFrm()
        {
            InitializeComponent();
        }

        private void XmlSortFrm_Load(object sender, EventArgs e)
        {
            var path = @"C:\Users\swang\Downloads\aaaa-reference--- 1st Quarter PCR 20170503 (5)\Copy Of Record Data.xml";

            var path2 = @"C:\Users\swang\Downloads\aaaa-reference--- 1st Quarter PCR 20170503 (5)\Copy Of Record Data-small.xml";

            var xdoc = XDocument.Load(path);
            var root = xdoc.Root;
            var elements = root.Elements().ToList();
            var samples = elements.Where(i => i.Name.LocalName == "Samples");
            var header = elements.Single(i => i.Name.LocalName == "ReportHeader");
            var headerCloned = new XElement(header);

            MessageBox.Show(xdoc.ToString().Length.ToString());

            foreach (var sampleNode in samples)
            {
                sampleNode.Remove();
            }


            MessageBox.Show(xdoc.ToString().Length.ToString());

            var newXDoc = new XDocument();
            //            newXDoc.Add(new XElement(new XElement("Cor")));

            newXDoc.Add(new XElement("Cor"));
            var newRoot = newXDoc.Root;

            var tags = elements.Select(i => i.Name.LocalName);
            var comments = elements.Single(i => i.Name.LocalName == "Comment");
            if (comments != null)
            {
                newRoot.Add(comments);
            }

            var fileManifest = elements.Single(i => i.Name.LocalName == "FileManifest");

            newRoot.Add(fileManifest);
            newRoot.Add(headerCloned);


            newXDoc.Save(path2);
        }
    }
}

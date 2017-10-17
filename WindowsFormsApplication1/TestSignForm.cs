using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class TestForm : Form
    {
        string certFile = @"C:\work\certificate\openssl\fd\fd-export-from-import.pfx";
        string certPassword = "watertrax";

        string sourceFolder = @"..\..\fileStore\source\";
        string targetFolder = @"..\..\fileStore\target\";

        DigitalSignManager digitalSignManager;
        CoreManager coreManager;

        public TestForm()
        {
            InitializeComponent();
             
            this.digitalSignManager = new DigitalSignManager(certFile, certPassword);

            ICoreManifestSerializer xmlCoreManifestSerializer = new XmlCoreManifestSerializer();
            ICoreManifestSerializer dbCoreManifestSerializer = new XmlCoreManifestSerializer();

            this.coreManager = new CoreManager(digitalSignManager, xmlCoreManifestSerializer, dbCoreManifestSerializer);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var coreManifest = coreManager.CreateCoreFromFolder(sourceFolder, targetFolder);

            if (coreManager.VerifyCoreSignature(coreManifest))
            {
                System.Console.WriteLine("Core signature is good ");
            }
        }

        private void VerifyCoreBtn_Click(object sender, EventArgs e)
        { 
            OpenFileDialog od = new OpenFileDialog();
            od.InitialDirectory = "..\\..\\"; 

            if(od.ShowDialog() == DialogResult.OK)
            {
                var fileName = od.FileName;
                var coreManifest = coreManager.ParseManifest(fileName);
                if (coreManager.VerifyCoreSignature(coreManifest))
                {
                    MessageBox.Show("Sigature good"); 
                }
                else
                {
                    MessageBox.Show("Sigature bad");
                }
            } 
        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }
    }
}

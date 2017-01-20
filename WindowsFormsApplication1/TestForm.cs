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
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var certFile = @"C:\work\certificate\openssl\fd\fd-export-from-import.pfx";
            var certPassword = "watertrax";

            var sourceFolder = @"..\..\fileStore\source\";
            var targetFolder = @"..\..\fileStore\target\";
            
            var digitalSignManager = new DigitalSignManager(certFile, certPassword);
            var coreManager = new CoreManager(digitalSignManager);
            var coreManifest = coreManager.CreateCoreFromFolder(sourceFolder, targetFolder);

            if (coreManager.VerifyCoreSignature(coreManifest))
            {
                System.Console.WriteLine("Core signature is good ");
            }
        }
    }
}

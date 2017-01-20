namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.zipStreamButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.zipfolder = new System.Windows.Forms.Button();
            this.zipFolderAndEncryptHash = new System.Windows.Forms.Button();
            this.zipfoldAndSignHash = new System.Windows.Forms.Button();
            this.findCertificationFromLocal = new System.Windows.Forms.Button();
            this.createXMLBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.zipFolderAndSignHash2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // zipStreamButton
            // 
            this.zipStreamButton.Location = new System.Drawing.Point(57, 49);
            this.zipStreamButton.Name = "zipStreamButton";
            this.zipStreamButton.Size = new System.Drawing.Size(274, 23);
            this.zipStreamButton.TabIndex = 0;
            this.zipStreamButton.Text = "Zip stream";
            this.zipStreamButton.UseVisualStyleBackColor = true;
            this.zipStreamButton.Click += new System.EventHandler(this.ZipStreamClicked);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(57, 115);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(274, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Certificate";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.CertificateClicked);
            // 
            // zipfolder
            // 
            this.zipfolder.Location = new System.Drawing.Point(57, 163);
            this.zipfolder.Name = "zipfolder";
            this.zipfolder.Size = new System.Drawing.Size(274, 23);
            this.zipfolder.TabIndex = 2;
            this.zipfolder.Text = "zip folder ";
            this.zipfolder.UseVisualStyleBackColor = true;
            this.zipfolder.Click += new System.EventHandler(this.zipfolder_Click);
            // 
            // zipFolderAndEncryptHash
            // 
            this.zipFolderAndEncryptHash.Location = new System.Drawing.Point(57, 211);
            this.zipFolderAndEncryptHash.Name = "zipFolderAndEncryptHash";
            this.zipFolderAndEncryptHash.Size = new System.Drawing.Size(274, 23);
            this.zipFolderAndEncryptHash.TabIndex = 3;
            this.zipFolderAndEncryptHash.Text = "zip folder and encrypt the hash";
            this.zipFolderAndEncryptHash.UseVisualStyleBackColor = true;
            this.zipFolderAndEncryptHash.Click += new System.EventHandler(this.zipFolderAndEncryptHash_Click);
            // 
            // zipfoldAndSignHash
            // 
            this.zipfoldAndSignHash.Location = new System.Drawing.Point(57, 277);
            this.zipfoldAndSignHash.Name = "zipfoldAndSignHash";
            this.zipfoldAndSignHash.Size = new System.Drawing.Size(274, 23);
            this.zipfoldAndSignHash.TabIndex = 4;
            this.zipfoldAndSignHash.Text = "zip folder and sign the hash";
            this.zipfoldAndSignHash.UseVisualStyleBackColor = true;
            this.zipfoldAndSignHash.Click += new System.EventHandler(this.zipFolderAndSignHash);
            // 
            // findCertificationFromLocal
            // 
            this.findCertificationFromLocal.Location = new System.Drawing.Point(57, 339);
            this.findCertificationFromLocal.Name = "findCertificationFromLocal";
            this.findCertificationFromLocal.Size = new System.Drawing.Size(274, 23);
            this.findCertificationFromLocal.TabIndex = 5;
            this.findCertificationFromLocal.Text = "find Certificate From Local";
            this.findCertificationFromLocal.UseVisualStyleBackColor = true;
            this.findCertificationFromLocal.Click += new System.EventHandler(this.findCertificateFromLocal);
            // 
            // createXMLBtn
            // 
            this.createXMLBtn.Location = new System.Drawing.Point(388, 49);
            this.createXMLBtn.Name = "createXMLBtn";
            this.createXMLBtn.Size = new System.Drawing.Size(302, 23);
            this.createXMLBtn.TabIndex = 6;
            this.createXMLBtn.Text = "Create XML file";
            this.createXMLBtn.UseVisualStyleBackColor = true;
            this.createXMLBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(419, 135);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(231, 76);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // zipFolderAndSignHash2
            // 
            this.zipFolderAndSignHash2.Location = new System.Drawing.Point(57, 306);
            this.zipFolderAndSignHash2.Name = "zipFolderAndSignHash2";
            this.zipFolderAndSignHash2.Size = new System.Drawing.Size(274, 23);
            this.zipFolderAndSignHash2.TabIndex = 8;
            this.zipFolderAndSignHash2.Text = "zip folder and sign the hash(2)";
            this.zipFolderAndSignHash2.UseVisualStyleBackColor = true;
            this.zipFolderAndSignHash2.Click += new System.EventHandler(this.zipFolderAndSignHash2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 437);
            this.Controls.Add(this.zipFolderAndSignHash2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.createXMLBtn);
            this.Controls.Add(this.findCertificationFromLocal);
            this.Controls.Add(this.zipfoldAndSignHash);
            this.Controls.Add(this.zipFolderAndEncryptHash);
            this.Controls.Add(this.zipfolder);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.zipStreamButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button zipStreamButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button zipfolder;
        private System.Windows.Forms.Button zipFolderAndEncryptHash;
        private System.Windows.Forms.Button zipfoldAndSignHash;
        private System.Windows.Forms.Button findCertificationFromLocal;
        private System.Windows.Forms.Button createXMLBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button zipFolderAndSignHash2;
    }
}


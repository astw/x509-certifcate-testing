namespace WindowsFormsApplication1.Encryption_Decryption
{
    partial class encDecrForm
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
            this.FindFolderBtn = new System.Windows.Forms.Button();
            this.decrypt = new System.Windows.Forms.Button();
            this.passwordText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // FindFolderBtn
            // 
            this.FindFolderBtn.Location = new System.Drawing.Point(52, 26);
            this.FindFolderBtn.Name = "FindFolderBtn";
            this.FindFolderBtn.Size = new System.Drawing.Size(75, 23);
            this.FindFolderBtn.TabIndex = 0;
            this.FindFolderBtn.Text = "FindFolder";
            this.FindFolderBtn.UseVisualStyleBackColor = true;
            this.FindFolderBtn.Click += new System.EventHandler(this.FindFolderBtn_Click);
            // 
            // decrypt
            // 
            this.decrypt.Location = new System.Drawing.Point(52, 88);
            this.decrypt.Name = "decrypt";
            this.decrypt.Size = new System.Drawing.Size(75, 23);
            this.decrypt.TabIndex = 2;
            this.decrypt.Text = "Decrypt File";
            this.decrypt.UseVisualStyleBackColor = true;
            this.decrypt.Click += new System.EventHandler(this.decrypt_Click);
            // 
            // passwordText
            // 
            this.passwordText.Location = new System.Drawing.Point(178, 29);
            this.passwordText.Name = "passwordText";
            this.passwordText.Size = new System.Drawing.Size(312, 20);
            this.passwordText.TabIndex = 3;
            this.passwordText.TextChanged += new System.EventHandler(this.passwordText_TextChanged);
            // 
            // encDecrForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 248);
            this.Controls.Add(this.passwordText);
            this.Controls.Add(this.decrypt);
            this.Controls.Add(this.FindFolderBtn);
            this.Name = "encDecrForm";
            this.Text = "encDecrForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button FindFolderBtn;
        private System.Windows.Forms.Button decrypt;
        private System.Windows.Forms.TextBox passwordText;
    }
}
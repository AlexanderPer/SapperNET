namespace Sapper
{
    partial class AboutBox
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
            this.btnOK = new System.Windows.Forms.Button();
            this.pbMineIcon = new System.Windows.Forms.PictureBox();
            this.lblBrand = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblFramework = new System.Windows.Forms.Label();
            this.lblMemory = new System.Windows.Forms.Label();
            this.lblOS = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblProcessor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbMineIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(313, 208);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // pbMineIcon
            // 
            this.pbMineIcon.Location = new System.Drawing.Point(11, 12);
            this.pbMineIcon.Name = "pbMineIcon";
            this.pbMineIcon.Size = new System.Drawing.Size(32, 32);
            this.pbMineIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMineIcon.TabIndex = 1;
            this.pbMineIcon.TabStop = false;
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.Location = new System.Drawing.Point(75, 12);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(66, 13);
            this.lblBrand.TabIndex = 2;
            this.lblBrand.Text = "Sapper.NET";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(75, 30);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(60, 13);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "Version 1.0";
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(75, 48);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(195, 13);
            this.lblAuthor.TabIndex = 5;
            this.lblAuthor.Text = "Copyright © 2008 Alexander Perepelitsa";
            // 
            // lblFramework
            // 
            this.lblFramework.AutoSize = true;
            this.lblFramework.Location = new System.Drawing.Point(75, 114);
            this.lblFramework.Name = "lblFramework";
            this.lblFramework.Size = new System.Drawing.Size(142, 13);
            this.lblFramework.TabIndex = 6;
            this.lblFramework.Text = "Версия .NET Framework:    ";
            // 
            // lblMemory
            // 
            this.lblMemory.AutoSize = true;
            this.lblMemory.Location = new System.Drawing.Point(75, 150);
            this.lblMemory.Name = "lblMemory";
            this.lblMemory.Size = new System.Drawing.Size(181, 13);
            this.lblMemory.TabIndex = 7;
            this.lblMemory.Text = "Доступная физическая память:    ";
            // 
            // lblOS
            // 
            this.lblOS.AutoSize = true;
            this.lblOS.Location = new System.Drawing.Point(75, 96);
            this.lblOS.Name = "lblOS";
            this.lblOS.Size = new System.Drawing.Size(66, 13);
            this.lblOS.TabIndex = 8;
            this.lblOS.Text = "Система:    ";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(75, 132);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(95, 13);
            this.lblUser.TabIndex = 9;
            this.lblUser.Text = "Пользователь:    ";
            // 
            // lblProcessor
            // 
            this.lblProcessor.AutoSize = true;
            this.lblProcessor.Location = new System.Drawing.Point(75, 168);
            this.lblProcessor.Name = "lblProcessor";
            this.lblProcessor.Size = new System.Drawing.Size(78, 13);
            this.lblProcessor.TabIndex = 10;
            this.lblProcessor.Text = "Процессор:    ";
            // 
            // AboutBox
            // 
            this.ClientSize = new System.Drawing.Size(413, 244);
            this.Controls.Add(this.lblProcessor);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblOS);
            this.Controls.Add(this.lblMemory);
            this.Controls.Add(this.lblFramework);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblBrand);
            this.Controls.Add(this.pbMineIcon);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "О программе \"Сапер\"";
            ((System.ComponentModel.ISupportInitialize)(this.pbMineIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox pbMineIcon;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblFramework;
        private System.Windows.Forms.Label lblMemory;
        private System.Windows.Forms.Label lblOS;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblProcessor;
    }
}
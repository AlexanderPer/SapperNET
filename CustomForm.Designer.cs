namespace Sapper
{
    partial class CustomForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblNumberMines = new System.Windows.Forms.Label();
            this.tbHeight = new System.Windows.Forms.TextBox();
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.tbNumberMines = new System.Windows.Forms.TextBox();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(127, 34);
            this.btnOK.Name = "btnOK";
            this.helpProvider1.SetShowHelp(this.btnOK, true);
            this.btnOK.Size = new System.Drawing.Size(60, 26);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "ОК";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(127, 76);
            this.btnCancel.Name = "btnCancel";
            this.helpProvider1.SetShowHelp(this.btnCancel, true);
            this.btnCancel.Size = new System.Drawing.Size(60, 26);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(10, 37);
            this.lblHeight.Name = "lblHeight";
            this.helpProvider1.SetShowHelp(this.lblHeight, true);
            this.lblHeight.Size = new System.Drawing.Size(48, 13);
            this.lblHeight.TabIndex = 2;
            this.lblHeight.Text = "Высота:";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(10, 61);
            this.lblWidth.Name = "lblWidth";
            this.helpProvider1.SetShowHelp(this.lblWidth, true);
            this.lblWidth.Size = new System.Drawing.Size(49, 13);
            this.lblWidth.TabIndex = 3;
            this.lblWidth.Text = "Ширина:";
            // 
            // lblNumberMines
            // 
            this.lblNumberMines.AutoSize = true;
            this.lblNumberMines.Location = new System.Drawing.Point(10, 85);
            this.lblNumberMines.Name = "lblNumberMines";
            this.helpProvider1.SetShowHelp(this.lblNumberMines, true);
            this.lblNumberMines.Size = new System.Drawing.Size(65, 13);
            this.lblNumberMines.TabIndex = 4;
            this.lblNumberMines.Text = "Число мин:";
            // 
            // tbHeight
            // 
            this.tbHeight.Location = new System.Drawing.Point(76, 34);
            this.tbHeight.Name = "tbHeight";
            this.helpProvider1.SetShowHelp(this.tbHeight, true);
            this.tbHeight.Size = new System.Drawing.Size(38, 20);
            this.tbHeight.TabIndex = 5;
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(76, 58);
            this.tbWidth.Name = "tbWidth";
            this.helpProvider1.SetShowHelp(this.tbWidth, true);
            this.tbWidth.Size = new System.Drawing.Size(38, 20);
            this.tbWidth.TabIndex = 6;
            // 
            // tbNumberMines
            // 
            this.tbNumberMines.Location = new System.Drawing.Point(76, 82);
            this.tbNumberMines.Name = "tbNumberMines";
            this.helpProvider1.SetShowHelp(this.tbNumberMines, true);
            this.tbNumberMines.Size = new System.Drawing.Size(38, 20);
            this.tbNumberMines.TabIndex = 7;
            // 
            // CustomForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(195, 138);
            this.Controls.Add(this.tbNumberMines);
            this.Controls.Add(this.tbWidth);
            this.Controls.Add(this.tbHeight);
            this.Controls.Add(this.lblNumberMines);
            this.Controls.Add(this.lblWidth);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Специальное поле";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblNumberMines;
        public System.Windows.Forms.TextBox tbHeight;
        public System.Windows.Forms.TextBox tbWidth;
        public System.Windows.Forms.TextBox tbNumberMines;
        private System.Windows.Forms.HelpProvider helpProvider1;
    }
}
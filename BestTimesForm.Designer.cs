namespace Sapper
{
    partial class BestTimesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BestTimesForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.lblBeginner = new System.Windows.Forms.Label();
            this.lblIntermediate = new System.Windows.Forms.Label();
            this.lblExpert = new System.Windows.Forms.Label();
            this.lblBeginnerTime = new System.Windows.Forms.Label();
            this.lblIntermediateTime = new System.Windows.Forms.Label();
            this.lblExpertTime = new System.Windows.Forms.Label();
            this.lblExpertName = new System.Windows.Forms.Label();
            this.lblIntermediateName = new System.Windows.Forms.Label();
            this.lblBeginnerName = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(174, 87);
            this.btnOK.Name = "btnOK";
            this.helpProvider1.SetShowHelp(this.btnOK, true);
            this.btnOK.Size = new System.Drawing.Size(45, 20);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lblBeginner
            // 
            this.lblBeginner.Location = new System.Drawing.Point(12, 22);
            this.lblBeginner.Name = "lblBeginner";
            this.helpProvider1.SetShowHelp(this.lblBeginner, true);
            this.lblBeginner.Size = new System.Drawing.Size(90, 13);
            this.lblBeginner.TabIndex = 1;
            this.lblBeginner.Text = "Новичок:";
            // 
            // lblIntermediate
            // 
            this.lblIntermediate.Location = new System.Drawing.Point(12, 40);
            this.lblIntermediate.Name = "lblIntermediate";
            this.helpProvider1.SetShowHelp(this.lblIntermediate, true);
            this.lblIntermediate.Size = new System.Drawing.Size(90, 13);
            this.lblIntermediate.TabIndex = 2;
            this.lblIntermediate.Text = "Любитель:";
            // 
            // lblExpert
            // 
            this.lblExpert.Location = new System.Drawing.Point(12, 58);
            this.lblExpert.Name = "lblExpert";
            this.helpProvider1.SetShowHelp(this.lblExpert, true);
            this.lblExpert.Size = new System.Drawing.Size(90, 13);
            this.lblExpert.TabIndex = 3;
            this.lblExpert.Text = "Профессионал:";
            // 
            // lblBeginnerTime
            // 
            this.lblBeginnerTime.Location = new System.Drawing.Point(104, 22);
            this.lblBeginnerTime.Name = "lblBeginnerTime";
            this.helpProvider1.SetShowHelp(this.lblBeginnerTime, true);
            this.lblBeginnerTime.Size = new System.Drawing.Size(62, 13);
            this.lblBeginnerTime.TabIndex = 4;
            this.lblBeginnerTime.Text = "999 сек.";
            // 
            // lblIntermediateTime
            // 
            this.lblIntermediateTime.Location = new System.Drawing.Point(104, 40);
            this.lblIntermediateTime.Name = "lblIntermediateTime";
            this.helpProvider1.SetShowHelp(this.lblIntermediateTime, true);
            this.lblIntermediateTime.Size = new System.Drawing.Size(62, 13);
            this.lblIntermediateTime.TabIndex = 5;
            this.lblIntermediateTime.Text = "999 сек.";
            // 
            // lblExpertTime
            // 
            this.lblExpertTime.Location = new System.Drawing.Point(104, 58);
            this.lblExpertTime.Name = "lblExpertTime";
            this.helpProvider1.SetShowHelp(this.lblExpertTime, true);
            this.lblExpertTime.Size = new System.Drawing.Size(62, 13);
            this.lblExpertTime.TabIndex = 6;
            this.lblExpertTime.Text = "999 сек.";
            // 
            // lblExpertName
            // 
            this.lblExpertName.Location = new System.Drawing.Point(168, 58);
            this.lblExpertName.Name = "lblExpertName";
            this.helpProvider1.SetShowHelp(this.lblExpertName, true);
            this.lblExpertName.Size = new System.Drawing.Size(78, 13);
            this.lblExpertName.TabIndex = 9;
            this.lblExpertName.Text = "Аноним";
            // 
            // lblIntermediateName
            // 
            this.lblIntermediateName.Location = new System.Drawing.Point(168, 40);
            this.lblIntermediateName.Name = "lblIntermediateName";
            this.helpProvider1.SetShowHelp(this.lblIntermediateName, true);
            this.lblIntermediateName.Size = new System.Drawing.Size(78, 13);
            this.lblIntermediateName.TabIndex = 8;
            this.lblIntermediateName.Text = "Аноним";
            // 
            // lblBeginnerName
            // 
            this.lblBeginnerName.Location = new System.Drawing.Point(168, 22);
            this.lblBeginnerName.Name = "lblBeginnerName";
            this.helpProvider1.SetShowHelp(this.lblBeginnerName, true);
            this.lblBeginnerName.Size = new System.Drawing.Size(78, 13);
            this.lblBeginnerName.TabIndex = 7;
            this.lblBeginnerName.Text = "Аноним";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(32, 87);
            this.btnReset.Name = "btnReset";
            this.helpProvider1.SetShowHelp(this.btnReset, true);
            this.btnReset.Size = new System.Drawing.Size(121, 20);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "Сброс результатов";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // BestTimesForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 120);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblExpertName);
            this.Controls.Add(this.lblIntermediateName);
            this.Controls.Add(this.lblBeginnerName);
            this.Controls.Add(this.lblExpertTime);
            this.Controls.Add(this.lblIntermediateTime);
            this.Controls.Add(this.lblBeginnerTime);
            this.Controls.Add(this.lblExpert);
            this.Controls.Add(this.lblIntermediate);
            this.Controls.Add(this.lblBeginner);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BestTimesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Чемпионы по категориям";
            this.Shown += new System.EventHandler(this.BestTimesForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblBeginner;
        private System.Windows.Forms.Label lblIntermediate;
        private System.Windows.Forms.Label lblExpert;
        private System.Windows.Forms.Label lblBeginnerTime;
        private System.Windows.Forms.Label lblIntermediateTime;
        private System.Windows.Forms.Label lblExpertTime;
        private System.Windows.Forms.Label lblExpertName;
        private System.Windows.Forms.Label lblIntermediateName;
        private System.Windows.Forms.Label lblBeginnerName;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.HelpProvider helpProvider1;
    }
}
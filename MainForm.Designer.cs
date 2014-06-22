namespace Sapper
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.miGame = new System.Windows.Forms.MenuItem();
            this.miNew = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.miBeginner = new System.Windows.Forms.MenuItem();
            this.miIntermediate = new System.Windows.Forms.MenuItem();
            this.miExpert = new System.Windows.Forms.MenuItem();
            this.miCustom = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.miMarks = new System.Windows.Forms.MenuItem();
            this.miColor = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.miBestTimes = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.miExit = new System.Windows.Forms.MenuItem();
            this.miHelp = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.miAbout = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miGame,
            this.miHelp});
            // 
            // miGame
            // 
            this.miGame.Index = 0;
            this.miGame.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miNew,
            this.menuItem1,
            this.miBeginner,
            this.miIntermediate,
            this.miExpert,
            this.miCustom,
            this.menuItem2,
            this.miMarks,
            this.miColor,
            this.menuItem3,
            this.miBestTimes,
            this.menuItem4,
            this.miExit});
            this.miGame.Text = "&Игра";
            // 
            // miNew
            // 
            this.miNew.Index = 0;
            this.miNew.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.miNew.Text = "Новая &игра";
            this.miNew.Click += new System.EventHandler(this.miNew_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.Text = "-";
            // 
            // miBeginner
            // 
            this.miBeginner.Index = 2;
            this.miBeginner.Text = "Н&овичок";
            this.miBeginner.Click += new System.EventHandler(this.miDifficulty_Click);
            // 
            // miIntermediate
            // 
            this.miIntermediate.Index = 3;
            this.miIntermediate.Text = "&Любитель";
            this.miIntermediate.Click += new System.EventHandler(this.miDifficulty_Click);
            // 
            // miExpert
            // 
            this.miExpert.Index = 4;
            this.miExpert.Text = "&Профессионал";
            this.miExpert.Click += new System.EventHandler(this.miDifficulty_Click);
            // 
            // miCustom
            // 
            this.miCustom.Index = 5;
            this.miCustom.Text = "О&собые...";
            this.miCustom.Click += new System.EventHandler(this.miDifficulty_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 6;
            this.menuItem2.Text = "-";
            // 
            // miMarks
            // 
            this.miMarks.Index = 7;
            this.miMarks.Text = "&Метки (?)";
            this.miMarks.Click += new System.EventHandler(this.miMarks_Click);
            // 
            // miColor
            // 
            this.miColor.Index = 8;
            this.miColor.Text = "Ц&вет";
            this.miColor.Click += new System.EventHandler(this.miColor_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 9;
            this.menuItem3.Text = "-";
            // 
            // miBestTimes
            // 
            this.miBestTimes.Index = 10;
            this.miBestTimes.Text = "&Чемпионы...";
            this.miBestTimes.Click += new System.EventHandler(this.miBestTimes_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 11;
            this.menuItem4.Text = "-";
            // 
            // miExit
            // 
            this.miExit.Index = 12;
            this.miExit.Text = "В&ыход";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miHelp
            // 
            this.miHelp.Index = 1;
            this.miHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem5,
            this.miAbout});
            this.miHelp.Text = "&Справка";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 0;
            this.menuItem5.Text = "-";
            // 
            // miAbout
            // 
            this.miAbout.Index = 1;
            this.miAbout.Text = "&О программе...";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 253);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Сапер";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem miGame;
        private System.Windows.Forms.MenuItem miHelp;
        private System.Windows.Forms.MenuItem miNew;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem miBeginner;
        private System.Windows.Forms.MenuItem miIntermediate;
        private System.Windows.Forms.MenuItem miExpert;
        private System.Windows.Forms.MenuItem miCustom;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem miMarks;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem miBestTimes;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem miExit;
        private System.Windows.Forms.MenuItem miColor;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem miAbout;
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sapper
{
    public partial class MainForm : Form
    {
        private MouseArrow mouse;
        private MouseMsgFilter msgFilter = new MouseMsgFilter();
        private Parameters parameters = new Parameters();

        private System.Drawing.Color backColor = System.Drawing.Color.Silver;
        private System.Drawing.Color darkBordersColor = System.Drawing.Color.Gray;
        private System.Drawing.Color lightBordersColor = System.Drawing.Color.White;
        
        public MainForm()
        {
            InitializeComponent();

            mouse = new MouseArrow();
            MineField.SetMouse(mouse);
            MainButton.SetMouse(mouse);
            FaceCreation();
            CellButton.SetMouse(mouse);
            msgFilter.SetMouse(mouse);
            Application.AddMessageFilter(msgFilter);
        }

        #region The сode creating all elements of interface.

        /// <summary>
        /// Making the interface part.
        /// </summary>
        MainPanel mainPanel;
        private void FaceCreation()
        {
            parameters.SetDialogsOwner(this);
            parameters.Load();

            this.Location = parameters.Location;

            this.miGame.MenuItems[this.miBeginner.Index + parameters.Difficulty].Checked = true;
            this.miMarks.Checked = parameters.QuestionMark;
            this.miColor.Checked = parameters.Color;

            mainPanel = new MainPanel(this);
            mainPanel.SetParameters(parameters);
            this.BackColor = System.Drawing.Color.Silver;
            mainPanel.InitColorRes();
            mainPanel.NotResized();
            mainPanel.NewGame();
        }
        #endregion

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            this.Capture = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            parameters.Location = this.Location;
            parameters.Save();
        }

        private FormWindowState windowState = FormWindowState.Normal;
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == windowState) return;
            if (this.WindowState == FormWindowState.Minimized)
            {
                windowState = FormWindowState.Minimized;
                mainPanel.Minimized();
            }

            if (this.WindowState == FormWindowState.Normal)
            {
                windowState = FormWindowState.Normal;
                mainPanel.Normal();
            } 
        }

        private void miNew_Click(object sender, EventArgs e)
        {
            mainPanel.NewGame();
        }

        private void miDifficulty_Click(object sender, EventArgs e)
        {
            for (int index = this.miBeginner.Index; index <= this.miCustom.Index; index++)
                this.miGame.MenuItems[index].Checked = false;

            MenuItem currentMenuItem = sender as MenuItem;

            currentMenuItem.Checked = true;
            parameters.Difficulty = currentMenuItem.Index - this.miBeginner.Index;
            if (!mainPanel.NotResized()) mainPanel.Invalidate();
            mainPanel.NewGame();
        }

        private void miBestTimes_Click(object sender, EventArgs e)
        {
            parameters.ShowBestTimes();
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void miMarks_Click(object sender, EventArgs e)
        {
            this.miMarks.Checked = !this.miMarks.Checked;
            this.parameters.QuestionMark = this.miMarks.Checked;
            CellButton.QuestionMark = parameters.QuestionMark;
        }

        private void miColor_Click(object sender, EventArgs e)
        {
            this.miColor.Checked = !this.miColor.Checked;
            this.parameters.Color = this.miColor.Checked;
            mainPanel.ChangeColor();
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            ShellAboutBox.Show(this);
        }

        private const string helpFileName = "\\Sapper.chm";

        private void miCallHelp_Click(object sender, EventArgs e)
        {
            try
            {
                HelpNavigator navigator = HelpNavigator.TableOfContents;
                Help.ShowHelp(this, Application.StartupPath + helpFileName, navigator);
            }
            catch (Exception)
            {                
                MessageBox.Show("Файл справки не найден.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void miKeywordHelp_Click(object sender, EventArgs e)
        {
            try
            {
                HelpNavigator navigator = HelpNavigator.KeywordIndex;
                Help.ShowHelp(this, Application.StartupPath + helpFileName, navigator);
            }
            catch (Exception)
            {
                MessageBox.Show("Файл справки не найден.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void miUsingHelp_Click(object sender, EventArgs e)
        {
            try
            {
                string sysDir = Environment.SystemDirectory;
                string fileName = sysDir.Remove(sysDir.LastIndexOf("\\")
                    + 1) + "Help\\nthelp.chm";
                Help.ShowHelp(this, fileName);
            }
            catch (Exception)
            {
                MessageBox.Show("Файл справки не найден.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sapper
{
    public partial class ChampionForm : Form
    {
        private string name = "";

        public string ChampName
        {
            get { return name; }
            set { name = value; }
        }
    
        public ChampionForm(string typeName)
        {
            InitializeComponent();
            this.lblCaption.Text += typeName + ".";
        }

        private void ChampionForm_Shown(object sender, EventArgs e)
        {
            this.tbName.Text = name;
        }

        private void ChampionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            name = this.tbName.Text;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sapper
{
    public partial class BestTimesForm : Form
    {
        private Parameters param;

        private void BestTimesForm_Shown(object sender, EventArgs e)
        {
            this.RefreshDialog();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            param.ChampionsReset();

            this.RefreshDialog();
        }

        /// <summary>
        /// Update the dialog window.
        /// </summary>
        private void RefreshDialog()
        {
            string sec = " сек.";

            Parameters.Champion[] champions = param.GetChampions();

            this.lblBeginnerTime.Text       = champions[0].Time.ToString() + sec;
            this.lblIntermediateTime.Text   = champions[1].Time.ToString() + sec;
            this.lblExpertTime.Text         = champions[2].Time.ToString() + sec;

            this.lblBeginnerName.Text       = champions[0].Name;
            this.lblIntermediateName.Text   = champions[1].Name;
            this.lblExpertName.Text         = champions[2].Name;
        }
                
        public BestTimesForm()
        {
            InitializeComponent();

            string labelHelpMessage = "   Лучшее время и имя игрока для каждого уровня\n" + 
                "   сложности: «Новичок», «Любитель» и «Профессионал». ";
            this.helpProvider1.SetHelpString(this.lblBeginner, labelHelpMessage);
            this.helpProvider1.SetHelpString(this.lblIntermediate, labelHelpMessage);
            this.helpProvider1.SetHelpString(this.lblExpert, labelHelpMessage);
            this.helpProvider1.SetHelpString(this.lblBeginnerTime, labelHelpMessage);
            this.helpProvider1.SetHelpString(this.lblIntermediateTime, labelHelpMessage);
            this.helpProvider1.SetHelpString(this.lblExpertTime, labelHelpMessage);
            this.helpProvider1.SetHelpString(this.lblBeginnerName, labelHelpMessage);
            this.helpProvider1.SetHelpString(this.lblIntermediateName, labelHelpMessage);
            this.helpProvider1.SetHelpString(this.lblExpertName, labelHelpMessage);
            this.helpProvider1.SetHelpString(this.btnReset,
                "\n  Очистка текущего списка чемпионов.  \n");
            this.helpProvider1.SetHelpString(this.btnOK,
                "    Закрытие диалогового окна с сохранением всех  \n    внесенных изменений.");
        }

        public void SetParameters(Parameters param) { this.param = param; }
    }
}
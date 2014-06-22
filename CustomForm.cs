using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sapper
{
    public partial class CustomForm : Form
    {
        private int minCountRows        = 9;
        private int maxCountRowsWidth   = 30;
        private int maxCountRowsHeight  = 24;
        private int minCountMines       = 10;
        private Parameters parameters;
        
        private int koef                = 67;
        private int b                   = -883;
        private int divisor             = 71;

        public CustomForm()
        {
            InitializeComponent();

            string numberSquares    = "  Число квадратиков на игровом поле по  \n";
            
            string horHelpMessage = numberSquares + "  горизонтали.";
            this.helpProvider1.SetHelpString(this.lblWidth, horHelpMessage);
            this.helpProvider1.SetHelpString(this.tbWidth, horHelpMessage);

            string verHelpMessage = numberSquares + "  вертикали.";
            this.helpProvider1.SetHelpString(this.lblHeight, verHelpMessage);
            this.helpProvider1.SetHelpString(this.tbHeight, verHelpMessage);

            string mineHelpMessage = "  Число мин, размещенных  \n  на игровом поле.";
            this.helpProvider1.SetHelpString(this.lblNumberMines, mineHelpMessage);
            this.helpProvider1.SetHelpString(this.tbNumberMines, mineHelpMessage);

            string close = "  Закрытие диалогового окна ";
            string changes = " всех  \n  внесенных изменений.";
            this.helpProvider1.SetHelpString(btnOK, close + "с сохранением" + changes);
            this.helpProvider1.SetHelpString(btnCancel, close + "без сохранения" + changes);
        }  
        
        public void SetParameters(Parameters parameters)
        {
            this.parameters = parameters;
            this.tbWidth.Text = parameters.Size.Width.ToString();
            this.tbHeight.Text = parameters.Size.Height.ToString();
            this.tbNumberMines.Text = parameters.Mines.ToString();
        }
        
        private int MaxCountMines(int colCount, int rowCount)
        {
            return (koef * colCount * rowCount + b) / divisor;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int width;
            int height;
            int mines;
        
            // Taking the amount a columns.
            try { width = Int32.Parse(this.tbWidth.Text); }
            catch (Exception) { width = parameters.Size.Width; }

            if (width < minCountRows) width = minCountRows;
            else if (width > maxCountRowsWidth) width = maxCountRowsWidth;

            // Taking the amount a rows.
            try { height = Int32.Parse(this.tbHeight.Text); }
            catch (Exception) { height = parameters.Size.Height; }

            if (height < minCountRows) height = minCountRows;
            else if (height > maxCountRowsHeight) height = maxCountRowsHeight;

            // Taking the amount a mines.
            try { mines = Int32.Parse(this.tbNumberMines.Text); }
            catch (Exception) { mines = parameters.Mines; }

            if (mines < minCountMines) mines = minCountMines;
            else
            {
                int maxCountMines = MaxCountMines(width, height);
                if (mines > maxCountMines) mines = maxCountMines;
            }
            parameters.Size = new Size(width, height);
            parameters.Mines = mines;            
        }
    }
}
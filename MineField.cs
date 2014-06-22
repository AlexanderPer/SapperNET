using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace Sapper
{
    /// <summary>
    /// Defines the minefield and contains all buttons.
	/// </summary>
	public class MineField : ContainerControl
    {
        #region Member Variables

        private int colCount = 8;
		private int rowCount = 8;
		private int qtyMines = 10;
		private Size size;
		private ControlPanel ctrlPanel;

		private Random rand = new Random();
        private static MouseArrow mouse;
        private Parameters parameters;
        private ArrayList detonatedMines = new ArrayList();
        private int numberOpenedCells = 0;
        private int currentCellIndex = -1;
        public new CellButton[] Controls;

        #endregion
        
        #region Variables Assignment
        public MineField()
		{
            CellButton.SetParentContainer(this);
            SetMouseEvents(MouseDown, MouseUp, MouseMove);
		}

        public static void SetMouse(MouseArrow mouseArrow) { mouse = mouseArrow; }

        public void SetControlPanel(ControlPanel ctrl)
		{
			ctrlPanel = ctrl;
			CellButton.SetControlPanel(ctrl);
		}

        public void SetParameters(Parameters parameters) { this.parameters = parameters; }

        #endregion

        #region Field Components Create
        
        private void CreateCells()
        {
            Controls = new CellButton[colCount * rowCount];
            for (int i = 0; i < Controls.Length; i++) Controls[i] = new CellButton();
        }

        /// <summary>
        /// Clearing of the whole field from mines.
        /// </summary>
        private void ClearField()
        {
            int oldIndex = CellButton.CurrentNumber;
            for (int i = 0; i < Controls.Length; i++) Controls[i].Clear(i);
            CellButton.CurrentNumber = oldIndex;
        }

        /// <summary>
        /// Making the new play, with new location of the mines.
        /// </summary>
        public void NewGame()
        {
            ClearField();
            PlacingMines();
            numberOpenedCells = 0;
            mouse.Blocked = false;
            detonatedMines.Clear();
            CellButton.StatClear();
        }

        public void ChangeColor()
        {
            graphics = GetGraphicsFromHwnd();
            CellButton.Color = parameters.Color;
            CellButton.LoadResource();
            this.Redraw(GetDrawRect());
        }

        /// <summary>
        /// The Placement of the mines in casual order.
        /// </summary>
        private void PlacingMines()
        {
            int qtyCells = colCount * rowCount;
            int oldIndex = CellButton.CurrentNumber;
            for (int i = 0; i < qtyMines; i++)
            {
                int currCell = rand.Next(qtyCells);
                CellButton.CurrentNumber = currCell;
                while (Controls[currCell].Mine)
                {
                    currCell++;
                    currCell %= qtyCells;
                    CellButton.CurrentNumber = currCell;
                }
                Controls[currCell].Mine = true;
            }
            CellButton.CurrentNumber = oldIndex;
        }
        #endregion   

        #region Geometrics Parameters

        public override Point GetLocation() { return this.location; }

        public override Size GetClientSize() { return this.size; }
        
        public int Width { get { return size.Width; } }

        public int Height { get { return size.Height; } }

        public bool NotResized()
        {
            this.qtyMines = parameters.Mines;
            CellButton.QuestionMark = parameters.QuestionMark;

            CellButton.Color = parameters.Color;
            CellButton.LoadResource();

            if ((this.colCount == parameters.Size.Width) && (this.rowCount == parameters.Size.Height)) return true;
            this.colCount = parameters.Size.Width;
            this.rowCount = parameters.Size.Height;
            this.size = new Size(CellButton.Size.Width * colCount,
                CellButton.Size.Height * rowCount);
            CreateCells();
            return false;
        }        
        
        #endregion

        #region Drawing
        protected override void Draw()
        {
            Redraw(clipRectangle);
        }

        protected void Redraw(Rectangle rect)
        {
            Rectangle drawRect = this.GetDrawRect();
            drawRect.Intersect(rect);
            Point beginColRow = GetColRow(drawRect.Location);
            Point endColRow = GetColRow(new Point(drawRect.Right - 1, drawRect.Bottom - 1));

            int oldNumber = CellButton.CurrentNumber;
            for (int col = beginColRow.X; col <= endColRow.X; col++)
                for (int row = beginColRow.Y; row <= endColRow.Y; row++)
                    GetCellButton(col, row).Drawing(GetIndex(col, row));
            CellButton.CurrentNumber = oldNumber;
        }

        public Graphics Graphics
        {
            get { return graphics; }
        }
        #endregion

        #region Cells

        #region Location

        public Point GetCellLocation(int index)
        {
            Point cellLocation = this.Location;
            Point colRow = GetColRow(index);
            cellLocation.Offset(colRow.X * CellButton.Size.Width,
                colRow.Y * CellButton.Size.Height);
            return cellLocation;
        }

        private int GetIndex(int col, int row)
        {
            int index = rowCount * col + row;
            return index;
        }

        private int GetIndex(Point pixPoint)
        {
            Point cell = GetColRow(pixPoint);
            return GetIndex(cell.X, cell.Y);
        }
     
        #endregion

        #region Get Cells

        private CellButton GetCellButton(int col, int row)
        {
            int index = rowCount * col + row;
            return (CellButton)Controls[index];
        }

        private Point GetColRow(Point pixPoint)
        {
            pixPoint -= (Size)GetClientRect().Location;
            return new Point(pixPoint.X / CellButton.Size.Width,
                pixPoint.Y / CellButton.Size.Height);
        }

        private Point GetColRow(int index)
        {
            return new Point(index / rowCount, index % rowCount);
        }
        
        private CellButton GetCellButton(Point mousePoint)
        {
            Point colRow = GetColRow(mousePoint);
            return GetCellButton(colRow.X, colRow.Y);
        }        

        public ArrayList GetCellsAround(int index)
        {
            Point colRow = GetColRow(index);
            ArrayList cellsAround = new ArrayList();
            for (int i = colRow.X - 1; i <= colRow.X + 1; i++)
                for (int j = colRow.Y - 1; j <= colRow.Y + 1; j++)
                {
                    if ((i >= 0) && (i <= (colCount - 1)) &&
                        (j >= 0) && (j <= (rowCount - 1)))
                        if (!((i == colRow.X) && (j == colRow.Y)))
                        {
                            cellsAround.Add(GetIndex(i, j));
                        }
                }
            return cellsAround;
        }        

        #endregion

        #endregion                    
        
        #region Mouse Events
        
        private void MouseDown(MouseEventArgs e)
        {
            if (mouse.Blocked) return;
            Rectangle clientRect = this.GetClientRect();
            if (clientRect.Contains(new Point(e.X, e.Y)))
                Controls[currentCellIndex].MouseDown(e);
        }

        private void MouseUp(MouseEventArgs e)
        {
            if (mouse.Blocked) return;
            Rectangle clientRect = this.GetClientRect();
            if (clientRect.Contains(new Point(e.X, e.Y))) Controls[currentCellIndex].MouseUp(e);
        }

        private void MouseMove(MouseEventArgs e)
        {
            if (mouse.Blocked) return;
            Rectangle clientRect = this.GetClientRect();
            Point mousePoint = new Point(e.X, e.Y);
            if (clientRect.Contains(mousePoint))
            {
                int newCellIndex = GetIndex(mousePoint);
                if (newCellIndex != currentCellIndex)
                {
                    if (currentCellIndex != -1) Controls[currentCellIndex].MouseLeave(e);
                    CellButton.CurrentNumber = newCellIndex;
                    Controls[newCellIndex].MouseEnter(e);
                    currentCellIndex = newCellIndex;
                }
            }
            else
            {
                if (currentCellIndex != -1)
                {
                    Controls[currentCellIndex].MouseLeave(e);
                    currentCellIndex = -1;
                }
            }
        }

        #endregion        

        #region Counts
        public void OpenCell()
        {
            numberOpenedCells++;
            if (numberOpenedCells + this.qtyMines == colCount * rowCount) 
                Success();
        }

        public int FlagsCount
        {
            get { return ctrlPanel.MinesCount; }
            set { ctrlPanel.MinesCount = value; }
        }
        #endregion
        
        #region End Game (Success And Explosion)
        private void Success()
        {
            CellButton.CurrentNumber = 0;
            foreach (CellButton cell in this.Controls)
            {
                if (cell.Mine && !cell.Flag)
                    cell.SetFlag();
                CellButton.CurrentNumber++;
            }
            ctrlPanel.Success();
            mouse.Blocked = true;
        }

        public void AddDetonatedMine(CellButton cb) { detonatedMines.Add(cb); }

        public bool IsDetonated(CellButton cb)
        {
            return detonatedMines.Contains(cb);
        }

        public void Explosion()
        {
            int oldIndex = CellButton.CurrentNumber;
            for (int i = 0; i < this.Controls.Length; i++)
            {
                CellButton.CurrentNumber = i;
                if (Controls[i].Mine && !Controls[i].Flag) Controls[i].SimpleOpen();
                Controls[i].TestErrorFlag();
            }
            CellButton.CurrentNumber = oldIndex;
            ctrlPanel.Explosion();
            mouse.Blocked = true;
        }
        #endregion        

    }
}

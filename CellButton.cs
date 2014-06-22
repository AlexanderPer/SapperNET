using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace Sapper
{
	/// <summary>
    /// Represents cell with button.
	/// </summary>

    public class CellButton
    {
        #region Member Variables

        private enum Mark { None = 0, Flag, Question };               
        
        private static MineField parentContainer;
		public static readonly Size Size = new Size(16, 16);
        private static MouseArrow mouse;

        private static Bitmap imgButton;
        private static Bitmap imgFlag;
        private static Bitmap imgQuiestBtn;
        private static Bitmap imgExplos;
        private static Bitmap imgCross;
        private static Bitmap imgMine;
        private static Bitmap imgQuiestCell;
        private static Bitmap imgEmpty;
        private static readonly Bitmap[] imgDigits = new Bitmap[9];        

        private static ControlPanel ctrlPanel;
        private static bool cellPressed = false;

        public static bool QuestionMark;
        private static bool color = false;

        private const byte ff = 0xff;
        private const byte mineBit = 0x04;
        private const byte openBit = 0x08;
        private const byte markBits = 0x03;

        private byte bits;
        public static int CurrentNumber;

        #endregion

        #region Variables Assignment
                
        public CellButton() { }

        public static void LoadResource()
        {
            BaseControl.ImageResource images = new BaseControl.ImageResource(typeof(CellButton), color, Size);
            imgButton = images.Get(0);
            imgFlag = images.Get(1);
            imgQuiestBtn = images.Get(2);
            imgExplos = images.Get(3);
            imgCross = images.Get(4);
            imgMine = images.Get(5);
            imgQuiestCell = images.Get(6);
            imgEmpty = images.Get(15);
            for (int i = 1; i < 9; i++) imgDigits[i] = images.Get(15 - i);
            imgDigits[0] = imgEmpty;
        }

        public static void SetParentContainer(MineField container)
        {
            parentContainer = container;
        }

        public static void SetMouse(MouseArrow mouseArrow)
        {
            mouse = mouseArrow;
        }

        public static void SetControlPanel(ControlPanel panel)
        {
            ctrlPanel = panel;
        }

        #endregion
                
        #region Geometrics Parameters

        public Point GetLocation()
		{
			return parentContainer.GetCellLocation(CurrentNumber);
		}

        public Size GetClientSize()
		{
			return Size;
		}

        public Point Location
		{
			get
			{
				return GetLocation();
			}
			set	{}
        }

        #endregion

        #region Clear

        public static void StatClear() 
        {
            cellPressed = false;
        }

        public void Clear(int index)
		{
            this.Mine = false;
            if ((!Opened) && (Marker == Mark.None)) return;
            
			this.Marker = Mark.None;
			this.Opened = false;
            CurrentNumber = index;
            BaseDraw();
        }

        #endregion

        #region Button Mark
        public void SetFlag()
        {
            Marker = Mark.Flag;
            parentContainer.FlagsCount--;
            this.Draw();
        }

        /// <summary>
        /// Consecutively changes the badge on button: flag - question mark - clean button - flag.
		/// </summary>
		private void ChangeMark()
		{
            if (Opened) return;
            if (!QuestionMark && Marker == Mark.Flag)
            {
                Marker = Mark.Question;
                parentContainer.FlagsCount++;
            }
            switch (Marker)
            {
                case Mark.None:
                    SetFlag();
                    break;
                case Mark.Flag:                    
                    Marker = Mark.Question;                    
                    parentContainer.FlagsCount++;
                    this.Draw();
                    break;
                case Mark.Question:
                    Marker = Mark.None;
                    this.Draw();
                    break;
            }
        }
        #endregion

        #region Button Behaviour

        public void Press()	
		{
            if ((!Opened) && (!Flag))
            {
                cellPressed = true;
                this.Draw();
            }
		}

        private void PressCell(int index)
        {
            CurrentNumber = index;
            parentContainer.Controls[index].Press();
        }

		public void Release()
		{
            cellPressed = false;
            if ((!Opened) && (!Flag)) this.Draw();
        }

        private void ReleaseCell(int index)
        {
            CurrentNumber = index;
            parentContainer.Controls[index].Release();
        }

        #endregion

        #region Drawing Procedures
        private void DrawCell()
		{
            Bitmap cellImage;
            if (this.Flag && !this.Mine)                cellImage = imgCross;            
            else if (parentContainer.IsDetonated(this)) cellImage = imgExplos;
            else if (this.Mine)                         cellImage = imgMine;
            else                                        cellImage = imgDigits[this.GetCountMinesAround()];

            parentContainer.Graphics.DrawImage(cellImage, Location.X, Location.Y);
		}

        private Bitmap GetImage()
        {                
            switch (Marker)
            {
                case Mark.Flag: return imgFlag;
                case Mark.Question:
                    {
                        if (cellPressed) return imgQuiestCell;
                        else return imgQuiestBtn;
                    }
                case Mark.None:
                    {
                        if (cellPressed) return imgEmpty;
                        else return imgButton;
                    }
                default: return imgButton;
            }
        }

        public void Drawing()
        {
            Draw();
        }

        public void Drawing(int index)
        {
            CurrentNumber = index;
            Draw();
        }

        private void BaseDraw()
        {
            parentContainer.Graphics.DrawImage(GetImage(), this.GetLocation());
        }
        
        private void Draw()
		{
            if (Opened) this.DrawCell();
            else BaseDraw();
        }
        #endregion

        #region Group Button Manipulation
        /// <summary>
        /// Striking surrounding buttons.
		/// </summary>
		private void PressAroundMines()
		{
            int oldIndex = CurrentNumber;
            foreach (int index in GetCellsAround()) PressCell(index);
            CurrentNumber = oldIndex;
		}

		/// <summary>
        /// Releases the surrounding buttons.
		/// </summary>
		private void ReleaseAroundMines()
		{
            int oldIndex = CurrentNumber;
            foreach (int index in GetCellsAround()) ReleaseCell(index);
            CurrentNumber = oldIndex;
		}

		/// <summary>
        /// The opening of the cells in groupping mode.
		/// </summary>
		private void OpenAroundMines()
		{
            // if cell open and there is mines around.
            if (this.Opened && (this.GetCountMinesAround() != 0))
			{
				int countFlags = 0;
                foreach (int index in GetCellsAround()) if (parentContainer.Controls[index].Flag) countFlags++;
                
				// if amount flag does not comply with amount surrounding mines simply release buttons
				if (this.GetCountMinesAround() != countFlags) ReleaseAroundMines();
				else
				{
					foreach (int index in GetCellsAround())
                    {
                        CellButton cellButton = parentContainer.Controls[index];
                        
                        if (cellButton.Flag != cellButton.Mine)
                        {
                            ReleaseAroundMines();
                            ExplosionAroundMines();
                            parentContainer.Explosion();
                            return;
                        }
                    }
                    // check is finished - all it is correct
                    // we open surrounding cells
                    int oldIndex = CurrentNumber;
                    foreach (int index in GetCellsAround())
                    {
                        CurrentNumber = index;
                        parentContainer.Controls[index].TestAroundMines();
                    }
                    CurrentNumber = oldIndex;
				}
			} 
			else 
			{
				ReleaseAroundMines();
			}
		}

        private void ExplosionAroundMines()
        {
            foreach (int index in GetCellsAround())
            {
                CellButton cellButton = parentContainer.Controls[index];
                if (cellButton.Mine && !cellButton.Flag)
                    parentContainer.AddDetonatedMine(cellButton);
            }

        }

        private int GetCountMinesAround()
        {
            int count = 0;
            foreach (int index in GetCellsAround())
            {
                if (parentContainer.Controls[index].Mine) count++;
            }
            return count;
        }

        public ArrayList GetCellsAround()
        {
            return parentContainer.GetCellsAround(CurrentNumber);
        }

        #endregion

        #region Mouse Events
        public void MouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			if ((Control.MouseButtons & System.Windows.Forms.MouseButtons.Right) == 
				System.Windows.Forms.MouseButtons.Right)
			{
				if ((Control.MouseButtons & System.Windows.Forms.MouseButtons.Left) == 
					System.Windows.Forms.MouseButtons.Left)
				{
					PressAroundMines();
					Press();
				}
				else ChangeMark();
			}
			if ((e.Button & System.Windows.Forms.MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left)
                Press();
		}

		public void MouseUp(System.Windows.Forms.MouseEventArgs e)
		{
            if((e.Button & System.Windows.Forms.MouseButtons.Left) == 
				System.Windows.Forms.MouseButtons.Left)
			{
                ctrlPanel.StartTimer();
				if ((Control.MouseButtons & System.Windows.Forms.MouseButtons.Right) == 
					System.Windows.Forms.MouseButtons.Right)
				{
					OpenAroundMines();
					Release();                    
				}
				else
                {
                    if (cellPressed && !mouse.GroupMode && !Flag)
                    {
                        cellPressed = false;
                        if (this.Mine)
                        {                            
                            parentContainer.AddDetonatedMine(this);
                            parentContainer.Explosion();
                        }
                        else TestAroundMines();
                    }                    
                }
			}
			if((e.Button & System.Windows.Forms.MouseButtons.Right) == 
				System.Windows.Forms.MouseButtons.Right)
			{
				if ((Control.MouseButtons & System.Windows.Forms.MouseButtons.Left) == 
					System.Windows.Forms.MouseButtons.Left)
				{
					ctrlPanel.StartTimer();
					OpenAroundMines();
					Release();
				}
			}
		}

		public void MouseLeave(System.Windows.Forms.MouseEventArgs e) 
		{
			if ((e.Button & System.Windows.Forms.MouseButtons.Left) == 
				System.Windows.Forms.MouseButtons.Left)
			{

                if (!mouse.GroupModePressOneButton) Release();
                
				if ((e.Button & System.Windows.Forms.MouseButtons.Right) == 
					System.Windows.Forms.MouseButtons.Right)
					ReleaseAroundMines();
				
			}
		}

		public void MouseEnter(System.Windows.Forms.MouseEventArgs e)
		{
			if ( ((Control.MouseButtons & System.Windows.Forms.MouseButtons.Left) == 
				System.Windows.Forms.MouseButtons.Left))
			{
                if (!mouse.GroupModePressOneButton) Press();
                
				if ((Control.MouseButtons & System.Windows.Forms.MouseButtons.Right) ==
                    System.Windows.Forms.MouseButtons.Right)
                    PressAroundMines();
			}
        }
        #endregion

        #region Opening
        public void SimpleOpen()
        {
            if (Opened) return;
            this.Opened = true;
            this.Draw();
        }

        /// <summary>
        /// Open the current cell.
        /// </summary>
        public void Open()
        {
            if (Opened) return;
            SimpleOpen();
            parentContainer.OpenCell();
        }
        #endregion

        #region Tests
        public void TestErrorFlag()
        {
            if (!this.Flag || this.Mine) return;
            this.Opened = true;
            this.Draw();
        }
		
		/// <summary>
        /// Checking for presence of the mine in current cell and nearby.
		/// </summary>
		private void TestAroundMines()
		{
            if (this.Mine) return;
            Open();
            if (this.GetCountMinesAround() != 0) return;
            int oldIndex = CurrentNumber;
            foreach (int index in GetCellsAround())
            {
                CurrentNumber = index;
                CellButton cellButton = parentContainer.Controls[index];
                if (!cellButton.Opened) cellButton.TestAroundMines();
            }
            CurrentNumber = oldIndex;
        }
        #endregion

        #region Propertys
                
        public byte Bits
        {            
            get { return bits; }
            set { bits = value; }
        }
        
        private bool Opened
        {
            get { return (Bits & openBit) == openBit; }
            set { Bits = (byte)(value ? Bits | openBit : Bits & (ff - openBit)); }
        }

        
        private Mark Marker
        {
            get { return (Mark)(Bits & markBits); }
            set { Bits = (byte)((Bits & (ff - markBits)) | (byte)value); }
        }

        public bool Mine
        {
            get { return (Bits & mineBit) == mineBit; }
            set { Bits = (byte)(value ? Bits | mineBit : Bits & (ff - mineBit)); }
        }

        /// <summary>
        /// Indicates, there is on button flag.
		/// </summary>
		public bool Flag
		{
			get { return Marker == Mark.Flag; }
        }

        public static bool Color
        {
            set { color = value; }
            get { return color; }
        }
        

        #endregion
    }
}

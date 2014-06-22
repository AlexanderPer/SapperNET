using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Sapper
{
	/// <summary>
    /// Defines main controlling button with smile.
	/// </summary>
	public class MainButton : BaseButton
	{
		static private Size size = new Size(24, 24);

		private Point location;
        private bool color = false;

        private Bitmap imgSmilePress;
        private Bitmap imgGlass;
        private Bitmap imgDead;
        private Bitmap imgAfraid;
        private Bitmap imgSmileRelease;

		private Bitmap currFace;
        private Bitmap image;

        private static MouseArrow mouse;
				
		public delegate void ClickDelegate();
		public event ClickDelegate Click;

        public MainButton()
		{
            SetMouseEvents(MouseDown, MouseUp, MouseMove);
		}

        public void LoadResources()
        {
            ImageResource images = new ImageResource(this, color, size);
            imgSmilePress = images.Get(0);
            imgGlass = images.Get(1);
            imgDead = images.Get(2);
            imgAfraid = images.Get(3);
            imgSmileRelease = images.Get(4);            
            image = imgSmileRelease;
        }

        public static void SetMouse(MouseArrow mouseArrow) { mouse = mouseArrow; }

		public override Point Location
		{
			get { return this.location; }
			set { this.location = value; }
		}

		public static Size Size { get { return size; } }

        public bool Color
        {
            set 
            {
                color = value;
                LoadResources();
            }
        }

		public override Point GetLocation() { return location; }

		public override Size GetClientSize() { return size; }

        protected override Bitmap GetImage() { return image; }

        protected override void Draw() { base.Draw(); }

		public void NormalState()
		{
            image = imgSmileRelease;
			this.Redraw();
		}

		public void Explosion()
		{
            image = this.imgDead;
			this.Redraw();
		}

        public void Success()
		{
            image = this.imgGlass;
			this.Redraw();
		}

        private bool pressed = false;
        private void Press()
        {
            this.pressed = true;           
            this.Redraw();
        }

        private void Release()
        {
            this.pressed = false;
            this.Redraw();
        }
		
		private bool capture = false;

        private void MouseDown(MouseEventArgs e)
		{
			Rectangle buttonRect = new Rectangle(location, size);
			
			if ((e.Button & System.Windows.Forms.MouseButtons.Left) == 
				System.Windows.Forms.MouseButtons.Left)
			{
				if (buttonRect.Contains(new Point(e.X, e.Y)))
				{
					currFace = this.image;
                    this.image = this.imgSmilePress;
					Press();
					capture = true;
				}
				else
				{
                    if (mouse.Blocked) return;
                    this.image = this.imgAfraid;
					this.Redraw();
				}
			}
            if (((e.Button & System.Windows.Forms.MouseButtons.Right) ==
                System.Windows.Forms.MouseButtons.Right) && mouse.GroupMode && !mouse.Blocked)
            {
                this.image = this.imgAfraid;
                this.Redraw();
            }
		}

        private void MouseUp(MouseEventArgs e)
		{
			if ((e.Button & System.Windows.Forms.MouseButtons.Left) == 
				System.Windows.Forms.MouseButtons.Left)
			{
				capture = false;
				Rectangle buttonRect = new Rectangle(location, size);
				if (buttonRect.Contains(new Point(e.X, e.Y)))
				{
					Release();
					Click();                    
				}
				else
				{
                    if (mouse.Blocked) return;
                    image = this.imgSmileRelease;
					this.Redraw();
				}
			}
            if (((e.Button & System.Windows.Forms.MouseButtons.Right) ==
                System.Windows.Forms.MouseButtons.Right) && mouse.GroupMode && !mouse.Blocked)
            {
                image = this.imgSmileRelease;
                this.Redraw();
            }
		}

		private void MouseMove(MouseEventArgs e)
		{
			if (capture)
			{
				Rectangle buttonRect = new Rectangle(location, size);
				if (buttonRect.Contains(new Point(e.X, e.Y))) 
				{					
                    if (!this.pressed)
					{
						currFace = this.image;
                        this.image = this.imgSmilePress;
						Press();
					}
				}
				else
				{
                    if (this.pressed)
					{
						image = this.currFace;
						Release();
					}
				}
			}
		}
	}
}

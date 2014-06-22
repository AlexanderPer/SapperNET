using System;
using System.Drawing;

namespace Sapper
{
	/// <summary>
    /// Drawing the frame around controls.
	/// </summary>
	public class Frame
	{
		private int borderWidth = 2;
		private SolidBrush leftUpBrush = new SolidBrush(System.Drawing.Color.White);
		private SolidBrush rightDownBrush = new SolidBrush(System.Drawing.Color.Gray);
		private bool leftUp = true;
		private bool rightDown = true;

		public Frame() {}
		
		public bool LeftUp
		{
			get { return leftUp; }
			set { leftUp = value; }
		}

		public bool RightDown
		{
			get { return rightDown; }
			set { rightDown = value; }
		}

		public int BorderWidth
		{
			get { return borderWidth; }
			set { borderWidth = value; }
		}
		
		public Color LeftUpColor
		{
			get { return leftUpBrush.Color; }
			set { leftUpBrush.Color = value; }
		}

		public Color RightDownColor
		{
			get { return rightDownBrush.Color; }
			set { rightDownBrush.Color = value; }
		}

		public void Draw(Graphics g, Rectangle rect)
		{
			int ribWidth = 1;
			Point leftTop = rect.Location;
			Point rightBottom = leftTop + rect.Size;
            // drawing edge on the right and adown
			if (this.rightDown)
				g.FillPolygon(rightDownBrush, new Point[] {
						rightBottom,
						new Point(rightBottom.X, leftTop.Y),
						new Point(rightBottom.X - borderWidth, leftTop.Y + borderWidth),
						new Point(rightBottom.X - borderWidth, rightBottom.Y - borderWidth),
						new Point(leftTop.X + borderWidth, rightBottom.Y - borderWidth),
						new Point(leftTop.X, rightBottom.Y)});

            // drawing edge on the left and top
			if (this.leftUp)
				g.FillPolygon(leftUpBrush, new Point[] {
						leftTop,
						new Point(leftTop.X, rightBottom.Y - ribWidth),
						new Point(leftTop.X + borderWidth, rightBottom.Y - borderWidth - ribWidth),
						new Point(leftTop.X + borderWidth, leftTop.Y + borderWidth),
						new Point(rightBottom.X - borderWidth - ribWidth, leftTop.Y + borderWidth),
						new Point(rightBottom.X - ribWidth, leftTop.Y)});
		}
	}

	/// <summary>
    /// Implements the basic functionality common to button controls. 
	/// </summary>
    public abstract class BaseButton : BaseControl
	{      

        public BaseButton() {}     

        protected virtual Bitmap GetImage() { return null; }

        protected override void Draw()
		{
            graphics.DrawImage(GetImage(), this.GetLocation());

			base.Draw ();
		}
	}

	/// <summary>
    /// Represents a lowered panel. 
	/// </summary>
	public class LoweredPanel : ContainerControl
	{
		protected Size size = new Size();
		protected Frame frame = new Frame();
				
		public LoweredPanel()
		{
			frame.BorderWidth = 1;
			frame.LeftUpColor = Color.Gray;
            frame.RightDownColor = Color.White;
		}

		public override Size GetClientSize()
		{
			return size;
		}

		protected override void Draw()
		{
			frame.Draw(graphics, this.GetClientRect());
			base.Draw ();
		}

		public Size Size
		{
			get { return size; }
			set { size = value; }
		}

		public int BorderWidth
		{
			get { return frame.BorderWidth; }
			set { frame.BorderWidth = value; }
		}

		public Color LeftUpColor
		{
			get { return  frame.LeftUpColor; }
			set { frame.LeftUpColor = value; }
		}

		public Color RightDownColor
		{
			get { return  frame.RightDownColor; }
			set { frame.RightDownColor = value; }
		}
	}
	/// <summary>
    /// One-sided raised panel.
	/// </summary>
	public class RaisedPanel : LoweredPanel
	{
		public RaisedPanel()
		{
			frame.BorderWidth = 3;
			frame.LeftUpColor = Color.White;
			frame.RightDown = false;
		}
		
		public override Size GetClientSize()
		{
			return new Size(size.Width + frame.BorderWidth, size.Height + frame.BorderWidth);
		}
	}
}

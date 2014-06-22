using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sapper
{
	/// <summary>
    /// Defines the graphic presentation of the decimal numeral.
	/// </summary>
	public class Digit : NonStaticBaseControl
	{
		private static Size size = new Size(13, 23);
		private static int minValue = 0;
		private static int maxValue = 9;
        private static Bitmap[] digitsImgs = new Bitmap[10];
        private static Bitmap minusImg;
		
		private int currValue = 0;
        private bool minus = false;
		
		public static void LoadResource(bool colored)
        {
            ImageResource images = new ImageResource(typeof(Digit), colored, size);
            for (int i = 0; i < digitsImgs.Length; i++) digitsImgs[i] = images.Get(11 - i);
            minusImg = images.Get(0);            
        }

		public static Size Size
		{
			get { return size; }
		}
		
        protected override void Draw()
		{
            if (minus)
            {
                graphics.DrawImage(minusImg, GetClientRect());
                return;
            }
            graphics.DrawImage(digitsImgs[currValue], GetClientRect());
		}

		public override System.Drawing.Size GetClientSize()
		{
			return size;
		}

		public int Value
		{
			set
			{ 
				if (value < minValue) currValue = minValue;
				else if (value > maxValue) currValue = maxValue;
				else currValue = value;
				this.Redraw();
			}
			get
			{ 
				return currValue; 
			}
		}

        public bool Minus
        {
            get { return minus; }
            set { minus = value; }
        }
	}
    	
    /// <summary>
    /// Represents the indicator, consisting of several numerals. 
	/// </summary>
	public class Indicator : LoweredPanel
	{
		private static int numberDigits = 3;
        private static new Size size = new Size(Digit.Size.Width * numberDigits + 2,
            Digit.Size.Height + 2);
		private static int minValue = -720;
		private static int maxValue = (int) (Math.Pow(10, numberDigits) - 1);
        private static bool colored;

		private int currValue = 0;
		
		public static new Size Size
		{
			get { return size; }
		}

		public Indicator()
		{          
            Digit digit;
            for (int i = 0; i < numberDigits; i++)
			{
				digit = new Digit();
                digit.Location = new Point(
                    this.BorderWidth + i * Digit.Size.Width,
                    this.BorderWidth);
				this.AddControl(digit);
			}
		}

		public override Size GetClientSize()
		{
			return size;
		}

        protected override void Draw()
        {
            if (colored) this.LeftUpColor = Color.Gray;
            else this.LeftUpColor = Color.Black;
            base.Draw();
        }
        
        private void Parse(int val)
		{
			int ten = 10;
			int quot = val;
			int tempVal;
			
			for (int i = numberDigits - 1; i >= 0; i--)
			{
				tempVal = quot;
				quot /= ten;
				(this.Controls[i] as Digit).Value = tempVal - quot * ten;
			}
		}

		public int Value
		{
			set
			{ 
				int tempVal;
				if (value < minValue) currValue = minValue;
				else if (value > maxValue) currValue = maxValue;
				else currValue = value;

				if (currValue < 0) 
				{
					tempVal = -currValue;
					(this.Controls[0] as Digit).Minus = true;
				}
				else 
				{
					tempVal = currValue;
					(this.Controls[0] as Digit).Minus = false;
				}
				Parse(tempVal);
			}
			get
			{ 
				return currValue; 
			}
		}

		public static int MaxValue
		{
			get { return maxValue; }
		}

        public static void MakeColored()
        {
            colored = true;
            Digit.LoadResource(true);
        }

        public static void MakeMonochromed()
        {
            colored = false;
            Digit.LoadResource(false);
        }
	}

	/// <summary>
    /// Represents the timer, displayed in field of the indicator.
	/// </summary>
	public class MineTimer : Indicator
	{
		private Timer timer = new Timer();
        private bool frozen = false;

		public MineTimer() : base()
		{
			timer.Interval = 1000;
			timer.Tick += new EventHandler(timer_Tick);
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if (this.Value < MaxValue) this.Value++;
			else timer.Enabled = false;
		}

		public void Start()
		{
			if (timer.Enabled) return;
			this.Value = 1;
			timer.Enabled = true;
		}

		public void Stop()
		{
			timer.Enabled = false;
            frozen = false;
		}

        public bool Frozen
        {
            get { return frozen; }
            set 
            {
                if (timer.Enabled)
                {
                    if (value)
                    {
                        frozen = value;
                        timer.Enabled = false;
                    }
                }
                else
                {
                    if (frozen)
                    {
                        frozen = false;
                        timer.Enabled = true;
                    }
                }
            }
        }
	}
}

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace Sapper
{
	/// <summary>
	/// Defines the base class for controls, which are components with visual representation. 
	/// </summary>
	public abstract class BaseControl
    {
        #region Types
        /// <summary>
        /// Loading picture from resource.
        /// </summary>
        public class ImageResource
        {
            private const string colourResources = "Color";
            private const string monochromeResources = "Monochrome";
            private Size size;
            private Bitmap bmp;           
            
            public ImageResource(System.Type classType, bool color, Size imageSize)
            {
                string resourceName;
                if (color) resourceName = colourResources;
                else resourceName = monochromeResources;
                size = imageSize;
                System.Resources.ResourceManager resources = new System.Resources.ResourceManager(classType);
                bmp = (Bitmap)resources.GetObject(resourceName);
            }

            public ImageResource(object control, bool color, Size imageSize)
                : this(control.GetType(), color, imageSize) { }

            public Bitmap Get(int number)
            {
                return bmp.Clone(new Rectangle(new Point(0, size.Height * number), size), bmp.PixelFormat);
            }
        }
        protected delegate void MouseEvent(MouseEventArgs e);
        #endregion

        #region Member Variables
        private static Control parentControl;
		protected static Rectangle clipRectangle;
        private static MouseEvent MouseDown;
        private static MouseEvent MouseUp;
        private static MouseEvent MouseMove;
		protected static Graphics graphics;
        protected static Graphics graphicsHwnd;
        #endregion

        #region Abstract Members
        public abstract Size GetClientSize();
        public abstract Point GetLocation();
        public abstract Point Location
        {
            get;
            set;
        }
        #endregion

        #region Misc
        public BaseControl() { }
        /// <summary>
        /// Set main form. This form contain all visual components.
        /// </summary>
        /// <param name="parent">Main form.</param>
        protected void SetParent(Control parent)
        {
            parentControl = parent;
            parentControl.Paint += new PaintEventHandler(parentControl_Paint);
            parentControl.MouseDown += new MouseEventHandler(parentControl_MouseDown);
            parentControl.MouseUp += new MouseEventHandler(parentControl_MouseUp);
            parentControl.MouseMove += new MouseEventHandler(parentControl_MouseMove);
        }
        #endregion

        #region Processing Mouse Events
        private void parentControl_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDown(e);
        }

        private void parentControl_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUp(e);
        }

        private void parentControl_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMove(e);
        }

        protected void SetMouseEvents(MouseEvent Down, MouseEvent Up, MouseEvent Move)
        {           
            MouseDown += Down;
            MouseUp += Up;
            MouseMove += Move;
        }
        #endregion

        #region Sizes Function
        protected void SetParentSize(Size size)
        {
            parentControl.ClientSize = size;
        }
        		
		public Rectangle GetDrawRect()
		{
			return new Rectangle(GetLocation(), GetClientSize());
		}
		
		public Rectangle GetClientRect()
		{
			return new Rectangle(GetLocation(), GetClientSize());
        }
        #endregion

        #region Drawing Functions
        private void parentControl_Paint(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;
            clipRectangle = e.ClipRectangle;
            Update();
        }

        protected Graphics GetGraphicsFromHwnd()
        {
            return Graphics.FromHwnd(parentControl.Handle);
        }

        protected virtual void Draw() {}

		public void Update()
		{
			if (clipRectangle.IntersectsWith(GetDrawRect())) Draw();
		}

		public void Redraw()
		{
			graphics = Graphics.FromHwnd(parentControl.Handle);
			clipRectangle = parentControl.ClientRectangle;
			Update();
		}

        public void Invalidate()
        {
            parentControl.Invalidate(this.GetClientRect());
        }
        #endregion
    }

	/// <summary>
    /// Defines the base class for controls, which are components with defined location.
	/// </summary>
	public abstract class NonStaticBaseControl : BaseControl
	{
		protected Point location;

		public NonStaticBaseControl() {}
		public NonStaticBaseControl(Point location) { this.location = location; }
		
		public override Point GetLocation()
		{
			return location;
		}
		public override Point Location
		{
			get { return location; }
			set	{ location = value; }
		}

		public int LocationX
		{
			set { this.location.X = value; }
		}
	}

    /// <summary>
    /// The base class for container controls.
    /// </summary>
    public abstract class BaseContainer : NonStaticBaseControl
    {
        #region Types Definition
        /// <summary>
        /// Represents a collection of BaseControl objects. 
        /// </summary>
        public class ControlsCollection : IEnumerable
        {
            private ArrayList controlList = new ArrayList();

            public ControlsCollection() { }

            public void Add(BaseControl control) { controlList.Add(control); }

            public int IndexOf(BaseControl control) { return controlList.IndexOf(control); }

            public int Size() { return controlList.Count; }

            public void Clear() { controlList.Clear(); }

            #region IEnumerable Members

            public IEnumerator GetEnumerator() { return controlList.GetEnumerator(); }

            #endregion

            public BaseControl this[int i]
            {
                get { return (BaseControl)controlList[i]; }
                set { controlList[i] = value; }
            }
        }
        #endregion

        #region Misc
        public BaseContainer()
        {
            this.controls = new ControlsCollection();
        }
        protected override void Draw()
        {
            foreach (BaseControl control in controls)
                control.Update();
        }
        #endregion

        #region Controls Manipulation
        private ControlsCollection controls;

        //public abstract ControlsCollection Controls { get { return controls; } }

        public virtual void AddControl(BaseControl control)
        {
            control.Location = control.Location + (Size)this.location;
            this.controls.Add(control);
        }
        #endregion

        #region Location
        public override System.Drawing.Point GetLocation()
        {
            return Location;
        }

        public override Point Location
        {
            set
            {
                Point diff = value - (Size)this.location;
                this.location = value;
                foreach (BaseControl control in controls)
                    control.Location = control.Location + (Size)diff;
            }
        }
        #endregion
    }

    /// <summary> 
    /// Defines the base class for controls, which contains other controls.
	/// </summary>
	public abstract class ContainerControl : NonStaticBaseControl
    {
        #region Types Definition
        /// <summary>
        /// Represents a collection of BaseControl objects.
		/// </summary>
		public class ControlsCollection : IEnumerable
		{
			private ArrayList controlList = new ArrayList();
		
			public ControlsCollection() {}

			public void Add(BaseControl control) { controlList.Add(control); }

            public int IndexOf(BaseControl control) { return controlList.IndexOf(control); }

            public int Size() { return controlList.Count; } 

			public void Clear() { controlList.Clear(); }

			#region IEnumerable Members

			public IEnumerator GetEnumerator() { return controlList.GetEnumerator(); }

			#endregion

			public BaseControl this[int i]
			{
				get	{	return (BaseControl) controlList[i]; }
				set	{	controlList[i] = value;	}
			}
        }
        #endregion

        #region Misc
        public ContainerControl()
        {
            this.controls = new ControlsCollection();
        }
        protected override void Draw()
        {
            foreach (BaseControl control in controls)
                control.Update();
        }
        #endregion

        #region Controls Manipulation
        private ControlsCollection controls;

        public ControlsCollection Controls { get { return controls; } }
		
		public virtual void AddControl(BaseControl control)
		{
			control.Location = control.Location + (Size)this.location;
			this.controls.Add(control);
        }
        #endregion

        #region Location
        public override System.Drawing.Point GetLocation()
        {
            return Location;
        }

        public override Point Location
		{
			set
			{
				Point diff = value - (Size)this.location;
				this.location = value;
				foreach (BaseControl control in controls)
					control.Location = control.Location + (Size) diff;
			}
        }
        #endregion      		
	}
}

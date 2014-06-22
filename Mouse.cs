using System;
using System.Windows.Forms;
using System.Drawing;

namespace Sapper
{
	/// <summary>
	/// Preprocessing mouse messages.
	/// </summary>
	public class MouseMsgFilter : IMessageFilter
	{
        private const int WM_NCLBUTTONDOWN  = 0x00A1;	// pressed the left mouse button while the cursor is within the nonclient area of a window.
        private const int WM_NCLBUTTONUP    = 0x00A2;	// released the left mouse button while the cursor is within the nonclient area of a window.
        private const int WM_NCRBUTTONDOWN  = 0x00A4;	// pressed the right mouse button while the cursor is within the nonclient area of a window. 
        private const int WM_NCRBUTTONUP    = 0x00A5;	// released the right mouse button while the cursor is within the nonclient area of a window.
        private const int WM_LBUTTONDOWN    = 0x0201;	// pressed the left mouse button while the cursor is in the client area of a window. 
        private const int WM_LBUTTONUP      = 0x0202;	// released the left mouse button while the cursor is in the client area of a window.
        private const int WM_RBUTTONDOWN    = 0x0204;	// pressed the right mouse button while the cursor is in the client area of a window.
        private const int WM_RBUTTONUP       = 0x0205;	// released the right mouse button while the cursor is in the client area of a window. 
        
		private System.Windows.Forms.Label debugLabel;
		private Sapper.MouseArrow mouse;

		#region IMessageFilter Members
		public MouseMsgFilter()	{ }

		public bool PreFilterMessage(ref Message m)
		{
			switch (m.Msg)
			{
				case WM_LBUTTONDOWN:
					mouse.LButton = true;
					break;
				case WM_LBUTTONUP:
					mouse.LButton = false;
					break;
				case WM_RBUTTONDOWN:
					mouse.RButton = true;
					break;
				case WM_RBUTTONUP:
					mouse.RButton = false;
					break;
			}
			return false;
		}

		public void SetDebugLabel(Label label) { debugLabel = label; }
		
		public void SetMouse(MouseArrow mouse) { this.mouse = mouse; }
		#endregion
	}
	
	/// <summary>
    /// Represents states of mouse.
	/// </summary>
	public class MouseArrow
	{
		private bool lButton    = false;
		private bool rButton    = false;
		private bool dButton    = false;
		private bool groupMode  = false;
        private bool blocked    = false;

		/// <summary>
        /// Initializing the group mode mouse buttons pressed (right and left together).
		/// </summary>
		private void DButtonState()
		{
			dButton = lButton && rButton; 
			if (dButton) groupMode = true;
			else groupMode = groupMode && (lButton || rButton);
		}
        
		public MouseArrow() {}
		
		/// <summary>
        /// The Left button is pressed.
		/// </summary>
		public bool LButton  
		{
			set 
			{
				lButton = value;
				DButtonState();
			}
			get { return lButton; }
		}

		/// <summary>
        /// The Right button is pressed.
		/// </summary>
		public bool RButton  
		{
			set 
			{
				rButton = value; 
				DButtonState();
			}
			get { return rButton; }
		}

		/// <summary>
        /// Left and Right buttons is pressed.
		/// </summary>
		public bool DButton  
		{
			set { dButton = value; }
			get { return dButton; }
		}

		/// <summary>
        /// Group mode is enabled (after pressed two buttons)
		/// </summary>
		public bool GroupMode
		{
			set { groupMode = value; }
			get { return groupMode; }
		}

        public bool Blocked
        {
            set { blocked = value; }
            get { return blocked; }
        }

        public bool GroupModePressOneButton { get { return groupMode && (lButton ^ rButton); } }
	}
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sapper
{
    /// <summary>
    /// The panel, containing timer, counter of the mines
    /// and button of the renovation of the play.
    /// </summary>
    public class ControlPanel : LoweredPanel
    {
        #region Member Variables
        public const int Height = 37;
        private Indicator indicator = new Indicator();
        private LoweredPanel mainButtonPanel = new LoweredPanel();
        private MineTimer timer = new MineTimer();
        private int width;
        private Parameters parameters;
        public MainButton mainButton;
        #endregion

        #region Variables Assignment

        public ControlPanel()
        {
            this.BorderWidth = 2;
            this.size.Height = Height;

            mainButton = new MainButton();
            mainButton.Location = new Point(1, 1);

            mainButtonPanel.Size = new Size(MainButton.Size.Width + 2,
                MainButton.Size.Height + 2);
            mainButtonPanel.BorderWidth = 1;
            mainButtonPanel.RightDownColor = mainButtonPanel.LeftUpColor;
            mainButtonPanel.AddControl(mainButton);

            indicator.Location = new Point(7, 6);

            this.AddControl(indicator);
            this.AddControl(mainButtonPanel);
            this.AddControl(timer);
        }

        public void SetParameters(Parameters parameters) { this.parameters = parameters; }

        #endregion

        #region Propertys

        public int Width
        {
            get { return width; }
            set
            {
                this.width = value;
                this.size = new Size(width, Height);
                timer.Location = new Point(
                    this.Location.X + width - 9 - MineTimer.Size.Width,
                    this.Location.Y + 6);
                this.mainButtonPanel.Location =
                    new Point(this.Location.X + width / 2 - MainButton.Size.Width / 2 - 1,
                    this.Location.Y + 6);
            }
        }

        public int MinesCount
        {
            get { return indicator.Value; }
            set { indicator.Value = value; }
        }

        #endregion

        #region External Commands
        public void NewGame()
        {
            this.mainButton.NormalState();
            MinesCount = parameters.Mines;
            StopTimer();
            timer.Value = 0;
        }

        public void InitColorRes()
        {
            this.mainButton.Color = parameters.Color;
            if (parameters.Color)
            {
                Indicator.MakeColored();
                mainButtonPanel.RightDownColor = this.LeftUpColor = mainButtonPanel.LeftUpColor = Color.Gray; 
            }
            else
            {
                Indicator.MakeMonochromed();
                mainButtonPanel.RightDownColor = this.LeftUpColor = mainButtonPanel.LeftUpColor = Color.Black;
            }
        }
        
        public void ChangeColor()
        {
            this.mainButton.Color = parameters.Color;
            if (parameters.Color)
            {
                Indicator.MakeColored();
                mainButtonPanel.RightDownColor = this.LeftUpColor = mainButtonPanel.LeftUpColor = Color.Gray;
            }
            else
            {
                Indicator.MakeMonochromed();
                mainButtonPanel.RightDownColor = this.LeftUpColor = mainButtonPanel.LeftUpColor = Color.Black;
            }
            this.Redraw();
        }

        public void Explosion()
        {
            this.mainButton.Explosion();
            timer.Stop();
        }

        public void Success()
        {
            this.mainButton.Success();
            timer.Stop();
            this.parameters.Modify(this.timer.Value);
        }
        #endregion

        #region Timers Manipulation
        public void StartTimer()
        {
            timer.Start();
        }

        public void StopTimer()
        {
            timer.Stop();
        }
        
        public bool FrozenTimer
        {
            get { return timer.Frozen; }
            set { timer.Frozen = value; }
        }
        #endregion
    }

    /// <summary>
    /// The lowered panel, containing minefield.
    /// </summary>
    public class MinePanel : LoweredPanel
    {
        #region Member Variables
        private MineField mineField = new MineField();
        private const int indent = 6;
        private Parameters parameters;
        #endregion

        #region Variables Assignment

        public MinePanel()
        {
            this.BorderWidth = 3;
            mineField.Location = new Point(this.BorderWidth, this.BorderWidth);
            this.AddControl(mineField);
        }

        public void SetControlPanel(ControlPanel ctrl) { mineField.SetControlPanel(ctrl); }

        public void SetParameters(Parameters parameters) 
        {
            this.mineField.SetParameters(parameters);
            this.parameters = parameters;
        }

        #endregion

        #region External Commands

        public void NewGame() { mineField.NewGame(); }

        public void ChangeColor()
        {
            mineField.ChangeColor();
            if (parameters.Color) this.LeftUpColor = Color.Gray;
            else this.LeftUpColor = Color.Black;
            this.Redraw();
        }

        public bool NotResized()
        {
            if (parameters.Color) this.LeftUpColor = Color.Gray;
            else this.LeftUpColor = Color.Black;
            if (mineField.NotResized()) return true;
            this.Size = new Size(2 * this.BorderWidth + mineField.Width,
                2 * this.BorderWidth + mineField.Height);
            return false;
        }

        #endregion
    }

    /// <summary>
    /// The panel, containing all controls.
    /// </summary>
    public class MainPanel : RaisedPanel
    {
        #region Member Variables
        private ControlPanel ctrlPanel;
        private MinePanel minePanel;

        public const int Indent = 6;
        public const int RightDownIndent = 5;
        #endregion

        #region Variables Assignment And Configurating
        public MainPanel(Control control)
        {
            SetParent(control);

            ctrlPanel = new ControlPanel();
            minePanel = new MinePanel();

            ctrlPanel.Location = new Point(this.BorderWidth + Indent, this.BorderWidth + Indent);
            ctrlPanel.mainButton.Click += new MainButton.ClickDelegate(NewGame);

            minePanel.Location = new Point(this.BorderWidth + Indent,
                this.BorderWidth + 2 * Indent + ControlPanel.Height);
            minePanel.SetControlPanel(ctrlPanel);

            this.AddControl(minePanel);
            this.AddControl(ctrlPanel);
        }

        public void SetParameters(Parameters parameters)
        {
            this.minePanel.SetParameters(parameters);
            this.ctrlPanel.SetParameters(parameters);
        }
        #endregion

        #region Sizes
        public bool NotResized()
        {
            if (minePanel.NotResized()) return true;
            this.Size = new Size(this.BorderWidth + Indent + minePanel.Size.Width + RightDownIndent,
                this.BorderWidth + ControlPanel.Height + 2 * Indent + minePanel.Size.Height + RightDownIndent);
            ctrlPanel.Width = minePanel.Size.Width;
            this.SetParentSize(this.Size);
            return false;
        }

        public void Minimized()
        {
            ctrlPanel.FrozenTimer = true;
        }

        public void Normal()
        {
            ctrlPanel.FrozenTimer = false;
        }
        #endregion

        #region Run New Game
        public void NewGame()
        {
            this.ctrlPanel.NewGame();
            this.minePanel.NewGame();
        }

        public void InitColorRes()
        {
            this.ctrlPanel.InitColorRes();
        }

        public void ChangeColor()
        {
            ctrlPanel.ChangeColor();
            minePanel.ChangeColor();
        }

        #endregion
    }
}

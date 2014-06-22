using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace Sapper
{
    public partial class AboutBox : Form
    {
        private void DrawDivisionLine(Graphics g)
        {
            int x = 77;
            int y = 88;
            int length = 320;
            g.DrawLine(new Pen(Color.FromKnownColor(KnownColor.ControlDark)),
                new Point(x, y), new Point(x + length, y));
            g.DrawLines(new Pen(Color.FromKnownColor(KnownColor.ControlLightLight)), new Point[] {
                new Point(x, y + 1),
                new Point(x + length + 1, y + 1),
                new Point(x + length + 1, y)
            });
        }
        private void ShowMemoryInfo()
        {
            try
            {
                ManagementClass managClass = new ManagementClass("Win32_OperatingSystem");
                ManagementObjectCollection managCollect = managClass.GetInstances();
                ManagementObject[] managArray = new ManagementObject[managCollect.Count];
                managCollect.CopyTo(managArray, 0);
                UInt64 memSize = (UInt64)managArray[0].Properties["TotalVisibleMemorySize"].Value;
                this.lblMemory.Text += string.Format("{0:#,#} КБ", memSize);                
            }
            catch (Exception)
            {

                this.lblMemory.Visible = false;
                this.lblProcessor.Location = this.lblMemory.Location;
            }
        }
        private void ShowProcInfo()
        {
            try
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0");
                string name = ((string)rk.GetValue("ProcessorNameString")).Trim();
                this.lblProcessor.Text += name;
            }
            catch (Exception)
            {

                this.lblProcessor.Visible = false;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawDivisionLine(e.Graphics);
        }
        public AboutBox()
        {
            InitializeComponent();
            
            this.lblOS.Text += Environment.OSVersion.VersionString;
            this.lblOS.Text += Environment.OSVersion.Platform;
            this.lblFramework.Text += Environment.Version.Major.ToString() + "." +
                Environment.Version.MajorRevision.ToString();
            this.lblUser.Text += Environment.UserName;

            ShowMemoryInfo();
            ShowProcInfo();                        
        }

        public void SetMineIcon(Icon mineIcon)
        {
            this.pbMineIcon.Image = mineIcon.ToBitmap();
        }
    }

    public class ShellAboutBox
    {
        [DllImport("shell32")]
        private static extern int ShellAbout(int hWnd, String pApp, String pOtherStuff, int hIcon);

        public static void Show(System.Windows.Forms.Form parent)
        {
            /*PlatformID platform = Environment.OSVersion.Platform;
            if ((platform == PlatformID.Win32NT) || (platform == PlatformID.Win32Windows))
                ShowAboutBoxForWindows(parent);
            else
                ShowAboutBoxForOthersPlatforms(parent);*/
            ShowAboutBoxForOthersPlatforms(parent);
        }

        private static void ShowAboutBoxForWindows(System.Windows.Forms.Form parent)
        {
            String pText = "Sapper.NET";
            String pCaption = "Copyright © 2008 Alexander Perepelitsa";
            ShellAbout(parent.Handle.ToInt32(), pText, pCaption, parent.Icon.Handle.ToInt32());
        }
        
        private static void ShowAboutBoxForOthersPlatforms(System.Windows.Forms.Form parent)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.Location = parent.Location + new Size(33, 74);
            aboutBox.SetMineIcon(parent.Icon);
            aboutBox.ShowDialog();
            aboutBox.Dispose();
        }
    }
}
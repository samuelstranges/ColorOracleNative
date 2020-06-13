using System;
using System.Drawing;
using System.Windows.Forms;

namespace Color_Test_WPF_App_NET_Framework
{
    /// <summary>
    /// The initialize of the SelectArea Form
    /// </summary>
    public partial class SelectArea : Form
    {
        /// <summary>
        /// Create a half transparent but remove the original borders
        /// </summary>
        public SelectArea()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None; // no borders
            this.DoubleBuffered = true;
            this.Opacity = .5D;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            WindowState = FormWindowState.Maximized;
        }
        int thickness = 5;
        

       
        /// <summary>
        /// Create four rectanges to replace the orginal border with darkCyan color
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle Top =new Rectangle(0, 0, this.ClientSize.Width, thickness);
            Rectangle Left = new Rectangle(0, 0, thickness, this.ClientSize.Height);
            Rectangle Bottom =new Rectangle(0, this.ClientSize.Height-thickness, this.ClientSize.Width, thickness);
            Rectangle Right = new Rectangle(this.ClientSize.Width-thickness, 0, thickness, this.ClientSize.Height);
            e.Graphics.FillRectangle(Brushes.DarkCyan, Top);
            e.Graphics.FillRectangle(Brushes.DarkCyan, Left);
            e.Graphics.FillRectangle(Brushes.DarkCyan, Right);
            e.Graphics.FillRectangle(Brushes.DarkCyan, Bottom);
        }
        
        ///private const int HT_CLIENT = 0x1;
        ///private const int HT_CAPTION = 0x2;
        /// <summary>
        /// The window message listner which would catch the window message such as mouse click. 
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {  // Trap WM_NCHITTEST
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.X < thickness)
                {
                    if (pos.Y < thickness)
                    {
                        m.Result = (IntPtr)13;  // TOPLEFT

                    }
                    else if (pos.Y > thickness && pos.Y < this.ClientSize.Height - thickness)
                    {
                        m.Result = (IntPtr)10; //LEFT


                    }
                    else if (pos.Y > this.ClientSize.Height - thickness && pos.Y < this.ClientSize.Height)
                    {
                        m.Result = (IntPtr)16;//BOTTOMLEFT

                    }
                    return;
                }
                else if (pos.X > thickness && pos.X < this.ClientSize.Width -  thickness)
                {
                    if (pos.Y < thickness)
                    {
                        m.Result = (IntPtr)12;//TOP
                    }
                    else if (pos.Y > this.ClientSize.Height - thickness & pos.Y < this.ClientSize.Height)
                    {
                        m.Result = (IntPtr)15;//BOTTOM
                    }
                    else if (pos.Y > thickness && pos.Y < this.ClientSize.Height - thickness)
                    {
                        m.Result = (IntPtr)2;//TITLEBAR 
                    }
                    return;
                }
                else if (pos.X > this.ClientSize.Width - thickness && pos.X < this.ClientSize.Width)
                {
                    if (pos.Y < thickness)
                    {
                        m.Result = (IntPtr)14;  // TOPRIGHT

                    }
                    else if (pos.Y > thickness && pos.Y < this.ClientSize.Height - thickness)
                    {
                        m.Result = (IntPtr)11; //RIGHT


                    }
                    else if (pos.Y > this.ClientSize.Height - thickness && pos.Y < this.ClientSize.Height)
                    {
                        m.Result = (IntPtr)17;//BOTTOMRIGHT

                    }
                    return;

                }
                
                
            }
            base.WndProc(ref m);
        }
        /// <summary>
        /// Save button listener to trigger the save screenshot method
        /// </summary>
        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Save_Screenshot save = new Save_Screenshot(this.Location.X, this.Location.Y, this.Width, this.Height, this.Size);

        }
        

       
    }
}

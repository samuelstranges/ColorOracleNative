using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;
using System.Drawing.Imaging;

namespace Color_Test_WPF_App_NET_Framework
{
    public partial class Live : Form
    {
        PictureBox pb = new PictureBox();
        Bitmap bmp;
        public Int32 x1, y1, w1, h1;
        public Size s1;
        private static System.Timers.Timer aTimer;
        public Live(Int32 x, Int32 y, Int32 w, Int32 h, Size s)
        {
            InitializeComponent();
            x1 = x;
            y1 = y;
            w1 = w;
            h1 = h;
            s1 = s;
            
            Rectangle rect = new Rectangle(x, y, w, h);
            bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bmp);
                g.CopyFromScreen(rect.Left, rect.Top, 0, 0, s, CopyPixelOperation.SourceCopy);
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Size = bmp.Size;

                pb.Dock = DockStyle.Fill;
                pb.Image = bmp;
                this.Controls.Add(pb);
                this.ShowDialog();


            
           
        }
        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true; 
            aTimer.Enabled = true;
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
        }
    }
}

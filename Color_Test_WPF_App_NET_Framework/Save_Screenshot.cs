using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Color_Test_WPF_App_NET_Framework
{
    public partial class Save_Screenshot : Form
    {
        Bitmap bmp;
        PictureBox pb = new PictureBox();
        
        public Save_Screenshot(Int32 x, Int32 y, Int32 w, Int32 h, Size s)
        {
            InitializeComponent();
            
            Rectangle rect = new Rectangle(x, y, w, h);
            bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, s, CopyPixelOperation.SourceCopy);
            bmp = FilterImg(bmp);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = bmp.Size;
            
            pb.Dock = DockStyle.Fill;
            pb.Image = bmp;
            this.Controls.Add(pb);
            this.ShowDialog();

            
        }
        private static Bitmap FilterImg(Bitmap bmp)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double GAMMA = 2.2;
            double GAMMA_INV = 1 / GAMMA;

            Matrix<double> RGB_TO_LMS_MATRIX = DenseMatrix.OfArray(new double[,] {
                { 17.8824, 43.5161, 4.11935 },
                { 3.45565, 27.1554, 3.86714 },
                { 0.0299566, 0.184309, 1.46709 }
            });

            Matrix<double> RGB_TO_LMS_MATRIX_INV = RGB_TO_LMS_MATRIX.Inverse();

            Matrix<double> PROTAN_MATRIX = DenseMatrix.OfArray(new double[,] {
                { 0, 2.02344, -2.52581 },
                { 0, 1, 0 },
                { 0, 0, 1 }
            });

            Matrix<double> DEUTAN_MATRIX = DenseMatrix.OfArray(new double[,] {
                { 1, 0, 0 },
                { 0.494207, 0, 1.24827 },
                { 0, 0, 1 }
            });

            double[] SRGB_TO_LRGB = generate_SRGB_TO_LRGB_TABLE();


            // string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\awesome.jpg";
            Bitmap image = bmp;

            unsafe
            {
                BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, image.PixelFormat);
                int bytesPerPixel = Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;


                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixels, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int oldBlue = currentLine[x];
                        int oldGreen = currentLine[x + 1];
                        int oldRed = currentLine[x + 2];

                        // process
                        Matrix<double> lrgb = DenseMatrix.OfArray(new double[,] {
                        { SRGB_TO_LRGB[oldRed] },
                        { SRGB_TO_LRGB[oldGreen] },
                        { SRGB_TO_LRGB[oldBlue] }
                    });

                        Matrix<double> lms = RGB_TO_LMS_MATRIX.Multiply(lrgb);
                        Matrix<double> adjusted_lms = PROTAN_MATRIX.Multiply(lms);
                        Matrix<double> tmp = RGB_TO_LMS_MATRIX_INV.Multiply(adjusted_lms);

                        int newRed = apply_gamma(tmp[0, 0]);
                        int newGreen = apply_gamma(tmp[1, 0]);
                        int newBlue = apply_gamma(tmp[2, 0]);

                        currentLine[x] = (byte)newBlue;
                        currentLine[x + 1] = (byte)newGreen;
                        currentLine[x + 2] = (byte)newRed;
                    }
                });

                image.UnlockBits(bitmapData);
            }

            //image.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\new_awesome.jpg");

            stopwatch.Stop();
            Console.WriteLine("process is done! time used: {0}ms", stopwatch.ElapsedMilliseconds);
            Console.ReadLine();
            return image;
        }
        private static int apply_gamma(double val)
        {
            return Convert.ToInt32(255 * Math.Pow(val, (1 / 2.2)));
        }
        private static double[] generate_SRGB_TO_LRGB_TABLE()
        {
            double[] SRGB_TO_LRGB = new double[256];
            double GAMMA = 2.2;

            for (int i = 0; i < 256; i++)
            {
                double col_without_gamma = Math.Pow(i / 255.0, GAMMA);
                double lin = 0.992052 * col_without_gamma + 0.003974;
                SRGB_TO_LRGB[i] = lin;
            }
            return SRGB_TO_LRGB;
        }
     
        private void Button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.CheckPathExists = true;
            sfd.FileName = "Capture";
            sfd.Filter = "PNG Image(*.png)|*.png|JPG Image(*.jpg)|*.jpg|BMP Image(*.bmp)|*.bmp";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pb.Image.Save(sfd.FileName);
            }
            this.Hide();
        }

        private void Save_Screenshot_Load(object sender, EventArgs e)
        {

        }
    }
}

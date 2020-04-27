using System;
using System.Runtime.InteropServices;

namespace Color_Test_WPF_App_NET_Framework
{
  
   
    public class Program1
    {
        const string Magnification = "Magnification.dll";

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagInitialize();

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagUninitialize();

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagSetFullscreenColorEffect(ref MAGCOLOREFFECT pEffect);

        public struct MAGCOLOREFFECT
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            public float[] transform;
        }

        public int color_filter_key;
        public Program1(int key) {
            this.color_filter_key = key;
            
        }
        
        public void live()
        {
            float redScale = 0.2126f, greenScale = 0.7152f, blueScale = 0.0722f;
            var grayscale = new MAGCOLOREFFECT
            {
                transform = new[] {
                    redScale,   redScale,   redScale,   0.0f,  0.0f,
                    greenScale, greenScale, greenScale, 0.0f,  0.0f,
                    blueScale,  blueScale,  blueScale,  0.0f,  0.0f,
                    0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                    0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                }
            };

            var protan = new MAGCOLOREFFECT
            {
                transform = new[] {
                   
                    0.5667f,   0.4333f,   0.0f,   0.0f,  0.0f,
                    0.5583f,   0.4416f,   0.0f, 0.0f,  0.0f,
                    0.0f,  0.2416f, 0.7583f,  0.0f,  0.0f,
                    0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                    0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                }
            };

            var deuteran = new MAGCOLOREFFECT
            {
                transform = new[] {
                    0.625f, 0.375f,   0.0f,   0.0f,  0.0f,
                    0.700f,   0.300f,   0.0f, 0.0f,  0.0f,
                    0.0f,  0.300f, 0.700f,  0.0f,  0.0f,
                    0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                    0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                }
            };

            var tritan = new MAGCOLOREFFECT
            {
                transform = new[] {
                    0.95f, 0.05f,   0.0f,   0.0f,  0.0f,
                    0.0f,   0.433f,   0.567f, 0.0f,  0.0f,
                    0.0f,  0.475f, 0.525f,  0.0f,  0.0f,
                    0.0f,       0.0f,       0.0f,       1.0f,  0.0f,
                    0.0f,       0.0f,       0.0f,       0.0f,  1.0f
                }
            };
            //default matrix
            var original = new MAGCOLOREFFECT
            {
                transform = new[] {
                   1.0f,  0.0f,  0.0f,  0.0f,  0.0f,
                   0.0f,  1.0f,  0.0f,  0.0f,  0.0f,
                   0.0f,  0.0f,  1.0f,  0.0f,  0.0f,
                   0.0f,  0.0f,  0.0f,  1.0f,  0.0f,
                   0.0f,  0.0f,  0.0f,  0.0f,  1.0f
                }
            };

            MagInitialize();
            switch (this.color_filter_key)
            {
                case 0:
                    MagSetFullscreenColorEffect(ref original);
                    break;
                case 1:
                    MagSetFullscreenColorEffect(ref deuteran);
                    break;
                case 2:
                    MagSetFullscreenColorEffect(ref protan);
                    break;
                case 3:
                    MagSetFullscreenColorEffect(ref tritan);
                    break;
                case 4:
                    MagSetFullscreenColorEffect(ref grayscale);
                    break;

            }
                
            Console.ReadLine();
            
        }
      
    }

    public class NativeMethods
    {
        const string Magnification = "Magnification.dll";

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagInitialize();

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagUninitialize();

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagSetFullscreenColorEffect(ref MAGCOLOREFFECT pEffect);
        
        public struct MAGCOLOREFFECT
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            public float[] transform;
        }
    }
}
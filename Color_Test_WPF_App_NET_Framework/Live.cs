﻿using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace Color_Test_WPF_App_NET_Framework
{
  
   
    /// <summary>
    /// Real time simulator for color-impaired vision (deuteranopia, protanopia, tritanopia and grayscale)
    /// </summary>
    public class Live
    {
        // import Magnification package
        const string Magnification = "Magnification.dll";

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagInitialize();

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagUninitialize();

        [DllImport(Magnification, ExactSpelling = true, SetLastError = true)]
        public static extern bool MagSetFullscreenColorEffect(ref MAGCOLOREFFECT pEffect);

        // construct a struct for color effect
        public struct MAGCOLOREFFECT
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 25)]
            public float[] transform;
        }


        /// <summary>
        /// key used to identified users selected color blindness type
        /// 1 - Deuteranopia
        /// 2 - Protanopia
        /// 3 - Tritanopia
        /// 4 - Grayscale
        /// </summary>
        public int color_filter_key;

        
        /// <summary>
        /// live update status
        /// true if it has been toggled on
        /// false if it has been toggle off or not in used
        /// </summary>
        public bool status = false;



        /// <summary>
        /// Create an instance of Program1
        /// </summary>
        /// <param name="key">specific color filter key</param>
        public Live(int key)
        {
            if (key >= 0 && key <=4)
            {
                color_filter_key = key;
            }
        }

        
        /// <summary>
        /// Apply specific color matrix values to the screen effect to allow
        /// color blindness simulation running in real time
        /// 
        /// The 5x5 matrices (for full homogeneous coordinates of RGBA) 
        /// transform RGB colors into colorspace which simulate the
        /// imparement of color blindness deficiency.
        ///
        /// RGB transform matrices generated by Michael of www.colorjack.com
        /// Which were created using code by Matthew Wickline and the
        /// Human-Computer Interaction Resource Network ( http://hcirn.com/ )
        /// 
        /// Values of the color matrices could be tweaked in the future to
        /// improve the accuracy of simulation.
        /// </summary>
        public void live()
        {
            // Assigns 5x5 color matrices to specific variables
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

            // color matrix for original vision
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

            // Creates and initializes the magnifier run-time objects
            MagInitialize();
            MAGCOLOREFFECT type;


            if (status) // run the simulation if live mode is toggled on
            {
                color_filter_key = MainWindow.color_filter_key;

                // Changes the color transformation matrix associated with the full-screen magnifier.
                switch (color_filter_key)
                {
                    case 1:
                        type = deuteran;
                        break;
                    case 2:
                        type = protan;
                        break;
                    case 3:
                        type = tritan;
                        break;
                    case 4:
                        type = grayscale;
                        break;
                    default:
                        type = original;
                        break;
                }
            }
            else // stop the simulation if live mode is toggled off (set to original)
            {
                type = original; 
            }

            try
            {
                MagSetFullscreenColorEffect(ref type);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            
        }
      
    }

   
}
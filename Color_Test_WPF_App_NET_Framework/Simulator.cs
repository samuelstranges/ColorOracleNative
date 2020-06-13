using System;

namespace Color_Test_WPF_App_NET_Framework
{
    /// <summary>
    /// A simulator for color-impaired vision (deuteranopia, protanopia, tritanopia and grayscale).
    /// 
    /// For a description of the algorithm, see: Vienot, F., Brettel, H., Mollon,
    /// J.D., (1999). Digital video colourmaps for checking the legibility of
    /// displays by dichromats. Color Research and Application 24, 243-252.
    /// 
    /// Code has been modified and converted from Java Implementation
    /// https://github.com/nvkelso/color-oracle-java/blob/master/src/ika/colororacle/Simulator.java
    /// </summary>
    internal class Simulator
    {
        
        /// <summary>
        /// Default screen gamma on Windows is 2.2
        /// </summary>
        private const double GAMMA = 2.2;
        private const double GAMMA_INV = 1.0 / GAMMA;

        /// <summary>
        /// A lookup table for the conversion from gamma-corrected sRGB values
        /// [0..255] to linear RGB values [0.32767]
        /// </summary>
        private readonly static short[] SRGB_TO_LINRGB;

        /// <summary>
        /// A lookup table for the conversion of linear RGB values [0..255]
        /// to gamma-corrected sRGB values [0..255]
        /// </summary>
        private readonly static byte[] LINRGB_TO_SRGB;
        

        /// <summary>
        /// Creates a new instance of Simulator
        /// </summary>
        static Simulator()
        {
            // initialize SRGB_TO_LINRGB 
            SRGB_TO_LINRGB = new short[256];
            for (int i = 0; i < 256; i++)
            {
                // compute linear rgb between 0 and 1
                double lin = 0.992052 * Math.Pow(i / 255.0, GAMMA) + 0.003974;

                // scale linear rgb to 0..32767
                SRGB_TO_LINRGB[i] = (short)(lin * 32767.0);
            }


            // initialize LINRGB_TO_SRGB
            LINRGB_TO_SRGB = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                LINRGB_TO_SRGB[i] = (byte)(255.0 * Math.Pow(i / 255.0, GAMMA_INV));
            }
        }


        /// <summary>
        /// Simulate color impaired vision
        /// </summary>
        /// <param name="inData">pixels data to be processed</param>
        /// <returns>processed pixels data</returns>
        public int[] Simulate(int[] inData)
        {
            switch (MainWindow.color_filter_key)
            {
                case 1:     // Deuteran
                    return new RedGreenFilter(9591, 23173, -730).filter(inData);
                case 2:     // Protan
                    return new RedGreenFilter(3683, 29084, 131).filter(inData);
                case 3:     // Tritan
                    return new TritanFilter().filter(inData);
                case 4:     // Grayscale
                    return new GrayscaleFilter().filter(inData);
                default:
                    return new GrayscaleFilter().filter(inData);
            }
        }

        /// <summary>
        /// A red-green blindness filter (deuteranopia and protanopia)
        /// </summary>
        private class RedGreenFilter
        {
            private readonly int k1;
            private readonly int k2;
            private readonly int k3;


            public RedGreenFilter(int k1, int k2, int k3)
            {
                this.k1 = k1;
                this.k2 = k2;
                this.k3 = k3;
            }


            /// <summary>
            /// Simulate red green color blindness (deuteranopia and protanopia) vision
            /// </summary>
            /// <param name="inData"></param>
            /// <returns></returns>
            public int[] filter(int[] inData)
            {
                int prevIn = 0;
                int prevOut = 0;
                int length = inData.Length;
                int[] outData = new int[length];

                for (int i = 0; i < length; i++)
                {
                    int nowIn = inData[i];

                    if (nowIn == prevIn)
                    {
                        outData[i] = prevOut;
                    }
                    else
                    {
                        int r = (0xff0000 & nowIn) >> 16;
                        int g = (0xff00 & nowIn) >> 8;
                        int b = 0xff & nowIn;


                        int r_lin = SRGB_TO_LINRGB[r];
                        int g_lin = SRGB_TO_LINRGB[g];
                        int b_lin = SRGB_TO_LINRGB[b];


                        // simulated red and green are identical
                        // scale the matrix values to 0..2^15 for integer computations 
                        // of the simulated protan values.
                        // divide after the computation by 2^15 to rescale.
                        // also divide by 2^15 and multiply by 2^8 to scale the linear rgb to 0..255
                        // total division is by 2^15 * 2^15 / 2^8 = 2^22
                        // shift the bits by 22 places instead of dividing
                        int r_blind = (k1 * r_lin + k2 * g_lin) >> 22;
                        int b_blind = (k3 * r_lin - k3 * g_lin + 32768 * b_lin) >> 22;

                        if (r_blind < 0)
                        {
                            r_blind = 0;
                        }
                        else if (r_blind > 255)
                        {
                            r_blind = 255;
                        }

                        if (b_blind < 0)
                        {
                            b_blind = 0;
                        }
                        else if (b_blind > 255)
                        {
                            b_blind = 255;
                        }



                        // convert reduced linear rgb to gamma corrected rgb
                        int red = LINRGB_TO_SRGB[r_blind];
                        red = red >= 0 ? red : 256 + red; // from unsigned to signed
                        int blue = LINRGB_TO_SRGB[b_blind];
                        blue = blue >= 0 ? blue : 256 + blue; // from unsigned to signed

                        int nowOut = (red << 16 | red << 8 | blue);

                        outData[i] = nowOut;
                        prevIn = nowIn;
                        prevOut = nowOut;
                    }
                }
                return outData;
            }
        }

        /// <summary>
        /// A Tritanopia blindness filter
        /// </summary>
        private class TritanFilter
        {
            /// <summary>
            /// convert image into tritanopia vision
            /// </summary>
            /// <param name="inData">pixels data to be process</param>
            /// <returns>processed pixels data</returns>
            public int[] filter(int[] inData)
            {
                /* Code for tritan simulation from GIMP 2.2
                 *  This could be optimised for speed.
                 *  Performs tritan color image simulation based on
                 *  Brettel, Vienot and Mollon JOSA 14/10 1997
                 *  L,M,S for lambda=475,485,575,660
                 *
                 * Load the LMS anchor-point values for lambda = 475 & 485 nm (for
                 * protans & deutans) and the LMS values for lambda = 575 & 660 nm
                 * (for tritans)
                 */
                const float anchor_e0 = 0.05059983f + 0.08585369f + 0.00952420f;
                const float anchor_e1 = 0.01893033f + 0.08925308f + 0.01370054f;
                const float anchor_e2 = 0.00292202f + 0.00975732f + 0.07145979f;
                const float inflection = anchor_e1 / anchor_e0;

                /* Set 1: regions where lambda_a=575, set 2: lambda_a=475 */
                const float a1 = -anchor_e2 * 0.007009f;
                const float b1 = anchor_e2 * 0.0914f;
                const float c1 = anchor_e0 * 0.007009f - anchor_e1 * 0.0914f;
                const float a2 = anchor_e1 * 0.3636f - anchor_e2 * 0.2237f;
                const float b2 = anchor_e2 * 0.1284f - anchor_e0 * 0.3636f;
                const float c2 = anchor_e0 * 0.2237f - anchor_e1 * 0.1284f;

                int prevIn = 0;
                int prevOut = 0;
                int length = inData.Length;
                int[] outData = new int[length];

                for (int i = 0; i < length; i++)
                {
                    int nowIn = inData[i];

                    if (nowIn == prevIn)
                    {
                        outData[i] = prevOut;
                    }
                    else
                    {
                        int r = (0xff0000 & nowIn) >> 16;
                        int g = (0xff00 & nowIn) >> 8;
                        int b = 0xff & nowIn;

                        // get linear rgb values in the range 0..2^15-1
                        r = SRGB_TO_LINRGB[r];
                        g = SRGB_TO_LINRGB[g];
                        b = SRGB_TO_LINRGB[b];

                        /* Convert to LMS (dot product with transform matrix) */
                        float L = (r * 0.05059983f + g * 0.08585369f + b * 0.00952420f) / 32767f;
                        float M = (r * 0.01893033f + g * 0.08925308f + b * 0.01370054f) / 32767f;
                        float S;

                        float tmp = M / L;

                        /* See which side of the inflection line we fall... */
                        if (tmp < inflection)
                        {
                            S = -(a1 * L + b1 * M) / c1;
                        }
                        else
                        {
                            S = -(a2 * L + b2 * M) / c2;
                        }

                        /* Convert back to RGB (cross product with transform matrix) */
                        int ired = (int)(255f * (L * 30.830854f - M * 29.832659f + S * 1.610474f));
                        int igreen = (int)(255f * (-L * 6.481468f + M * 17.715578f - S * 2.532642f));
                        int iblue = (int)(255f * (-L * 0.375690f - M * 1.199062f + S * 14.273846f));

                        // convert reduced linear rgb to gamma corrected rgb
                        if (ired < 0)
                        {
                            ired = 0;
                        }
                        else if (ired > 255)
                        {
                            ired = 255;
                        }
                        else
                        {
                            ired = LINRGB_TO_SRGB[ired];
                            ired = ired >= 0 ? ired : 256 + ired; // from unsigned to signed
                        }
                        if (igreen < 0)
                        {
                            igreen = 0;
                        }
                        else if (igreen > 255)
                        {
                            igreen = 255;
                        }
                        else
                        {
                            igreen = LINRGB_TO_SRGB[igreen];
                            igreen = igreen >= 0 ? igreen : 256 + igreen; // from unsigned to signed
                        }
                        if (iblue < 0)
                        {
                            iblue = 0;
                        }
                        else if (iblue > 255)
                        {
                            iblue = 255;
                        }
                        else
                        {
                            iblue = LINRGB_TO_SRGB[iblue];
                            iblue = iblue >= 0 ? iblue : 256 + iblue; // from unsigned to signed
                        }

                        int nowOut = ired << 16 | igreen << 8 | iblue;

                        outData[i] = nowOut;
                        prevIn = nowIn;
                        prevOut = nowOut;
                    }
                }
                return outData;
            }
        }

        
        /// <summary>
        /// A filter for grayscale conversion: perceptual luminance-preserving
        /// conversion to grayscale
        /// https://en.wikipedia.org/wiki/Grayscale#Colorimetric_(perceptual_luminance-preserving)_conversion_to_grayscale
        /// </summary>
        private class GrayscaleFilter
        { 
            /// <summary>
            /// convert image into grayscale vision
            /// </summary>
            /// <param name="inData">pixels data to be process</param>
            /// <returns>processed pixels data</returns>
            public int[] filter(int[] inData)
            {
                int prevIn = 0;
                int prevOut = 0;
                int length = inData.Length;
                int[] outData = new int[length];

                for (int i = 0; i < length; i++)
                {
                    int nowIn = inData[i];

                    if (nowIn == prevIn)
                    {
                        outData[i] = prevOut;
                    }
                    else
                    {
                        int r = (0xff0000 & nowIn) >> 16;
                        int g = (0xff00 & nowIn) >> 8;
                        int b = 0xff & nowIn;

                        // get linear rgb values in range 0..2^15-1
                        int r_lin = SRGB_TO_LINRGB[r];
                        int g_lin = SRGB_TO_LINRGB[g];
                        int b_lin = SRGB_TO_LINRGB[b];

                        // perceptual luminance-preserving conversion to grayscale
                        // https://en.wikipedia.org/wiki/Grayscale#Colorimetric_(perceptual_luminance-preserving)_conversion_to_grayscale
                        double luminance = 0.2126 * r_lin + 0.7152 * g_lin + 0.0722 * b_lin;
                        int linRGB = ((int)(luminance)) >> 8; // divide by 2^8 to rescale

                        // convert linear rgb to gamma corrected sRGB
                        if (linRGB < 0)
                        {
                            linRGB = 0;
                        }
                        else if (linRGB > 255)
                        {
                            linRGB = 255;
                        }
                        else
                        {
                            linRGB = LINRGB_TO_SRGB[linRGB];
                            linRGB = linRGB >= 0 ? linRGB : 256 + linRGB; // from unsigned to signed
                        }

                        int nowOut = linRGB << 16 | linRGB << 8 | linRGB;

                        outData[i] = nowOut;
                        prevIn = nowIn;
                        prevOut = nowOut;
                    }
                }
                return outData;
            }
        }
    }
}
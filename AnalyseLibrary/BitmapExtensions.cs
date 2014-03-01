/**
 * Wyższa Szkoła Informatyki i Zarządzania w Rzeszowie
 * 
 * Marek Bar 33808
 * 
 * Zastosowanie informatyki w eksploracji danych medycznych
 *  
 * Projekt: Analiza obrazu prześwietlenia rentgenowskiego zawierającego kości.
 * Cele projektu:
 * - odnajdywanie wzoru linii przylegającej do kości - położenie kości na obrazie
 * - ustalanie kąta pomiędzy liniami - próba detekcji złamania na obrazie,
 * - ustalanie punktów przecięcia - próba lokalizacji złamania na obrazie.
 * 
 * Kontakt: marekbar1985@gmail.com lub w33808@wsiz.rzeszow.pl
 * 
 **/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace AnalyseLibrary
{
    /// <summary>
    /// Some custom Bitmap extensions
    /// </summary>
    public static class BitmapExtensions
    {
        /// <summary>
        /// Convert to grayscale - natural
        /// </summary>
        /// <param name="color">Bitmap - color image</param>
        /// <returns>Bitmap - in grayscale</returns>
        public static Bitmap ToGrayscale(this Bitmap color)
        {
            try
            {
                Bitmap tmp = color;
                int pixelSize = (color.PixelFormat == PixelFormat.Format48bppRgb) ? 3 : 4;
                int width = color.Width;
                int height = color.Height;
                BitmapData data = tmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, tmp.PixelFormat);

                IntPtr ptr = data.Scan0;
                int bytes = Math.Abs(data.Stride) * tmp.Height;
                byte[] rgbValues = new byte[bytes];

                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

                double factorR = 0.2125;
                double factorG = 0.7154;
                double factorB = 0.0721;
                byte divider = 3;
                for (int counter = 0; counter < rgbValues.Length; counter += pixelSize)
                {
                    rgbValues[counter] = rgbValues[counter + 1]  = rgbValues[counter + 2] = (byte)((
                        (rgbValues[counter] * factorR) +
                        (rgbValues[counter + 1] * factorG) +
                        (rgbValues[counter + 2] * factorB)
                        ) / divider);
                }

                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

                tmp.UnlockBits(data);

                return tmp;
            }
            catch
            {
                return color;
            }
        }

        /// <summary>
        /// Converrt 8 bit image ttto 24 bit image - for dawing ccolor linespurpose
        /// </summary>
        /// <param name="bmp">Bitmap - 8 bit</param>
        /// <returns>Bitmap - 24  bit</returns>
        public static Bitmap Bit8ToBit24(this Bitmap bmp)
        {
            Bitmap tmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb);

            for (var i = 0; i < bmp.Height; i++)
                for (var j = 0; j < bmp.Width; j++)
                    tmp.SetPixel(j, i, bmp.GetPixel(j, i));
            return tmp;
        }

        /// <summary>
        /// Bitmap threshold
        /// </summary>
        /// <param name="bmp">Bitmap</param>
        /// <param name="isAdaptive">bool - is adaptive thresold</param>
        /// <param name="level">byte - threshold level</param>
        /// <returns>Bitmap - after threshold</returns>
        public static Bitmap Threshold(this Bitmap bmp, bool isAdaptive = true, byte level = 128)
        {
            try
            {
                Bitmap tmp = bmp;
                int pixelSize = (bmp.PixelFormat == PixelFormat.Format48bppRgb) ? 3 : 4;
                int width = bmp.Width;
                int height = bmp.Height;
                BitmapData data = tmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, tmp.PixelFormat);

                IntPtr ptr = data.Scan0;
                int bytes = Math.Abs(data.Stride) * tmp.Height;
                byte[] rgbValues = new byte[bytes];

                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

                if (isAdaptive)
                {
                    byte min = rgbValues.Min();
                    byte max = rgbValues.Max();
                    level = (byte)((min + max) / 2);
                }

                for (int counter = 0; counter < rgbValues.Length; counter += pixelSize)
                {
                    rgbValues[counter] = rgbValues[counter + 1] = rgbValues[counter + 2] = (byte)(rgbValues[counter] >= level ? 255 : 0);
                }


                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

                tmp.UnlockBits(data);

                return tmp;
              
            }
            catch
            {
                return bmp;
            }
        }

        /// <summary>
        /// Adaptive threshold
        /// </summary>
        /// <param name="bmp">Bitmap - original bitmap - in grayscale</param>
        /// <returns>Bitmap - thresholded bitmap</returns>
        public static Bitmap ThresholdAdaptive(this Bitmap bmp)
        {
            try
            {
                Bitmap tmp = bmp;
                int pixelSize = (bmp.PixelFormat == PixelFormat.Format48bppRgb) ? 3 : 4;
                int width = bmp.Width;
                int height = bmp.Height;
                BitmapData data = tmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, tmp.PixelFormat);

                IntPtr ptr = data.Scan0;
                int bytes = Math.Abs(data.Stride) * tmp.Height;
                byte[] values = new byte[bytes];
                byte[] output = new byte[bytes];

                Marshal.Copy(ptr, values, 0, bytes);

                byte level = 128;
                int minPixel = 3 * width * pixelSize + 4 * pixelSize;
                int maxPixel = values.Length - (3 * width * pixelSize + 4 * pixelSize);
                int stride = width * pixelSize;


                int maskSize = 25;
                int maskHalfSize = maskSize/2;
                for (var row = 0; row < height; ++row)
                {
                    for (var col = 0; col < width; ++col)
                    {
                        int index = row * stride / pixelSize + col;
                        if (row > maskHalfSize && row < height - maskHalfSize && col > maskHalfSize && col < width - maskHalfSize)
                        {
                            List<byte> local = new List<byte>();
                            var indexes = index.GetPixelIndexes(stride, pixelSize, row, col, maskSize);
                            int sum = 0;
                            foreach (var ind in indexes)
                            {
                                local.Add(values[ind]);
                                sum += values[ind];
                            }

                            level = (byte)(sum / local.Count());
                        }
                        else
                        {
                            level = 128;
                        }
                        output[index] = output[index + 1] = output[index + 2] = (byte)(values[index] >= level ? 255 : 0);
                    }
                }


                Marshal.Copy(output, 0, ptr, bytes);

                tmp.UnlockBits(data);

                return tmp;

            }
            catch
            {
                return bmp;
            }
        }

        /// <summary>
        /// Gets pixel surrounding pixels indexes
        /// </summary>
        /// <param name="i">int - current pixel index</param>
        /// <param name="stride">int - image stride</param>
        /// <param name="pixelSize">int - pixel size(channels)</param>
        /// <param name="row">int - current row</param>
        /// <param name="col">int - current col</param>
        /// <param name="maskSize">int - mask size</param>
        /// <returns>int[] - pixels' indexes</returns>
        public static int[] GetPixelIndexes(this int i, int stride, int pixelSize, int row, int col, int maskSize)
        {
            var indexes = new List<int>();
            //int index = row * stride / pixelSize + col;
            int ms = maskSize / 2;
            for (int r = row - ms; r < row + ms; r++)
            {
                for (int c = col - ms; c < col + ms; c++)
                {
                    indexes.Add(r * stride / pixelSize + c);
                }
            }
            return indexes.ToArray();
        }

        /// <summary>
        /// Copy Bitmap
        /// </summary>
        /// <param name="srcBitmap">Bitmap - source</param>
        /// <param name="section">Rectangle - region to copy</param>
        /// <returns>Bitmap - copied Bitmap region</returns>
        public static Bitmap Copy(this Bitmap srcBitmap, Rectangle section)
        {
            // Create the new bitmap and associated graphics object
            Bitmap bmp = new Bitmap(section.Width, section.Height, srcBitmap.PixelFormat);
  
            Graphics g = Graphics.FromImage(bmp);

            // Draw the specified section of the source bitmap to the new one
            g.DrawImage(srcBitmap, 0, 0, section, GraphicsUnit.Pixel);

            // Clean up
            g.Dispose();

            // Return the bitmap
            return bmp;
        }

        /// <summary>
        /// Gets bitmap bounds as rectangle
        /// </summary>
        /// <param name="bmp">Bitmap</param>
        /// <returns>Rectangle</returns>
        public static Rectangle Rectangle(this Bitmap bmp)
        {
            return new Rectangle(0, 0, bmp.Width, bmp.Height);
        }

        /// <summary>
        /// Gets direct access to pixels
        /// </summary>
        /// <param name="bmp">Bitmap</param>
        /// <returns>BitmapData</returns>
        public static BitmapData Lock(this Bitmap bmp)
        {
            return bmp.LockBits(bmp.Rectangle(),
                   System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
        }

        /// <summary>
        /// Unlock pixels
        /// </summary>
        /// <param name="bmp">Bitmap</param>
        /// <param name="data">BitmapData</param>
        public static void Unlock(this Bitmap bmp, BitmapData data)
        {
            bmp.UnlockBits(data);
        }

        /// <summary>
        /// Get Bitmap center point
        /// </summary>
        /// <param name="bmp">Bitmap</param>
        /// <returns>Point</returns>
        public static Point GetCenter(this Bitmap bmp)
        {
            Point p = new Point();
            p.X = bmp.Width / 2;
            p.Y = bmp.Height / 2;
            return p;
        }

        /// <summary>
        /// Convert degrees to radians
        /// </summary>
        /// <param name="degrees">double degrees</param>
        /// <returns>double - radians</returns>
        public static double DegreesToRadians(this double degrees)
        {
            return (degrees / 180) * Math.PI;
        }

        /// <summary>
        /// Get sinus
        /// </summary>
        /// <param name="value">double - value</param>
        /// <returns>double - sinus value</returns>
        public static double Sinus(this double value)
        {
            return Math.Sin(value);
        }

        /// <summary>
        /// Get cosinus
        /// </summary>
        /// <param name="value">double - value</param>
        /// <returns>double - cosinus value</returns>
        public static double Cosinus(this double value)
        {
            return Math.Cos(value);
        }

        public static void Deduplicate(this List<int> values, int delta)
        {
            int index = 0;
            while (index < values.Count)
            {
                int value = values[index];

                int i = index + 1;
                while (i < values.Count)
                {
                    if (value - values[i] < delta)
                        values.RemoveAt(i);
                    else
                        ++i;
                }
                ++index;
            }
        }
    }
}

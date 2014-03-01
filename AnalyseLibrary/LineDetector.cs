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
using System.Text;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace AnalyseLibrary
{
    /// <summary>
    /// Detects lines in image - looking for lines aligning to bone edged
    /// </summary>
    public class LineDetector
    {
        /// <summary>
        /// Lines found in image
        /// </summary>
        public List<Line> Lines = new List<Line>();

        /// <summary>
        /// Operating area - region of interest
        /// </summary>
        public Rectangle ROI;

        /// <summary>
        /// Apply - search for broken bones - by finding lines and detecting angles between them
        /// </summary>
        /// <param name="bmp">Bitmap - original</param>
        /// <returns>Bitmap - processed</returns>
        public Bitmap Apply(Bitmap bmp)
        {
            System.Drawing.Imaging.PixelFormat pf = bmp.PixelFormat;
            ROI = bmp.Rectangle();
            var lineTransform = new AForge.Imaging.HoughLineTransformation();
            lineTransform.MinLineIntensity = 60;
            lineTransform.LocalPeakRadius = 15;
            //lineTransform.StepsPerDegree = 10;
            //lineTransform.LocalPeakRadius = 10;
            // apply Hough line transofrm
            lineTransform.ProcessImage(bmp);
            Bitmap houghLineImage = lineTransform.ToBitmap();
            // get lines using relative intensity
            var lines = lineTransform.GetLinesByRelativeIntensity(0.5);
            lines = filterLinesByTheta(lines);
            

            Dilatation dl = new Dilatation();
            dl.ApplyInPlace(bmp);

            Invert filter = new Invert();
            filter.ApplyInPlace(bmp);

            bmp = bmp.Bit8ToBit24();

            foreach (var line in lines)
            {
                // get line's radius and theta values
                int radius = line.Radius;
                double lineSlopeInDegrees = line.Theta;

                // check if line is in lower part of the image
                if (radius < 0)
                {
                    lineSlopeInDegrees += 180;
                    radius = -radius;
                }

                var radians = lineSlopeInDegrees.DegreesToRadians();

                var imageCenter = bmp.GetCenter();

                double  x0 = 0, //most left point
                        y0 = 0,
                        x1 = 0, //most right point
                        y1 = 0;

                if (line.Theta != 0)
                {
                    // none-vertical line
                    x0 = -imageCenter.X; // most left point
                    x1 = imageCenter.X;  // most right point

                    // calculate corresponding y values
                    y0 = (-radians.Cosinus() * x0 + radius) / radians.Sinus();
                    y1 = (-radians.Cosinus() * x1 + radius) / radians.Sinus();
                }
                else
                {
                    // vertical line
                    x0 = line.Radius;
                    x1 = line.Radius;

                    y0 = imageCenter.Y;
                    y1 = -imageCenter.Y;
                }

                var line2 = new Line(
                    (int)x0 + imageCenter.X, imageCenter.Y - (int)y0,
                    (int)x1 + imageCenter.X, imageCenter.Y - (int)y1);
                line2.X1 = (int)x0 + imageCenter.X;
                line2.Y1 = imageCenter.Y - (int)y0;
                line2.X2 = (int)x1 + imageCenter.X;
                line2.Y2 = imageCenter.Y - (int)y1;
                Lines.Add(line2);

            }
            return drawLines(bmp);
        }

        private HoughLine[] filterLinesByTheta(HoughLine[] lines)
        {
            List<int> thetas = new List<int>();
            foreach (var l in lines) thetas.Add((int)l.Theta);
            thetas.Deduplicate(20);

            List<HoughLine> hls = new List<HoughLine>();

            foreach (var l in lines)
            {
                if (thetas.Contains((int)l.Theta)) hls.Add(l);
            }
            return hls.ToArray();
        }

        private Bitmap drawLines(Bitmap bmp)
        {
            // draw line on the image
            var data = bmp.Lock();

            foreach (var line in Lines)
            {
                Drawing.Line(data, new IntPoint(line.X1, line.Y1), new IntPoint(line.X2, line.Y2), Color.Red);
            }
            bmp.Unlock(data);
            return bmp;
        }

        /// <summary>
        /// Get list of points where lines cross each other
        /// </summary>
        public List<System.Drawing.Point> CrossPoints
        {
            get
            {
                List<System.Drawing.Point> cp = new List<System.Drawing.Point>();

                foreach (var line in Lines)
                {
                    foreach (var l in Lines)
                    {
                        if (line.A != l.A && line.B != l.B && !line.IsPararrel(l))
                        {
                            var tmp = line.CrossPoint(l);
                            if (tmp.X >= 0 && tmp.Y >= 0 && tmp.X <= ROI.Width*4 && tmp.Y <= ROI.Height*4)
                            {
                                cp.Add(tmp);
                            }
                        }
                    }
                }

                return cp;
            }
        }

        /// <summary>
        /// Get text information about found lines, angles between them and cross points
        /// </summary>
        /// <returns>String</returns>
        public String GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Wzory linii:"); 
            foreach (var line in Lines)
            {
                sb.AppendLine(line.ToString());
            }

            sb.AppendLine("Kąty:");
            foreach (var x in Lines)
            {
                foreach (var y in Lines)
                {
                    var ang = Math.Abs(x.GetAngle(y));
                    if(ang != 180 && ang != 0)
                    sb.AppendLine("Kąt: " + ang.ToString());
                }
            }

            sb.AppendLine("Punkty przecięcia:");
            foreach (var p in CrossPoints)
            {
                sb.AppendLine(String.Format("[{0}, {1}]", p.X, p.Y));
            }

            return sb.ToString();
        }
        
    }
}

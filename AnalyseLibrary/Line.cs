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

namespace AnalyseLibrary
{
    /// <summary>
    /// Line
    /// </summary>
    public class Line
    {
        public int X1;
        public int X2;
        public int Y1;
        public int Y2;
        /// <summary>
        /// Default constructor - defines line parameters
        /// </summary>
        /// <param name="A">double - direction factor</param>
        /// <param name="B">double - shift factor</param>
        public Line(double A, double B)
        {
            this.A = A;
            this.B = B;
        }

        /// <summary>
        /// Default constructor - defines line by two its points
        /// </summary>
        /// <param name="x1">double - point 1 X</param>
        /// <param name="y1">double - point 1 Y</param>
        /// <param name="x2">double - point 2 X</param>
        /// <param name="y2">double - point 2 Y</param>
        /// <remarks>direction and shift factors are calculated inside</remarks>
        public Line(double x1, double y1, double x2, double y2)
        {
            var y = y1 - y2;
            var x = x1 - x2;
            A = x / y;
            B = y1 - x1 * A;

        }

        /// <summary>
        /// Direction factor
        /// </summary>
        public double A { get; set; }

        /// <summary>
        /// Shift factor
        /// </summary>
        public double B { get; set; }

        /// <summary>
        /// Gets line pattern
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return String.Format("Linia: y = {0}x + {1}", Math.Round(A,2), Math.Round(B,2));
        }

        /// <summary>
        /// Gets two lines common point
        /// </summary>
        /// <param name="other">Line - other line to check if cuts with this one</param>
        /// <returns>Point</returns>
        public System.Drawing.Point CrossPoint(Line other)
        {
            var p = new System.Drawing.Point();

            //x = (b1 - b2)/(a2 - a1);
            p.X = (int)((this.B - other.B) / (other.A - this.A));

            //y = (a2 * b1 - b2 * a1) / (a2 - a1); 
            p.Y = (int)((other.A * this.B - other.B * this.A) / (other.A - this.A));

            return p;
        }

        /// <summary>
        /// Get angle in between two lines in degrees
        /// </summary>
        /// <param name="other">Line - second line</param>
        /// <returns>int - angle without fraction part</returns>
        public int GetAngle(Line other)
        {
            double xDiff = other.A - this.B;
            double yDiff = other.B - this.B;
            double angle = Math.Atan2(yDiff, xDiff) * (180 / Math.PI);

            return (int)angle;

        }

        /// <summary>
        /// Convert radian to degrees
        /// </summary>
        /// <param name="radian">int - radians</param>
        /// <returns>int - degrees</returns>
        private int RadianToDegree(int radian)
        {
            return (int)(radian * (180 / Math.PI));
        }

        /// <summary>
        /// Check if line is parrarel to other line
        /// </summary>
        /// <param name="other">Line - other line</param>
        /// <returns>bool - true - parrarel, false- no parrarel</returns>
        public bool IsPararrel(Line other)
        {
            return (this.A * other.B - this.B * other.A) == 0;
        }

        public System.Drawing.Point GetPoint()
        {
            System.Drawing.Point p = new System.Drawing.Point();
            p.X = X1;
            p.Y = Y1;
            return p;
        }
        public int distanceToLine(System.Drawing.Point p)
        {
            System.Drawing.Point p1 = new System.Drawing.Point();
            p1.X = this.X1;
            p1.Y = this.Y1;

            System.Drawing.Point p2 = new System.Drawing.Point();
            p2.X = this.X2;
            p2.Y = this.Y2;
            var A = p2.X - p1.X;
            var B = p2.Y - p1.Y;
            System.Drawing.Point p3 = new System.Drawing.Point();
            var u = (A * (p.X - p1.X) + B * (p.Y - p1.Y)) / (Math.Pow(A, 2) + Math.Pow(B, 2));
            if (u <= 0)
            {
                p3 = p1;
            }
            else if (u >= 1)
            {
                p3 = p2;
            }
            else
            {
                p3.X = (int)(p1.X + u * A);
                p3.Y = (int)(p1.Y + u * B);

            }
            return (int)(Math.Sqrt(Math.Pow(p.X - p3.X, 2) + Math.Pow(p.Y - p3.Y, 2)));
        }
    }
}

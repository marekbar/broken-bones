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
using System.Drawing;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace AnalyseLibrary
{
    /// <summary>
    /// Bone detector filter
    /// </summary>
    public class BoneDetector
    {
        /// <summary>
        /// Operating medical image
        /// </summary>
        private Bitmap bitmap = null;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="bmp">Bitmap - image with X-ray image</param>
        public BoneDetector(Bitmap bmp)
        {
            bitmap = bmp;
        }

        /// <summary>
        /// Apply filter
        /// </summary>
        /// <returns>Bitmap - filtered image</returns>
        public Bitmap Apply()
        {
            try
            {
                bitmap = Grayscale.CommonAlgorithms.BT709.Apply(bitmap);

                var normalizer = new ContrastStretch();
                normalizer.ApplyInPlace(bitmap);

                var er = new Erosion();
                er.ApplyInPlace(bitmap);

                var ed = new SobelEdgeDetector();
                ed.ApplyInPlace(bitmap);


                var th = new Threshold();
                th.ApplyInPlace(bitmap);

                return bitmap;
            }
            catch
            {
                return bitmap;
            }
        }
    }
}

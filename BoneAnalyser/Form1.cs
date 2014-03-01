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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AnalyseLibrary;
namespace BoneAnalyser
{
    public partial class forma : Form
    {
        #region PROPS_AND_FIELDS
        private Bitmap image = null;
        private Bitmap original = null;
        private String filename = "";
        private Rectangle roi = new Rectangle();
        private String bonesInfo = "";
        #endregion

        #region CONSTRUCTOR
        public forma()
        {
            InitializeComponent();
        }
        #endregion

        #region MENU_AND_INTERFACE

        private void menuCloseApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuOpenFromFile_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void menuInfo_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }
        #endregion

        #region PRIVATE
        private void openFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Pliki graficzne (*.bmp, *.jpg, *.jpeg, *.png) | *.bmp; *.jpg; *.jpeg; *.png";
            ofd.Title = "Wybór pliku z obrazem medycznym";
            ofd.Multiselect = false;
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = ofd.FileName;
                status.Text = filename;
                loadFileIntoMemory(filename);
            }
            else
            {
                status.Text = "Nie wybrano żadnego pliku.";
            }
        }

        private void loadFileIntoMemory(String path)
        {
            try
            {
                image = new Bitmap(Bitmap.FromFile(path));
                picture.Image = image;
                original = image.Clone(new Rectangle(0, 0, image.Width, image.Height), image.PixelFormat);
                picture2.Image = original;
            }
            catch (Exception ex)
            {
                status.Text = ex.GetError();
            }
        }
        #endregion

        private void cmGrayscale_Click(object sender, EventArgs e)
        {
            try
            {
                if (image == null)
                {
                    openFile();
                }

                try
                {
                    image = Grayscale.CommonAlgorithms.BT709.Apply(image);
                    picture.Image = image;
                }
                catch
                {
                    status.Text = "Wczytaj obraz, aby go modyfikować.";
                }
            }
            catch
            {
                status.Text = "Obraz jest już w skali szarości.";
            }
        }

        private void picture_DoubleClick(object sender, EventArgs e)
        {
            openFile();
        }

        private void cmThreshold_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                openFile();
            }
            //var sharpen = new AForge.Imaging.Filters.Sharpen();
            //sharpen.ApplyInPlace(image);
            //var thresh = new AForge.Imaging.Filters.Threshold();
            //thresh.ApplyInPlace(image);

            picture.Image = image.Threshold();
        }

        private void cmBones_Click(object sender, EventArgs e)
        {
            var bw = new BackgroundWorker();
            bw.DoWork += (a, b) => 
            {
                Data dat = new Data();
                try
                {
                    Bitmap img = (Bitmap)b.Argument;

                    var detector = new BoneDetector(img);

                    var ld = new LineDetector();
                    image = ld.Apply(detector.Apply());
                    picture.Image = image;

                    if (ld.Lines.Count == 2)
                    {
                        var data2 = image.Lock();
                        var um2 = new UnmanagedImage(data2);
                        var crossed = ld.Lines[0].CrossPoint(ld.Lines[1]);

                        Drawing.Rectangle(um2, new Rectangle(crossed.X - 10, crossed.Y - 10, 20, 20), Color.Green);

                        image.Unlock(data2);
                    }
                    dat.image = image;
                    dat.message = ld.GetInfo();
                }
                catch (Exception ex)
                {
                    dat.image = null;
                    dat.message = ex.GetError();
                }
                b.Result = dat;
            };
            bw.RunWorkerCompleted += (a, b) =>
            {
                Data dat = (Data)b.Result;
                picture.Image = dat.image;
                bonesInfo = dat.message;
                blockInterface(false);
                progress.Visible = false;
                status.Text = "Przetwarzanie zakończone";
                MessageBox.Show(dat.message);
            };

            if (image == null)
            {
                openFile();
            }

            blockInterface(true);
            progress.Enabled = true;
            progress.Style = ProgressBarStyle.Marquee;
            progress.Visible = true;
            status.Text = "Przetwarzanie rozpoczęte...";
            bw.RunWorkerAsync(image);
        }



        private void cmContrastRepair_Click(object sender, EventArgs e)
        {
            try
            {
                if (image == null)
                {
                    openFile();
                }
                //var normalizer = new ContrastStretch();
                //normalizer.ApplyInPlace(image);



                picture.Image = image;

            }
            catch (Exception ex) { status.Text = ex.GetError(); }
        }

        #region IMAGE_PIECE_CUT
        private void picture2_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    roi.X = e.X;
                    roi.Y = e.Y;
                    status.Text = string.Format("{0}, {1} ({2}x{3})", roi.X, roi.Y, roi.Width, roi.Height);
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    image = original.Copy(roi);
                    picture.Image = image;
                }
            }
            catch { }
        }

        private void picture2_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    roi.Width = e.X - roi.X;
                    roi.Height = e.Y - roi.Y;
                    picture2.Refresh();

                    System.Drawing.Graphics g = picture2.CreateGraphics();
                    System.Drawing.Pen p = new System.Drawing.Pen(System.Drawing.Color.Red);
                    g.DrawRectangle(p, roi);
                    p.Dispose();
                    g.Dispose();

                    status.Text = string.Format("{0}, {1} ({2}x{3})", roi.X, roi.Y, roi.Width, roi.Height);
                }
            }
            catch { }
        }

        private void picture2_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    var xP = ((picture.ClientSize.Width - original.Width)) / 2;
                    var yP = ((picture.ClientSize.Height - original.Height)) / 2;
                    roi.X = roi.X - xP;
                    roi.Y = roi.Y - yP;
                    roi.Width = (e.X - xP) - roi.X;
                    roi.Height = (e.Y - yP) - roi.Y;
                    status.Text = string.Format("{0}, {1} ({2}x{3})", roi.X, roi.Y, roi.Width, roi.Height);
                }
            }
            catch { }
        }
        #endregion

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {
                if (image == null)
                {
                    status.Text = "Nie wczytano obrazu.";
                    return;
                }
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.AddExtension = true;
                sfd.DefaultExt = "bmp";
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                sfd.Filter = "Bitmapa (*.bmp)| *.bmp";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    image.Save(sfd.FileName);
                    status.Text = "Plik graficzny został zapisany: " + sfd.FileName;

                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(sfd.FileName.Replace("bmp", "txt"), true))
                    {
                        writer.WriteLine(this.bonesInfo);
                    }
                    System.Diagnostics.Process.Start("notepad.exe", sfd.FileName.Replace("bmp", "txt"));
                }
            }
            catch (Exception ex)
            {
                status.Text = ex.GetError();
            }
        }

        private void loadSamplesMenuList()
        {
            String title = "Przykład ";
            for(int i = 1; i <= 17; i++)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = title + i.ToString();
                item.Tag = i.ToString();
                item.Click += (a, b) =>
                {
                    String where = "https://sites.google.com/site/academicprojectresourcepage/home/broken-bones/{0}.jpg";
                    String address = String.Format(where,(String)(((ToolStripMenuItem)a).Tag));
                    loadSample(address);
                };
                samplesParent.DropDownItems.Add(item);
            }
        }

        private void blockInterface(bool block = true)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                this.Controls[i].Enabled = !block;
            }
        }

        private void loadSample(String url)
        {

            status.Text = "Pobieranie pliku z sieci...";
            imageLabel.Text = url;
            progress.Style = ProgressBarStyle.Marquee;
            progress.Visible = true;

            var bw = new BackgroundWorker();
            bw.DoWork += (a, b) =>
            {
                try
                {
                    String address = (String)b.Argument;
                    var request = WebRequest.Create(url);
                    request.Timeout = 60000;

                    Bitmap bmp = null;
                    using (var response = request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            bmp = new Bitmap(Bitmap.FromStream(stream));
                        }
                    }
                    b.Result = bmp;
                }
                catch
                {
                    b.Result = null;
                }
            };
            bw.RunWorkerCompleted += (a, b) =>
            {
                try
                {
                    original = (Bitmap)b.Result;
                    image = original.Copy(original.Rectangle());
                    picture2.Image = original;
                    picture.Image = original;
                    status.Text = "Plik został pobrany.";
                }
                catch
                {
                    status.Text = "Plik nie został pobrany.";
                }
                finally
                {
                    progress.Visible = false;
                    blockInterface(false);
                }
            };
            blockInterface(true);
            progress.Style = ProgressBarStyle.Marquee;
            progress.Visible = true;
            progress.Enabled = true;
            bw.RunWorkerAsync(url);




        }

        /// <summary>
        /// Takes in an image, scales it maintaining the proper aspect ratio of the image such it fits in the PictureBox's canvas size and loads the image into picture box.
        /// Has an optional param to center the image in the picture box if it's smaller then canvas size.
        /// </summary>
        /// <param name="image">The Image you want to load, see LoadPicture</param>
        /// <param name="canvas">The canvas you want the picture to load into</param>
        /// <param name="centerImage"></param>
        /// <returns></returns>

        public static System.Drawing.Image ResizeImage(System.Drawing.Image image, PictureBox canvas, bool centerImage)
        {
            if (image == null || canvas == null)
            {
                return null;
            }

            int canvasWidth = canvas.Size.Width;
            int canvasHeight = canvas.Size.Height;
            int originalWidth = image.Size.Width;
            int originalHeight = image.Size.Height;

            System.Drawing.Image thumbnail =
                new Bitmap(canvasWidth, canvasHeight); // changed parm names
            System.Drawing.Graphics graphic =
                         System.Drawing.Graphics.FromImage(thumbnail);

            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            /* ------------------ new code --------------- */

            // Figure out the ratio
            double ratioX = (double)canvasWidth / (double)originalWidth;
            double ratioY = (double)canvasHeight / (double)originalHeight;
            double ratio = ratioX < ratioY ? ratioX : ratioY; // use whichever multiplier is smaller

            // now we can get the new height and width
            int newHeight = Convert.ToInt32(originalHeight * ratio);
            int newWidth = Convert.ToInt32(originalWidth * ratio);

            // Now calculate the X,Y position of the upper-left corner 
            // (one of these will always be zero)
            int posX = Convert.ToInt32((canvasWidth - (image.Width * ratio)) / 2);
            int posY = Convert.ToInt32((canvasHeight - (image.Height * ratio)) / 2);

            if (!centerImage)
            {
                posX = 0;
                posY = 0;
            }
            graphic.Clear(Color.White); // white padding
            graphic.DrawImage(image, posX, posY, newWidth, newHeight);

            /* ------------- end new code ---------------- */

            System.Drawing.Imaging.ImageCodecInfo[] info =
                             System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            System.Drawing.Imaging.EncoderParameters encoderParameters;
            encoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
            encoderParameters.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality,
                             100L);

            System.IO.Stream s = new System.IO.MemoryStream();
            thumbnail.Save(s, info[1],
                              encoderParameters);

            return System.Drawing.Image.FromStream(s);
        }

        private void forma_Load(object sender, EventArgs e)
        {
            loadSamplesMenuList();
        }

        private void jakToDziałaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new Alg1().ShowDialog();
            }
            catch (Exception ex) { status.Text = ex.GetError(); }
        }

        private void jakZacząćUżywaćProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new Help().ShowDialog();
            }
            catch (Exception ex) { status.Text = ex.GetError(); }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        private void pobierzOpisProjektuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status.Text = "Pobieranie pliku z sieci...";
            progress.Style = ProgressBarStyle.Marquee;
            progress.Visible = true;

            var bw = new BackgroundWorker();
            bw.DoWork += (a, b) =>
            {
                try
                {
                    String url = "https://sites.google.com/site/academicprojectresourcepage/home/broken-bones/Projekt%20-%20Marek%20Bar%2033808.odt?attredirects=0&d=1";
                    var request = WebRequest.Create(url);
                    request.Timeout = 60000;

                    String filename = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).Trim(new char[] { '\\' }) + "\\" + "Marek_Bar_33808_projekt.odt";

                    using (var response = request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (Stream file = File.Create(filename))
                            {
                                CopyStream(stream, file);
                            }
                        }
                    }
                    b.Result = filename;
                }
                catch
                {
                    b.Result = "";
                }
            };
            bw.RunWorkerCompleted += (a, b) =>
            {
                try
                {
                    String file = (String)b.Result;
                    if (file == null) throw new Exception();
                    status.Text = "Plik został pobrany.";
                    try
                    {
                        System.Diagnostics.Process.Start(file);
                    }
                    catch
                    {
                        status.Text = "Otwarcie pliku wymaga OpenOffice";
                    }
                }
                catch
                {
                    status.Text = "Plik nie został pobrany.";
                }
                finally
                {
                    progress.Visible = false;
                    blockInterface(false);
                }
            };
            blockInterface(true);
            progress.Style = ProgressBarStyle.Marquee;
            progress.Visible = true;
            progress.Enabled = true;
            bw.RunWorkerAsync();
        }

        private void stronaProjektuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://sites.google.com/site/academicprojectresourcepage/home/broken-bones");
            }
            catch { status.Text = "Nie można otworzyć strony projektu."; }
        }

    }

    #region OTHER
    public class Data
    {
        public Bitmap image;
        public String message;
    }
    /// <summary>
    /// Form extensions
    /// </summary>
    public static class FormExtensions
    {
        /// <summary>
        /// Get full error
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>String</returns>
        public static String GetError(this Exception ex)
        {
            return ex.Message + (ex.InnerException != null ? ex.InnerException.Message : "");
        }
    }
    #endregion
}

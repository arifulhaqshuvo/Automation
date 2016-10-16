using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoWeb.ImageProcess
{
    public static class ScreenshotUtils
    {

        public static Rectangle Offseted(this Rectangle r, Point p)
        {
            r.Offset(p);
            return r;
        }

        public static Bitmap GetScreenshot(this Control c)
        {
            return GetScreenshot(new Rectangle(c.PointToScreen(Point.Empty), c.Size));
        }

        public static Bitmap GetScreenshot(Rectangle bounds)
        {
            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
                g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            return bitmap;
        }

        public const string DefaultImagesavefiledialogTitle = "Save image";
        public const string DefaultImagesavefiledialogFilter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg|Bitmap Image (*.bmp)|*.bmp|GIF Image (*.gif)|*.gif";

        public const string CustomplacesComputer = "0AC0837C-BBF8-452A-850D-79D08E667CA7";
        public const string CustomplacesDesktop = "B4BFCC3A-DB2C-424C-B029-7FE99A87C641";
        public const string CustomplacesDocuments = "FDD39AD0-238F-46AF-ADB4-6C85480369C7";
        public const string CustomplacesPictures = "33E28130-4E1E-4676-835A-98395C3BC3BB";
        public const string CustomplacesPublicpictures = "B6EBFB86-6907-413C-9AF7-4FC2ABF07CC5";
        public const string CustomplacesRecent = "AE50C081-EBD2-438A-8655-8A092E34987A";

        public static SaveFileDialog GetImageSaveFileDialog(
          string title = DefaultImagesavefiledialogTitle,
          string filter = DefaultImagesavefiledialogFilter)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Title = title;
            dialog.Filter = filter;


            /* //this seems to throw error on Windows Server 2008 R2, must be for Windows Vista only
            dialog.CustomPlaces.Add(CUSTOMPLACES_COMPUTER);
            dialog.CustomPlaces.Add(CUSTOMPLACES_DESKTOP);
            dialog.CustomPlaces.Add(CUSTOMPLACES_DOCUMENTS);
            dialog.CustomPlaces.Add(CUSTOMPLACES_PICTURES);
            dialog.CustomPlaces.Add(CUSTOMPLACES_PUBLICPICTURES);
            dialog.CustomPlaces.Add(CUSTOMPLACES_RECENT);
            */

            return dialog;
        }

        public static void ShowSaveFileDialog(this Image image, IWin32Window owner = null)
        {
            using (SaveFileDialog dlg = GetImageSaveFileDialog())
                if (dlg.ShowDialog(owner) == DialogResult.OK)
                    image.Save(dlg.FileName);
        }

    }
}

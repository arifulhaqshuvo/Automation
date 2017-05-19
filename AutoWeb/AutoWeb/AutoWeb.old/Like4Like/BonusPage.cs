using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace AutoWeb.Like4Like
{
    class BonusPage : Page
    {
        public Page OldPage { get; set; }

        private Bitmap LoadPicture(string url)
        {
            HttpWebRequest wreq;
            HttpWebResponse wresp;
            Stream mystream;
            Bitmap bmp;

            bmp = null;
            mystream = null;
            wresp = null;
            try
            {
                wreq = (HttpWebRequest)WebRequest.Create(url);
                wreq.AllowWriteStreamBuffering = true;

                wresp = (HttpWebResponse)wreq.GetResponse();

                if ((mystream = wresp.GetResponseStream()) != null)
                    bmp = new Bitmap(mystream);
            }
            finally
            {
                if (mystream != null)
                    mystream.Close();

                if (wresp != null)
                    wresp.Close();
            }
            return (bmp);
        }


        public override void Execute(Like4LikeManager mgr)
        {
            //var element = mgr.Driver.FindElement(By.XPath("//*[@id=\"result\"]/img"));
            ////var src = element.GetAttribute("src");

            //var htmlElement = mgr.Driver.FindElement(By.TagName("html"));
            ////Point viewPortLocation = ((ILocatable)htmlElement).Coordinates.onScreen();


            //var img = new GrayImageProcess(ScreenshotUtils.GetScreenshot(new Rectangle(element.Location, element.Size)));
            //img.Image.Save("D:/a.png");
            //var ls = img.Split(7);
            //var arr = GrayImageProcess.Process(ls).Select(x => x.Name).ToArray();
            //var w = ls[0].Width;

            //foreach (var i in GrayImageProcess.Process(ls))
            //{
            //    i.Image.Save("d:/" + i.Name + ".png", ImageFormat.Png);
            //}

            //var builder = new Actions(mgr.Driver);
            //builder.MoveByOffset(element.Location.X + (w * arr[0] + 20), element.Location.Y + 20).Click().Perform();
            //builder.MoveByOffset(element.Location.X + (w * arr[1] + 20), element.Location.Y + 20).Click().Perform();


            MessageBox.Show("Click bonus and ok");
            mgr.CurrentState = OldPage;
            //mgr.CurrentState = new LikeFacebookPage();
        }
    }
}

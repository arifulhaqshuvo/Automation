using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using AutoWeb.ImageProcess;
using AutoWeb.Like4Like;

namespace AutoWeb
{
    static class Program
    {
        public static void Main()
        {
            //var img = new ImageProcess.GrayImageProcess((Bitmap)Image.FromFile(@"C:\Users\ASC689561\Desktop\captcha11.php"));
            //var ls = img.Split(7);


            Cow.Common.Unity.UnityFacade.Reinit("unity");
            Cow.Common.Unity.UnityFacade.Resolve<Like4LikeManager>().Init().Execute();
        }
    }
}

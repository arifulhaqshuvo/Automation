using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AutoWeb.Like4Like
{
    class LogedInPage : Page
    {
        public override void Execute(Like4LikeManager mgr)
        {
            Common.Try(() => { Process(mgr); });
        }

        private void Process(Like4LikeManager mgr)
        {
            mgr.Driver.WaitUntilDocumentIsReady();
            mgr.Driver.Navigate().GoToUrl("http://www.like4like.org/free-facebook-likes.php");
            mgr.Driver.WaitUntilDocumentIsReady();

            var successElement = mgr.Driver.FindElements(By.XPath("//*[@id=\"content\"]/h2"));
            if (successElement.Count > 0)
                mgr.CurrentState = new LikeFacebookPage();
            else
                throw new Exception("Can't go to http://www.like4like.org/free-facebook-likes.php");
        }
    }
}

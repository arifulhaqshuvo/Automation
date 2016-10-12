using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AutoWeb.Like4Like
{
    class LikeFacebookPage : Page
    {
        public override void Execute(Like4LikeManager mgr)
        {

            var oldWindow = mgr.Driver.WindowHandles.First();

            while (true)
            {
                if (mgr.Driver.Url == "http://www.like4like.org/user/bonus-page.php")
                {
                    mgr.CurrentState = new BonusPage();
                    break;
                }

                mgr.Driver.SwitchTo().Window(oldWindow);
                var buttons = new Support.ButtonGetter().Execute(mgr.Driver);


                // ProcessLike(mgr, buttons, oldWindow, "url(http://www.like4like.org/img/icon/earn-facebook-post-like.png)", "//a[@class=\"UFILikeLink _4x9- _4x9_ _48-k\"]");
                ProcessLike(mgr, buttons, oldWindow, "url(http://www.like4like.org/img/icon/earn-facebook-like.png)", "//button[@class=\"likeButton _4jy0 _4jy4 _517h _51sy _42ft\"]", "//span[@class=\"_49vh _2pi7\"]");
                ProcessLike(mgr, buttons, oldWindow, "url(http://www.like4like.org/img/icon/earn-facebook-photo-like.png)", "//a[@class=\"UFILikeLink _4x9- _4x9_ _48-k\"]");

                mgr.Driver.SwitchTo().Window(oldWindow);
                mgr.Driver.Navigate().Refresh();
            }


        }

        private static void ProcessLike(Like4LikeManager mgr, Support.ButtonGetter buttons, string old, string imgBackground, params string[] clickXPath)
        {
            foreach (var button in buttons.Buttons.Where(x => x.BackgroundUrl == imgBackground))
            {
                try
                {
                    mgr.Driver.WaitUntilDocumentIsReady();
                    System.Threading.Thread.Sleep(2000);
                    button.Element.Click();
                    System.Threading.Thread.Sleep(2000);
                    mgr.Driver.SwitchTo().Window(mgr.Driver.WindowHandles.Last());
                    mgr.Driver.WaitUntilDocumentIsReady();

                    foreach (var s in clickXPath)
                    {
                        var likes = mgr.Driver.FindElements(By.XPath(s));

                        if (likes.Count <= 0) continue;
                       
                        foreach (var e in likes)
                        {
                            try
                            {
                                e.Click();
                                break;
                            }
                            catch
                            {
                                // nothing
                            }
                        }
                        System.Threading.Thread.Sleep(2000);
                    }

                    try
                    {
                        while (mgr.Driver.WindowHandles.Count > 1)
                        {
                            mgr.Driver.SwitchTo().Window(mgr.Driver.WindowHandles[1]);
                            mgr.Driver.Close();
                        }
                    }
                    catch
                    {
                        //nothing
                    }
                    finally
                    {
                        mgr.Driver.SwitchTo().Window(old);
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                catch (Exception)
                {
                    // nothing
                }
            }
        }
    }
}

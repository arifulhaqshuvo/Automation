using System;
using System.Collections.Generic;
using System.Linq;
using AutoWeb.Like4Like.Support;
using Cow.Common.Unity;
using OpenQA.Selenium;

namespace AutoWeb.Like4Like
{
    abstract class BaseLike : Page
    {
        public override void Execute(Like4LikeManager mgr)
        {
            Login(mgr);

            while (true)
            {
                ReNavigateEarnUrl(mgr);
                var oldWindow = mgr.Driver.WindowHandles.First();

                if (mgr.Driver.Url == mgr.BonusUrl)
                {
                    var bonus = UnityFacade.Resolve<BonusPage>();
                    bonus.OldPage = this;
                    mgr.CurrentState = bonus;

                    break;
                }

                mgr.Driver.SwitchTo().Window(oldWindow);
                var buttons = new ButtonGetter().Execute(mgr.Driver);

                foreach (var type in GetTypes())
                {
                    var arr = type.Split(new[] { "###" }, StringSplitOptions.RemoveEmptyEntries);
                    ProcessLike(mgr, buttons.Buttons, arr[0], arr[1]);
                }

                mgr.Driver.SwitchTo().Window(oldWindow);
                //mgr.Driver.Navigate().Refresh();
            }
        }

        protected abstract void ReNavigateEarnUrl(Like4LikeManager mgr);
        protected abstract void Login(Like4LikeManager mgr);
        protected abstract string[] GetTypes();

        internal void ReNavigateEarnUrl(Like4LikeManager mgr, string earnUrl)
        {
            mgr.Driver.WaitUntilDocumentIsReady();
            mgr.Driver.Navigate().GoToUrl(earnUrl);
            mgr.Driver.WaitUntilDocumentIsReady();
        }

        protected void ProcessLike(Like4LikeManager mgr, List<ButtonGetter.Button> buttons, string imgBackgroundUrl, string clickXPath)
        {
            var oldUrl = mgr.Driver.WindowHandles.First();
            foreach (var button in buttons.Where(x => x.BackgroundUrl == imgBackgroundUrl))
            {
                try
                {
                    mgr.Driver.WaitUntilDocumentIsReady();
                    System.Threading.Thread.Sleep(2000);
                    button.Element.Click();
                    System.Threading.Thread.Sleep(2000);
                    mgr.Driver.SwitchTo().Window(mgr.Driver.WindowHandles.Last());
                    mgr.Driver.WaitUntilDocumentIsReady();

                    foreach (var xpath in new[] { clickXPath })
                    {
                        var likes = mgr.Driver.FindElements(By.XPath(xpath));
                        likes.ToList().ForEach(x => { try { x.Click(); } catch {/* nothing */} });
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
                        mgr.Driver.SwitchTo().Window(oldUrl);
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

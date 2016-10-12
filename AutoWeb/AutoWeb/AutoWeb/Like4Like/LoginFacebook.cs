using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AutoWeb.Like4Like
{
    class LoginFacebook:Page
    {
        public override void Execute(Like4LikeManager mgr)
        {
            mgr.Driver.Navigate().GoToUrl("https://www.facebook.com/");
            mgr.Driver.WaitUntilDocumentIsReady();
            mgr.Driver.FindElement(By.Id("email")).SendKeys("tinhlagio89@yahoo.com");
            mgr.Driver.FindElement(By.Id("pass")).SendKeys("****");
            mgr.Driver.FindElement(By.Id("u_0_l")).Click();
            mgr.Driver.WaitUntilDocumentIsReady();
            mgr.CurrentState = new LoginPage();


        }
    }
}

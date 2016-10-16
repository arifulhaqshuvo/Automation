using System;
using System.Threading;
using Cow.Common.Unity;
using OpenQA.Selenium;

namespace AutoWeb.Like4Like
{
    class LoginPage : Page
    {
        public string LoginUrl { get; set; }// = "http://www.like4like.org/user/login.php";
        public string ElementUserNameXPath { get; set; } //= "//input[@name='username']";
        public string UserName { get; set; }// = "trang1hoa1";
        public string ElementPasswordXPath { get; set; } //= "//input[@name='password']";
        public string Password { get; set; }// = "Tinhlagio1989";
        public string ElementSubmitXPath { get; set; }// = "//input[@name='submit']";
        public string ElementCheckLoginSuccessXPath { get; set; } //= "//*[@id='earned-credits']";
        public string ElementCheckFailedXPath { get; set; } //= "//*[@id='adcopy-outer']";

        public override void Execute(Like4LikeManager mgr)
        {
            var driver = mgr.Driver;
            Common.Try(() =>
            {
                driver.Navigate().GoToUrl(LoginUrl);
                var myField = driver.FindElement(By.XPath(ElementUserNameXPath));
                myField.SendKeys(UserName);

                myField = driver.FindElement(By.XPath(ElementPasswordXPath));
                myField.SendKeys(Password);

                myField = driver.FindElement(By.XPath(ElementSubmitXPath));
                myField.Click();
                Thread.Sleep(3000);

                driver.WaitUntilDocumentIsReady();

                CheckForNavigate(mgr, driver);
            });
        }


        private void CheckForNavigate(Like4LikeManager mgr, IWebDriver driver)
        {
            if (driver.FindElements(By.XPath(ElementCheckLoginSuccessXPath)).Count > 0)
                mgr.CurrentState = UnityFacade.Resolve<LogedInPage>();
            else
            if (driver.FindElements(By.XPath(ElementCheckFailedXPath)).Count > 0)
                mgr.CurrentState = UnityFacade.Resolve<EnterCodePage>();
            else
            {
                Thread.Sleep(2000);
                throw new Exception("Unknown page");
            }
        }
    }
}

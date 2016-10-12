using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AutoWeb.Like4Like
{
    class LoginPage : Page
    {
        private const string Url = "http://www.like4like.org/user/login.php";
        private const string UserNameId = "username";
        private const string UserName = "daubung08";
        private const string PassId = "password";
        private const string Pass = "Tinhlagio89";
        private const string SubmitId = "submit";
        private const string LoginFailedId = "adcopy-outer";
        private const string LoginSuccessId = "earned-credits";

        public override void Execute(Like4LikeManager mgr)
        {
            var driver = mgr.Driver;
            Common.Try(() =>
            {
                driver.Navigate().GoToUrl(Url);
                var myField = driver.FindElement(By.Name(UserNameId));
                myField.SendKeys(UserName);

                myField = driver.FindElement(By.Name(PassId));
                myField.SendKeys(Pass);

                myField = driver.FindElement(By.Name(SubmitId));
                myField.Click();
                Thread.Sleep(3000);

                driver.WaitUntilDocumentIsReady();

                CheckForNavigate(mgr, driver);
            });
        }

        private static void CheckForNavigate(Like4LikeManager mgr, IWebDriver driver)
        {
            if (driver.FindElements(By.Id(LoginFailedId)).Count > 0)
                mgr.CurrentState = new EnterCodePage();
            else if (driver.FindElements(By.Id(LoginSuccessId)).Count > 0)
                mgr.CurrentState = new LogedInPage();
            else
            {
                Thread.Sleep(2000);
                throw new Exception("Unknown page");
            }
        }
    }
}

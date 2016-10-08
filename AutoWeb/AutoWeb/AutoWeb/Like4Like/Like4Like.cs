using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace AutoWeb.Like4Like
{
    [Binding]
    public class Like4Like
    {
        private IWebDriver driver;

        [BeforeScenario]
        void Before()
        {
            driver = new ChromeDriver(@"C:\Program Files\ChromeDriver");
        }

        [AfterScenario]
        void After()
        {
            driver.Close();
        }

        [Given(@"Open url ""(.*)""")]
        public void GivenOpenUrl(string p0)
        {
            driver.Navigate().GoToUrl(p0);
        }

        [Given(@"Enter username ""(.*)"" and pass ""(.*)""")]
        public void GivenEnterUsernameAndPass(string p0, string p1)
        {
            var e = driver.FindElement(By.Name("username"));
            e.SendKeys(p0);

            e = driver.FindElement(By.Name("password"));
            e.SendKeys(p1);
        }

        [Given(@"Click submit")]
        public void GivenClickSubmit()
        {
            var e = driver.FindElement(By.Name("submit"));
            e.Click();
        }

        [Given(@"Wait to login success")]
        public void GivenWaitToLoginSuccess()
        {
            Thread.Sleep(3000);
        }

        [Given(@"Process like")]
        public void GivenProcessLike()
        {

        }
    }
}

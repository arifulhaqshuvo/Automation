using System.Linq;
using System.Threading;
using Cow.Common.Support.Log;
using OpenQA.Selenium;

namespace WebAuto.PostFacebookGroup
{
    class Poster : PageBase
    {
        public string PendingXPath { get; set; } = "//*[text()='Pending']";
        public string JoinedXPath { get; set; } = "//*[text()='Joined']";
        public string JoinButtonXPath { get; set; } = "//*[text()='Join group']";
        public string PostXPath { get; set; } = "//*[@placeholder='Write something...']";
        public string PostXPath1 { get; set; } = "//*[text()='Write something...']";
        public string PostButtonXPath { get; set; } = "//*[text()='Post']";
        public string[] Group { get; set; }

        public string Content { get; set; } =
            "Giờ này bạn nào còn ship đồ ăn ở Nguyên Hồng gần nguyễn chí thanh không ạ";

        public Poster()
        {
            Group = new[]
            {
                "https://www.facebook.com/groups/1576200712687457/",
                "https://www.facebook.com/groups/CHOBANDOANNGONCUADANHATHANH/",
                "https://www.facebook.com/sieuthimyphamhuyenchi/?fref=ts"
            };
        }

        private bool IsJoined(IWebDriver driver)
        {
            return driver.FindElements(By.XPath(JoinedXPath)).Count > 0;
        }

        private bool IsPendding(IWebDriver driver)
        {
            return driver.FindElements(By.XPath(PendingXPath)).Count > 0;
        }


        public override void Do(DriverManager driverManager)
        {
            var driver = driverManager.Driver;
            foreach (var s in Group)
            {
                driver.Navigate().GoToUrl(s);
                driver.WaitUntilDocumentIsReady();
                if (IsJoined(driver))
                    PostContent(driver);
                else if (!IsPendding(driver))
                    JoinGroup(driver);
                else
                    continue;
            }
        }

        private void JoinGroup(IWebDriver driver)
        {
            driver.ClickEx(driver.FindElement(By.XPath(JoinButtonXPath)));
            driver.WaitUntilDocumentIsReady();
        }

        private void PostContent(IWebDriver driver)
        {
            var postElement = driver.FindElements(By.XPath(PostXPath)).FirstOrDefault();
            if (postElement != null)
            {
                postElement.Click();
                driver.WaitUntilDocumentIsReady();

                var postElement1 = driver.FindElement(By.XPath(PostXPath1));
                driver.SendKeysEx(postElement1, Content);

                var postButton = driver.FindElement(By.XPath(PostButtonXPath));
                postButton.Click();
                driver.WaitUntilDocumentIsReady();
                Thread.Sleep(1000);
            }
            else
            {
                LogManager.GetLogger().Debug("Post element not found:" + PostXPath);
            }
        }
    }
}

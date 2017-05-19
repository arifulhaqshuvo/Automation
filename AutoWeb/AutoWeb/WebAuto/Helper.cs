using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace WebAuto
{
    public static class Helper
    {

        public static void ClickEx(this IWebDriver driver, IWebElement element)
        {
            var ex = (IJavaScriptExecutor)driver;
            ex.ExecuteScript("arguments[0].click();", element);
        }

        public static void SendKeysEx(this IWebDriver driver, IWebElement element, string content)
        {
            var actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Click();
            actions.SendKeys(content);
            actions.Build().Perform();
        }

        public static void WaitUntilDocumentIsReady(this IWebDriver driver, int sec = 15)
        {
            var javaScriptExecutor = (IJavaScriptExecutor)driver;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(sec));
            Func<IWebDriver, object> readyCondition = webDriver => javaScriptExecutor.ExecuteScript("return (document.readyState == 'complete')");
            wait.Until(readyCondition);
        }
    }
}

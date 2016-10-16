using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutoWeb
{
    internal static class Common
    {
        public static void Try(Action action, int time = 3)
        {
            for (var i = 0; i < time; i++)
            {
                try
                {
                    action.Invoke();
                    return;
                }
                catch (Exception e)
                {
                    if (i == time - 1)
                        throw;
                }
            }
        }

        public static void WaitUntilDocumentIsReady(this IWebDriver driver, int sec=15)
        {
            var javaScriptExecutor = (IJavaScriptExecutor)driver;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(sec));
            Func<IWebDriver, object> readyCondition = webDriver => javaScriptExecutor.ExecuteScript("return (document.readyState == 'complete')");
            wait.Until(readyCondition);
        }

        public static ReadOnlyCollection<IWebElement> SafeUntil(this WebDriverWait wait, Func<IWebDriver, ReadOnlyCollection<IWebElement>> condition)
        {
            try
            {
                return wait.Until(condition);
            }
            catch (Exception)
            {
                return new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
            }
        }

        public static bool IsLogedIn()
        {
            return false;
        }
    }
}

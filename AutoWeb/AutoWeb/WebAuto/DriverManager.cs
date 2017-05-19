using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebAuto
{
    public class DriverManager
    {
        public string Arg { get; set; }
        public string DriverPath { get; set; }
        public int DriverTimeOut { get; set; }
        public string BonusUrl { get; set; }

        public IWebDriver Driver { get; set; }
        public PageBase CurrentState { get; set; }

        public DriverManager Init()
        {
            var chromeOption = new ChromeOptions();
            if (!string.IsNullOrEmpty(Arg))
                chromeOption.AddArgument(Arg);
            Driver = new ChromeDriver(DriverPath, chromeOption);
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
            return this;
        }

        public void Do()
        {
            CurrentState.Do(this);
        }
    }
}

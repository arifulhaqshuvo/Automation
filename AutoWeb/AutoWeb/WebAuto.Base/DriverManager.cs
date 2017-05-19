using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebAuto.Base
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
            //  CurrentState = Cow.Common.Unity.UnityFacade.Resolve<LoginPage>();
            var chromeOption = new ChromeOptions();
            chromeOption.AddArgument(Arg);
            Driver = new ChromeDriver(DriverPath, chromeOption);
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(DriverTimeOut));
            return this;
        }

        public void Do()
        {
            CurrentState.Do(this);
        }
    }
}

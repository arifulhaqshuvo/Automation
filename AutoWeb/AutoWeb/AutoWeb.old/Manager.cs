using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutoWeb
{
    public class Manager
    {
        public string Arg { get; set; }
        public string DriverPath { get; set; }
        public int DriverTimeOut { get; set; }
        public string BonusUrl { get; set; }

        public IWebDriver Driver { get; set; }
        public Page CurrentState { get; set; }

        public Manager Init()
        {
            //  CurrentState = Cow.Common.Unity.UnityFacade.Resolve<LoginPage>();
            var chromeOption = new ChromeOptions();
            chromeOption.AddArgument(Arg);
            Driver = new ChromeDriver(DriverPath, chromeOption);
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(DriverTimeOut));
            return this;
        }

        public void Execute()
        {
            while (true)
                CurrentState.Execute(this);
        }
    }
}

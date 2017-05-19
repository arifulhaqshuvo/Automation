using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutoWeb.Like4Like
{
    public class Like4LikeManager
    {
        public string Arg { get; set; } //= "--disable-notifications";
        public string DriverPath { get; set; } //= @"C:\Program Files\ChromeDriver";
        public int DriverTimeOut { get; set; } //= 15;
        public string BonusUrl { get; set; } //= "";

        public IWebDriver Driver { get; set; }
        public Page CurrentState { get; set; }

        public Like4LikeManager Init()
        {
            CurrentState = Cow.Common.Unity.UnityFacade.Resolve<LoginPage>();
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

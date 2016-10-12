using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutoWeb.Like4Like
{
    public class Like4LikeManager
    {
        public IWebDriver Driver { get; set; }
        public Page CurrentState { get; set; }

        public Like4LikeManager()
        {
            CurrentState = new LoginFacebook();
            var chromeOption = new ChromeOptions();
            chromeOption.AddArgument("--disable-notifications");
            Driver = new ChromeDriver(@"C:\Program Files\ChromeDriver", chromeOption);
        }

        public void Execute()
        {
            while (true)
                CurrentState.Execute(this);
        }
    }
}

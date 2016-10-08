using OpenQA.Selenium;

namespace AutoWeb.Like4Like
{
    class CommonSteps
    {
        public static void ProcessLike(IWebDriver driver)
        {
            var buttons = new ButtonGetter().Execute(driver).Buttons;
        }
    }
}

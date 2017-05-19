using OpenQA.Selenium;

namespace WebAuto.PostFacebookGroup
{
    class LoginPage : PageBase
    {
        public string UserName { get; set; } //= "trang4hoa4@gmail.com";
        public string Password { get; set; } //= "trang290994";
        public string LoginUrl { get; set; } //= "https://www.facebook.com/";
        public string ElementLoginButtonXPath { get; set; }// = "//input[@value=\"Đăng nhập\"]";
        public string ElementEmailId { get; set; }// = "//*[@id='email']";
        public string ElementPassId { get; set; }// = "//*[@id='pass']"; 
        public string ElementCheckSuccessXPath { get; set; }

        public override void Do(DriverManager driverManager)
        {
            Login(driverManager);
        }

        protected void Login(DriverManager mgr)
        {
            mgr.Driver.Navigate().GoToUrl(LoginUrl);
            mgr.Driver.WaitUntilDocumentIsReady();

            var btnLogin = mgr.Driver.FindElements(By.XPath(ElementLoginButtonXPath));
            if (btnLogin.Count == 0)
                return;

            mgr.Driver.FindElement(By.XPath(ElementEmailId)).SendKeys(UserName);
            mgr.Driver.FindElement(By.XPath(ElementPassId)).SendKeys(Password);
            mgr.Driver.FindElement(By.XPath(ElementLoginButtonXPath)).Click();
            mgr.Driver.WaitUntilDocumentIsReady();

            var successElement = mgr.Driver.FindElements(By.XPath(ElementCheckSuccessXPath));
            if (successElement.Count == 1)
            {
                mgr.CurrentState = new Poster();
            }
        }
    }
}

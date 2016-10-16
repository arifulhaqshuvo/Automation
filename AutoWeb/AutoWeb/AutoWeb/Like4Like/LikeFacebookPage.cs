using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace AutoWeb.Like4Like
{
    class LikeFacebookPage : BaseLike
    {
        public string UserName { get; set; } //= "trang4hoa4@gmail.com";
        public string Password { get; set; } //= "trang290994";
        public string LoginUrl { get; set; } //= "https://www.facebook.com/";
        public string ElementLoginButtonXPath { get; set; }// = "//input[@value=\"Đăng nhập\"]";
        public string ElementEmailId { get; set; }// = "//*[@id='email']";
        public string ElementPassId { get; set; }// = "//*[@id='pass']";
        public string EarnUrl { get; set; }// = "http://www.like4like.org/free-facebook-likes.php";
        public string[] Types { get; set; } //= {
        //    "url(http://www.like4like.org/img/icon/earn-facebook-post-like.png)###//a[@class=\"UFILikeLink _4x9- _4x9_ _48-k\"]",
        //    "url(http://www.like4like.org/img/icon/earn-facebook-like.png)###//button[@class=\"likeButton _4jy0 _4jy4 _517h _51sy _42ft\"]",
        //    "url(http://www.like4like.org/img/icon/earn-facebook-photo-like.png)###//a[@class=\"UFILikeLink _4x9- _4x9_ _48-k\"]"
        //};


        protected override void ReNavigateEarnUrl(Like4LikeManager mgr)
        {
            ReNavigateEarnUrl(mgr, EarnUrl);
        }

        protected override void Login(Like4LikeManager mgr)
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
        }

        protected override string[] GetTypes()
        {
            return Types;
        }
    }
}

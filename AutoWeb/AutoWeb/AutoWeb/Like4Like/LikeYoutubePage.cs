using System;
using System.Collections.Generic;
using AutoWeb.Like4Like.Support;
using OpenQA.Selenium;

namespace AutoWeb.Like4Like
{
    class LikeYoutubePage : BaseLike
    {
        public string UserName { get; set; }// = "trang4hoa4@gmail.com";
        public string Password { get; set; }// = "trang290994";
        public string LoginUrl { get; set; } //= "https://www.youtube.com/";
        public string EarnUrl { get; set; } //= "http://www.like4like.org/user/earn-youtube.php";
        public string[] Types { get; set; } //= { "url(http://www.like4like.org/img/icon/earn-youtube-like.png)###//*[contains(@alt,'người khác thích video này')] | //button[@title='I like this'] | //button[@title='Tôi thích video này']" };
        public string ElementGotoLoginXPath { get; set; } //= "//span[text()='Đăng nhập' or text()='Sign in']";
        public string ElementEmailId { get; set; } //= "Email";
        public string ElementNextId { get; set; } //= "next";
        public string ElementPassId { get; set; } //= "Passwd";
        public string ElementSiginId { get; set; }// = "signIn";



        protected override void ReNavigateEarnUrl(Like4LikeManager mgr)
        {
            ReNavigateEarnUrl(mgr, EarnUrl);
        }

        protected override void Login(Like4LikeManager mgr)
        {
            mgr.Driver.Navigate().GoToUrl(LoginUrl);
            mgr.Driver.WaitUntilDocumentIsReady();

            var loginBtn = mgr.Driver.FindElements(By.XPath(ElementGotoLoginXPath));
            if (loginBtn.Count == 0)
                return;
             
            mgr.Driver.FindElement(By.XPath(ElementGotoLoginXPath)).Click();
            mgr.Driver.WaitUntilDocumentIsReady();

            mgr.Driver.FindElement(By.Id(ElementEmailId)).SendKeys(UserName);
            mgr.Driver.FindElement(By.Id(ElementNextId)).Click();
            mgr.Driver.WaitUntilDocumentIsReady();
            mgr.Driver.FindElement(By.Id(ElementPassId)).SendKeys(Password);
            mgr.Driver.FindElement(By.Id(ElementSiginId)).Click();
            mgr.Driver.WaitUntilDocumentIsReady();
        }

        protected override string[] GetTypes()
        {
            return Types;
        }
    }
}

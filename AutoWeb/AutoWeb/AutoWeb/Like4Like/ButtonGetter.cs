using System.Collections.Generic;
using System.Linq;
using ExCSS;
using OpenQA.Selenium;

namespace AutoWeb.Like4Like
{
    class ButtonGetter
    {
        public class Button
        {
            public IWebElement Element { get; set; }
            public string BackgroundUrl { get; set; }
        }

        public List<Button> Buttons { get; set; } = new List<Button>();

        private Dictionary<string, StyleRule> GetStyle(IWebDriver driver)
        {
            var styles = driver.FindElements(By.TagName("style"));
            var styleDic = new Dictionary<string, StyleRule>();
            foreach (var webElement in styles)
            {
                var cssText = webElement.GetAttribute("innerHTML");
                var parser = new Parser();
                var stylesheet = parser.Parse(cssText);
                foreach (var r in stylesheet.StyleRules)
                    styleDic.Add(r.Value, r);
            }
            return styleDic;
        }

        public ButtonGetter Execute(IWebDriver driver)
        {
            var styleDic = GetStyle(driver);

            var links = driver.FindElements(By.TagName("a"));
            foreach (var webElement in links)
            {
                var cls = webElement.GetAttribute("class");
                if (string.IsNullOrEmpty(cls))
                    continue;
                var clss = cls.Split(' ');
                foreach (var s in clss)
                {
                    if (!styleDic.ContainsKey("." + s)) continue;

                    var backGroundImage =
                        styleDic["." + s].Declarations.Properties.FirstOrDefault(x => x.Name == "background-image");
                    if (backGroundImage == null)
                        continue;
                    Buttons.Add(new Button { Element = webElement, BackgroundUrl = backGroundImage.Term.ToString() });
                }
            }
            return this;
        }
    }
}

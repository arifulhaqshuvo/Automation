using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cow.Common.Unity;
using OpenQA.Selenium;

namespace AutoWeb.Like4Like
{
    class LogedInPage : Page
    {
        public override void Execute(Like4LikeManager mgr)
        {
            Common.Try(() => { Process(mgr); });
        }

        private void Process(Like4LikeManager mgr)
        {
            ToYoutubeLike(mgr);
            // ToFacebookLike(mgr);

        }

        private static void ToYoutubeLike(Like4LikeManager mgr)
        {
            mgr.CurrentState = UnityFacade.Resolve<LikeYoutubePage>();// new LikeYoutubePage();
        }

        private static void ToFacebookLike(Like4LikeManager mgr)
        {
            mgr.CurrentState = UnityFacade.Resolve<LikeFacebookPage>();//new LikeFacebookPage();
        }
    }
}

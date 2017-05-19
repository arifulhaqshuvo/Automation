using Cow.Common.Unity;
using WebAuto.PostFacebookGroup;

namespace WebAuto
{
    static class Program
    {
        static void Main()
        {
            UnityFacade.Reinit("unity");
            var driver = UnityFacade.Resolve<DriverManager>();
            driver.Init();
            driver.CurrentState = UnityFacade.Resolve<LoginPage>();

            while (true)
            {
                driver.Do();
            }
        }
    }
}

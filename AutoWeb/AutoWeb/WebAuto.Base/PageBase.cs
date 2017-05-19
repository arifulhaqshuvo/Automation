using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAuto.Base;

namespace WebAuto.Base
{
    public abstract class PageBase
    {
        public abstract void Do(DriverManager driverManager);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.Like4Like
{
    public abstract class Page
    { 
        public abstract void Execute(Like4LikeManager mgr);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoWeb.Like4Like
{
    class BonusPage : Page
    {
        public override void Execute(Like4LikeManager mgr)
        {
            MessageBox.Show("Click bonus and ok");
            mgr.CurrentState = new LikeFacebookPage();
        }
    }
}

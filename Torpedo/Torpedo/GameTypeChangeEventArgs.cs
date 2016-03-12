using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torpedo
{
    public class GameTypeChangeEventArgs : EventArgs
    {
        public GameType NewValue;
        
        public GameTypeChangeEventArgs(GameType newvalue)
        {
            NewValue = newvalue;
        }
    }
}

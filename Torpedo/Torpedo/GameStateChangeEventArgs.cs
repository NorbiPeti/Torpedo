using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torpedo
{
    public class GameStateChangeEventArgs : EventArgs
    {
        public GameState NewState;
        public GameStateChangeEventArgs(GameState newstate)
        {
            NewState = newstate;
        }
    }
}

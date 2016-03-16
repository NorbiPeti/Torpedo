using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torpedo
{
    public static class AIPlayer
    {
        private static Random rand = new Random();
        public static void PlaceShips()
        {
            while (Player.Player2.NextShip != -1)
            {
                int x = rand.Next(Game.GameSize.Width);
                int y = rand.Next(Game.GameSize.Height);
                bool vertical = rand.Next(2) == 0;
                Ship ship = new Ship(x, y, Player.Player2.NextShip, (vertical ? ShipDirection.Vertical : ShipDirection.Horizontal), Player.Player2);
                if (!Ship.CheckHasShip(ship))
                {
                    Player.Player2.Ships.Add(ship);
                }
            }
        }

        public static void Attack()
        {
            //TODO
        }
    }
}

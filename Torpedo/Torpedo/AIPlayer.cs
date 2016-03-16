using System;
using System.Collections.Generic;
using System.Drawing;
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
            int x, y;
            do
            {
                x = rand.Next(Game.GameSize.Width);
                y = rand.Next(Game.GameSize.Height);
            } while (Player.Player2.Shots.Any(p => p.X == x && p.Y == y));
                Ship ship = Ship.GetShipAtField(Player.CurrentEnemy, x, y);
            if (ship == null)
            {
                Player.CurrentOwn.Shots.Add(new Point(x, y));
            }
            else
            {
                if (ship.Direction == ShipDirection.Horizontal)
                    ship.DamagedParts[x - ship.X] = true;
                else
                    ship.DamagedParts[y - ship.Y] = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torpedo
{
    public class Ship
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        private int size;
        public int Size
        {
            get
            {
                return size;
            }
            private set
            {
                size = value;
                DamagedParts = new bool[value];
            }
        }
        public ShipDirection Direction { get; set; }
        public bool[] DamagedParts;
        public Player Owner;
        public bool Enemy
        {
            get
            {
                return Owner == Player.CurrentEnemy;
            }
        }

        public Ship(int x, int y, int size, ShipDirection direction, Player owner)
        {
            X = x;
            Y = y;
            Size = size; //A DamagedParts-ot is inicializálja
            Direction = direction;
            Owner = owner;
        }

        /// <summary>
        /// Mozgatja a hajót
        /// </summary>
        /// <param name="dx">X elmozdulás</param>
        /// <param name="dy">Y elmozdulás</param>
        public void Move(int dx, int dy)
        {
            if (CheckHasShip(this, dx, dy))
                return;
            if (Enemy)
                GameRenderer.Enemy.DerenderShip(this);
            else
                GameRenderer.Own.DerenderShip(this);
            X += dx;
            Y += dy;
            if (Enemy)
                GameRenderer.Enemy.RenderShip(this);
            else
                GameRenderer.Own.RenderShip(this);
        }

        public void Rotate()
        {
            if (CheckHasShip(this, rotated: true))
                return;
            if (Enemy)
                GameRenderer.Enemy.DerenderShip(this);
            else
                GameRenderer.Own.DerenderShip(this);
            if (Direction == ShipDirection.Horizontal)
                Direction = ShipDirection.Vertical;
            else
                Direction = ShipDirection.Horizontal;
            if (Enemy)
                GameRenderer.Enemy.RenderShip(this);
            else
                GameRenderer.Own.RenderShip(this);
        }

        public static bool CheckHasShip(Ship ship, int dx = 0, int dy = 0, bool rotated = false)
        {
            bool hasship = false;
            bool dircheck = ship.Direction == ShipDirection.Horizontal;
            if (rotated)
                dircheck = !dircheck;
            if (ship.X + dx < 0 || ship.Y + dy < 0)
                return true;
            if (dircheck)
            {
                if (ship.X + dx + ship.Size - 1 >= Game.GameSize.Width || ship.Y + dy >= Game.GameSize.Height)
                    return true;
            }
            else
            {
                if (ship.X + dx >= Game.GameSize.Width || ship.Y + dy + ship.Size - 1 >= Game.GameSize.Height)
                    return true;
            }
            for (int i = 0; i < ship.Size; i++)
            {
                if (dircheck)
                {
                    Ship ship2 = GetShipAtField(ship.Owner, ship.X + dx + i, ship.Y + dy);
                    if (ship2 != null && ship2 != ship)
                    {
                        hasship = true;
                        break;
                    }
                }
                else
                {
                    Ship ship2 = GetShipAtField(ship.Owner, ship.X + dx, ship.Y + dy + i);
                    if (ship2 != null && ship2 != ship)
                    {
                        hasship = true;
                        break;
                    }
                }
            }
            return hasship;
        }

        public static Ship GetShipAtField(Player player, int x, int y)
        {
            return player.Ships.SingleOrDefault(s =>
             {
                 if (s.Direction != ShipDirection.Horizontal)
                 {
                     if (s.X != x)
                         return false;
                     else
                     {
                         if (y >= s.Y && y < s.Y + s.Size)
                             return true;
                         else
                             return false;
                     }
                 }
                 else
                 {
                     if (s.Y != y)
                         return false;
                     else
                     {
                         if (x >= s.X && x < s.X + s.Size)
                             return true;
                         else
                             return false;
                     }
                 }
             });
        }
    }

    public enum ShipDirection
    {
        Horizontal,
        Vertical
    }
}

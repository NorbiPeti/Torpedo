﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torpedo
{
    public class Player
    {
        /// <summary>
        /// <para>Az első játékos</para>
        /// </summary>
        public static Player Player1 { get; set; } = new Player();

        private static Player p2 = new Player();
        /// <summary>
        /// <para>A második játékos</para>
        /// </summary>
        public static Player Player2
        {
            get
            {
                return p2;
            }
            private set
            {
                p2 = value;
            }
        }

        private static bool swapped = false;
        /// <summary>
        /// Az aktuális játékos
        /// </summary>
        public static Player CurrentOwn
        {
            get
            {
                return (swapped ? Player2 : Player1);
            }
        }

        /// <summary>
        /// Az aktuális ellenfél
        /// </summary>
        public static Player CurrentEnemy
        {
            get
            {
                return (swapped ? Player1 : Player2);
            }
        }

        public static void SwapPlayers()
        {
            swapped = !swapped;
        }

        static Player()
        {
            Game.GameTypeChange += Game_GameTypeChange_Global;
        }

        private static void Game_GameTypeChange_Global(object sender, GameTypeChangeEventArgs e)
        {
            Player1 = new Player();
            Player2 = new Player();
            swapped = false;
        }

        /// <summary>
        /// <para>Létrhehoz egy új játékost</para>
        /// <para>Csak a Player osztályban használható</para>
        /// </summary>
        private Player()
        {

        }

        /// <summary>
        /// A játékos hajóinak listája
        /// </summary>
        public List<Ship> Ships = new List<Ship>();

        /// <summary>
        /// <para>Megadja a következő hajó nagyságát</para>
        /// </summary>
        public int NextShip
        {
            get
            {
                if (!Ships.Any(s => s.Size == 5))
                    return 5;
                if (!Ships.Any(s => s.Size == 4))
                    return 4;
                if (Ships.Count(s => s.Size == 3) < 2)
                    return 3;
                if (!Ships.Any(s => s.Size == 2))
                    return 2;
                else
                    return -1;
            }
        }

        public List<Point> Shots = new List<Point>();
    }
}

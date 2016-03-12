using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torpedo
{
    public static class Game
    {
        /// <summary>
        /// A játék mérete mezőkben
        /// </summary>
        public static Size GameSize { get; private set; } = new Size(10, 10);

        /// <summary>
        /// A jelenlegi játékos
        /// </summary>
        public static Player CurrentPlayer = Player.Player1;

        private static GameType type = GameType.Singleplayer;
        /// <summary>
        /// <para>Megadja a játék típusát (egyjátékos, többjátékos)</para>
        /// </summary>
        public static GameType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                if (GameTypeChange != null)
                    GameTypeChange(null, new GameTypeChangeEventArgs(value));
            }
        }

        /// <summary>
        /// A játékmód változásakor vagy új játék kezdésekor hívódik meg
        /// </summary>
        public static event EventHandler<GameTypeChangeEventArgs> GameTypeChange;

        /// <summary>
        /// A játék állapota
        /// </summary>
        public static GameState State { get; set; } = GameState.Prepare;

        static Game()
        {
            GameTypeChange += Game_GameTypeChange;
        }

        private static void Game_GameTypeChange(object sender, GameTypeChangeEventArgs e)
        {
            State = GameState.Prepare; //TODO
            CurrentPlayer = Player.Player1;
        }
    }

    public enum GameType
    {
        Singleplayer,
        Multiplayer
    }
    
    public enum GameState
    {
        Prepare,
        Battle
    }
}

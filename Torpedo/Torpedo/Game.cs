using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Torpedo
{
    public static class Game
    {
        /// <summary>
        /// A játék mérete mezőkben
        /// </summary>
        public static Size GameSize { get; private set; } = new Size(10, 10);

        /*/// <summary>
        /// A jelenlegi játékos
        /// </summary>
        public static Player CurrentPlayer = Player.Player1;*/

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
        /// A játék állapotának változásakor hívódik meg (Prepare, Battle)
        /// </summary>
        public static event EventHandler<GameStateChangeEventArgs> GameStateChange;

        private static GameState state = GameState.Prepare;
        /// <summary>
        /// A játék állapota
        /// </summary>
        public static GameState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                if (GameStateChange != null)
                    GameStateChange(null, new GameStateChangeEventArgs(value));
            }
        }

        public static void NextTurn()
        {
            if (State == GameState.Prepare)
            {
                if (Player.CurrentOwn == Player.Player1)
                {
                    if (Type == GameType.Singleplayer)
                    {
                        Player.SwapPlayers();
                        AIPlayer.PlaceShips();
                        State = GameState.Battle;
                        Player.SwapPlayers();
                    }
                }
                else if (Type == GameType.Multiplayer)
                {
                    Player.SwapPlayers();
                    State = GameState.Battle;
                }
                else
                    throw new InvalidOperationException("Az első játékos csak egyszer rakhat le hajókat.");

                if (Type == GameType.Multiplayer)
                {
                    GameForm.Instance.SetPanelsVisible(false);
                    GameRenderer.Own.RenderLines();
                    GameRenderer.Enemy.RenderLines();
                    Timer t = new Timer();
                    t.Interval = 2000;
                    t.Tick += delegate
                    {
                        t.Stop();
                        Player.SwapPlayers();
                        GameRenderer.Own.RenderGameField();
                        GameRenderer.Enemy.RenderGameField();
                        GameForm.Instance.SetPanelsVisible(true);
                    };
                    t.Start();
                }
            }
            else
            {
                if (Player.CurrentEnemy.Ships.All(s => s.DamagedParts.All(d => d)))
                {
                    MessageBox.Show("Győzelem!");
                    GameForm.Instance.Reset();
                    return;
                }
                if (Player.CurrentOwn.Ships.All(s => s.DamagedParts.All(d => d)))
                {
                    MessageBox.Show("Vereség!");
                    GameForm.Instance.Reset();
                    return;
                }
                if (Type == GameType.Singleplayer)
                {
                    GameForm.Instance.SetPanelsEnabled(false);
                    Player.SwapPlayers();
                    if (Player.CurrentOwn == Player.Player2)
                        AIPlayer.Attack();
                    Player.SwapPlayers();
                    GameForm.Instance.SetPanelsEnabled(true);
                }
                else if (Type == GameType.Multiplayer)
                {
                    Timer t = new Timer();
                    t.Interval = 2000;
                    int c = 0;
                    GameForm.Instance.SetPanelsEnabled(false);
                    t.Tick += delegate
                    {
                        if (c >= 1) //Kétszer fut le
                        {
                            t.Stop();
                            Player.SwapPlayers();
                            GameRenderer.Own.RenderGameField();
                            GameRenderer.Enemy.RenderGameField();
                            GameForm.Instance.SetPanelsVisible(true);
                            GameForm.Instance.SetPanelsEnabled(true);
                        }
                        else
                        {
                            GameForm.Instance.SetPanelsVisible(false);
                            GameRenderer.Own.RenderLines();
                            GameRenderer.Enemy.RenderLines();
                            c++;
                        }
                    };
                    t.Start();
                }
            }
        }

        static Game()
        {
            GameTypeChange += Game_GameTypeChange;
        }

        private static void Game_GameTypeChange(object sender, GameTypeChangeEventArgs e)
        {
            State = GameState.Prepare;
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

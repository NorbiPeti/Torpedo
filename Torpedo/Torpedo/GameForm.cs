using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Torpedo
{
    public partial class GameForm : Form
    {
        public static GameForm Instance;
        public GameForm()
        {
            if (Instance != null)
                throw new InvalidOperationException("Csak egy példány létezhet a formból.");
            InitializeComponent();
            GameRenderer.Own = new OwnGameRenderer(ownPanel);
            GameRenderer.Enemy = new EnemyGameRenderer(enemyPanel);
            Instance = this;
            Game.GameTypeChange += Game_GameTypeChange;
            Game.Type = GameType.Singleplayer;
        }

        private void Game_GameTypeChange(object sender, GameTypeChangeEventArgs e)
        {
            egyjátékosToolStripMenuItem.Enabled = false;
            többjátékosToolStripMenuItem.Enabled = false;
            if (Player.Player1.NextShip != -1)
                shipSizeLabel.Text = "Következő hajó: " + Player.Player1.NextShip;
            else
                shipSizeLabel.Text = "";
            switch(e.NewValue)
            {
                case GameType.Singleplayer:
                    többjátékosToolStripMenuItem.Enabled = true; //Csak a másik játékmódot hagyja bekapcsolva, hogy át leheseen váltani
                    break;
                case GameType.Multiplayer:
                    egyjátékosToolStripMenuItem.Enabled = true;
                    break;
            }
        }

        private void ownPanel_Paint(object sender, PaintEventArgs e)
        {
            GameRenderer.Own.RenderGame();
        }

        private void GameTypeMenuClick(object sender, EventArgs e)
        {
            if (sender == egyjátékosToolStripMenuItem)
                Game.Type = GameType.Singleplayer;
            else if (sender == többjátékosToolStripMenuItem)
                Game.Type = GameType.Multiplayer;
        }

        private void enemyPanel_Paint(object sender, PaintEventArgs e)
        {
            GameRenderer.Enemy.RenderGame();
        }

        private Ship lastship;
        private void ownPanel_Click(object sender, EventArgs e)
        {
            if (Game.State != GameState.Prepare)
                return;
            if (Player.Player1.NextShip == -1)
                return;
            Point clickedfield = GameRenderer.Own.PixelsToFields(ownPanel.PointToClient(Cursor.Position));
            Ship ship = new Ship(clickedfield.X, clickedfield.Y, Game.CurrentPlayer.NextShip, ShipDirection.Horizontal, false);
            if (Ship.CheckHasShip(ship))
                return;
            Game.CurrentPlayer.Ships.Add(ship);
            GameRenderer.Own.RenderShip(ship);
            lastship = ship;
            //TODO
            if (Player.Player1.NextShip != -1)
                shipSizeLabel.Text = "Következő hajó: " + Player.Player1.NextShip;
            else
                shipSizeLabel.Text = "";
        }

        private void MoveShip(object sender, EventArgs e)
        {
            if (lastship != null)
            {
                if (sender == moveUpButton)
                    lastship.Move(0, -1);
                else if (sender == moveDownButton)
                    lastship.Move(0, 1);
                else if (sender == moveLeftButton)
                    lastship.Move(-1, 0);
                else if (sender == moveRightButton)
                    lastship.Move(1, 0);
            }
        }

        private void rotateButton_Click(object sender, EventArgs e)
        {
            if (lastship != null)
            {
                lastship.Rotate();
            }
        }
    }
}

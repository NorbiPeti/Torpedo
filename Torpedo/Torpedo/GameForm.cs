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
            Game.GameStateChange += Game_GameStateChange;
        }

        public void SetPanelsEnabled(bool enabled)
        {
            ownPanel.Enabled = enabled;
            enemyPanel.Enabled = enabled;
        }

        public void SetPanelsVisible(bool visible)
        {
            ownPanel.Visible = visible;
            enemyPanel.Visible = visible;
        }

        public void Reset()
        {
            egyjátékosToolStripMenuItem.Enabled = true;
            egyjátékosToolStripMenuItem.PerformClick();
            GameRenderer.Own.RenderGame();
            GameRenderer.Enemy.RenderGame();
        }

        private void Game_GameTypeChange(object sender, GameTypeChangeEventArgs e)
        {
            egyjátékosToolStripMenuItem.Enabled = false;
            többjátékosToolStripMenuItem.Enabled = false;
            moveUpButton.Enabled = true;
            moveDownButton.Enabled = true;
            moveLeftButton.Enabled = true;
            moveRightButton.Enabled = true;
            rotateButton.Enabled = true;
            if (Player.CurrentOwn.NextShip != -1)
                shipSizeLabel.Text = "Következő hajó: " + Player.CurrentOwn.NextShip;
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
            if (Player.CurrentOwn.NextShip == -1)
                return;
            Point clickedfield = GameRenderer.Own.PixelsToFields(ownPanel.PointToClient(Cursor.Position));
            Ship ship = new Ship(clickedfield.X, clickedfield.Y, Player.CurrentOwn.NextShip, ShipDirection.Horizontal, Player.CurrentOwn);
            if (Ship.CheckHasShip(ship))
                return;
            Player.CurrentOwn.Ships.Add(ship);
            GameRenderer.Own.RenderShip(ship);
            lastship = ship;
            if (Player.CurrentOwn.NextShip != -1)
                shipSizeLabel.Text = "Következő hajó: " + Player.CurrentOwn.NextShip;
            else
            {
                shipSizeLabel.Text = "";
                lastship = null;
                Game.NextTurn(false);
            }
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

        private void enemyPanel_Click(object sender, EventArgs e)
        {
            if (Game.State != GameState.Battle)
                return;
            Point clickedfield = GameRenderer.Enemy.PixelsToFields(enemyPanel.PointToClient(Cursor.Position));
            Ship ship = Ship.GetShipAtField(Player.CurrentEnemy, clickedfield.X, clickedfield.Y);
            if (ship == null)
            {
                Player.CurrentOwn.Shots.Add(clickedfield);
            }
            else
            {
                if (ship.Direction == ShipDirection.Horizontal)
                    ship.DamagedParts[clickedfield.X - ship.X] = true;
                else
                    ship.DamagedParts[clickedfield.Y - ship.Y] = true;
            }
            GameRenderer.Enemy.RenderGameField();
            Game.NextTurn(ship != null);
        }

        private void Game_GameStateChange(object sender, GameStateChangeEventArgs e)
        {
            moveUpButton.Enabled = false;
            moveDownButton.Enabled = false;
            moveLeftButton.Enabled = false;
            moveRightButton.Enabled = false;
            rotateButton.Enabled = false;
        }
    }
}

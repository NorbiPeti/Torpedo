using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Torpedo
{
    public abstract class GameRenderer
    {
        public static OwnGameRenderer Own;
        public static EnemyGameRenderer Enemy;

        public GameRenderer(Control parent)
        {
            Parent = parent;
            Game.GameTypeChange += Game_GameTypeChange;
        }

        private void Game_GameTypeChange(object sender, GameTypeChangeEventArgs e)
        {
            RenderGame();
        }

        private Control Parent;
        public void RenderGame()
        {
            RenderLines();
            RenderGameField();
        }
        public void RenderLines()
        {
            using (Graphics gr = Parent.CreateGraphics())
            {
                gr.Clear(Parent.BackColor);
                int width = Parent.Width / Game.GameSize.Width;
                int height = Parent.Height / Game.GameSize.Height;
                for (int i = 1; i < Game.GameSize.Width; i++)
                {
                    gr.DrawLine(Pens.Black, i * width, 0, i * width, Parent.Height);
                }
                for (int i = 1; i < Game.GameSize.Height; i++)
                {
                    gr.DrawLine(Pens.Black, 0, i * height, Parent.Width, i * height);
                }
            }
        }

        public abstract void RenderGameField();

        public Point PixelsToFields(Point p)
        {
            int width = Parent.Width / Game.GameSize.Width;
            int height = Parent.Height / Game.GameSize.Height;
            return new Point(p.X / width, p.Y / height);
        }

        public void DerenderShip(Ship ship)
        {
            for (int i = 0; i < ship.Size; i++)
            {
                if (ship.Direction == ShipDirection.Horizontal)
                    UpdateField(ship.X + i, ship.Y, Parent.BackColor);
                else
                    UpdateField(ship.X, ship.Y + i, Parent.BackColor);
            }
            using (Graphics gr = Parent.CreateGraphics())
            {
                int width = Parent.Width / Game.GameSize.Width;
                int height = Parent.Height / Game.GameSize.Height;
                if (ship.Direction == ShipDirection.Horizontal)
                {
                    for (int i = ship.X; i < ship.X + ship.Size; i++)
                    {
                        gr.DrawLine(Pens.Black, i * width, ship.Y * height, i * width, (ship.Y + 1) * height);
                    }
                    gr.DrawLine(Pens.Black, ship.X * width, ship.Y * height, (ship.X + ship.Size) * width, ship.Y * height);
                    gr.DrawLine(Pens.Black, ship.X * width, (ship.Y + 1) * height, (ship.X + ship.Size) * width, (ship.Y + 1) * height);
                }
                else
                {
                    for (int i = ship.Y; i < ship.Y + ship.Size; i++)
                    {
                        gr.DrawLine(Pens.Black, ship.X * width, i * height, (ship.X + 1) * width, i * height);
                    }
                    gr.DrawLine(Pens.Black, ship.X * width, ship.Y * height, ship.X * width, (ship.Y + ship.Size) * height);
                    gr.DrawLine(Pens.Black, (ship.X + 1) * width, ship.Y, (ship.X + 1) * width, (ship.Y + ship.Size) * height);
                }
            }
        }

        protected void UpdateField(int x, int y, Color color)
        {
            using (Graphics gr = Parent.CreateGraphics())
            {
                int width = Parent.Width / Game.GameSize.Width;
                int height = Parent.Height / Game.GameSize.Height;
                gr.FillRectangle(new SolidBrush(color), x * width, y * height, width, height);
            }
        }

        public abstract void RenderShip(Ship ship);
    }
}

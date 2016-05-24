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
    public class EnemyGameRenderer : GameRenderer
    {
        public EnemyGameRenderer(Control parent) : base(parent)
        {

        }

        private static ReadOnlyDictionary<FieldTypeEnemy, Color> FieldTypes = new ReadOnlyDictionary<FieldTypeEnemy, Color>(new Dictionary<FieldTypeEnemy, Color>()
        {
            {FieldTypeEnemy.Missed, Color.LightBlue},
            {FieldTypeEnemy.TargetHit, Color.Red},
            {FieldTypeEnemy.EnemyShipDestroyed, Color.Black}
        });

        public void UpdateField(int x, int y, FieldTypeEnemy fieldtype)
        {
            base.UpdateField(x, y, FieldTypes[fieldtype]);
        }

        public override void RenderShip(Ship ship)
        {
            bool destroyed = ship.DamagedParts.All(p => p);
            for (int i = 0; i < ship.Size; i++)
            {
                FieldTypeEnemy ft = FieldTypeEnemy.Missed;
                if (destroyed)
                    ft = FieldTypeEnemy.EnemyShipDestroyed;
                else if (ship.DamagedParts[i])
                    ft = FieldTypeEnemy.TargetHit;
                if (ft == FieldTypeEnemy.Missed) //A Missed itt nem használható, ezért alapértelmezett értékként van használva
                    continue; //Nem rajzolja meg a sértetlen hajókat és hajórészeket
                
                if (ship.Direction == ShipDirection.Horizontal)
                    UpdateField(ship.X + i, ship.Y, ft);
                else
                    UpdateField(ship.X, ship.Y + i, ft);
            }
        }

        public override void RenderGameField()
        {
            Player.CurrentOwn.Shots.ForEach(s => UpdateField(s.X, s.Y, FieldTypeEnemy.Missed));
            Player.CurrentEnemy.Ships.ForEach(s => RenderShip(s));
        }
    }

    public enum FieldTypeEnemy
    {
        Missed,
        TargetHit,
        EnemyShipDestroyed
    }
}

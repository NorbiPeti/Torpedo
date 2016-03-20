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
    public class OwnGameRenderer : GameRenderer
    {
        public OwnGameRenderer(Control parent) : base(parent)
        {
            
        }

        private static ReadOnlyDictionary<FieldTypeOwn, Color> FieldTypes = new ReadOnlyDictionary<FieldTypeOwn, Color>(new Dictionary<FieldTypeOwn, Color>()
        {
            { FieldTypeOwn.OwnShip, Color.LightGray },
            { FieldTypeOwn.OwnShipDamaged, Color.DarkRed },
            { FieldTypeOwn.OwnShipDestroyed, Color.Black },
            { FieldTypeOwn.Missed, Color.LightBlue },
        });

        public void UpdateField(int x, int y, FieldTypeOwn fieldtype)
        {
            base.UpdateField(x, y, FieldTypes[fieldtype]);
        }

        public override void RenderShip(Ship ship)
        {
            bool destroyed = ship.DamagedParts.All(p => p);
            for (int i = 0; i < ship.Size; i++)
            {
                FieldTypeOwn fto = FieldTypeOwn.OwnShip;
                if (destroyed)
                    fto = FieldTypeOwn.OwnShipDestroyed;
                else if (ship.DamagedParts[i])
                    fto = FieldTypeOwn.OwnShipDamaged;
                if (ship.Direction == ShipDirection.Horizontal)
                    UpdateField(ship.X + i, ship.Y, fto);
                else
                    UpdateField(ship.X, ship.Y + i, fto);
            }
        }

        public override void RenderGameField()
        {
            Player.CurrentOwn.Ships.ForEach(s => RenderShip(s));
            Player.CurrentEnemy.Shots.ForEach(s => UpdateField(s.X, s.Y, FieldTypeOwn.Missed));
        }
    }

    public enum FieldTypeOwn
    {
        OwnShip,
        OwnShipDamaged,
        OwnShipDestroyed,
        Missed
    }
}

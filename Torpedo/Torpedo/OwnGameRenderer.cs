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
            {FieldTypeOwn.OwnShip, Color.LightGray},
            {FieldTypeOwn.OwnShipDamaged, Color.DarkRed}
        });

        public void UpdateField(int x, int y, FieldTypeOwn fieldtype)
        {
            base.UpdateField(x, y, FieldTypes[fieldtype]);
        }

        public override void RenderShip(Ship ship)
        {
            //TODO: Képek az egyes hajótípusokhoz
            for (int i = 0; i < ship.Size; i++)
            {
                if (ship.Direction == ShipDirection.Horizontal)
                    UpdateField(ship.X + i, ship.Y, FieldTypeOwn.OwnShip);
                else
                    UpdateField(ship.X, ship.Y + i, FieldTypeOwn.OwnShip);
            }
            //TODO: Rárajzolni a képet a mezőkre
        }

        public override void RenderGameField()
        {
            Player.Player1.Ships.ForEach(s => RenderShip(s)); //TODO
        }
    }

    public enum FieldTypeOwn
    {
        OwnShip,
        OwnShipDamaged
    }
}

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
            {FieldTypeEnemy.Targeted, Color.LightBlue},
            {FieldTypeEnemy.TargetHit, Color.Red},
            {FieldTypeEnemy.EnemyShipDestroyed, Color.Black}
        });

        public void UpdateField(int x, int y, FieldTypeEnemy fieldtype)
        {
            base.UpdateField(x, y, FieldTypes[fieldtype]);
        }

        public override void RenderShip(Ship ship)
        {
            //TODO: Képek az egyes hajótípusokhoz
            for (int i = 0; i < ship.Size; i++)
            {
                if (ship.Direction == ShipDirection.Horizontal)
                    UpdateField(ship.X + i, ship.Y, FieldTypeEnemy.Targeted);
                else
                    UpdateField(ship.X, ship.Y + i, FieldTypeEnemy.Targeted);
            }
            //TODO: Rárajzolni a képet a mezőkre
        }

        public override void RenderGameField()
        {
            Player.Player2.Ships.ForEach(s => RenderShip(s));
        }
    }

    public enum FieldTypeEnemy
    {
        Targeted,
        TargetHit,
        EnemyShipDestroyed
    }
}

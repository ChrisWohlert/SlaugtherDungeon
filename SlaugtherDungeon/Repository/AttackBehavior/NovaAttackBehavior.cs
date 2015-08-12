using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHWGameEngine;
using CHWGameEngine.CHWGraphics;
using CHWGameEngine.GameObject;
using Repository.Spells;

namespace Repository.AttackBehavior
{
    class NovaAttackBehavior : IAttackBehavior
    {
        public GameWorld GameWorld { get; set; }
        public NovaAttackBehavior(GameWorld gameWorld)
        {
            GameWorld = gameWorld;
        }
        public void Attack(IGameObject gameObject)
        {
            var spell = gameObject as Spell;
            bool isCircle = gameObject.DrawBehavior.IsCircle;
            gameObject.DrawBehavior = new GrowingImageDrawBehavior(gameObject);
            GrowingImageDrawBehavior dh = (GrowingImageDrawBehavior)gameObject.DrawBehavior;
            dh.IsCircle = isCircle;
            dh.SpeedX = 15;
            dh.SpeedY = 15;
            if (spell != null)
            {
                spell.Location = spell.Source.Location;
                dh.SizeChanged += (size) =>
                {
                    if (size.Width < spell.Range) return;
                    spell.SpellHandler.Remove(spell);
                };
            }
        }
    }
}

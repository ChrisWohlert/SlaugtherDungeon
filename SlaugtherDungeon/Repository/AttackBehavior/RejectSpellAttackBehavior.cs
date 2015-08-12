using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using Repository.DamageBehavior;
using Repository.Spells;

namespace Repository.AttackBehavior
{
    class RejectSpellAttackBehavior : IAttackBehavior
    {
        public GameWorld GameWorld { get; set; }

        public RejectSpellAttackBehavior(GameWorld gameWorld)
        {
            GameWorld = gameWorld;
        }

        public void Attack(IGameObject gameObject)
        {
            var spell = gameObject as Spell;
            if (spell == null) return;

            spell.Collision += (s, t) =>
            {
                if (s.Source == t) return;
                var target = t as Spell;
                if (target == null) return;

                target.Angle = (int)GameWorld.CalcAngle(s.Location.ToPoint(), t.Location.ToPoint());
                target.DamageBehavior.Source = spell.DamageBehavior.Source;
                spell.DamageBehavior.TargetTypes.ForEach(x => target.DamageBehavior.TargetTypes.Add(x));
                target.Source = spell.Source;
            };
        }
    }
}

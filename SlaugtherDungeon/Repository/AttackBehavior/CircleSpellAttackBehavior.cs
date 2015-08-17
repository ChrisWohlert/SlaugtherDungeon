using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using CHWGameEngine.Motion;
using Repository.Spells;

namespace Repository.AttackBehavior
{
    class CircleSpellAttackBehavior : IAttackBehavior
    {
        public GameWorld GameWorld { get; set; }
        private List<Spell> affectedSpells;

        public CircleSpellAttackBehavior(GameWorld gameWorld)
        {
            GameWorld = gameWorld;
            affectedSpells = new List<Spell>();
        }

        public void Attack(IGameObject gameObject)
        {
            var spell = gameObject as Spell;
            if (spell == null) return;

            spell.Collision += (s, t) =>
            {
                var target = t as Spell;
                if (target == null) return;
                if (s.Source == target.Source) return;

                target.DamageBehavior.Source = spell.DamageBehavior.Source;
                spell.DamageBehavior.TargetTypes.ForEach(x => target.DamageBehavior.TargetTypes.Add(x));
                target.Source = spell.Source;
                if (!affectedSpells.Contains(target))
                {
                    target.TargetSpeed = 5;
                    target.MotionBehavior = new CircleMotionBehavior(target, GameWorld, spell.Source.Location);
                    target.Range = 0;
                    affectedSpells.Add(target);
                }
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using CHWGameEngine.Motion;
using Repository.Characters;
using Repository.Spells;

namespace Repository.AttackBehavior
{
    class TargetAttackBehavior : IAttackBehavior
    {
        public IGameObject Target { get; set; }
        public GameWorld GameWorld { get; set; }

        public TargetAttackBehavior(GameWorld gameWorld, IGameObject target)
        {
            this.Target = target;
            GameWorld = gameWorld;
        }
        public TargetAttackBehavior(GameWorld gameWorld)
        {
            GameWorld = gameWorld;
        }

        public void Attack(IGameObject gameObject)
        {
            var spell = gameObject as Spell;
            if (spell != null)
            {
                if (Target == null) Target = spell.Source.Target;
                spell.MotionBehavior.Moved += (s, e) =>
                {
                    if (e.TotalDistanceMoved < 300) return;
                    if (Target != null && spell.MotionBehavior.GetType() != typeof(OffensiveMotionBehavior))
                        spell.MotionBehavior = new OffensiveMotionBehavior(spell, GameWorld, Target.Location);
                };
            }
        }
    }
}

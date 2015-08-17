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
    class ChainAttackBehavior : IAttackBehavior
    {
        public int Jumps { get; set; }
        public GameWorld GameWorld { get; set; }

        public ChainAttackBehavior(GameWorld gameWorld)
        {
            GameWorld = gameWorld;
            Jumps = 2;
        }

        public void Attack(IGameObject gameObject)
        {
            var s = gameObject as Spell;
            if (s == null) return;
            List<IGameObject> excludedList = new List<IGameObject>();
            excludedList.Add(s.Source);

            s.Collision += (spell, target) =>
            {
                if (spell != gameObject) return;
                if (target.GetType().IsSubclassOf(typeof(Spell))) return;
                if (target == spell.Source) return;
                excludedList.Add(target);
                IGameObject newTarget = GameWorld.GetClosestGameObjectFromMap(spell.Location.ToPoint(), 500, excludedList);

                if (spell.DamageBehavior.DoDamage(target))
                {
                    if(spell.DamageBehavior.CanDamage(newTarget))
                        if (newTarget != null)
                            spell.MotionBehavior = new OffensiveMotionBehavior(spell, GameWorld, newTarget.Location);
                }

                if (newTarget == null || Jumps-- <= 0)
                {
                    spell.SpellHandler.Remove(spell);
                }
            };

            s.MotionBehavior.Moved += (movementObject, e) =>
            {

                if (e.TotalDistanceMoved < s.Range) return;
                s.SpellHandler.Remove(s);
            };
        }
    }
}

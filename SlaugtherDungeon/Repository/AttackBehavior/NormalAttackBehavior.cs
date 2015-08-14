using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using Repository.Spells;

namespace Repository.AttackBehavior
{
    class NormalAttackBehavior : IAttackBehavior
    {
        public GameWorld GameWorld { get; set; }
        public void Attack(IGameObject gameObject)
        {
            var s = gameObject as Spell;
            if (s != null)
            {
                s.Collision += (spell, target) =>
                {
                    if (spell != s) return;
                    if (target == spell.Source) return;

                    if (spell.DamageBehavior.DoDamage(target))
                        spell.SpellHandler.Remove(spell);
                };

                s.MotionBehavior.Moved += (movementObject, e) =>
                {
                    if (s.Range == 0) return;
                    if (e.TotalDistanceMoved < s.Range) return;
                    s.SpellHandler.Remove(s);
                };
            }
        }

        public NormalAttackBehavior(GameWorld gameWorld)
        {
            GameWorld = gameWorld;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using Repository.Spells;

namespace Repository.AttackBehavior
{
    class RemoveTargetSpellAttackBehavior : IAttackBehavior
    {
        public GameWorld GameWorld { get; set; }
        public RemoveTargetSpellAttackBehavior(GameWorld gameWorld)
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
                if(target != null)
                    target.SpellHandler.Remove(target);
            };
        }
    }
}

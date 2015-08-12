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
    class SpawnSpellAttackBehavior : IAttackBehavior
    {
        public GameWorld GameWorld { get; set; }
        public Func<Spell> NewSpell { get; set; }

        public SpawnSpellAttackBehavior(GameWorld gameWorld, Func<Spell> newSpell)
        {
            GameWorld = gameWorld;
            NewSpell = newSpell;
        }

        public void Attack(IGameObject gameObject)
        {
            var s = gameObject as Spell;
            if (s == null) return;

            s.Collision += (spell, target) =>
            {
                if (spell != gameObject) return;
                if (target.GetType().IsSubclassOf(typeof(Spell))) return;
                if (target == spell.Source) return;

                GameWorld.GameObjects.Add(NewSpell());
            };
        }
    }
}

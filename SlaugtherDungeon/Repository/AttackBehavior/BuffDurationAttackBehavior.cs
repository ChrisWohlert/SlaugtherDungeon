using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using Repository.Buffs;
using Repository.Characters;
using Repository.Spells;

namespace Repository.AttackBehavior
{
    class BuffDurationAttackBehavior : IAttackBehavior
    {
        public GameWorld GameWorld { get; set; }
        public Buff Buff { get; set; }

        public BuffDurationAttackBehavior(GameWorld gameWorld, Buff buff)
        {
            GameWorld = gameWorld;
            Buff = buff;
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

                var c = target as Character;
                if (c == null) return;

                Buff.Character = c;
                Buff.Start();
            };
        }
    }
}

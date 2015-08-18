using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CHWGameEngine;
using Repository.AttackBehavior;
using Repository.Buffs;
using Repository.Characters;

namespace Repository.Spells
{
    public class SpellTree
    {
        public Character Character { get; set; }
        public GameWorld GameWorld { get; set; }

        public Dictionary<ActionSlot, Func<Spell>> AttackMap;
        public Dictionary<Type, List<IAttackBehavior>> AttackBehaviorMap;

        public SpellTree(Character character, GameWorld gameWorld)
        {
            Character = character;
            GameWorld = gameWorld;
            AttackMap = new Dictionary<ActionSlot, Func<Spell>>();
            AttackBehaviorMap = new Dictionary<Type, List<IAttackBehavior>>();
        }

        public void AddSpell(ActionSlot actionSlot, Type spellType)
        {
            if (spellType == typeof(Fireball))
                AttackMap.Add(actionSlot, CreateSpell(new Fireball(Character, GameWorld, 900)));
            else if (spellType == typeof(Frostball))
                AttackMap.Add(actionSlot, CreateSpell(new Frostball(Character, GameWorld, 600)));
            else if (spellType == typeof(IceGround))
                AttackMap.Add(actionSlot, CreateSpell(new IceGround(Character, GameWorld, 900)));
            else if (spellType == typeof(SpellShieldNova))
                AttackMap.Add(actionSlot, CreateSpell(new SpellShieldNova(Character, GameWorld, 900)));
            else if (spellType == typeof(Lightningbolt))
                AttackMap.Add(actionSlot, CreateSpell(new Lightningbolt(Character, GameWorld, 900)));

            Spell s = AttackMap[actionSlot]();
        }

        private Func<Spell> CreateSpell(Spell s)
        {
            return () =>
            {
                foreach (var ab in AttackBehaviorMap[s.GetType()])
                {
                    s.AttackBehaviors.Add(ab);
                }

                return s;
            };
        }
    }
}

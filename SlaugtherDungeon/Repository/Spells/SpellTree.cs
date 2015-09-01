using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CHWGameEngine;
using Repository.AttackBehavior;
using Repository.Buffs;
using Repository.Characters;
using Repository.Spells.SpellDecorators;

namespace Repository.Spells
{
    public class SpellTree
    {
        public Character Character { get; set; }
        public SpellHandler SpellHandler { get; set; }
        public GameWorld GameWorld { get; set; }

        private Dictionary<ActionSlot, Func<Spell>> attackMap;
        private Dictionary<Type, List<Func<Spell, Spell>>> attackDecorators; 

        public SpellTree(Character character, SpellHandler spellHandler, GameWorld gameWorld)
        {
            Character = character;
            SpellHandler = spellHandler;
            GameWorld = gameWorld;
            attackMap = new Dictionary<ActionSlot, Func<Spell>>();
            attackDecorators = new Dictionary<Type, List<Func<Spell, Spell>>>();
        }

        public void AddSpell(ActionSlot actionSlot, Type spellType)
        {
            Func<Spell> spell = CreateSpell(spellType);

            if (spell != null)
            {
                attackMap.Add(actionSlot, spell);
                attackDecorators[spellType] = new List<Func<Spell, Spell>>();
            }
        }

        public void AddAttackBehavior(Type spellType, Type attackType)
        {
            attackDecorators[spellType].Add(spell => new NormalAttack(spell));
        }

        public Spell GetSpell(ActionSlot actionSlot)
        {
            return attackMap.ContainsKey(actionSlot) ? attackMap[actionSlot]() : null;
        }

        private Func<Spell> CreateSpell(Type spellType)
        {
            Spell s = null;
            Cooldown cd = null;
            return () =>
            {
                if (spellType == typeof (Fireball))
                {
                    s = new Fireball(Character, GameWorld, 900);
                    cd = new Cooldown(2);
                }
                else if (spellType == typeof (Frostball))
                {
                    s = new Frostball(Character, GameWorld, 600);
                    cd = new Cooldown(3);
                }
                else if (spellType == typeof (IceGround))
                {
                    s = new IceGround(Character, GameWorld, 900);
                    cd = new Cooldown(5);
                }
                else if (spellType == typeof (SpellShieldNova))
                {
                    s = new SpellShieldNova(Character, GameWorld, 900);
                    cd = new Cooldown(7);
                }
                else if (spellType == typeof (Lightningbolt))
                {
                    s = new Lightningbolt(Character, GameWorld, 900);
                    cd = new Cooldown(1);
                }

                if (s != null)
                {
                    foreach (var decorator in attackDecorators[spellType])
                            s = decorator(s);

                    if (!SpellHandler.Cooldowns.ContainsKey(s.GetType()))
                        SpellHandler.Cooldowns[s.GetType()] = cd;
                }

                return s;
            };
        }
    }
}

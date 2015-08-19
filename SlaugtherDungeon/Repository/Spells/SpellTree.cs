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

namespace Repository.Spells
{
    public class SpellTree
    {
        public Character Character { get; set; }
        public SpellHandler SpellHandler { get; set; }
        public GameWorld GameWorld { get; set; }

        private Dictionary<ActionSlot, Func<Spell>> attackMap;
        private Dictionary<Type, List<IAttackBehavior>> attackBehaviorMap;

        public SpellTree(Character character, SpellHandler spellHandler, GameWorld gameWorld)
        {
            Character = character;
            SpellHandler = spellHandler;
            GameWorld = gameWorld;
            attackMap = new Dictionary<ActionSlot, Func<Spell>>();
            attackBehaviorMap = new Dictionary<Type, List<IAttackBehavior>>();
        }

        public void AddSpell(ActionSlot actionSlot, Type spellType)
        {
            Func<Spell> spell = CreateSpell(spellType);

            if(spell != null)
                attackMap.Add(actionSlot, spell);
        }

        public void AddAttackBehavior(Type spellType, Type attackType, Buff buff = null, Type spawnSpellType = null)
        {
            IAttackBehavior attack = null;

            if(!attackBehaviorMap.ContainsKey(spellType))
                attackBehaviorMap[spellType] = new List<IAttackBehavior>();

            if (attackType == typeof (BuffDurationAttackBehavior))
            {
                if (buff != null)
                    attackBehaviorMap[spellType].Add(new BuffDurationAttackBehavior(GameWorld, buff));
            }
            else if (attackType == typeof (ChainAttackBehavior))
                attack = new ChainAttackBehavior(GameWorld);
            else if (attackType == typeof (ChangeImageAttackBehavior))
               attack = new ChangeImageAttackBehavior(GameWorld);
            else if (attackType == typeof (CircleSpellAttackBehavior))
                attack = new CircleSpellAttackBehavior(GameWorld);
            else if (attackType == typeof (NormalAttackBehavior))
                attack = new NormalAttackBehavior(GameWorld);
            else if (attackType == typeof (NovaAttackBehavior))
                 attack = new NovaAttackBehavior(GameWorld);
            else if (attackType == typeof (RejectSpellAttackBehavior))
                attack = new RejectSpellAttackBehavior(GameWorld);
            else if (attackType == typeof (RemoveTargetSpellAttackBehavior))
                attack = new RemoveTargetSpellAttackBehavior(GameWorld);
            else if (attackType == typeof (SplitShotAttackBehavior))
                attack = new SplitShotAttackBehavior(GameWorld);
            else if (attackType == typeof (TargetAttackBehavior))
                attack = new TargetAttackBehavior(GameWorld);
            else if (attackType == typeof (SpawnSpellAttackBehavior))
                if (spawnSpellType != null)
                    attack = new SpawnSpellAttackBehavior(GameWorld, CreateSpell(spawnSpellType));

            if (attack != null)
                attackBehaviorMap[spellType].Add(attack);
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

                if (!SpellHandler.Cooldowns.ContainsKey(spellType))
                    SpellHandler.Cooldowns[spellType] = cd;

                if(s != null)
                    foreach (var ab in attackBehaviorMap[s.GetType()])
                    {
                        s.AttackBehaviors.Add(ab);
                    }

                return s;
            };
        }
    }
}

using System;
using System.Collections.Generic;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using CHWGameEngine.Motion;
using Repository.AttackBehavior;
using Repository.Buffs;
using Repository.DamageBehavior;
using Repository.Energies;
using Repository.Experience;
using Repository.Spells;

namespace Repository.Characters
{
    public class Player : Character
    {
        public Player(GameWorld gameWorld, DecimalPoint location) : base(gameWorld, location)
        {
            Image = Properties.Resources.Player;
            Acceleration = 2;
            Inertia.X = 2;
            Inertia.Y = 2;
            DrawBehavior.IsCircle = true;
            Health.Max = 5000;
            Health.Current = Health.Max;
            Targets.Add(typeof(Enemy));
            CreateAttackMap();
        }

        private void CreateAttackMap()
        {
            AttackMap.Add(ActionSlot.First, () =>
            {
                var f = new Fireball(this, GameWorld, 900);
                f.AttackBehaviors.Add(new SplitShotAttackBehavior(GameWorld));
                f.AttackBehaviors.Add(new TargetAttackBehavior(GameWorld));
                f.AttackBehaviors.Add(new NormalAttackBehavior(GameWorld));
                return f;
            });
            AttackMap.Add(ActionSlot.Second, () =>
            {
                var f = new Frostball(this, GameWorld, 900);
                f.AttackBehaviors.Add(new BuffDurationAttackBehavior(GameWorld, new Slow(30) { Duration = 3 }));
                f.AttackBehaviors.Add(new NormalAttackBehavior(GameWorld));
                return f;
            });
            AttackMap.Add(ActionSlot.Third, () =>
            {
                var nova = new SpellShieldNova(this, GameWorld, 500);
                nova.AttackBehaviors.Add(new RejectSpellAttackBehavior(GameWorld));
                nova.AttackBehaviors.Add(new NovaAttackBehavior(GameWorld));
                return nova;
            });

            SpellHandler.Cooldowns.Add(typeof(Fireball), new Cooldown(2));
            SpellHandler.Cooldowns.Add(typeof(Frostball), new Cooldown(5));
            SpellHandler.Cooldowns.Add(typeof(SpellShieldNova), new Cooldown(1));


        }
    }
}

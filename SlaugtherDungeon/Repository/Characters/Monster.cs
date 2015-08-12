using System;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using CHWGameEngine.Motion;
using Repository.AttackBehavior;
using Repository.DamageBehavior;
using Repository.Experience;
using Repository.Spells;

namespace Repository.Characters
{
    class Monster : Enemy
    {
        public Monster(GameWorld gameWorld, DecimalPoint location) : base(gameWorld, location)
        {
            Image = Properties.Resources.MonsterSmall;
            Inertia.X = 2;
            Inertia.Y = 2;
            TargetSpeed = 7;
            DrawBehavior.IsCircle = true;
            MotionBehavior = new WanderingMotionBehavior(this, gameWorld);
            CreateAttackMap();
        }

        private void CreateAttackMap()
        {
            AttackMap.Add(ActionSlot.First, () =>
            {
                var f = new Fireball(this, GameWorld, 600);
                f.AttackBehaviors.Add(new NormalAttackBehavior(GameWorld));
                return f;
            });
            SpellHandler.Cooldowns.Add(typeof(Fireball), new Cooldown(2));
        }
    }
}

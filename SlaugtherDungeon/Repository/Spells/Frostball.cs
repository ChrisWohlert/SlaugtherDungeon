using System;
using System.Collections.Generic;
using System.Drawing;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using Repository.AttackBehavior;
using Repository.Characters;

namespace Repository.Spells
{
    class Frostball : Spell
    {
        public Frostball(Character source, GameWorld gameWorld, int range)
            : base(source, gameWorld, range)
        {
            Image = Properties.Resources.Frostbolt_small;
            if(!SpellHandler.Cooldowns.ContainsKey(typeof(IceGround)))
                SpellHandler.Cooldowns.Add(typeof(IceGround), new Cooldown(0));
        }

        protected override void MotionBehavior_WallCollision(object sender, EventArgs e)
        {
            SpellHandler.Remove(this);
        }

        public override object Clone()
        {
            return new Frostball(Source, GameWorld, Range)
            {
                Acceleration = this.Acceleration,
                ActualSpeed = this.ActualSpeed,
                Angle = this.Angle,
                AttackBehaviors = new List<IAttackBehavior>(this.AttackBehaviors),
                Cost = this.Cost,
                Damage = this.Damage,
                Image = this.Image,
                Location = new DecimalPoint(this.Location.X, this.Location.Y)
            };
        }

        private void Explode()
        {
            SpellHandler.Cast(new IceGround(Source, GameWorld, 50) 
            { Location = new DecimalPoint(this.Location.X, this.Location.Y), Angle = this.Angle });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using Repository.AttackBehavior;
using Repository.Characters;

namespace Repository.Spells
{
    class Fireball : Spell
    {
        public Fireball(Character source, GameWorld gameWorld, int range)
            : base(source, gameWorld, range)
        {
            Image = Properties.Resources.Fireball;
            Init();
        }

        protected override void MotionBehavior_WallCollision(object sender, EventArgs e)
        {
            SpellHandler.Remove(this);
        }

        public override object Clone()
        {
            return new Fireball(Source, GameWorld, Range)
            {
                Acceleration = this.Acceleration,
                ActualSpeed = this.ActualSpeed,
                Angle = this.Angle,
                Cost = this.Cost,
                Damage = this.Damage,
                Image = this.Image,
                Location = new DecimalPoint(this.Location.X, this.Location.Y)
            };
        }
    }
}

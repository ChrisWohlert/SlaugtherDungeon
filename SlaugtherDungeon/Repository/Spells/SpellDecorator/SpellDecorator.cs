using System;
using System.Drawing;
using CHWGameEngine;
using CHWGameEngine.CHWGraphics;
using CHWGameEngine.GameObject;
using CHWGameEngine.Motion;
using Repository.Characters;
using Repository.DamageBehavior;

namespace Repository.Spells.SpellDecorator
{
    public abstract class SpellDecorator : Spell
    {
        public Spell Decoratee { get; set; }
        public override Character Source { get { return Decoratee.Source; } set { Decoratee.Source = value; } }
        public override GameWorld GameWorld { get { return Decoratee.GameWorld; } set { Decoratee.GameWorld = value; } }
        public override double Damage { get { return Decoratee.Damage; } set { Decoratee.Damage = value; } }
        public override int Cost { get { return Decoratee.Cost; } set { Decoratee.Cost = value; } }
        public override int Range { get { return Decoratee.Range; } set { Decoratee.Range = value; } }
        public override SpellHandler SpellHandler { get { return Decoratee.SpellHandler; } set { Decoratee.SpellHandler = value; } }
        public override DecimalPoint Location { get { return Decoratee.Location; } set { Decoratee.Location = value; } }
        public override Image Image { get { return Decoratee.Image; } set { Decoratee.Image = value; } }
        public override int Angle { get { return Decoratee.Angle; } set { Decoratee.Angle = value; } }
        public override double ActualSpeed { get { return Decoratee.ActualSpeed; } set { Decoratee.ActualSpeed = value; } }
        public override double Acceleration { get { return Decoratee.Acceleration; } set { Decoratee.Acceleration = value; } }
        public override double TargetSpeed { get { return Decoratee.TargetSpeed; } set { Decoratee.TargetSpeed = value; } }
        public override DecimalPoint Inertia { get { return Decoratee.Inertia; } set { Decoratee.Inertia = value; } }
        public override Paralax Paralax { get { return Decoratee.Paralax; } set { Decoratee.Paralax = value; } }
        public override IMotionBehavior MotionBehavior { get { return Decoratee.MotionBehavior; } set { Decoratee.MotionBehavior = value; } }
        public override bool IsPhysicalObject { get { return Decoratee.IsPhysicalObject; } set { Decoratee.IsPhysicalObject = value; } }
        public override IDrawBehavior DrawBehavior { get { return Decoratee.DrawBehavior; } set { Decoratee.DrawBehavior = value; } }
        public override IDamageBehavior DamageBehavior { get { return Decoratee.DamageBehavior; } set { Decoratee.DamageBehavior = value; } }

        protected SpellDecorator(Spell decoratee)
        {
            Decoratee = decoratee;
        }
    }
}
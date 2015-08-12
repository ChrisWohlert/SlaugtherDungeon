using System;
using System.Collections.Generic;
using System.Drawing;
using CHWGameEngine;
using CHWGameEngine.CHWGraphics;
using CHWGameEngine.GameObject;
using CHWGameEngine.Motion;
using Repository.AttackBehavior;
using Repository.Characters;
using Repository.DamageBehavior;

namespace Repository.Spells
{
    public delegate void SendSpell(Spell spell, IGameObject target);
    public abstract class Spell : IGameObject, ICloneable
    {
        public Character Source { get; set; }
        public GameWorld GameWorld { get; set; }
        private IMotionBehavior motionBehavior;

        public double Damage { get; set; }
        public int Cost { get; set; }
        public int Range { get; set; }
        public SpellHandler SpellHandler { get; set; }

        public List<IAttackBehavior> AttackBehaviors { get; set; } 

        #region IGameObject Properties

        public DecimalPoint Location { get; set; }
        public Image Image { get; set; }
        public int Angle { get; set; }
        public double ActualSpeed { get; set; }
        public double Acceleration { get; set; }
        public double TargetSpeed { get; set; }
        public DecimalPoint Inertia { get; set; }
        public Paralax Paralax { get; set; }
        public IMotionBehavior MotionBehavior
        {
            get { return motionBehavior; }
            set
            {
                motionBehavior = value;
                motionBehavior.CanMoveThroughObjects = true;
                motionBehavior.WallCollision += MotionBehavior_WallCollision;
            }
        }

        public bool IsPhysicalObject { get; set; }
        public IDrawBehavior DrawBehavior { get; set; }

        #endregion

        public IDamageBehavior DamageBehavior { get; set; }

        public event SendSpell Collision;

        public event SendSpell Split;

        protected Spell(Character source, GameWorld gameWorld, int range)
        {
            Source = source;
            GameWorld = gameWorld;
            Range = range;
            Init();
        }

        private void Init()
        {
            Acceleration = 5;
            Angle = Source.Target != null ? (int)GameWorld.CalcAngle(Source.Location.ToPoint(), Source.Target.Location.ToPoint()) : Source.Angle;
            Cost = 20;
            Damage = 200;
            DamageBehavior = new NormalDamageBehavior(Source, Source.Targets, (int)Damage);
            DrawBehavior = new TopDownRotationDrawBehavior(this);
            Inertia = new DecimalPoint(5, 5);
            ActualSpeed = Source.ActualSpeed;
            Location = new DecimalPoint(Source.Location.X, Source.Location.Y);
            MotionBehavior = new NormalMotionBehavior(this, GameWorld);
            Paralax = Paralax.Middleground;
            TargetSpeed = 20;
            IsPhysicalObject = false;
            MotionBehavior.CanMoveThroughObjects = true;
            SpellHandler = Source.SpellHandler;
            SpellHandler.Spells.Add(this);
            AttackBehaviors = new List<IAttackBehavior>();

            GameWorld.Collision += GameWorld_Collision;
            GameWorld.Split += GameWorld_Split;
        }

        public void Cast()
        {
            foreach (var attack in AttackBehaviors)
            {
                attack.Attack(this);
            }

            GameWorld.GameObjects.Add(this);
        }
        void GameWorld_Collision(ref IGameObject object1, ref IGameObject object2)
        {
            var spell = object1 as Spell;
            if (spell != null)
            {
                if (spell == this)
                {
                    if (Collision != null) Collision(spell, object2);
                    return;
                }
            }

            spell = object2 as Spell;
            if (spell != null)
            {
                if (spell == this)
                    if (Collision != null) Collision(spell, object1);
            }
        }

        void GameWorld_Split(ref IGameObject object1, ref IGameObject object2)
        {
            var spell = object1 as Spell;
            if (spell != null)
            {
                if (spell == this)
                {
                    if (Split != null) Split(spell, object2);
                    return;
                }
            }

            spell = object2 as Spell;
            if (spell != null)
            {
                if (spell == this)
                    if (Split != null) Split(spell, object1);
            }
        }

        protected abstract void MotionBehavior_WallCollision(object sender, EventArgs e);
        public abstract object Clone();
    }
}

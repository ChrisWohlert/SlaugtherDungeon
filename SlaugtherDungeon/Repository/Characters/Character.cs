using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CHWGameEngine;
using CHWGameEngine.CHWGraphics;
using CHWGameEngine.GameObject;
using CHWGameEngine.Motion;
using Repository.DamageBehavior;
using Repository.Energies;
using Repository.Experience;
using Repository.Loots;
using Repository.Spells;

namespace Repository.Characters
{
    public abstract class Character : IGameObject, IKillable
    {
        #region Properties

        public IExperience Experience { get; set; }
        public int Level { get; private set; }
        public Health Health { get; set; }
        public double MaxHealth { get; set; }
        public IEnergy Energy { get; set; }
        public SpellHandler SpellHandler { get; set; }
        public SpellTree SpellTree { get; set; }
        public List<Type> Targets { get; private set; }

        #endregion

        #region IGameObject Properties

        public DecimalPoint Location { get; set; }
        public Image Image { get; set; }
        public int Angle { get; set; }
        public double ActualSpeed { get; set; }
        public double Acceleration { get; set; }
        public double TargetSpeed { get; set; }
        public DecimalPoint Inertia { get; set; }
        public Paralax Paralax { get; set; }
        public IMotionBehavior MotionBehavior { get; set; }
        public bool IsPhysicalObject { get; set; }
        public IDrawBehavior DrawBehavior { get; set; }

        #endregion

        #region IKillable Properties

        public int ExperienceWhenKilled { get; set; }
        public Loot Loot { get; set; }

        public event SendIKillable Killed;

        #endregion

        public GameWorld GameWorld { get; private set; }

        public IGameObject Target { get; set; }


        protected Character(GameWorld gameWorld, DecimalPoint location)
        {
            Location = location;
            GameWorld = gameWorld;
            MotionBehavior = new NormalMotionBehavior(this, gameWorld);
            Targets = new List<Type>();
            Init();
        }

        private void Init()
        {
            Energy = new Mana(300);
            Health = new Health(1000);

            DrawBehavior = new TopDownRotationDrawBehavior(this);

            Level = 1;
            Experience = new NormalExperience();

            Inertia = new DecimalPoint(0.5, 0.5);
            Acceleration = 5;
            IsPhysicalObject = true;

            Paralax = Paralax.Middleground;

            SpellHandler = new SpellHandler(GameWorld, this);
            SpellTree = new SpellTree();

            ExperienceWhenKilled = 300;

            Loot = new Loot();
            Loot.Items.Add(new HealthPowerUp(Location, GameWorld));

            Experience.LevelUp += Experience_LevelUp;
            Killed += Character_Killed;
        }

        public void Character_Killed(IKillable killed, IGameObject source)
        {
            var s = source as Character;
            if (s.Experience != null) s.Experience.Add(ExperienceWhenKilled);
            Loot.Drop();
            GameWorld.Remove(this);
        }

        void Experience_LevelUp(object sender, EventArgs e)
        {
            Level++;
        }

        public void TakeDamage(IDamageBehavior damageBehavior, Character source)
        {
            Health.Use(damageBehavior.Damage);
            if (Health.Current <= 0) Killed(this, source);
        }

        public void Attack(ActionSlot actionSlot)
        {
            SpellHandler.Cast(SpellTree.AttackMap[actionSlot]());
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}

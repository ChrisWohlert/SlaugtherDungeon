using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHWGameEngine;
using CHWGameEngine.CHWGraphics;
using CHWGameEngine.GameObject;
using CHWGameEngine.Motion;
using Repository.Buffs;

namespace Repository.Loots
{
    public abstract class PowerUp : Buff, ILootable
    {
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
        public double DropChance { get; set; }

        public GameWorld GameWorld { get; set; }

        protected PowerUp(DecimalPoint location, GameWorld gameWorld)
        {
            Location = location;
            GameWorld = gameWorld;
            GameWorld.Collision += gameWorld_Collision;

            Inertia = new DecimalPoint(0, 0);
            MotionBehavior = new StationaryMotionBehavior(this, gameWorld);
            Paralax = Paralax.Middleground;
            DrawBehavior = new TopDownRotationDrawBehavior(this);

            IsPhysicalObject = false;
        }

        public abstract void gameWorld_Collision(ref IGameObject object1, ref IGameObject object2);
        public void Drop()
        {
            GameWorld.GameObjects.Add(this);
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}

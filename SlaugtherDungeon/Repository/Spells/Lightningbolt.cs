using System;
using System.Drawing;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using Repository.Characters;

namespace Repository.Spells
{
    class Lightningbolt : Spell
    {

        protected override void MotionBehavior_WallCollision(object sender, EventArgs e)
        {
            
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        public Lightningbolt(Character source, GameWorld gameWorld, int range) : base(source, gameWorld, range)
        {
        }
    }
}

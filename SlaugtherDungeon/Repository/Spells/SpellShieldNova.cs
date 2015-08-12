using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHWGameEngine;
using CHWGameEngine.Motion;
using Repository.Characters;

namespace Repository.Spells
{
    class SpellShieldNova : Spell
    {
        public SpellShieldNova(Character source, GameWorld gameWorld, int range) : base(source, gameWorld, range)
        {
            Image = Properties.Resources.SpellNova;
            DrawBehavior.IsCircle = true;
            MotionBehavior = new StationaryMotionBehavior(this, gameWorld);
        }

        protected override void MotionBehavior_WallCollision(object sender, EventArgs e)
        {
            
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}

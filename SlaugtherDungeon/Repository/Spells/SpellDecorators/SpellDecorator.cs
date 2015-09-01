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

namespace Repository.Spells.SpellDecorators
{
    public abstract class SpellDecorator : Spell
    {
        public Spell RefSpell{ get; set; }
        public override double Damage { get { return RefSpell.Damage; }  }
        public override int Cost { get { return RefSpell.Cost; }  }
        public override SpellHandler SpellHandler { get { return RefSpell.SpellHandler; }  }
        public override DecimalPoint Location { get { return RefSpell.Location; }  }
        public override Image Image { get { return RefSpell.Image; }  }
        public override int Angle { get { return RefSpell.Angle; }  }
        public override double ActualSpeed { get { return RefSpell.ActualSpeed; }  }
        public override double Acceleration { get { return RefSpell.Acceleration; }  }
        public override double TargetSpeed { get { return RefSpell.TargetSpeed; }  }
        public override DecimalPoint Inertia { get { return RefSpell.Inertia; }  }
        public override Paralax Paralax { get { return RefSpell.Paralax; }  }
        public override IMotionBehavior MotionBehavior { get { return RefSpell.MotionBehavior; }  }
        public override bool IsPhysicalObject { get { return RefSpell.IsPhysicalObject; }  }
        public override IDrawBehavior DrawBehavior { get { return RefSpell.DrawBehavior; }  }
        public override IDamageBehavior DamageBehavior { get { return RefSpell.DamageBehavior; }  }

        protected SpellDecorator(Spell refSpell)
            : base(refSpell.Source, refSpell.GameWorld, refSpell.Range)
        {
            RefSpell = refSpell;
        }
        public override void Cast() {
            RefSpell.Cast();
        }
    }
}
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

        protected SpellDecorator(Spell refSpell)
            : base(refSpell.Source, refSpell.GameWorld, refSpell.Range)
        {
            RefSpell = refSpell;
        }
    }
}
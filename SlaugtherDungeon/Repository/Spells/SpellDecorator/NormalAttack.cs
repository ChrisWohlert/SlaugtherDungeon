using System;

namespace Repository.Spells.SpellDecorator
{
    public class NormalAttack : SpellDecorator
    {
        public NormalAttack(Spell decoratee) : base(decoratee)
        {
        }

        public override void Cast()
        {
            Decoratee.Collision += (spell, target) =>
            {
                if (spell != Decoratee) return;
                if (target == spell.Source) return;

                if (spell.DamageBehavior.DoDamage(target))
                    spell.SpellHandler.Remove(spell);
            };

            Decoratee.MotionBehavior.Moved += (movementObject, e) =>
            {
                if (Decoratee.Range == 0) return;
                if (e.TotalDistanceMoved < Decoratee.Range) return;
                Decoratee.SpellHandler.Remove(Decoratee);
            };

            Decoratee.Cast();
        }

        protected override void MotionBehavior_WallCollision(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
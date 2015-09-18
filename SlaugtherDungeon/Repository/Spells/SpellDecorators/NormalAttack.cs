using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Spells.SpellDecorators {
    class NormalAttack : SpellDecorator {
        public NormalAttack(Spell refSpell)
            : base(refSpell) {
        }

        public override void Cast() {
            RefSpell.Collision += (spell, target) => {
                if (spell != RefSpell) return;
                if (target == spell.Source) return;

                if (spell.DamageBehavior.DoDamage(target))
                    spell.SpellHandler.Remove(spell);
            };

            RefSpell.MotionBehavior.Moved += (movementObject, e) => {
                if (this.Range == 0) return;
                if (e.TotalDistanceMoved < this.Range) return;
                this.SpellHandler.Remove(RefSpell);
            };

            RefSpell.Cast();
        }

        protected override void MotionBehavior_WallCollision(object sender, EventArgs e) {

        }

        public override object Clone() {
            return this;
        }
    }
}

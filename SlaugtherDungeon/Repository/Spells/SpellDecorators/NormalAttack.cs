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
            this.Collision += (spell, target) => {
                if (spell != this) return;
                if (target == spell.Source) return;

                if (spell.DamageBehavior.DoDamage(target))
                    spell.SpellHandler.Remove(spell);
            };

            this.MotionBehavior.Moved += (movementObject, e) => {
                if (this.Range == 0) return;
                if (e.TotalDistanceMoved < this.Range) return;
                this.SpellHandler.Remove(this);
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

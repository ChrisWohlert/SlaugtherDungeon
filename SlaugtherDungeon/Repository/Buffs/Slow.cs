using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Characters;

namespace Repository.Buffs
{
    class Slow : Buff
    {
        private double oldActualSpeed;
        private double oldTargetSpeed;
        public int Percentage { get; set; }

        public Slow(int percentage)
        {
            Percentage = percentage;
        }

        public override void Effect()
        {
            oldActualSpeed = Character.ActualSpeed;
            oldTargetSpeed = Character.TargetSpeed;
            Character.ActualSpeed -= oldActualSpeed / (100d / Percentage);
            Character.TargetSpeed = Character.ActualSpeed;
        }

        public override void UndoEffect()
        {
            Character.TargetSpeed = oldTargetSpeed;
        }
    }
}

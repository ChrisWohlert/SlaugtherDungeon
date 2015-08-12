using System;

namespace Repository.Energies
{
    public class Health : IEnergy
    {
        private double current;
        public event EventHandler HealthGone;
        public event EventHandler HealthChanged;
        public int Max { get; set; }

        public double Current
        {
            get { return current; }
            set
            {
                current = value;
                if (HealthChanged != null) HealthChanged(this, EventArgs.Empty);
                if (current <= 0)
                    if (HealthGone != null) HealthGone(this, EventArgs.Empty);
            }
        }

        public double RegenerationRate { get; set; }

        public int Percentage
        {
            get { return ((int)(Current / Max * 100)); }
            set { Current = 100 / value * Max; }
        }

        public Health(int max)
        {
            Max = max;
            Current = Max;
            RegenerationRate = 1;
        }

        public void Regenerate()
        {
            Regenerate(RegenerationRate);
        }

        public void Regenerate(double amount)
        {
            double newCurrent = Current + amount;
            Current = newCurrent > Max ? Max : newCurrent;
        }

        public bool Use(double amount)
        {
            Current -= amount;
            return true;
        }
    }
}
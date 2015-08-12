using System.Timers;

namespace Repository.Energies
{
    class Mana : IEnergy
    {
        private Timer regenTimer;
        private double current;
        public int Max { get; set; }

        public double Current
        {
            get
            {
                return current;
            }
            set
            {
                current = value;
                if (current < Max)
                    if (!regenTimer.Enabled)
                        regenTimer.Enabled = true;
            }
        }

        public int Percentage
        {
            get { return ((int)(Current / Max * 100)); }
            set { Current = 100 / value * Max; }
        }

        public double RegenerationRate { get; set; }
        public void Regenerate()
        {
            Current += RegenerationRate;
        }
        public void Regenerate(double amount)
        {
            Current = Current + amount <= Max ? Current += amount : Max;
        }
        public bool Use(double amount)
        {
            if (amount > Current) return false;
            Current -= amount;
            return true;
        }
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="max">The maximum amount of mana</param>
        public Mana(int max)
        {
            Max = max;
            Current = max;
            RegenerationRate = 1;
            regenTimer = new Timer(200);
            regenTimer.Elapsed += regenTimer_Elapsed;
        }

        void regenTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(Current < Max)
                Regenerate();
        }
    }
}

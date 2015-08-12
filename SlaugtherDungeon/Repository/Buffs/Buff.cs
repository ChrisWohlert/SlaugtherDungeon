using System;
using System.Timers;
using Repository.Characters;

namespace Repository.Buffs
{
    public abstract class Buff
    {
        private Timer durationTimer;
        private int tickCounter;
        public bool IsMoreTime { get { return Interval*tickCounter < (int) Duration*1000; } }
        public double Duration { get; set; }
        public int Interval { get; set; }

        public event EventHandler Undo;
        public Character Character { get; set; }

        protected Buff()
        {
            Duration = 0;
            tickCounter = 1;
            Interval = (int)Duration;
        }

        public void Start()
        {
            if (Duration > 0)
            {
                if (Interval == 0) Interval = (int)Duration*1000 + 1;
                durationTimer = new Timer(Interval);
                durationTimer.Elapsed += durationTimer_Elapsed;
                durationTimer.Enabled = true;
                Effect();
            }
            else
            {
                Effect();
            }
        }

        void durationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (IsMoreTime)
            {
                Effect();
                tickCounter++;
            }
            else
            {
                durationTimer.Enabled = false;
                
                UndoEffect();
                OnUndo();
            }
        }

        protected virtual void OnUndo()
        {
            if (Undo != null) Undo(this, EventArgs.Empty);
        }

        public abstract void Effect();
        public abstract void UndoEffect();
    }
}
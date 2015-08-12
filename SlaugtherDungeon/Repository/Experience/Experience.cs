using System;

namespace Repository.Experience
{
    abstract class Experience : IExperience
    {
        public int Max { get; set; }
        public int Current { get; private set; }

        public int Percentage
        {
            get { return (int)((double)Current / Max * 100); }
            set { Current = 100/value*Max; }
        }

        public event EventHandler LevelUp;

        protected Experience()
        {
            Max = 1000;
            Current = 0;
        }
        public void Add(int amount)
        {
            Current += amount;
            if (Current > Max)
            {
                if (LevelUp != null) LevelUp(this, EventArgs.Empty);
                Increase();
            }
        }

        public void Subtract(int amount)
        {
            Current = Current - amount < 0 ? 0 : Current - amount;
        }

        public void Increase()
        {
            Max += (int) (Max*1.3 + 2000);
        }
    }
}

using System.Timers;

namespace Repository.Spells
{
    public class Cooldown
    {
        private Timer t;
        public int Max { get; set; }
        public int Current { get; set; }
        public Cooldown(int max)
        {
            Max = max;
            t = new Timer(1000);
            t.Start();
            t.Elapsed += t_Elapsed;
        }

        public void Reset()
        {
            Current = Max;
        }

        public bool Start()
        {
            if (Max == 0) return true;
            if (Current == 0)
            {
                Reset();
                t.Start();
                return true;
            }
            return false;
        }

        void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Current > 0)
                Current--;
            else
            {
                t.Stop();
            }
        }
    }
}

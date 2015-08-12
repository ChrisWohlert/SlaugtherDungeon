using System;

namespace Repository.Experience
{
    public interface IExperience
    {
        int Max { get; set; }
        int Current { get; }
        int Percentage { get; set; }

        event EventHandler LevelUp;
        void Add(int amount);
        void Subtract(int amount);
        void Increase();
    }
}

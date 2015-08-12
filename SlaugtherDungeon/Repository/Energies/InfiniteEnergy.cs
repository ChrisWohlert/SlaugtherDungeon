namespace Repository.Energies
{
    class InfiniteEnergy : IEnergy
    {
        public int Max { get; set; }
        public double Current { get; set; }
        public int Percentage { get; set; }
        public double RegenerationRate { get; set; }
        public void Regenerate()
        {
            
        }

        public void Regenerate(double amount)
        {
            
        }
        /// <summary>
        /// There will always be enough mana
        /// </summary>
        /// <param name="amount">redundant amount</param>
        /// <returns>returns true</returns>
        public bool Use(double amount)
        {
            return true;
        }
    }
}

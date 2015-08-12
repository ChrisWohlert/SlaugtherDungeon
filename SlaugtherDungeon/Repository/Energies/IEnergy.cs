namespace Repository.Energies
{
    public interface IEnergy
    {
        /// <summary>
        /// Maximum amount energy
        /// </summary>
        int Max { get; set; }
        /// <summary>
        /// Current amount of energy
        /// </summary>
        double Current { get; set; }
        /// <summary>
        /// Gets or sets the percentage of energy
        /// </summary>
        int Percentage { get; set; }
        /// <summary>
        /// At what rate energy regenerates
        /// </summary>
        double RegenerationRate { get; set; }
        /// <summary>
        /// Regenerates energy
        /// </summary>
        void Regenerate();
        /// <summary>
        /// Regenerates energy
        /// </summary>
        /// <param name="amount">Amount to regenerate</param>
        void Regenerate(double amount);
        /// <summary>
        /// Uses energy, if there is enough
        /// </summary>
        /// <param name="amount">Amount of energy to use</param>
        /// <returns>Returns true if there is enough energy, false otherwise</returns>
        bool Use(double amount);
    }
}

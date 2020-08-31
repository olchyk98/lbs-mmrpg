namespace lbs_rpg.classes.instances.villages
{
    public class VillageTask
    {
        #region Fields
        // Parent village
        public Village Village;
        
        // Task description
        public string Description { get; }
        
        // Value that represets how many reputation points the player will gain after finishing this task
        public int ReputationBonus { get; }
        
        // Value that represents how many animation ticks it will take for player to finish this task
        public int DurationTicks { get; }

        #endregion
        
        #region Constructor

        /// <summary>
        /// {VillageTask}:Constructor
        /// </summary>
        /// <param name="village">
        /// Memory Address to the parent village 
        /// </param>
        /// <param name="description">
        /// Task's title/description
        /// </param>
        /// <param name="bonus">
        /// Bonus that player will get after the completing this task
        /// </param>
        /// <param name="duration">
        /// Time (ticks) that player should spend to finish this task
        /// </param>
        public VillageTask(Village village, string description, int bonus, int duration)
        {
            Village = village;
            Description = description;
            ReputationBonus = bonus;
            DurationTicks = duration;
        }
        #endregion
    }
}
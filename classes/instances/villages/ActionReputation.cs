using System;

namespace lbs_rpg.classes.instances.villages
{
    /// <summary>
    /// ENUM-LIKE ActionReputation class
    /// </summary>
    public class ActionReputation
    {
        public readonly int Reputation;
        private readonly string myActionLabel;
        private static readonly Random Random = new Random();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="actionLabel">
        /// Original item label.
        /// This value is used to compare items from ActionReputation
        /// </param>
        /// <param name="reputation">
        /// Array of two numbers that represent minimal and maximal
        /// </param>
        public ActionReputation(string actionLabel, int[] reputation)
        {
            Reputation = Random.Next(reputation[0], reputation[1]);
            myActionLabel = actionLabel;
        }
        
#nullable enable
        public override bool Equals(object? obj)
        {
            // Check if compared to a MonsterType
            if (!(obj is ActionReputation)) return false;
            
            // Cast to MonsterType
            ActionReputation otherAction = (ActionReputation) obj;
            
            // Return the comparison by name (since names are unique)
            return obj is ActionReputation && this.myActionLabel == otherAction.myActionLabel;
        }
#nullable disable
        
        public static ActionReputation BuyShopItem => new ActionReputation("BUY_SHOP_ITEM", new[] { 2, 18 });
    }
}
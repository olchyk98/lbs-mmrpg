using lbs_rpg.contracts.gui;

namespace lbs_rpg.contracts.entity
{
    /// <summary>
    /// Can be instantiated only in the 2D context
    /// </summary>
    public interface IMonster : IEntity, IObject2D
    {
        public int TicksPerMove { get; }
        public float HeadPrice { get; }
        public float AttackDamage { get; }
        
        // Char representation of monster's look on the canvas
        public char ModelChar { get; }

        /// <summary>
        /// Calculates monster's next position that will bring
        /// monster nearer to the target.
        /// </summary>
        /// <param name="target">
        /// Any canvas object
        /// </param>
        public void MoveToObject(IObject2D target)
        {
            // Define position direction
            int directionX = (target.Position[0] < Position[0]) ? -1 : 1;
            int directionY = (target.Position[1] < Position[1]) ? -1 : 1;
            
            // Move the monster
            MoveDirection(directionX, directionY);
        }

        /// <summary>
        /// Check if monster is in the same position as the target.
        /// </summary>
        /// <param name="target">
        /// Any canvas object
        /// </param>
        /// <returns>
        /// Boolean that represents if objects touch each other
        /// </returns>
        public bool IsTouchingObject(IObject2D target)
        {
            // Check if entites are on the same position
            bool isXSame = target.Position[0] == Position[0];
            bool isYSame = target.Position[1] == Position[1];

            // Return the result
            return isXSame && isYSame;
        }
    }
}
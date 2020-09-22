using System;
using lbs_rpg.classes.instances.player;
using lbs_rpg.contracts.gui;

namespace lbs_rpg.contracts.entity
{
    /// <summary>
    /// Can be instantiated only in the 2D context
    /// </summary>
    public abstract class AMonster : IEntity, IObject2D
    {
        #region FIelds
        public abstract int TicksPerMove { get; }
        public static string Name = "NO_NAME_SPECIFIED";
        public abstract float HeadPrice { get; }
        // Letter that represents how hard it is to fight the monster.
        // Possible letters: A, B, C, D, E. This value is used only in UI.
        public static string FightDifficulty = "?";
        public abstract float AttackDamage { get; }
        public abstract char ModelChar { get; }
        public virtual float Health { get; set; }
        public abstract float MaxHealth { get; }
        public abstract int[] Position { get; set; }
        public virtual Object2DRotation Rotation { get; set; } = Object2DRotation.RIGHT;
        #endregion
        
        #region Construtor

        protected AMonster()
        {
            Health = MaxHealth;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Calculates monster's next position that will bring
        /// monster nearer to the target.
        /// </summary>
        /// <param name="target">
        /// Any canvas object
        /// </param>
        public void MoveToObject(IObject2D target)
        {
            // Instantiate random
            Random random = new Random();
            
            // Define position direction
            int directionX = (target.Position[0] == Position[0]) ? 0 : target.Position[0] < Position[0] ? -1 : 1;
            int directionY = (target.Position[1] == Position[1]) ? 0 : (target.Position[1] < Position[1]) ? -1 : 1;
            
            // Randomize current direction (null one number)
            if (directionX != 0 && directionY != 0)
            {
                // 1 to 4 that monster will prefer to go up/down instead of going right/left
                if (random.Next(4) == 0) directionX = 0;
                else directionY = 0;
            }

            // Move the monster
            ((IObject2D) this).MoveDirection(directionX, directionY);
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
        #endregion
    }
}
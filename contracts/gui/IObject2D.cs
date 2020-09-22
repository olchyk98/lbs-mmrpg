using System;
using lbs_rpg.classes.instances.player;

namespace lbs_rpg.contracts.gui
{
    public interface IObject2D
    {
        public int[] Position { get; set; }
        public Object2DRotation Rotation { get; set; }

        /// <summary>
        /// Moves player at the target direction.
        /// </summary>
        /// <param name="directionX">
        /// Value 1 or -1 (right or left). Indicates horizontal movement direction.
        /// </param>
        /// <param name="directionY">
        /// Value 1 or -1 (bottom or top). Indicates vertical movement direction.
        /// </param>
        public void MoveDirection(int directionX, int directionY)
        {
            // Validate arguments
            if (Math.Abs(directionX) + Math.Abs(directionY) > 2)
            {
                throw new Exception($"directionX and directionY should be 0, 1 or -1. Got: {directionX}:{directionY}");
            }
            
            // Update position
            Position[0] += directionX;
            Position[1] += directionY;
            
            // Update rotation
            if (directionX != 0)
            {
                Rotation = (directionX == 1) ? Object2DRotation.RIGHT : Object2DRotation.LEFT;
            }
            else if(directionY != 0)
            {
                Rotation = (directionY == 1) ? Object2DRotation.BOTTOM : Object2DRotation.TOP;
            }
        }
    }
}
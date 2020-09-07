using System;
using System.Threading;
using System.Threading.Tasks;

namespace lbs_rpg.contracts.gui
{
    public interface IObject2D
    {
        public int[] Position { get; set; }
        
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
            Position[0] += directionX;
            Position[1] += directionY;
        }
    }
}
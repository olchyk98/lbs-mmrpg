namespace lbs_rpg.contracts
{
    public interface IEntity
    {
        public float Health { get; }
        public float MaxHealth { get; }

        /// <summary>
        /// Applies damage to the entity
        /// </summary>
        /// <param name="damage">
        /// Damage
        /// </param>
        /// <returns>
        /// Boolean, that represents if the entity was killed
        /// </returns>
        public bool ApplyDamage(float damage);

        public bool IsAlive()
        {
            return Health > 0;
        }
    }
}
namespace lbs_rpg.contracts.entity
{
    public interface IEntityBot : IEntity
    {
        public int TicksPerMove { get; }
    }
}
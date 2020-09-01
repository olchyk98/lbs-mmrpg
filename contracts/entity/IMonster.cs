using System;

namespace lbs_rpg.contracts.entity
{
    public interface IMonster : IEntity
    {
        int TicksPerMove { get; }
        static string Name { get; }
        float HeadPrice { get; }
    }
}
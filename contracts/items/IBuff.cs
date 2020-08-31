namespace lbs_rpg.contracts.items
{
    /// <summary>
    /// Interface that describes an item that can be used to permanently improve player's stats
    /// Instance has function ApplyEffectsTo(Player player) that modifies player's stats by accessing them directly. 
    /// </summary>
    public interface IBuff : IUseable
    {}
}
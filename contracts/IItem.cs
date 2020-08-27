namespace lbs_rpg.contracts
{
    public interface IItem
    {
        public int Amount { get; set; }

        public void SetAmount(int amount)
        {
            Amount = amount;
        }
    }
}
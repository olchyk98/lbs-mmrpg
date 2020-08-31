namespace lbs_rpg.classes.instances.monsters
{
    // Thanks for a great pattern,
    // https://stackoverflow.com/questions/630803/associating-enums-with-strings-in-c-sharp
    // DEVNOTE [olesodynets]: BUT THIS SHIT SHOULD BE REFACTORED, BECAUSE PEOPLE INVENTED CLASSES FOR
    // THESE TYPES OF THINGS.
    public class MonsterType
    {
        public readonly string Name;
        public readonly float DefaultHealth;
        public readonly int TicksPerMove;
        
        private MonsterType(string name, float defaultHealth, int ticksPerMove)
        {
            Name = name; // Unique
            DefaultHealth = defaultHealth;
            TicksPerMove = ticksPerMove;
        }

#nullable enable
        public override bool Equals(object? obj)
        {
            // Check if compared to a MonsterType
            if (!(obj is MonsterType)) return false;
            
            // Cast to MonsterType
            MonsterType otherMonster = (MonsterType) obj;
            
            // Return the comparison by name (since names are unique)
            return obj is MonsterType && this.Name == otherMonster.Name;
        }
#nullable disable

        public static MonsterType Zombie => new MonsterType("Zombie", 50, 8);
        public static MonsterType Onyx => new MonsterType("Onyx", 80, 8);
        public static MonsterType Spider => new MonsterType("Spider", 30, 7);
        public static MonsterType Gutpod => new MonsterType("Gutpod", 50, 6);
        public static MonsterType Cobra => new MonsterType("Cobra", 110, 4);
        public static MonsterType Hawk => new MonsterType("Hawk", 200, 10);
        public static MonsterType SelestialSnake => new MonsterType("Selestial Snake", 800, 8);
    }
}
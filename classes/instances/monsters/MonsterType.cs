namespace lbs_rpg.classes.instances.monsters
{
    // Thanks for a great pattern,
    // https://stackoverflow.com/questions/630803/associating-enums-with-strings-in-c-sharp
    // DEVNOTE [olesodynets]: BUT THIS SHIT SHOULD BE REFACTORED, BECAUSE PEOPLE INVENTED CLASSES FOR
    // THESE TYPES OF THINGS.
    public class MonsterType
    {
        private MonsterType(string name, float defaultHealth, int ticksPerMove)
        {
            Name = name; // Unique
            DefaultHealth = defaultHealth;
            TicksPerMove = ticksPerMove;
        }

        public readonly string Name;
        public readonly float DefaultHealth;
        public readonly int TicksPerMove;

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

        public static MonsterType ZOMBIE => new MonsterType("Zombie", 50, 8);
        public static MonsterType ONYX => new MonsterType("Onyx", 80, 8);
        public static MonsterType SPIDER => new MonsterType("Spider", 30, 7);
        public static MonsterType GUTPOD => new MonsterType("Gutpod", 50, 6);
        public static MonsterType COBRA => new MonsterType("Cobra", 110, 4);
        public static MonsterType HAWK => new MonsterType("Hawk", 200, 10);
        public static MonsterType SELESTIAL_SNAKE => new MonsterType("Selestial Snake", 800, 8);
    }
}
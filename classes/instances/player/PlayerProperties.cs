using System;

namespace lbs_rpg.classes.instances.player
{
    public class PlayerProperties
    {
        public float DefenseProcent { get; private set; }
        public float HealthRegeneration { get; private set; }
        public float Level { get; private set; }

        public PlayerProperties()
        {
            DefenseProcent = 0;
            HealthRegeneration = .05f;
            Level = 1;
        }

        public void IncreaseLevel()
        {
            throw new NotImplementedException();
        }
    }
}
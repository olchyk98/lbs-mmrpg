using System;
using System.Collections.Generic;
using lbs_rpg.classes.utils;

namespace lbs_rpg.classes.instances.villages
{
    /// <summary>
    /// Villages container.
    /// Instance of this class will generate and store different villages.
    /// </summary>
    public class GameVillages
    {
        private const int NUMBER_OF_VILLAGES = 10;
        private const float MIN_VILLAGE_POSITION_GAP = 3f;
        private const float MAX_VILLAGE_POSITION_GAP = 12f; // km
        
        public IList<Village> Villages { get; }

        public GameVillages()
        {
            Villages = GenerateVillages();
        }

        private IList<Village> GenerateVillages()
        {
            // Load (and randomize) village names using IO & Take only [NUMBER_OF_VILLAGES] villages from the array.
            string[] villageNames = FsTools.ReadListRandomized("./resources/villages/names.txt", NUMBER_OF_VILLAGES);
            
            // Declare list of generated villages
            IList<Village> generatedVillages = new List<Village>();
            
            // Define position generator cursor & Instantiate random
            float positionCursor = 0;
            Random random = new Random();
            
            // Generate villages
            foreach (string name in villageNames)
            {
                // Randomize position
                float minPos = positionCursor + MIN_VILLAGE_POSITION_GAP;
                float maxPos = positionCursor + MAX_VILLAGE_POSITION_GAP;
                positionCursor = Convert.ToSingle(random.NextDouble() * (maxPos - minPos) + minPos);

                // Instantiate instance
                var village = new Village(name, positionCursor);
                generatedVillages.Add(village);
            }
         
            // Return generated villages
            return generatedVillages;
        }

        /// <summary>
        /// Returns a random village from the villages list.
        /// </summary>
        /// <returns>
        /// Random village
        /// </returns>
        /// <exception cref="ApplicationException">
        /// Throwed if no villages were generated before using this command.
        /// </exception>
        public Village GetRandomVillage()
        {
            // Check if villages were previously generated
            if (Villages.Count == 0)
            {
                throw new ApplicationException("Villages list is empty. No villages were generated.");
            }
            
            // Get a random village
            Village randomizedVillage = Villages.RandomElement();
            
            // Return the village
            return randomizedVillage;
        }
    }
}
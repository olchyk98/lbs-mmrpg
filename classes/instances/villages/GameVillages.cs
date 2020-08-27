using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using lbs_rpg.classes.utils;
using lbs_rpg.contracts.items;

namespace lbs_rpg.classes.instances.villages
{
    /// <summary>
    /// Villages container.
    /// Instance of this class will generate and store different villages.
    /// </summary>
    public class GameVillages
    {
        private const int NUMBER_OF_VILLAGES = 10; 
        
        public IList<Village> Villages { get; private set; }

        public GameVillages()
        {
            Villages = GenerateVillages();
        }

        private IList<Village> GenerateVillages()
        {
            // Load (and randomize) village names using IO & Take only [NUMBER_OF_VILLAGES] villages from the array.
            string[] villageNames = FSTools.ReadListRandomized("./resources/villages/names.txt", NUMBER_OF_VILLAGES);
            
            // Declare list of generated villages
            IList<Village> generatedVillages = new List<Village>();
            
            // Generate villages
            foreach (string name in villageNames)
            {
                var village = new Village(name);
                generatedVillages.Add(village);
            }
         
            // Return generated villages
            return generatedVillages;
        }

        public Village GetRandomVillage()
        {
            // Check if villages were previously generated
            if (Villages.Count == 0)
            {
                throw new ApplicationException("Villages list is empty. No villages were generated.");
            }
            
            // Randomize a village
            Village randomizedVillage = Villages.RandomElement();
            
            // Return the village
            return randomizedVillage;
        }
    }
}
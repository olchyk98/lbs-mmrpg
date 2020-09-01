using System;
using System.Collections.Generic;
using lbs_rpg.classes.utils;

namespace lbs_rpg.classes.instances.villages
{
    public class VillageTasks
    {
        #region Fields

        public Village Village;
        
        private IList<VillageTask> myCurrentTasks = new List<VillageTask>();
        private DateTime myLastTasksUpdate;
        private static readonly Random Random = new Random();
        private const int TASKS_REFRESH_TIME = 80 * 1000; // 80 seconds
        #endregion

        #region Constructor
        public VillageTasks(Village village)
        {
            Village = village;
            GenerateTasks();
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Generates task descriptions.
        /// </summary>
        /// <param name="count">
        /// Number of descriptions that should be generated
        /// </param>
        /// <returns>
        /// Generated strings
        /// </returns>
        private static IList<string> GenerateTaskDescriptions(int count)
        {
            // Read villager names from the file 
            IList<string> villagerNames = FsTools.ReadListRandomized("./resources/villages/villager_names.txt", count);
            
            // Read task description templates from the file
            IList<string> taskTemplates = FsTools.ReadListRandomized("./resources/villages/tasks.txt", count);
            
            // Declare output list
            IList<string> taskDescriptions = new List<string>();
            
            // Build task descriptions
            for (var ma = 0; ma < taskTemplates.Count; ma++)
            {
                // Access template value
                string template = taskTemplates[ma];
                
                // Generate description
                string description = string.Format(template, villagerNames[ma]);
                    
                // Add description to the output list
                taskDescriptions.Add(description);
            }

            // Return build list
            return taskDescriptions;
        }
        
        /// <summary>
        /// Generates list of tasks for the village
        /// </summary>
        public void GenerateTasks()
        {
            // Update refresh timer
            myLastTasksUpdate = DateTime.Now;
            
            // Randomize number of tasks
            int numberOfTasks = Random.Next(3, 7);
            
            // Load descriptions
            IList<string> taskDescriptions = GenerateTaskDescriptions(numberOfTasks);
            
            // Declare village tasks list
            IList<VillageTask> tasks = new List<VillageTask>();
            
            // Generate tasks
            foreach(string description in taskDescriptions)
            {
                // Randomize task duration and reputation bonus
                int duration = Random.Next(50, 160);
                int bonus = (int) Math.Floor(duration * (Random.NextDouble() * (2f - .3f) + .3f));
                
                // Instantiate task
                VillageTask task = new VillageTask(Village, description, bonus, duration);
                
                // Add to the tasks list
                tasks.Add(task);
            }
            
            // Update tasks field
            myCurrentTasks = tasks;
        }

        /// <summary>
        /// Removes target task from the village's tasks list.
        /// </summary>
        /// <param name="task">
        /// Target task
        /// </param>
        public void RemoveTask(VillageTask task)
        {
            myCurrentTasks.Remove(task);
        }

        /// <summary>
        /// Refreshes the tasks list when neeed and returns
        /// the relevant tasks list.
        /// </summary>
        /// <returns>
        /// List of task without any filtering
        /// </returns>
        public IList<VillageTask> GetAvailableTasks()
        {
            // Check if it's time to update the tasks list
            if (DateTime.Now > myLastTasksUpdate.AddMilliseconds(TASKS_REFRESH_TIME))
            {
                GenerateTasks();
            }
            
            // Return the tasks
            return myCurrentTasks;
        }
        #endregion
    }
}
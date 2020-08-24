

// TODO: Check screen resolution
// TODO: Refactor -> Documentation [///]
// TODO: Refactor -> Class Regions
// TODO: Describe and simply each method

using System;
using System.Threading.Tasks;
using lbs_mmrpg.classes.gui.components;
using lbs_mmrpg.classes.gui.templates;

namespace lbs_mmrpg
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(WelcomeScreen.Display);
            Console.ReadKey(true);
        }
    }
}

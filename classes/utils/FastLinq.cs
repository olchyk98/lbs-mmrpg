using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lbs_rpg.classes.utils
{
    public static class FastLinq
    {
        private static readonly Random Random = new Random();
        
        public static int[] GetStrArrayBoundaries(string[] content)
        {
            // [x,y]
            var sizes = new int[2];
            //
            sizes[0] = FastLinq.GetLongestArrayString(content).Length;
            sizes[1] = content.Length;
            //
            return sizes;
        }
        
        public static string GetLongestArrayString(string[] content)
        {
            return content.OrderByDescending( s => s.Length ).First();
        }
        
        public static T GetRandomValue2D<T>(T[,] array)
        {
            int values = array.GetLength(0) * array.GetLength(1);
            int index = Random.Next(values);
            return array[index / array.GetLength(0), index % array.GetLength(0)];
        }
        
        public static void Shuffle<T>(this IList<T> list)  
        {  
            int listLength = list.Count;  
            while (listLength > 1) {  
                listLength--;  
                int randomizedIndex = Random.Next(listLength + 1);  
                T randomizedValue = list[randomizedIndex];  
                list[randomizedIndex] = list[listLength];  
                list[listLength] = randomizedValue;  
            }  
        }

        public static string[] Convert2DCharToString(char[,] aChars)
        {
            int sizeX = aChars.GetLength(1);
            int sizeY = aChars.GetLength(0);
            string[] strings = new string[sizeY];

            for (int my = 0; my < sizeY; my++)
            {
                // Create the string container
                StringBuilder line = new StringBuilder();
                
                // Iterate through the array and compile all chars into a string
                for (int mx = 0; mx < sizeX; mx++)
                {
                    line.Append(aChars[my, mx]);
                }
                
                // Add compiled string to array
                strings[my] = line.ToString();
            }

            return strings;
        }
    }
}
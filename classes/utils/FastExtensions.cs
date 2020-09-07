using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace lbs_rpg.classes.utils
{
    public static class FastExtensions
    {
        private static readonly Random Random = new Random();

        /// <summary>
        /// Returns the longest string item in the target array. 
        /// </summary>
        /// <param name="content">
        /// Target array
        /// </param>
        /// <returns>
        /// Longest string in the array
        /// </returns>
        private static string GetLongestArrayString(string[] content)
        {
            return content.OrderByDescending(s => s.Length).First();
        }

        /// <summary>
        /// Returns dimensions of the array in 2D space by calculating
        /// its height and length of the longest string in the array.
        /// </summary>
        /// <param name="content">
        /// Target array
        /// </param>
        /// <returns>
        /// Array's dimensions in the abstract 2D space.
        /// </returns>
        public static int[] GetStrArrayBoundaries(string[] content)
        {
            // [x,y]
            var sizes = new int[2];
            //
            sizes[0] = GetLongestArrayString(content).Length;
            sizes[1] = content.Length;
            //
            return sizes;
        }

        /// <summary>
        /// Shuffles the target list.
        /// </summary>
        /// <param name="list">
        /// Target list that implements IList
        /// </param>
        /// <typeparam name="T">
        /// Optinal generic specification
        /// </typeparam>
        /// <returns>
        /// Copy of the shuffled array
        /// </returns>
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            int listLength = list.Count;
            while (listLength > 1)
            {
                listLength--;
                int randomizedIndex = Random.Next(listLength + 1);
                T randomizedValue = list[randomizedIndex];
                list[randomizedIndex] = list[listLength];
                list[listLength] = randomizedValue;
            }

            return list;
        }

        /// <summary>
        /// Gets a random element in the list.
        /// </summary>
        /// <param name="list">
        /// Target list that implements IList
        /// </param>
        /// <typeparam name="T">
        /// Optinal generic specification
        /// </typeparam>
        /// <returns>
        /// A random element from the list
        /// </returns>
        public static T RandomElement<T>(this IList<T> list)
        {
            // Get list length
            int length = list.Count;

            // Randomize element index
            int randPosition = Random.Next(length);

            // Return an item on that position
            return list[randPosition];
        }

        /// <summary>
        /// Completely clones an object
        /// </summary>
        /// <param name="obj">
        /// Serializable object
        /// </param>
        /// <typeparam name="T">
        /// Object's type
        /// </typeparam>
        /// <returns>
        /// The cloned object
        /// </returns>
        public static T DeepClone<T>(this T obj)
        {
            using var memoryStream = new MemoryStream();
            var formatter = new BinaryFormatter();

            formatter.Serialize(memoryStream, obj);
            memoryStream.Position = 0;

            return (T) formatter.Deserialize(memoryStream);
        }
    }
}
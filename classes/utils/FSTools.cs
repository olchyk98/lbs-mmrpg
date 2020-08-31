using System.IO;
using System.Linq;
using System.Text;

namespace lbs_rpg.classes.utils
{
    public static class FsTools
    {
        /// <summary>
        /// PUBLIC..asdmlasd method that is used by other public methods of this class.
        /// Loads content from file as text (UTF-8)
        /// </summary>
        /// <param name="fpath">
        /// Relalative/Absolute path to the target file.
        /// </param>
        /// <returns></returns>
        public static string ReadText(string fpath)
        {
            return File.ReadAllText(fpath, Encoding.UTF8);
        }

        /// <summary>
        /// Reads a file and returns the content of it in a line-splitted type.
        /// Encoding: UTF-8
        /// </summary>
        /// <param name="fpath">
        /// Relalative/Absolute path to the target file.
        /// </param>
        /// <param name="limit">
        /// Limit of objects that are presented in the output array. Optional.
        /// </param>
        /// <returns>
        /// Lines of imported file as string array.
        /// </returns>
        public static string[] ReadList(string fpath, int limit = default)
        {
            string[] list = File.ReadAllLines(fpath, Encoding.UTF8);

            // Check if limit value provided, and use it if yes
            return (limit == default) ? list : list.Take(limit).ToArray();
        }

        /// <summary>
        /// Reads a file and returns the content of it in a randomized-line-splitted type.
        /// </summary>
        /// <param name="fpath">
        /// Relative/Absollute path to the target file.
        /// </param>
        /// <param name="limit">
        /// Limit of objects that are presented in the output array. Optional.
        /// </param>
        /// <returns>
        /// Lines of imported file in a randomized order.
        /// </returns>
        /// <devnotes>
        /// * I won't be using ReadList's limit argument, since after randomization,
        /// we will shuffle only first [limit] elements of the array, instead of shuffling the whole array
        /// and picking [limit] elements. 
        /// </devnotes>
        public static string[] ReadListRandomized(string fpath, int limit = default)
        {
            string[] list = ReadList(fpath).Shuffle().ToArray();
            
            // Check if limit value provided, and use it if yes
            return (limit == default) ? list : list.Take(limit).ToArray();
        }
    }
}
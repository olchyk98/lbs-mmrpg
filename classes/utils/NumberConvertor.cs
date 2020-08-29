using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace lbs_rpg.classes.utils
{
    public static class NumberConvertor
    {
        /// <summary>
        /// Takes number and shortens it.
        /// For example: 3300 -> 3.3k
        /// </summary>
        /// <param name="number">
        /// Target number. Can be: float, double, int, long
        /// </param>
        /// <returns>
        /// Stringed and converted number
        /// </returns>
        public static string ShortenNumber<T>([NotNull] T number)
        {
            // Declare whitelisted number types
            Type[] whitelistedTypes = {typeof(double), typeof(float), typeof(long), typeof(int)};

            // Validate type
            if (!whitelistedTypes.Contains(number.GetType()))
            {
                throw new ApplicationException("Invalid number type. Can be only Double, Short, Int32 and Int64");
            }

            // Unsafely cast the type
            // DEVNOTE [olesodynets]: It is safe now, because we check type before the casting
            long convertedNumber = Convert.ToInt64(number);

            // Convert number
            if (convertedNumber < 1e3) return convertedNumber + "";
            if (convertedNumber >= 1e3 && convertedNumber < 1e6) return (convertedNumber / 1e3).ToString("0.0") + "K";
            if (convertedNumber >= 1e6 && convertedNumber < 1e9) return (convertedNumber / 1e6).ToString("0.0") + "M";
            if (convertedNumber >= 1e9 && convertedNumber < 1e12) return (convertedNumber / 1e9).ToString("0.0") + "B";
            return (convertedNumber / 1e12).ToString("0.0") + "T";
        }
    }
}
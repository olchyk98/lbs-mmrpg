using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Implementation of "Colorize" by Oles Odynets
// @2020

namespace lbs_rpg.classes.gui.components.colorize
{
    public static class ColorizeString
    {
        #region Fields
        private static readonly IDictionary<ColorizeColor, string> AnsiCodes = new Dictionary<ColorizeColor, string>();
        private static string resetAnsi = "\u001b[0m";
        #endregion

        #region Constructor

        static ColorizeString()
        {            
            // Support windows
            SupportOs.SupportWindows();
            
            // Declare available ansi codes
            AnsiCodes.Add(ColorizeColor.RED, "\u001b[31m");
            AnsiCodes.Add(ColorizeColor.BLACK, "\u001b[30m");
            AnsiCodes.Add(ColorizeColor.GREEN, "\u001b[32m");
            AnsiCodes.Add(ColorizeColor.YELLOW, "\u001b[33m");
            AnsiCodes.Add(ColorizeColor.BLUE, "\u001b[34m");
            AnsiCodes.Add(ColorizeColor.MAGENTA, "\u001b[35m");
            AnsiCodes.Add(ColorizeColor.CYAN, "\u001b[36m");
            AnsiCodes.Add(ColorizeColor.WHITE, "\u001b[37m");
            
            AnsiCodes.Add(ColorizeColor.BGRED, "\u001b[41m");
            AnsiCodes.Add(ColorizeColor.BGBLACK, "\u001b[40m");
            AnsiCodes.Add(ColorizeColor.BGGREEN, "\u001b[42m");
            AnsiCodes.Add(ColorizeColor.BGYELLOW, "\u001b[43m");
            AnsiCodes.Add(ColorizeColor.BGBLUE, "\u001b[44m");
            AnsiCodes.Add(ColorizeColor.BGMAGENTA, "\u001b[45m");
            AnsiCodes.Add(ColorizeColor.BGCYAN, "\u001b[46m");
            AnsiCodes.Add(ColorizeColor.BGWHITE, "\u001b[47m");
            
            AnsiCodes.Add(ColorizeColor.BOLD, "\u001b[1m");
            AnsiCodes.Add(ColorizeColor.ITALIC, "\u001b[3m");
        }

        #endregion

        /// <summary>
        /// Changes the target's color/background color.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="color">
        /// Name of the color. Like, red, white, black.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///  Occured if the softColor is invalid.
        ///  May happen since this module doesn't support very few colors.
        /// </exception>
        public static string Colorize(this string str, ColorizeColor color)
        {
            // Try get ansi sequence for this color
            AnsiCodes.TryGetValue(color, out var ansiColor);

            // Apply color to the target
            return ansiColor + str + resetAnsi;
        }

        /// <summary>
        /// Removes all ansi characters from the string.
        /// This function uses regex. 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Decolorize(this string str)
        {
            Regex ansiRegex = new Regex(@"\e\[(\d+;)*(\d+)?[ABCDHJKfmsu]");
            return ansiRegex.Replace(str, "");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using lbs_mrpg.classes.gui.components.colorize;

// Colorize implementation by Oles Odynets
// @2020

namespace lbs_mrpg.classes.gui.components
{
    public static class ColorizeString
    {
        #region Fields
        /*
         * Examples: 
         *     hard_color -> FORE_COLOR_RED, BACK_COLOR_WHITE
         *     soft_color -> red, bgwhite
         */
        
        // Gen::Dictionary (hard_label -> ansi_color)
        private static readonly IDictionary<HardColor, string> AnsiCodes = new Dictionary<HardColor, string>();

        // Gen::Dictionary (soft_color -> hard_label)
        private static readonly IDictionary<string, HardColor> ColorLabels = new Dictionary<string, HardColor>();

        #endregion

        #region Constructor

        static ColorizeString()
        {
            // ANSI CODES
            AnsiCodes.Add(HardColor.FORE_COLOR_RED, "\u001b[31m");
            AnsiCodes.Add(HardColor.FORE_COLOR_BLACK, "\u001b[30m");
            AnsiCodes.Add(HardColor.FORE_COLOR_GREEN, "\u001b[32m");
            AnsiCodes.Add(HardColor.FORE_COLOR_YELLOW, "\u001b[33m");
            AnsiCodes.Add(HardColor.FORE_COLOR_BLUE, "\u001b[34m");
            AnsiCodes.Add(HardColor.FORE_COLOR_MAGENTA, "\u001b[35m");
            AnsiCodes.Add(HardColor.FORE_COLOR_CYAN, "\u001b[36m");
            AnsiCodes.Add(HardColor.FORE_COLOR_WHITE, "\u001b[37m");
            
            AnsiCodes.Add(HardColor.BACK_COLOR_RED, "\u001b[401");
            AnsiCodes.Add(HardColor.BACK_COLOR_BLACK, "\u001b[40m");
            AnsiCodes.Add(HardColor.BACK_COLOR_GREEN, "\u001b[42m");
            AnsiCodes.Add(HardColor.BACK_COLOR_YELLOW, "\u001b[43m");
            AnsiCodes.Add(HardColor.BACK_COLOR_BLUE, "\u001b[44m");
            AnsiCodes.Add(HardColor.BACK_COLOR_MAGENTA, "\u001b[45m");
            AnsiCodes.Add(HardColor.BACK_COLOR_CYAN, "\u001b[46m");
            AnsiCodes.Add(HardColor.BACK_COLOR_WHITE, "\u001b[47m");
            
            AnsiCodes.Add(HardColor.STYLE_BOLD, "\u001b[1m");
            AnsiCodes.Add(HardColor.STYLE_UNDERLINE, "\u001b[4m");
            AnsiCodes.Add(HardColor.STYLE_MARKED, "\u001b[7m");
            
            AnsiCodes.Add(HardColor.SYS_RESET, "\u001b[0m");

            // COLOR LABELS
            ColorLabels.Add("red", HardColor.FORE_COLOR_RED);
            ColorLabels.Add("black", HardColor.FORE_COLOR_BLACK);
            ColorLabels.Add("green", HardColor.FORE_COLOR_GREEN);
            ColorLabels.Add("yellow", HardColor.FORE_COLOR_YELLOW);
            ColorLabels.Add("blue", HardColor.FORE_COLOR_BLUE);
            ColorLabels.Add("magenta", HardColor.FORE_COLOR_MAGENTA);
            ColorLabels.Add("cyan", HardColor.FORE_COLOR_CYAN);
            ColorLabels.Add("white", HardColor.FORE_COLOR_WHITE);
            
            ColorLabels.Add("bgred", HardColor.BACK_COLOR_RED);
            ColorLabels.Add("bgblack", HardColor.BACK_COLOR_BLACK);
            ColorLabels.Add("bggreen", HardColor.BACK_COLOR_GREEN);
            ColorLabels.Add("bgyellow", HardColor.BACK_COLOR_YELLOW);
            ColorLabels.Add("bgblue", HardColor.BACK_COLOR_BLUE);
            ColorLabels.Add("bgmagenta", HardColor.BACK_COLOR_MAGENTA);
            ColorLabels.Add("bgcyan", HardColor.BACK_COLOR_CYAN);
            ColorLabels.Add("bgwhite", HardColor.BACK_COLOR_WHITE);
        }

        #endregion

        #region Shortcuts

        /// <summary>
        /// Quick access to the available soft colors.
        /// </summary>
        /// <returns>
        /// Array of available soft colors.
        /// </returns>
        private static string[] GetSoftColors()
        {
            return ColorLabels.Keys.ToArray();
        }

        /// <summary>
        /// Quick access to the reset ansi.
        /// Prevents developer for copy pasting the same code.
        /// </summary>
        /// <returns>
        /// Color reset ansi.
        /// </returns>
        private static string GetResetAnsi()
        {
            return AnsiCodes[HardColor.SYS_RESET];
        }

        #endregion

        /// <summary>
        /// Changes the target's color/background color.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="softColor">
        /// Text color. Like, red, white, black.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        ///  Occured if the softColor is invalid.
        ///  May happen since this module doesn't support very few colors.
        /// </exception>
        public static string Colorize(this String str, string softColor)
        {
            // Convert softColor to hardColor
            HardColor hardColor;
            bool isValidSoftColor = ColorLabels.TryGetValue(softColor, out hardColor);

            // Check if hardColor is extracted
            if (!isValidSoftColor)
            {
                throw new ArgumentException($@"
Used an invalid colorLabel [Colorize]: colorLabel:{softColor}.
Valid colors: {String.Join(", ", GetSoftColors())}");
            }
                
            // Convert hardColor to ansiColor
            string ansiColor = null;
            AnsiCodes.TryGetValue(hardColor, out ansiColor);

            // Apply color to the target
            return ansiColor + str + GetResetAnsi();
        }

        /// <summary>
        /// Makes the target string bold.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MakeBold(this String str)
        {
            return AnsiCodes[HardColor.STYLE_BOLD] + str + GetResetAnsi();
        }

        /// <summary>
        /// Makes the target string underlined.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MakeUnderline(this String str)
        {
            return AnsiCodes[HardColor.STYLE_UNDERLINE] + str + GetResetAnsi();
        }

        /// <summary>
        /// Makes the target string marked.
        /// On the black background the text foreground becomes black, background - white.  
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MakeMarked(this String str)
        {
            return AnsiCodes[HardColor.STYLE_MARKED] + str + GetResetAnsi();
        }

        /// <summary>
        /// Removes all ansi characters from the string.
        /// This function uses regex. 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Decolorize(this String str)
        {
            Regex ansiRegex = new Regex(@"\e\[(\d+;)*(\d+)?[ABCDHJKfmsu]");
            return ansiRegex.Replace(str, "");
        }
    }
}
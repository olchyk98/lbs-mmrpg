using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

// Colorize implementation by Oles Odynets
// @2020

namespace lbs_rpg.classes.gui.components.colorize
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
            // Support windows
            SupportOs.SupportWindows();

            // REFACTOR: Move a separate JSON file.
            // ANSI CODES
            AnsiCodes.Add(HardColor.ForeColorRed, "\u001b[31m");
            AnsiCodes.Add(HardColor.ForeColorBlack, "\u001b[30m");
            AnsiCodes.Add(HardColor.ForeColorGreen, "\u001b[32m");
            AnsiCodes.Add(HardColor.ForeColorYellow, "\u001b[33m");
            AnsiCodes.Add(HardColor.ForeColorBlue, "\u001b[34m");
            AnsiCodes.Add(HardColor.ForeColorMagenta, "\u001b[35m");
            AnsiCodes.Add(HardColor.ForeColorCyan, "\u001b[36m");
            AnsiCodes.Add(HardColor.ForeColorWhite, "\u001b[37m");
            
            AnsiCodes.Add(HardColor.BackColorRed, "\u001b[41m");
            AnsiCodes.Add(HardColor.BackColorBlack, "\u001b[40m");
            AnsiCodes.Add(HardColor.BackColorGreen, "\u001b[42m");
            AnsiCodes.Add(HardColor.BackColorYellow, "\u001b[43m");
            AnsiCodes.Add(HardColor.BackColorBlue, "\u001b[44m");
            AnsiCodes.Add(HardColor.BackColorMagenta, "\u001b[45m");
            AnsiCodes.Add(HardColor.BackColorCyan, "\u001b[46m");
            AnsiCodes.Add(HardColor.BackColorWhite, "\u001b[47m");

            AnsiCodes.Add(HardColor.SysReset, "\u001b[0m");

            // COLOR LABELS
            ColorLabels.Add("red", HardColor.ForeColorRed);
            ColorLabels.Add("black", HardColor.ForeColorBlack);
            ColorLabels.Add("green", HardColor.ForeColorGreen);
            ColorLabels.Add("yellow", HardColor.ForeColorYellow);
            ColorLabels.Add("blue", HardColor.ForeColorBlue);
            ColorLabels.Add("magenta", HardColor.ForeColorMagenta);
            ColorLabels.Add("cyan", HardColor.ForeColorCyan);
            ColorLabels.Add("white", HardColor.ForeColorWhite);
            
            ColorLabels.Add("bgred", HardColor.BackColorRed);
            ColorLabels.Add("bgblack", HardColor.BackColorBlack);
            ColorLabels.Add("bggreen", HardColor.BackColorGreen);
            ColorLabels.Add("bgyellow", HardColor.BackColorYellow);
            ColorLabels.Add("bgblue", HardColor.BackColorBlue);
            ColorLabels.Add("bgmagenta", HardColor.BackColorMagenta);
            ColorLabels.Add("bgcyan", HardColor.BackColorCyan);
            ColorLabels.Add("bgwhite", HardColor.BackColorWhite);
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
            return AnsiCodes[HardColor.SysReset];
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
        public static string Colorize(this string str, string softColor)
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
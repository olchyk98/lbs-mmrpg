namespace lbs_rpg.classes.utils
{
    public static class NumberConvertor
    {
        public static string ShortenNumber(int number)
        {
            if (number < 1e3) return number + "";
            if (number >= 1e3 && number < 1e6) return (number / 1e3).ToString("0.0") + "K";
            if (number >= 1e6 && number < 1e9) return (number / 1e6).ToString("0.0") + "M";
            if (number >= 1e9 && number < 1e12) return (number / 1e9).ToString("0.0") + "B";
            else return (number / 1e12).ToString("0.0") + "T";
        }
    }
}